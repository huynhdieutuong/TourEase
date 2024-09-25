import React from 'react'
import TourJobForm from '../../TourJobForm'
import { getTourJobById } from '@/app/actions/tourJobActions'

export default async function Update({ params }: { params: { id: string } }) {
  const res = await getTourJobById(params.id)

  return (
    <div className='max-w-[75%] mx-auto p-10 shadow-md rounded-md'>
      <h2 className='text-3xl font-bold mb-6 text-center text-yellow-600'>
        Update Tour Job
      </h2>
      <TourJobForm tourJob={res.data} />
    </div>
  )
}
