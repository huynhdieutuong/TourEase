import { ApiPageResult, TourJob } from '@/types'
import React from 'react'
import TourJobCard from './TourJobCard'

async function getData(): Promise<ApiPageResult<TourJob>> {
  const res = await fetch('http://localhost:6001/tourjobs?pageSize=12')

  if (!res.ok) throw new Error('Failed to fetch data')

  return res.json()
}

export default async function TourJobList() {
  const result = await getData()

  return (
    <div className='grid grid-cols-4 gap-6'>
      {result?.data?.map((tourjob) => (
        <TourJobCard tourJob={tourjob} key={tourjob.id} />
      ))}
    </div>
  )
}
