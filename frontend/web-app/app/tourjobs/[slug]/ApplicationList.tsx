'use client'

import React, { useEffect } from 'react'
import { Badge } from 'flowbite-react'
import { Application } from '@/types'
import ApplicationItem from './ApplicationItem'
import { useApplicationStore } from '@/hooks/useApplicationStore'
import { User } from 'next-auth'
import { Role, TourJobStatus } from '@/types/enums'

type Props = {
  applications: Application[]
  tourGuide: string | undefined
  user: User | null
  tourJobOwner: string
  tourJobStatus: TourJobStatus
}

export default function ApplicationList({
  applications,
  tourGuide,
  user,
  tourJobOwner,
  tourJobStatus,
}: Props) {
  const applicationsInStore = useApplicationStore((state) => state.applications)
  const tourGuideInStore = useApplicationStore((state) => state.tourGuide)
  const setApplications = useApplicationStore((state) => state.setApplications)
  const setCompletedJob = useApplicationStore((state) => state.setCompletedJob)

  useEffect(() => {
    setApplications(applications)
    if (tourGuide) setCompletedJob(tourGuide)
  }, [applications, setApplications, setCompletedJob, tourGuide])

  return (
    <div className='border-2 border-yellow-400'>
      <div className='bg-yellow-400 flex items-center justify-between p-3'>
        <h3 className='text-md leading-none'>
          Selected:{' '}
          <span className='font-medium'>
            {tourGuideInStore ? tourGuideInStore : 'Pending selection'}
          </span>
        </h3>
        <Badge color='info'>{applicationsInStore.length} applicants</Badge>
      </div>
      <div className='p-3 flex flex-col gap-3 overflow-y-auto h-80'>
        {applicationsInStore.map((application) => (
          <ApplicationItem
            key={application.id}
            application={application}
            isOwner={
              user?.roles.includes(Role.TRAVELAGENCY) &&
              user?.username === tourJobOwner
            }
            isCompleted={
              !!tourGuideInStore || tourJobStatus !== TourJobStatus.Live
            }
          />
        ))}
      </div>
    </div>
  )
}
