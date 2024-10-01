'use client'

import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import toast from 'react-hot-toast'
import { HiOutlineExclamationCircle } from 'react-icons/hi'
import { reapplyApplication } from '../actions/applicationActions'

type Props = {
  applicationId: string
}

export default function ReapplyButton({ applicationId }: Props) {
  const [openModal, setOpenModal] = useState(false)

  async function handleCancelApplication(applicationId: string) {
    try {
      await reapplyApplication(applicationId)
      setOpenModal(false)
    } catch (error: any) {
      toast.error(error.message)
    }
  }

  return (
    <>
      <Button
        size='xs'
        color='green'
        title='Reapply Application'
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
                onClick={() => handleCancelApplication(applicationId)}
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
