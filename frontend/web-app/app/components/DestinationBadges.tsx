'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { Badge } from 'flowbite-react'
import React from 'react'
import { FaMapMarkerAlt } from 'react-icons/fa'

type Props = {
  destinationIds: string[]
  showIcon?: boolean
}

export default function DestinationBadges({ destinationIds, showIcon }: Props) {
  const destinations = useDestinationStore((state) => state.destinations)

  return (
    <div className='flex items-center'>
      {showIcon && <FaMapMarkerAlt className='text-yellow-500 mr-2' />}
      <span className='flex gap-2'>
        {destinations
          .filter((des) => destinationIds.includes(des.id))
          .map((des) => (
            <Badge key={des.id} color='success'>
              {des.name}
            </Badge>
          ))}
      </span>
    </div>
  )
}
