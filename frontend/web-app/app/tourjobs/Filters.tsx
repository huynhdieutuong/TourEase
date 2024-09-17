import { FilterOption } from '@/types'
import { HiSortAscending, HiSortDescending } from 'react-icons/hi'
import FilterDropdown from '../components/FilterDropdown'
import { useDestinationStore } from '@/hooks/useDestinationStore'
import { useParamsStore } from '@/hooks/useParamsStore'
import { DestinationType } from '@/types/enums'
import { useEffect, useState } from 'react'
import { Checkbox, Label } from 'flowbite-react'

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

const orderOptions: FilterOption[] = [
  { label: 'End date', value: 'end' },
  { label: 'Recently added', value: 'new' },
  { label: 'Ascending salary', value: 'ascSalary', icon: HiSortAscending },
  { label: 'Descending salary', value: 'dscSalary', icon: HiSortDescending },
]

export default function Filters() {
  const destinations = useDestinationStore((state) => state.destinations)
  const setParams = useParamsStore((state) => state.setParams)
  const [cityOptions, setCityOptions] = useState<FilterOption[]>([])
  const [selectedCountry, setSelectedCountry] = useState('')
  const [selectedCity, setSelectedCity] = useState('')
  const [selectedDuration, setSelectedDuration] = useState('')
  const [selectedCurrency, setSelectedCurrency] = useState('')
  const [includeFinished, setIncludeFinished] = useState(false)
  const [orderBy, setOrderBy] = useState('end')

  const countryOptions: FilterOption[] = destinations
    .filter((x) => !x.parentId && x.type === DestinationType.COUNTRY)
    .map((des) => ({ label: des.name, value: des.id }))

  useEffect(() => {
    if (selectedCountry) {
      const options = destinations
        .filter(
          (x) =>
            x.parentId === selectedCountry && x.type === DestinationType.CITY
        )
        .map((des) => ({ label: des.name, value: des.id }))
      setCityOptions(options)
    }
  }, [selectedCountry])

  useEffect(() => {
    const destinationIds = selectedCity || selectedCountry
    setParams({ destinationIds })
  }, [selectedCountry, selectedCity])

  useEffect(() => {
    setParams({ duration: selectedDuration })
  }, [selectedDuration])

  useEffect(() => {
    setParams({ currency: selectedCurrency })
  }, [selectedCurrency])

  useEffect(() => {
    setParams({ includeFinished })
  }, [includeFinished])

  useEffect(() => {
    setParams({ orderBy })
  }, [orderBy])

  return (
    <div className='flex justify-between mb-6'>
      <div className='flex items-center w-[60%]'>
        <span className='uppercase text-sm text-gray-500 mr-2'>Filter by</span>
        <div className='flex justify-between flex-grow'>
          <FilterDropdown
            options={countryOptions}
            label='Country'
            selectedValue={selectedCountry}
            onSelect={(country) => {
              setSelectedCountry(country)
              setSelectedCity('')
            }}
          />
          <FilterDropdown
            options={cityOptions}
            label='City'
            selectedValue={selectedCity}
            onSelect={setSelectedCity}
            disabled={!selectedCountry}
          />
          <FilterDropdown
            options={durationOptions}
            label='Duration'
            selectedValue={selectedDuration}
            onSelect={setSelectedDuration}
          />
          <FilterDropdown
            options={currencyOptions}
            label='Currency'
            selectedValue={selectedCurrency}
            onSelect={setSelectedCurrency}
          />
          <div className='flex items-center gap-2'>
            <Checkbox
              id='finished'
              checked={includeFinished}
              onChange={() => setIncludeFinished(!includeFinished)}
              color='warning'
            />
            <Label htmlFor='finished'>Include Finished</Label>
          </div>
        </div>
      </div>
      <div className='flex items-center'>
        <span className='uppercase text-sm text-gray-500 mr-2'>Order by</span>
        <FilterDropdown
          options={orderOptions}
          label='Order'
          selectedValue={orderBy}
          onSelect={setOrderBy}
        />
      </div>
    </div>
  )
}
