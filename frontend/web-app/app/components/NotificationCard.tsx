import Image from 'next/image'
import Link from 'next/link'
import React from 'react'
import noImage from '../no-image.jpg'

type Props = {
  imageUrl?: string
  linkUrl: string
  message: string
}

export default function NotificationCard({
  imageUrl,
  linkUrl,
  message,
}: Props) {
  return (
    <Link href={linkUrl} className='flex flex-col items-center'>
      <div className='flex flex-row items-center gap-2'>
        <Image
          src={imageUrl || noImage}
          alt='Image of tour job'
          height={80}
          width={80}
          className='rounded-lg w-auto h-auto'
        />
        <span>{message}</span>
      </div>
    </Link>
  )
}
