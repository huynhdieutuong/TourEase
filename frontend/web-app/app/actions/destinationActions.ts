'use server'

import { ApiResult, Destination } from '@/types'

export async function getDestinations(): Promise<ApiResult<Destination>> {
  const res = await fetch('http://localhost:6001/destinations')

  if (!res.ok) throw new Error('Failed to fetch destinations')

  return res.json()
}
