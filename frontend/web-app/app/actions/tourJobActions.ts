'use server'

import { fetchWrapper } from '@/lib/fetchWrapper'
import { ApiPageResult, ApiResult, TourJob } from '@/types'
import { revalidatePath } from 'next/cache'
import { FieldValues } from 'react-hook-form'

export async function authTourJobTest() {
  return await fetchWrapper.get(
    '/auth/tourjobs/f8441a8c-14f6-443f-9900-dac436db11c2'
  )
}

export async function getTourJobs(
  query: string
): Promise<ApiPageResult<TourJob[]>> {
  return await fetchWrapper.get(`/tourjobs?${query}`)
}

export async function getMyTourJob(): Promise<ApiResult<TourJob[]>> {
  return await fetchWrapper.get('/auth/tourjobs')
}

export async function getTourJobBySlug(
  slug: string
): Promise<ApiResult<TourJob>> {
  return await fetchWrapper.get(`/tourjobs/${slug}`)
}

export async function getTourJobById(id: string): Promise<ApiResult<TourJob>> {
  return await fetchWrapper.get(`/auth/tourjobs/${id}`)
}

export async function createTourJob(body: FieldValues) {
  const res = await fetchWrapper.post('/auth/tourjobs', body)
  revalidatePath('/tourjobs/list')
  return res
}

export async function updateTourJob(id: string, body: FieldValues) {
  const res = await fetchWrapper.put(`/auth/tourjobs/${id}`, body)
  revalidatePath('/tourjobs/list')
  return res
}

export async function deleteTourJob(id: string) {
  const res = await fetchWrapper.del(`/auth/tourjobs/${id}`)
  revalidatePath('/tourjobs/list')
  return res
}
