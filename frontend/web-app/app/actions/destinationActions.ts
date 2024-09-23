'use server'

import { fetchWrapper } from '@/lib/fetchWrapper'
import { ApiResult, Destination } from '@/types'
import { revalidatePath } from 'next/cache'
import { FieldValues } from 'react-hook-form'

export async function getDestinations(): Promise<ApiResult<Destination[]>> {
  return await fetchWrapper.get('/destinations')
}

export async function getDestinationsTree(): Promise<ApiResult<Destination[]>> {
  return await fetchWrapper.get('/auth/destinations?mode=tree')
}

export async function getDestinationById(
  id: string
): Promise<ApiResult<Destination>> {
  return await fetchWrapper.get(`/auth/destinations/${id}`)
}

export async function createDestination(
  body: FieldValues
): Promise<ApiResult<Destination>> {
  const res = await fetchWrapper.post('/auth/destinations', body)
  revalidatePath('/destinations/list')
  return res
}

export async function updateDestination(
  id: string,
  body: FieldValues
): Promise<ApiResult<Destination>> {
  const res = await fetchWrapper.put(`/auth/destinations/${id}`, body)
  revalidatePath('/destinations/list')
  return res
}

export async function deleteDestination(id: string) {
  const res = await fetchWrapper.del(`/auth/destinations/${id}`)
  revalidatePath('/destinations/list')
  return res
}
