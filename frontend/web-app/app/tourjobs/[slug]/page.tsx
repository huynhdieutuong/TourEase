import { getApplicationsByTourJobId } from '@/app/actions/applicationActions'
import { getCurrentUser } from '@/app/actions/authActions'
import { getTourJobBySlug } from '@/app/actions/tourJobActions'
import { redirect } from 'next/navigation'
import TourJobDetails from './TourJobDetails'

export default async function DetailsBySlug({
  params,
}: {
  params: { slug: string }
}) {
  const user = await getCurrentUser()
  const res = await getTourJobBySlug(params.slug)
  if (!res.isSucceeded) return redirect('/api/404')
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
