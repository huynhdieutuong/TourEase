'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { Destination } from '@/types'

type Props = {
  destinations: Destination[]
}

export default function WrapperProvider({ destinations }: Props) {
  const setDestinations = useDestinationStore((state) => state.setDestinations)
  setDestinations(destinations)

  return <></>
}
