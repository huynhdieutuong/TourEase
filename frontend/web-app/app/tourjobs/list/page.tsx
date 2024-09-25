import React from 'react'
import { MyTourJobList } from './TourJobList'
import { getMyTourJob } from '@/app/actions/tourJobActions'
import { Color } from '@/app/components/CustomTheme'

export default async function List() {
  const res = await getMyTourJob()

  return (
    <div className='max-w-7xl mx-auto p-6'>
      <h1
        className={`text-3xl font-bold mb-8 text-center text-${Color.PRIMARY}`}
      >
        Tour Job Listings
      </h1>
      <MyTourJobList tourJobs={res.data} />
    </div>
  )
}
