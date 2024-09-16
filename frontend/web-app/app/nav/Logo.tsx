'use client'

import Image from 'next/image'
import logo from '../logo.png'
import { useParamsStore } from '@/hooks/useParamsStore'

export default function Logo() {
  const reset = useParamsStore((state) => state.reset)

  return (
    <div>
      <Image
        className='cursor-pointer'
        src={logo}
        alt='logo'
        width={80}
        sizes='100vw'
        priority
        onClick={reset}
      />
    </div>
  )
}
