import React from 'react'
import TourJobForm from '../../TourJobForm'
import { getTourJobById } from '@/app/actions/tourJobActions'
import { redirect } from 'next/navigation'

export default async function Update({ params }: { params: { id: string } }) {
  const res = await getTourJobById(params.id)
  if (!res.isSucceeded) return redirect('/api/404')
  if (res.data.tourGuide) return redirect('/tourjobs/list')

  return (
    <div className='max-w-[75%] mx-auto p-10 shadow-md rounded-md'>
      <h2 className='text-3xl font-bold mb-6 text-center text-yellow-600'>
        Update Tour Job
      </h2>
      <TourJobForm tourJob={res.data} />
    </div>
  )
}
