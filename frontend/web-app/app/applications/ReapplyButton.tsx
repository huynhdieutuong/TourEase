'use client'

import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import toast from 'react-hot-toast'
import { HiOutlineExclamationCircle } from 'react-icons/hi'
import { reapplyApplication } from '../actions/applicationActions'
import { AiOutlineLoading } from 'react-icons/ai'

type Props = {
  applicationId: string
  isFinished: boolean
  updateStatus: () => void
}

export default function ReapplyButton({
  applicationId,
  isFinished,
  updateStatus,
}: Props) {
  const [openModal, setOpenModal] = useState(false)
  const [loading, setLoading] = useState(false)

  async function handleReapplyApplication(applicationId: string) {
    setLoading(true)
    const res = await reapplyApplication(applicationId)
    if (!res.isSucceeded) {
      toast.error(res.message)
    } else {
      updateStatus()
    }
    setOpenModal(false)
    setLoading(false)
  }

  return (
    <>
      <Button
        size='xs'
        color='green'
        title='Reapply Application'
        disabled={isFinished}
        onClick={() => setOpenModal(true)}
      >
        Reapply
      </Button>
      <Modal
        show={openModal}
        size='md'
        onClose={() => setOpenModal(false)}
        popup
      >
        <Modal.Header />
        <Modal.Body>
          <div className='text-center'>
            <HiOutlineExclamationCircle className='mx-auto mb-4 h-14 w-14 text-gray-400 dark:text-gray-200' />
            <h3 className='mb-5 text-lg font-normal text-gray-500 dark:text-gray-400'>
              Reapply {applicationId}?
            </h3>
            <div className='flex justify-center gap-4'>
              <Button
                color='success'
                isProcessing={loading}
                processingSpinner={
                  <AiOutlineLoading className='h-6 w-6 animate-spin' />
                }
                onClick={() => handleReapplyApplication(applicationId)}
              >
                {"Yes, I'm sure"}
              </Button>
              <Button color='gray' onClick={() => setOpenModal(false)}>
                No
              </Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
