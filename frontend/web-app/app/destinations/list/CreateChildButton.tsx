import { Button, Modal } from 'flowbite-react'
import { useState } from 'react'
import { FaPlus } from 'react-icons/fa'
import DestinationForm from '../DestinationForm'
import { Destination } from '@/types'

type Props = {
  parentId?: string | null
  addDestinationInState: (destination: Destination) => void
}
export default function CreateChildButton({
  parentId,
  addDestinationInState,
}: Props) {
  const [openModal, setOpenModal] = useState(false)

  return (
    <>
      <Button
        size='xs'
        color='yellow'
        title='Create Child'
        onClick={() => setOpenModal(true)}
      >
        <FaPlus />
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
              Create Child Destination
            </h3>
            <DestinationForm
              parentId={parentId}
              addDestinationInState={addDestinationInState}
              onCloseModal={() => setOpenModal(false)}
            />
          </div>
        </Modal.Body>
      </Modal>
    </>
  )
}
