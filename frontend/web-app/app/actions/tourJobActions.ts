'use server'

import { ApiPageResult, TourJob } from '@/types'

export async function getData(query: string): Promise<ApiPageResult<TourJob>> {
  const res = await fetch(`http://localhost:6001/tourjobs?${query}`)

  if (!res.ok) throw new Error('Failed to fetch data')

  return res.json()
}
