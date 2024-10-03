'use client'

import { Button, Modal } from 'flowbite-react'
import React, { useState } from 'react'
import ApplicationForm from './ApplicationForm'
import { User } from 'next-auth'
import { signIn } from 'next-auth/react'

type Props = {
  tourJobId: string
  tourJobSlug: string
  user: User | null
}

export default function ApplyButton({ tourJobId, tourJobSlug, user }: Props) {
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
            <h3 className='text-xl font-medium'>
              {user
                ? 'Apply to this tour job'
                : 'Please login to apply for this job'}
            </h3>
            {user ? (
              <ApplicationForm
                tourJobId={tourJobId}
                onCloseModal={() => setOpenModal(false)}
              />
            ) : (
              <Button
                color='warning'
                className='mt-5'
                onClick={() =>
                  signIn('id-server', {
                    redirectTo: `/tourjobs/${tourJobSlug}`,
                  })
                }
              >
                Login
              </Button>
            )}
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
