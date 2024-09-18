import React, { useEffect, useState } from 'react'
import FilterDropdown from '../components/FilterDropdown'
import { FilterOption } from '@/types'
import { HiSortAscending, HiSortDescending } from 'react-icons/hi'
import { useParamsStore } from '@/hooks/useParamsStore'

const orderOptions: FilterOption[] = [
  { label: 'End date', value: 'end' },
  { label: 'Recently added', value: 'new' },
  { label: 'Ascending salary', value: 'ascSalary', icon: HiSortAscending },
  { label: 'Descending salary', value: 'dscSalary', icon: HiSortDescending },
]

export default function TourJobOrder() {
  const [orderBy, setOrderBy] = useState('end')
  const setParams = useParamsStore((state) => state.setParams)

  useEffect(() => {
    setParams({ orderBy })
  }, [orderBy])

  return (
    <div className='flex items-center'>
      <span className='uppercase text-sm text-gray-500 mr-2'>Order by</span>
      <FilterDropdown
        options={orderOptions}
        label='Order'
        selectedValue={orderBy}
        onSelect={setOrderBy}
      />
    </div>
  )
}
