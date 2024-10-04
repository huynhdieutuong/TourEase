'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { Destination } from '@/types'
import { User } from 'next-auth'
import { signIn, signOut } from 'next-auth/react'

type Props = {
  destinations: Destination[]
  user: User | null
}

export default function WrapperProvider({ destinations, user }: Props) {
  if (user?.error === 'TokenExpired') {
    signOut() // Sign out the user
    signIn() // Redirect to sign in page
  }

  const setDestinations = useDestinationStore((state) => state.setDestinations)
  setDestinations(destinations)

  return <></>
}
