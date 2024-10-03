import React from 'react'
import TourJobDetails from './TourJobDetails'
import { getTourJobBySlug } from '@/app/actions/tourJobActions'
import { getApplicationsByTourJobId } from '@/app/actions/applicationActions'
import { getCurrentUser } from '@/app/actions/authActions'

export default async function DetailsBySlug({
  params,
}: {
  params: { slug: string }
}) {
  const user = await getCurrentUser()
  const res = await getTourJobBySlug(params.slug)
  const applicationsRes = await getApplicationsByTourJobId(res.data.id)

  return (
    <div>
      <TourJobDetails
        tourJob={res.data}
        applications={applicationsRes.data}
        user={user}
      />
    </div>
  )
}
