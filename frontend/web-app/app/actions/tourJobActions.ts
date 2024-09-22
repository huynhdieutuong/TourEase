'use server'

import { fetchWrapper } from '@/lib/fetchWrapper'
import { ApiPageResult, ApiResult, TourJob } from '@/types'
import { FieldValues } from 'react-hook-form'

export async function getTourJobs(
  query: string
): Promise<ApiPageResult<TourJob[]>> {
  return await fetchWrapper.get(`/tourjobs?${query}`)
}

export async function getTourJobBySlug(
  slug: string
): Promise<ApiResult<TourJob>> {
  return await fetchWrapper.get(`/tourjobs/${slug}`)
}

export async function authTourJobTest() {
  return await fetchWrapper.get(
    '/auth/tourjobs/f8441a8c-14f6-443f-9900-dac436db11c2'
  )
}

export async function createTourJob(body: FieldValues) {
  return await fetchWrapper.post('/auth/tourjobs', body)
}

export async function getMyTourJob(): Promise<ApiResult<TourJob[]>> {
  return await fetchWrapper.get('/auth/tourjobs')
}
