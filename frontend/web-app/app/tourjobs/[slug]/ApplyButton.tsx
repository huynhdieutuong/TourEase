'use client'

import { Button, Modal } from 'flowbite-react'
import React, { useState } from 'react'
import ApplicationForm from './ApplicationForm'

type Props = {
  tourJobId: string
}

export default function ApplyButton({ tourJobId }: Props) {
  const [openModal, setOpenModal] = useState(false)

  return (
    <>
      <Button color='warning' onClick={() => setOpenModal(true)}>
        Apply Now
      </Button>
      <Modal
        show={openModal}
        size='2xl'
        onClose={() => setOpenModal(false)}
        popup
      >
        <Modal.Header />
        <Modal.Body>
          <div className='space-y-2'>
            <h3 className='text-xl font-medium'>Apply to this tour job</h3>
            <ApplicationForm
              tourJobId={tourJobId}
              onCloseModal={() => setOpenModal(false)}
            />
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
