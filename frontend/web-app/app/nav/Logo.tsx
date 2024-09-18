'use client'

import Image from 'next/image'
import logo from '../logo.png'
import { useParamsStore } from '@/hooks/useParamsStore'
import { useFiltersStore } from '@/hooks/useFiltersStore'

export default function Logo() {
  const resetParams = useParamsStore((state) => state.resetParams)
  const resetFilters = useFiltersStore((state) => state.resetFilters)

  function handleRemoveFilters() {
    resetFilters()
    resetParams()
  }

  return (
    <div>
      <Image
        className='cursor-pointer'
        src={logo}
        alt='logo'
        width={80}
        sizes='100vw'
        priority
        onClick={handleRemoveFilters}
      />
    </div>
  )
}
