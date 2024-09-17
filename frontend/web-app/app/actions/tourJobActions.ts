'use server'

import { ApiPageResult, TourJob } from '@/types'

export async function getTourJobs(
  query: string
): Promise<ApiPageResult<TourJob>> {
  const res = await fetch(`http://localhost:6001/tourjobs?${query}`)

  if (!res.ok) throw new Error('Failed to fetch tour jobs')

  return res.json()
}
