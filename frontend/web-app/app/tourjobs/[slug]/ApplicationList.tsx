'use client'

import React, { useEffect } from 'react'
import { Badge } from 'flowbite-react'
import { Application } from '@/types'
import ApplicationItem from './ApplicationItem'
import { useApplicationStore } from '@/hooks/useApplicationStore'

type Props = {
  applications: Application[]
  tourGuide: string | undefined
}

export default function ApplicationList({ applications, tourGuide }: Props) {
  const setApplications = useApplicationStore((state) => state.setApplications)
  const applicationsInStore = useApplicationStore((state) => state.applications)

  useEffect(() => {
    setApplications(applications)
  }, [])

  return (
    <div className='border-2 border-yellow-400'>
      <div className='bg-yellow-400 flex items-center justify-between p-3'>
        <h3 className='text-md leading-none'>
          Selected:{' '}
          <span className='font-medium'>
            {tourGuide ? tourGuide : 'Pending selection'}
          </span>
        </h3>
        <Badge color='info'>{applicationsInStore.length} applicants</Badge>
      </div>
      <div className='p-3 flex flex-col gap-3 overflow-y-auto h-80'>
        {applicationsInStore.map((application) => (
          <ApplicationItem key={application.id} application={application} />
        ))}
      </div>
    </div>
  )
}
