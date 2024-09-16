'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import { FaSearch } from 'react-icons/fa'

export default function Search() {
  const setParams = useParamsStore((state) => state.setParams)
  const value = useParamsStore((state) => state.searchValue)
  const setValue = useParamsStore((state) => state.setSearchValue)

  function handleChange(e: any) {
    setValue(e.target.value)
  }

  function handleSearch() {
    setParams({ searchTerm: value })
  }

  function handleEnter(e: any) {
    if (e.key === 'Enter') {
      setParams({ searchTerm: value })
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
        value={value}
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
