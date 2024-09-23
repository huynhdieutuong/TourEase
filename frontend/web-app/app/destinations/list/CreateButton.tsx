'use client'

import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import DestinationForm from '../DestinationForm'
import { Destination } from '@/types'

type Props = {
  addDestinationInState: (destination: Destination) => void
}

export default function CreateButton({ addDestinationInState }: Props) {
  const [openModal, setOpenModal] = useState(false)

  return (
    <>
      <Button size='sm' color='warning' onClick={() => setOpenModal(true)}>
        Create Destination
      </Button>
      <Modal
        show={openModal}
        size='md'
        onClose={() => setOpenModal(false)}
        popup
      >
        <Modal.Header />
        <Modal.Body>
          <div className='space-y-6'>
            <h3 className='text-xl font-medium text-gray-900 dark:text-white'>
              Create Destination
            </h3>
            <DestinationForm
              addDestinationInState={addDestinationInState}
              onCloseModal={() => setOpenModal(false)}
            />
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
