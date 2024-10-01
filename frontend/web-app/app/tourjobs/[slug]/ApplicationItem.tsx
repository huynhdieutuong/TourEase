'use client'

import { Application } from '@/types'
import Image from 'next/image'
import React, { useState } from 'react'
import noImage from '../../no-image.jpg'
import { formatDate } from '@/utils'
import { Button } from 'flowbite-react'
import ChooseButton from './ChooseButton'

type Props = {
  application: Application
}

export default function ApplicationItem({ application }: Props) {
  const [showChooseButton, setShowChooseButton] = useState(false)

  return (
    <div
      className='flex items-center space-x-4 relative'
      onMouseEnter={() => setShowChooseButton(true)}
      onMouseLeave={() => setShowChooseButton(false)}
    >
      <div className='shrink-0'>
        <Image
          src={noImage}
          alt={application.tourGuide}
          height='32'
          width='32'
          className='rounded-full'
        />
      </div>
      <div className='min-w-0 flex-1'>
        <div className='flex justify-between gap-4'>
          <p className='truncate text-sm font-medium'>
            {application.tourGuide}
          </p>
          <p className='text-xs italic min-w-24'>
            {formatDate(application.appliedDate)}
          </p>
        </div>
        <p className='text-sm line-clamp-3'>{application.comment}</p>
      </div>
      {showChooseButton && (
        <ChooseButton
          applicationId={application.id}
          tourGuide={application.tourGuide}
        />
      )}
    </div>
  )
}
