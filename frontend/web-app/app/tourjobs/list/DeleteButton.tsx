'use client'

import { deleteTourJob } from '@/app/actions/tourJobActions'
import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import toast from 'react-hot-toast'
import { FaTrashAlt } from 'react-icons/fa'
import { HiOutlineExclamationCircle } from 'react-icons/hi'

type Props = {
  title: string
  id: string
  updateJobList: (id: string) => void
}

export function DeleteButton({ title, id, updateJobList }: Props) {
  const [openModal, setOpenModal] = useState(false)

  async function handleDeleteTourJob(id: string) {
    const res = await deleteTourJob(id)
    if (!res.isSucceeded) {
      toast.error(res.message)
    } else {
      updateJobList(id)
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
              <Button color='failure' onClick={() => handleDeleteTourJob(id)}>
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
