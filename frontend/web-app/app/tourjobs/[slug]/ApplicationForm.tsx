import { applyTourJob } from '@/app/actions/applicationActions'
import FormTextarea from '@/app/components/FormTextarea'
import { applicationSchema } from '@/validations/applicationSchema'
import { yupResolver } from '@hookform/resolvers/yup'
import { Button } from 'flowbite-react'
import React from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import toast from 'react-hot-toast'
import { AiOutlineLoading } from 'react-icons/ai'

type Props = {
  tourJobId: string
  onCloseModal: () => void
}

export default function ApplicationForm({ tourJobId, onCloseModal }: Props) {
  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid },
    reset,
  } = useForm({
    mode: 'onTouched',
    resolver: yupResolver(applicationSchema),
  })

  async function onSubmit(data: FieldValues) {
    const body = {
      tourJobId,
      comment: data.comment,
    }
    const res = await applyTourJob(body)
    if (!res.isSucceeded) toast.error(res.message)
    reset()
    onCloseModal()
  }

  return (
    <form className='grid gap-6 m-auto' onSubmit={handleSubmit(onSubmit)}>
      <FormTextarea name='comment' label='Comment' rows={8} control={control} />
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
          Submit
        </Button>
      </div>
    </form>
  )
}
