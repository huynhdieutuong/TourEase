import { TourJob } from '@/types'
import React from 'react'
import CardImage from './CardImage'
import CountdownTimer from './CountdownTimer'
import CardDays from './CardDays'
import CardSalary from './CardSalary'

type Props = {
  tourJob: TourJob
}

export default function TourJobCard({ tourJob }: Props) {
  return (
    <a href='#' className='group'>
      <div className='relative aspect-[16/10] rounded-lg overflow-hidden'>
        <CardImage imageUrl={tourJob.imageUrl} title={tourJob.title} />
        <div className='absolute bottom-2 right-2'>
          <CountdownTimer expireDate={tourJob.expiredDate} />
        </div>
        <div className='absolute top-2 left-2'>
          <CardDays days={tourJob.days} />
        </div>
        <div className='absolute top-2 right-2'>
          <CardSalary salary={tourJob.salary} currency={tourJob.currency} />
        </div>
      </div>
      <div className='mt-4'>
        <h3 className='text-gray-700'>{tourJob.title}</h3>
      </div>
    </a>
  )
}
