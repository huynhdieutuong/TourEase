'use client'

import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'
import React from 'react'

export default function LoginButton() {
  return (
    <Button
      outline
      color='warning'
      onClick={() =>
        signIn('id-server', { redirectTo: '/' }, { prompt: 'login' })
      }
    >
      Login
    </Button>
  )
}
