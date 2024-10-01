'use client'

import { Application } from '@/types'
import { Badge, Button, Table } from 'flowbite-react'
import Image from 'next/image'
import React from 'react'
import noImage from '../no-image.jpg'
import { formatDate } from '@/utils'
import CancelButton from './CancelButton'
import ReapplyButton from './ReapplyButton'
import { ApplicationStatus } from '@/types/enums'

type Props = {
  applications: Application[]
}

export default function ApplicationTable({ applications }: Props) {
  function renderBadgeColor(status: ApplicationStatus) {
    switch (status) {
      case ApplicationStatus.Pending:
        return 'warning'
      case ApplicationStatus.Canceled:
        return 'gray'
      case ApplicationStatus.Rejected:
        return 'failure'
      case ApplicationStatus.Accepted:
        return 'success'
      default:
        return 'info'
    }
  }
  return (
    <div className='overflow-x-auto mt-4'>
      <Table hoverable>
        <Table.Head>
          <Table.HeadCell>TourJob</Table.HeadCell>
          <Table.HeadCell>Comment</Table.HeadCell>
          <Table.HeadCell>Date</Table.HeadCell>
          <Table.HeadCell>Status</Table.HeadCell>
          <Table.HeadCell>Actions</Table.HeadCell>
        </Table.Head>
        <Table.Body className='divide-y text-gray-600'>
          {applications?.map((application) => (
            <Table.Row key={application.id} className='hover:bg-yellow-50'>
              <Table.Cell>
                <div className='flex items-center space-x-2'>
                  <Image
                    src={noImage}
                    alt={application.tourJobId}
                    width={40}
                    height={40}
                    className='object-cover rounded'
                  />
                  <span>{application.tourJobId}</span>
                </div>
              </Table.Cell>
              <Table.Cell>{application.comment}</Table.Cell>
              <Table.Cell>{formatDate(application.appliedDate)}</Table.Cell>
              <Table.Cell>
                <Badge color={renderBadgeColor(application.status)}>
                  {application.status}
                </Badge>
              </Table.Cell>
              <Table.Cell>
                <div className='flex space-x-2'>
                  {application.status === ApplicationStatus.Pending && (
                    <CancelButton applicationId={application.id} />
                  )}
                  {application.status === ApplicationStatus.Canceled && (
                    <ReapplyButton applicationId={application.id} />
                  )}
                </div>
              </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>
    </div>
  )
}
