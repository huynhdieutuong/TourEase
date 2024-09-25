'use client'

import { useDestinationStore } from '@/hooks/useDestinationStore'
import { Destination } from '@/types'
import { Table } from 'flowbite-react'
import Image from 'next/image'
import React from 'react'
import noImage from '../../no-image.jpg'
import CreateButton from './CreateButton'
import CreateChildButton from './CreateChildButton'
import DeleteButton from './DeleteButton'
import UpdateButton from './UpdateButton'

export default function DestinationTable() {
  const destinationsTree = useDestinationStore(
    (state) => state.destinationsTree
  )
  const destinationList = useDestinationStore((state) => state.destinations)
  const setDestinationList = useDestinationStore(
    (state) => state.setDestinations
  )

  function addDestinationInState(addedDestination: Destination) {
    setDestinationList([addedDestination, ...destinationList])
  }

  function removeDestinationInState(removedId: string) {
    setDestinationList(destinationList.filter((des) => des.id !== removedId))
  }

  function updateDestinationInState(updatedDestination: Destination) {
    const { name, slug, type } = updatedDestination
    const destinationIndex = destinationList.findIndex(
      (des) => des.id === updatedDestination.id
    )
    const destination = destinationList[destinationIndex]
    setDestinationList([
      ...destinationList.slice(0, destinationIndex),
      { ...destination, name, slug, type },
      ...destinationList.slice(destinationIndex + 1),
    ])
  }

  function renderDestinations(destinations: Destination[], level = 1) {
    return destinations.map((destination) => (
      <React.Fragment key={destination.id}>
        <Table.Row className='hover:bg-yellow-50'>
          <Table.Cell style={{ paddingLeft: `${level * 20}px` }}>
            <div className='flex items-center space-x-2'>
              <Image
                src={destination.imageUrl || noImage}
                alt={destination.name}
                width={40}
                height={40}
                className='object-cover rounded'
              />
              <span>{destination.name}</span>
            </div>
          </Table.Cell>
          <Table.Cell>{destination.type}</Table.Cell>
          <Table.Cell>{destination.slug}</Table.Cell>
          <Table.Cell>
            <div className='flex space-x-2'>
              <CreateChildButton
                parentId={destination.id}
                addDestinationInState={addDestinationInState}
              />
              <UpdateButton
                destination={destination}
                updateDestinationInState={updateDestinationInState}
              />
              <DeleteButton
                title={destination.name}
                id={destination.id}
                removeDestinationInState={removeDestinationInState}
              />
            </div>
          </Table.Cell>
        </Table.Row>
        {destination.subDestinations &&
          renderDestinations(destination.subDestinations, level + 1)}
      </React.Fragment>
    ))
  }

  return (
    <>
      <CreateButton addDestinationInState={addDestinationInState} />
      <div className='overflow-x-auto mt-4'>
        <Table hoverable>
          <Table.Head>
            <Table.HeadCell>Destination</Table.HeadCell>
            <Table.HeadCell>Type</Table.HeadCell>
            <Table.HeadCell>Slug</Table.HeadCell>
            <Table.HeadCell>Actions</Table.HeadCell>
          </Table.Head>
          <Table.Body className='divide-y text-gray-600'>
            {renderDestinations(destinationsTree)}
          </Table.Body>
        </Table>
      </div>
    </>
  )
}
