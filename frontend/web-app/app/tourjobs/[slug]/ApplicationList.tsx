import React from 'react'
import { Badge } from 'flowbite-react'
import { Application } from '@/types'
import ApplicationItem from './ApplicationItem'

type Props = {
  applications: Application[]
  tourGuide: string | undefined
}

export default function ApplicationList({ applications, tourGuide }: Props) {
  return (
    <div className='border-2 border-yellow-400'>
      <div className='bg-yellow-400 flex items-center justify-between p-3'>
        <h3 className='text-md leading-none'>
          Selected:{' '}
          <span className='font-medium'>
            {tourGuide ? tourGuide : 'Pending selection'}
          </span>
        </h3>
        <Badge color='info'>{applications.length} applicants</Badge>
      </div>
      <div className='p-3 flex flex-col gap-3 overflow-y-auto h-80'>
        {applications.map((application) => (
          <ApplicationItem key={application.id} application={application} />
        ))}
      </div>
    </div>
  )
}
