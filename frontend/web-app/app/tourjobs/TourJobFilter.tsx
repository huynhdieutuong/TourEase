import { useDestinationStore } from '@/hooks/useDestinationStore'
import { useFiltersStore } from '@/hooks/useFiltersStore'
import { useParamsStore } from '@/hooks/useParamsStore'
import { FilterOption } from '@/types'
import { DestinationType } from '@/types/enums'
import { Checkbox, Label } from 'flowbite-react'
import { useEffect, useState } from 'react'
import { useShallow } from 'zustand/shallow'
import FilterDropdown from '../components/FilterDropdown'

const durationOptions: FilterOption[] = [
  { label: '1 - 5 Days', value: '1-5' },
  { label: '6 - 10 Days', value: '6-10' },
  { label: '11 - 16 Days', value: '11-16' },
  { label: '> 16 Days', value: '16' },
]

const currencyOptions: FilterOption[] = [
  { label: 'USD', value: '$' },
  { label: 'VND', value: 'đ' },
]

export default function Filters() {
  const setParams = useParamsStore((state) => state.setParams)
  const resetSearch = useParamsStore((state) => state.resetSearch)
  const setFilters = useFiltersStore((state) => state.setFilters)
  const filters = useFiltersStore(
    useShallow((state) => ({
      selectedCountry: state.selectedCountry,
      selectedCity: state.selectedCity,
      selectedDuration: state.selectedDuration,
      selectedCurrency: state.selectedCurrency,
      includeFinished: state.includeFinished,
    }))
  )

  const [cityOptions, setCityOptions] = useState<FilterOption[]>([])
  const destinations = useDestinationStore((state) => state.destinations)
  const countryOptions: FilterOption[] = destinations
    ?.filter((x) => !x.parentId && x.type === DestinationType.COUNTRY)
    .map((des) => ({ label: des.name, value: des.id }))

  useEffect(() => {
    if (filters.selectedCountry) {
      const options = destinations
        .filter(
          (x) =>
            x.parentId === filters.selectedCountry &&
            x.type === DestinationType.CITY
        )
        .map((des) => ({ label: des.name, value: des.id }))
      setCityOptions(options)
    }
  }, [filters.selectedCountry])

  useEffect(() => {
    setParams({
      destinationIds: filters.selectedCity || filters.selectedCountry,
      duration: filters.selectedDuration,
      currency: filters.selectedCurrency,
      includeFinished: filters.includeFinished,
    })
  }, [JSON.stringify(filters)])

  function handleSelectCountry(country: string) {
    resetSearch()
    setFilters({
      selectedCountry: country,
      selectedCity: '',
    })
  }

  return (
    <div className='flex items-center w-[60%]'>
      <span className='uppercase text-sm text-gray-500 mr-2'>Filter by</span>
      <div className='flex justify-between flex-grow'>
        <FilterDropdown
          options={countryOptions}
          label='Country'
          selectedValue={filters.selectedCountry}
          onSelect={handleSelectCountry}
        />
        <FilterDropdown
          options={cityOptions}
          label='City'
          selectedValue={filters.selectedCity}
          onSelect={(city) => setFilters({ selectedCity: city })}
          disabled={!filters.selectedCountry}
        />
        <FilterDropdown
          options={durationOptions}
          label='Duration'
          selectedValue={filters.selectedDuration}
          onSelect={(duration) => setFilters({ selectedDuration: duration })}
        />
        <FilterDropdown
          options={currencyOptions}
          label='Currency'
          selectedValue={filters.selectedCurrency}
          onSelect={(currency) => setFilters({ selectedCurrency: currency })}
        />
        <div className='flex items-center gap-2'>
          <Checkbox
            id='finished'
            checked={filters.includeFinished}
            onChange={() =>
              setFilters({ includeFinished: !filters.includeFinished })
            }
            color='yellow'
          />
          <Label htmlFor='finished'>Include Finished</Label>
        </div>
      </div>
    </div>
  )
}
