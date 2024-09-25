import { Destination } from '@/types'
import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import { FaPencilAlt } from 'react-icons/fa'
import DestinationForm from '../DestinationForm'

type Props = {
  destination: Destination
  updateDestinationInState: (destination: Destination) => void
}
export default function UpdateButton({
  destination,
  updateDestinationInState,
}: Props) {
  const [openModal, setOpenModal] = useState(false)

  return (
    <>
      <Button
        size='xs'
        color='blue'
        title='Update'
        onClick={() => setOpenModal(true)}
      >
        <FaPencilAlt />
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
              Update Destination
            </h3>
            <DestinationForm
              destination={destination}
              updateDestinationInState={updateDestinationInState}
              onCloseModal={() => setOpenModal(false)}
            />
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
