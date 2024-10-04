'use client'

import { Application } from '@/types'
import { formatDate } from '@/utils'
import Image from 'next/image'
import { useState } from 'react'
import noImage from '../../no-image.jpg'
import ChooseButton from './ChooseButton'

type Props = {
  application: Application
  isOwner: boolean | undefined
  isCompleted: boolean
}

export default function ApplicationItem({
  application,
  isOwner,
  isCompleted,
}: Props) {
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
      {!isCompleted && isOwner && showChooseButton && (
        <ChooseButton
          applicationId={application.id}
          tourGuide={application.tourGuide}
        />
      )}
    </div>
  )
}
