import { TourJob } from '@/types'
import React from 'react'
import CardImage from './CardImage'
import CountdownTimer from './CountdownTimer'
import CardDays from './CardDays'
import CardSalary from './CardSalary'
import Link from 'next/link'
import { Badge } from 'flowbite-react'
import { TourJobStatus } from '@/types/enums'

type Props = {
  tourJob: TourJob
}

export default function TourJobCard({ tourJob }: Props) {
  return (
    <Link href={`/tourjobs/${tourJob.slug}`} className='group'>
      <div className='relative aspect-[16/10] rounded-lg overflow-hidden'>
        <CardImage imageUrl={tourJob.imageUrl} title={tourJob.title} />
        <div className='absolute bottom-2 right-2'>
          <CountdownTimer
            expireDate={tourJob.expiredDate}
            forceCompleted={tourJob.status !== TourJobStatus.Live}
          />
        </div>
        <div className='absolute top-2 left-2'>
          <CardDays days={tourJob.days} />
        </div>
        <div className='absolute top-2 right-2'>
          <CardSalary salary={tourJob.salary} currency={tourJob.currency} />
        </div>
      </div>
      <div className='mt-4 flex justify-between items-center'>
        <h3 className='text-gray-700'>{tourJob.title}</h3>
        <p className='text-gray-700 text-sm'>
          <Badge color='info'>
            {tourJob.totalApplicants ? tourJob.totalApplicants : 0} applicants
          </Badge>
        </p>
      </div>
    </Link>
  )
}
