'use server'

import { auth } from '@/auth'
import { ApiPageResult, TourJob } from '@/types'

export async function getTourJobs(
  query: string
): Promise<ApiPageResult<TourJob>> {
  const res = await fetch(`http://localhost:6001/tourjobs?${query}`)

  if (!res.ok) throw new Error('Failed to fetch tour jobs')

  return res.json()
}

export async function authTourJobTest() {
  const data = {
    title: 'China Tour 2',
  }

  const session = await auth()

  const res = await fetch(
    'http://localhost:6001/auth/tourjobs/f8441a8c-14f6-443f-9900-dac436db11c2',
    {
      method: 'GET',
      headers: {
        'Content-type': 'application/json',
        Authorization: `Bearer ${session?.accessToken}`,
      },
      // body: JSON.stringify(data),
    }
  )

  if (!res.ok) return { status: res.status, message: res.statusText }

  return res.json()
}
