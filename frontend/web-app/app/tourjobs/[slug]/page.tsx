import React from 'react'
import TourJobDetails from './TourJobDetails'
import { getTourJobBySlug } from '@/app/actions/tourJobActions'

export default async function DetailsBySlug({
  params,
}: {
  params: { slug: string }
}) {
  const res = await getTourJobBySlug(params.slug)

  return (
    <div>
      <TourJobDetails tourJob={res.data} />
    </div>
  )
}
