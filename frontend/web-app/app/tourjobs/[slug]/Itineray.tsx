'use client'

import { Button } from 'flowbite-react'
import React, { useState } from 'react'

type Props = {
  itinerary: string
}

export default function Itineray({ itinerary }: Props) {
  const [showItinerary, setShowItinerary] = useState(false)

  function toggleItinerary() {
    setShowItinerary(!showItinerary)
  }

  return (
    <div>
      <div className='mb-6'>
        <Button color='yellow' onClick={toggleItinerary}>
          {showItinerary ? 'Hide Itinerary' : 'Show Itinerary'}
        </Button>
      </div>

      {showItinerary && (
        <div className='bg-gray-100 p-4 rounded-md mb-6 transition-all duration-300'>
          <h3 className='text-xl font-semibold mb-2'>Itinerary</h3>
          <p>{itinerary}</p>
        </div>
      )}
    </div>
  )
}
