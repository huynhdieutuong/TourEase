'use client'

import { useFiltersStore } from '@/hooks/useFiltersStore'
import { useParamsStore } from '@/hooks/useParamsStore'
import Image from 'next/image'
import { usePathname, useRouter } from 'next/navigation'
import logo from '../logo.png'

export default function Logo() {
  const router = useRouter()
  const pathname = usePathname()

  const resetParams = useParamsStore((state) => state.resetParams)
  const resetFilters = useFiltersStore((state) => state.resetFilters)

  function handleRemoveFilters() {
    if (pathname !== '/') router.push('/')
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
