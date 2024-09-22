// import { getTokenWorkaround } from '@/app/actions/authActions'
import { auth } from '@/auth'

const baseUrl = 'http://localhost:6001'

async function get(url: string) {
  const requestOptions = {
    method: 'GET',
    headers: await getHeaders(),
  }

  const response = await fetch(baseUrl + url, requestOptions)
  return await handleResponse(response)
}

async function post(url: string, body: object) {
  const requestOptions = {
    method: 'POST',
    headers: await getHeaders(),
    body: JSON.stringify(body),
  }

  const response = await fetch(baseUrl + url, requestOptions)
  return await handleResponse(response)
}

async function put(url: string, body: object) {
  const requestOptions = {
    method: 'PUT',
    headers: await getHeaders(),
    body: JSON.stringify(body),
  }

  const response = await fetch(baseUrl + url, requestOptions)
  return await handleResponse(response)
}

async function del(url: string) {
  const requestOptions = {
    method: 'DELETE',
    headers: await getHeaders(),
  }

  const response = await fetch(baseUrl + url, requestOptions)
  return await handleResponse(response)
}

async function getHeaders() {
  const session = await auth()
  const headers = { 'Content-type': 'application/json' } as any
  if (session) {
    headers.Authorization = `Bearer ${session.accessToken}`
  }
  return headers
}

async function handleResponse(response: Response) {
  const contentType = response.headers.get('Content-Type') || ''

  // Check if the content type is JSON
  let data
  if (contentType.includes('application/json')) {
    const text = await response.text()
    data = text ? JSON.parse(text) : null
  } else {
    // Fallback to raw text for non-JSON responses
    data = await response.text()
  }

  if (response.ok) {
    return data || response.statusText
  } else {
    const error = {
      status: response.status,
      message: data || response.statusText,
    }
    console.log(error)
    return { error }
  }
}

export const fetchWrapper = {
  get,
  post,
  put,
  del,
}
