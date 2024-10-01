import React from 'react'
import TourJobDetails from './TourJobDetails'
import { getTourJobBySlug } from '@/app/actions/tourJobActions'
import { getApplicationsByTourJobId } from '@/app/actions/applicationActions'

export default async function DetailsBySlug({
  params,
}: {
  params: { slug: string }
}) {
  const res = await getTourJobBySlug(params.slug)
  const applicationsRes = await getApplicationsByTourJobId(res.data.id)

  return (
    <div>
      <TourJobDetails tourJob={res.data} applications={applicationsRes.data} />
    </div>
  )
}
