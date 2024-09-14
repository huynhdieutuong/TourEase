'use client'

import Image from 'next/image'
import React, { useState } from 'react'
import noImage from '../no-image.jpg'

type Props = {
  imageUrl?: string
  title: string
}

export default function CardImage({ imageUrl, title }: Props) {
  const [loading, setLoading] = useState(true)

  return (
    <Image
      src={imageUrl || noImage}
      alt={title}
      fill
      className={`group-hover:opacity-75 duration-700 ease-in-out ${
        loading
          ? 'grayscale blur-2xl scale-110'
          : 'grayscale-0 blur-0 scale-100'
      }`}
      sizes='(max-width: 768px) 100vw, (max-width:1200px) 50vw, 25vw'
      onLoad={() => setLoading(false)}
    />
  )
}
