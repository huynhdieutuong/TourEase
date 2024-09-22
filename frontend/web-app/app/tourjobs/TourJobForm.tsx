'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { SelectOption } from '@/types'
import { Currency, DestinationType, LanguageSpoken } from '@/types/enums'
import { tourJobSchema } from '@/validations/tourJobSchema'
import { yupResolver } from '@hookform/resolvers/yup'
import { Button } from 'flowbite-react'
import { useEffect, useState } from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import { FaDollarSign, FaUsers } from 'react-icons/fa'
import FormDate from '../components/FormDate'
import FormDropdown from '../components/FormDropdown'
import FormEnhancedDropdown from '../components/FormEnhancedDropdown'
import FormInput from '../components/FormInput'
import FormTextarea from '../components/FormTextarea'
import { createTourJob } from '../actions/tourJobActions'
import toast from 'react-hot-toast'
import { useRouter } from 'next/navigation'

export default function TourJobForm() {
  const router = useRouter()
  const destinations = useDestinationStore((state) => state.destinations)
  const countryOptions: SelectOption[] = destinations
    .filter((d) => d.type === DestinationType.COUNTRY)
    .map((d) => ({
      value: d.id,
      label: d.name,
    }))
  const [cityOptions, setCityOptions] = useState<SelectOption[]>([])

  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid },
    watch,
    setValue,
  } = useForm({
    mode: 'onTouched',
    resolver: yupResolver(tourJobSchema),
    defaultValues: {
      expiredDate: new Date(),
      startDate: new Date(),
      endDate: new Date(),
    },
  })
  const selectedCountryIds = watch('countries')?.map((c) => c.value)
  const selectedCities = watch('cities')

  useEffect(() => {
    // When select Country, CityOptions will update
    const cities = destinations
      .filter(
        (d) =>
          d.type === DestinationType.CITY &&
          selectedCountryIds?.includes(d.parentId)
      )
      .map((d) => ({ value: d.id, label: d.name }))
    setCityOptions(cities)

    // Remove Cities not contains in CityOptions after update Country
    const optionCityIds = cities?.map((x) => x.value)
    const filteredCities = selectedCities?.filter((selectedCity) =>
      optionCityIds?.includes(selectedCity.value)
    )
    setValue('cities', filteredCities)
  }, [selectedCountryIds?.length])

  async function onSubmit(data: FieldValues) {
    const { countries, cities, ...restData } = data
    const body = {
      destinationIds: [
        ...countries.map((country: SelectOption) => country.value),
        ...cities.map((city: SelectOption) => city.value),
      ],
      ...restData,
    }
    try {
      const res = await createTourJob(body)
      if (res.error) {
        throw res.error
      }
      router.push('/tourjobs/list')
    } catch (error: any) {
      toast.error(error.status + ' ' + error.message)
    }
  }

  return (
    <form
      className='grid grid-cols-1 md:grid-cols-2 gap-6'
      onSubmit={handleSubmit(onSubmit)}
    >
      <FormEnhancedDropdown
        name='countries'
        label='Countries'
        options={countryOptions}
        control={control}
        isMulti
        closeMenuOnSelect={false}
      />
      <FormEnhancedDropdown
        name='cities'
        label='Cities'
        options={cityOptions}
        control={control}
        isMulti
        closeMenuOnSelect={false}
      />
      <FormInput name='title' label='Title' control={control} />
      <FormDate
        name='expiredDate'
        label='Expired Date'
        dateFormat='dd MMMM yyyy h:mm a'
        showTimeSelect
        showIcon
        control={control}
      />
      <FormDate
        name='startDate'
        label='Start Date'
        dateFormat='dd MMMM yyyy'
        showIcon
        control={control}
      />
      <FormDate
        name='endDate'
        label='End Date'
        dateFormat='dd MMMM yyyy'
        showIcon
        control={control}
      />
      <FormDropdown
        name='currency'
        label='Salary Currency'
        options={[
          { value: Currency.USD, label: 'USD' },
          { value: Currency.VND, label: 'VND' },
        ]}
        control={control}
      />
      <FormInput
        name='salaryPerDay'
        label='Salary per Day'
        type='number'
        icon={FaDollarSign}
        control={control}
      />
      <FormInput
        name='participants'
        label='Number of Participants'
        type='number'
        icon={FaUsers}
        control={control}
      />
      <FormDropdown
        name='languageSpoken'
        label='Language Spoken'
        options={[
          { value: LanguageSpoken.English, label: 'English' },
          { value: LanguageSpoken.Vietnamese, label: 'Vietnamese' },
        ]}
        control={control}
      />
      <div className='col-span-full'>
        <FormTextarea name='itinerary' label='Itinerary' control={control} />
      </div>
      {/* <FormUploadImage name='image' control={control} /> */}

      <div className='flex justify-end items-end col-span-full'>
        <Button
          isProcessing={isSubmitting}
          disabled={!isValid}
          type='submit'
          color='warning'
          className='w-[100%]'
        >
          Submit Tour Job
        </Button>
      </div>
    </form>
  )
}
