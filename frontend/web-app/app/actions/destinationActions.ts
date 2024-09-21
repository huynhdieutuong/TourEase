'use server'

import { fetchWrapper } from '@/lib/fetchWrapper'
import { ApiResult, Destination } from '@/types'

export async function getDestinations(): Promise<ApiResult<Destination>> {
  return await fetchWrapper.get('/destinations')
}
