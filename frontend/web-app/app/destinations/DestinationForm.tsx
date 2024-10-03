'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { Destination } from '@/types'
import { DesType } from '@/types/enums'
import { destinationSchema } from '@/validations/destinationSchema'
import { yupResolver } from '@hookform/resolvers/yup'
import { Button } from 'flowbite-react'
import { usePathname } from 'next/navigation'
import { useEffect } from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import toast from 'react-hot-toast'
import { AiOutlineLoading } from 'react-icons/ai'
import {
  createDestination,
  updateDestination,
} from '../actions/destinationActions'
import FormDropdown from '../components/FormDropdown'
import FormEnhancedDropdown from '../components/FormEnhancedDropdown'
import FormInput from '../components/FormInput'

type Props = {
  destination?: Destination
  parentId?: string | null
  updateDestinationInState?: (destination: Destination) => void
  addDestinationInState?: (destination: Destination) => void
  onCloseModal: () => void
}

export default function DestinationForm({
  destination,
  parentId,
  updateDestinationInState,
  addDestinationInState,
  onCloseModal,
}: Props) {
  const pathname = usePathname()
  const destinations = useDestinationStore((state) => state.destinations)

  const defaultSelectedDestination = { value: null, label: 'None' }
  const parentOptions = destinations
    .filter((des) => des.parentId == null)
    .map((des) => ({
      value: des.id,
      label: des.name,
    }))

  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid },
    reset,
  } = useForm({
    mode: 'onTouched',
    resolver: yupResolver(destinationSchema),
    defaultValues: {
      parent: defaultSelectedDestination,
    },
  })

  useEffect(() => {
    if (parentId !== undefined) {
      reset({
        parent: {
          value: parentId,
          label:
            destinations.find((des) => des.id === parentId)?.name || 'None',
        },
      })
    }

    if (destination) {
      reset({
        name: destination.name,
        type: DesType[destination.type],
        parent: {
          value: destination.parentId,
          label:
            destinations.find((des) => des.id === destination.parentId)?.name ||
            'None',
        },
      })
    }
  }, [destination, parentId])

  async function onSubmit(data: FieldValues) {
    const body = {
      name: data.name,
      type: data.type,
      parentId: data.parent.value,
    }

    let res
    if (!destination) {
      res = await createDestination(body)
      if (!res.isSucceeded) return toast.error(res.message)

      if (typeof addDestinationInState === 'function')
        addDestinationInState(res.data)
    } else {
      res = await updateDestination(destination.id, body)
      if (!res.isSucceeded) return toast.error(res.message)

      if (typeof updateDestinationInState === 'function')
        updateDestinationInState(res.data)
    }
    onCloseModal()
  }

  return (
    <form className='grid gap-6 m-auto' onSubmit={handleSubmit(onSubmit)}>
      <FormInput name='name' label='Name' control={control} />

      <FormDropdown
        name='type'
        label='Type'
        options={[
          { value: 0, label: 'Country' },
          { value: 1, label: 'City' },
        ]}
        control={control}
      />

      {!destination && (
        <FormEnhancedDropdown
          name='parent'
          label='Parent'
          options={[defaultSelectedDestination, ...parentOptions]}
          control={control}
          isDisabled={parentId !== undefined}
        />
      )}
      <div className='flex justify-end items-end mt-4'>
        <Button
          isProcessing={isSubmitting}
          processingSpinner={
            <AiOutlineLoading className='h-6 w-6 animate-spin' />
          }
          disabled={!isValid}
          type='submit'
          color='warning'
          className='w-[100%]'
        >
          {pathname === '/destinations/create' ? 'Create' : 'Update'}{' '}
          Destination
        </Button>
      </div>
    </form>
  )
}
