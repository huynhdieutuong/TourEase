'use client'

import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'
import React from 'react'

export default function SignIn({
  searchParams,
}: {
  searchParams: { callbackUrl: string }
}) {
  return (
    <div className='h-[40vh] border-2 rounded-md shadow-md flex justify-center items-center flex-col'>
      <h3 className='text-2xl font-bold'>
        You need to be logged in to do that
      </h3>
      <span className='mt-2'>Please click below to login</span>
      <Button
        onClick={() =>
          signIn('id-server', { redirectTo: searchParams.callbackUrl })
        }
        className='mt-4 text-sm font-bold'
        color='yellow'
      >
        Login
      </Button>
    </div>
  )
}
