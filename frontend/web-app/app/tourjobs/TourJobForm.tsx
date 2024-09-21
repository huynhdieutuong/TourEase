'use client'

import React, { useEffect } from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import FormInput from '../components/FormInput'
import FormDropdown from '../components/FormDropdown'
import FormTextarea from '../components/FormTextarea'
import FormDate from '../components/FormDate'
import { Button } from 'flowbite-react'
import FormUploadImage from '../components/FormUploadImage'
import { yupResolver } from '@hookform/resolvers/yup'
import { tourJobSchema } from '@/validations/tourJobSchema'
import { Currency, LanguageSpoken } from '@/types/enums'

export default function TourJobForm() {
  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid },
  } = useForm({
    mode: 'onTouched',
    resolver: yupResolver(tourJobSchema),
    defaultValues: {
      expiredDate: new Date(),
      startDate: new Date(),
      endDate: new Date(),
    },
  })

  async function onSubmit(data: FieldValues) {
    console.log(data)
  }

  return (
    <form
      className='grid grid-cols-1 md:grid-cols-2 gap-6'
      onSubmit={handleSubmit(onSubmit)}
    >
      <FormInput name='title' label='Title' control={control} />
      <FormDate
        name='expiredDate'
        label='Expired Date'
        dateFormat='dd MMMM yyyy h:mm a'
        showTimeSelect
        control={control}
      />
      <FormDate
        name='startDate'
        label='Start Date'
        dateFormat='dd MMMM yyyy'
        control={control}
      />
      <FormDate
        name='endDate'
        label='End Date'
        dateFormat='dd MMMM yyyy'
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
        name='salary'
        label='Salary per Day'
        type='number'
        control={control}
      />
      <FormInput
        name='participants'
        label='Number of Participants'
        type='number'
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
      <FormTextarea
        name='itinerary'
        label='Itinerary'
        control={control}
        fullwidth
      />
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
