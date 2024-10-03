'use client'

import { deleteDestination } from '@/app/actions/destinationActions'
import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import toast from 'react-hot-toast'
import { FaTrashAlt } from 'react-icons/fa'
import { HiOutlineExclamationCircle } from 'react-icons/hi'

type Props = {
  title: string
  id: string
  removeDestinationInState: (id: string) => void
}

export default function DeleteButton({
  title,
  id,
  removeDestinationInState,
}: Props) {
  const [openModal, setOpenModal] = useState(false)

  async function handleDeleteDestination(id: string) {
    const res = await deleteDestination(id)
    if (!res.isSucceeded) {
      toast.error(res.message)
    } else {
      removeDestinationInState(id)
    }
    setOpenModal(false)
  }

  return (
    <>
      <Button
        size='xs'
        color='red'
        title='Delete'
        onClick={() => setOpenModal(true)}
      >
        <FaTrashAlt />
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
              Delete {title}?
            </h3>
            <div className='flex justify-center gap-4'>
              <Button
                color='failure'
                onClick={() => handleDeleteDestination(id)}
              >
                {"Yes, I'm sure"}
              </Button>
              <Button color='gray' onClick={() => setOpenModal(false)}>
                No, cancel
              </Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
