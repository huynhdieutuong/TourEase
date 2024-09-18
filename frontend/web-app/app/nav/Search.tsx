'use client'

import { useFiltersStore } from '@/hooks/useFiltersStore'
import { useParamsStore } from '@/hooks/useParamsStore'
import { useEffect } from 'react'
import { FaSearch } from 'react-icons/fa'

export default function Search() {
  const setParams = useParamsStore((state) => state.setParams)
  const searchValue = useParamsStore((state) => state.searchValue)
  const setSearchValue = useParamsStore((state) => state.setSearchValue)
  const resetParams = useParamsStore((state) => state.resetParams)
  const resetFilters = useFiltersStore((state) => state.resetFilters)

  function handleChange(e: any) {
    setSearchValue(e.target.value)
  }

  function handleSearch() {
    resetFilters()
    setParams({ searchTerm: searchValue })
  }

  function handleEnter(e: any) {
    if (e.key === 'Enter') {
      handleSearch()
    }
  }

  return (
    <div className='flex w-[30%] items-center border-2 rounded-full shadow-sm'>
      <input
        className='w-full bg-transparent border-transparent
        focus:ring-0 focus:border-transparent focus:outline-none
        text-white placeholder:text-white placeholder:opacity-80'
        placeholder='Search for tour jobs by title or itinerary'
        type='text'
        value={searchValue}
        onChange={handleChange}
        onKeyDown={handleEnter}
      />
      <button>
        <FaSearch
          size={34}
          className='text-white rounded-full p-2 cursor-pointer mx-2'
          onClick={handleSearch}
        />
      </button>
    </div>
  )
}
