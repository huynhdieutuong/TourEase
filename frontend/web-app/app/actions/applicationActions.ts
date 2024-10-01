'use server'

import { fetchWrapper } from '@/lib/fetchWrapper'
import { ApiResult, Application } from '@/types'
import { FieldValues } from 'react-hook-form'

export async function getApplicationsByTourJobId(
  tourJobId: string
): Promise<ApiResult<Application[]>> {
  return await fetchWrapper.get(`/applications?tourJobId=${tourJobId}`)
}

export async function getMyApplications(): Promise<ApiResult<Application[]>> {
  return await fetchWrapper.get('/applications/my')
}

export async function applyTourJob(
  body: FieldValues
): Promise<ApiResult<Application>> {
  const res = await fetchWrapper.post('/applications', body)
  return res
}

export async function cancelApplication(
  applicationId: string,
  body: FieldValues = {}
): Promise<ApiResult<boolean>> {
  const res = await fetchWrapper.put(
    `/applications/${applicationId}/cancel`,
    body
  )
  return res
}

export async function reapplyApplication(
  applicationId: string,
  body: FieldValues = {}
): Promise<ApiResult<boolean>> {
  const res = await fetchWrapper.put(
    `/applications/${applicationId}/reapply`,
    body
  )
  return res
}

export async function chooseTourGuide(
  applicationId: string,
  body: FieldValues = {}
): Promise<ApiResult<boolean>> {
  const res = await fetchWrapper.put(
    `/applications/${applicationId}/choose`,
    body
  )
  return res
}
