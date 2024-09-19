import { auth } from '@/auth'
import React from 'react'
import AuthTest from './AuthTest'

export default async function Session() {
  const session = await auth()

  return (
    <div>
      <pre className='whitespace-pre-wrap break-all'>
        {JSON.stringify(session, null, 2)}
      </pre>

      <div className='mt-4'>
        <AuthTest />
      </div>
    </div>
  )
}
