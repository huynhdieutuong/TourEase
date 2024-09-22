'use client'

import DestinationBadges from '@/app/components/DestinationBadges'
import { TourJob } from '@/types'
import { formatCurrency, formatDate } from '@/utils'
import { Badge, Button, Table } from 'flowbite-react'
import Link from 'next/link'
import { FaInfoCircle, FaPencilAlt } from 'react-icons/fa'
import { DeleteButton } from './DeleteButton'
import { useState } from 'react'
import { useRouter } from 'next/navigation'

type Props = {
  tourJobs: TourJob[]
}

export function MyTourJobList({ tourJobs }: Props) {
  const router = useRouter()
  const [jobList, setjobList] = useState<TourJob[]>(tourJobs)

  function updateJobList(id: string) {
    setjobList(jobList.filter((job) => job.id !== id))
  }

  function getStatusColor(status: string) {
    switch (status) {
      case 'Live':
        return 'success'
      case 'Expired':
        return 'danger'
      case 'Finished':
        return 'info'
      default:
        return 'gray'
    }
  }

  if (!jobList.length)
    return (
      <div className='h-[40vh] border-2 rounded-md shadow-md flex justify-center items-center flex-col'>
        <h3 className='text-2xl font-bold'>You have no tour jobs?</h3>
        <span className='mt-2'>Try creating one using the button below.</span>
        <Button
          className='mt-4 text-sm font-bold'
          color='yellow'
          onClick={() => router.push('/tourjobs/create')}
        >
          Create Tour Job
        </Button>
      </div>
    )

  return (
    <div className='overflow-x-auto'>
      <Table hoverable>
        <Table.Head>
          <Table.HeadCell>Title</Table.HeadCell>
          <Table.HeadCell>Duration</Table.HeadCell>
          <Table.HeadCell>Salary</Table.HeadCell>
          <Table.HeadCell>Destinations</Table.HeadCell>
          <Table.HeadCell>Start Date</Table.HeadCell>
          <Table.HeadCell>End Date</Table.HeadCell>
          <Table.HeadCell>Expiry Date</Table.HeadCell>
          <Table.HeadCell>Status</Table.HeadCell>
          <Table.HeadCell>Actions</Table.HeadCell>
        </Table.Head>
        <Table.Body className='divide-y text-gray-600'>
          {jobList.map((tourJob) => (
            <Table.Row key={tourJob.id} className='hover:bg-yellow-50'>
              <Table.Cell>{tourJob.title}</Table.Cell>
              <Table.Cell>{tourJob.days} days</Table.Cell>
              <Table.Cell>
                <div className='flex items-center'>
                  {formatCurrency(tourJob.currency, tourJob.salaryPerDay)}/day
                </div>
              </Table.Cell>
              <Table.Cell>
                <DestinationBadges destinationIds={tourJob.destinationIds} />
              </Table.Cell>
              <Table.Cell>
                <div className='flex items-center'>
                  {formatDate(tourJob.startDate)}
                </div>
              </Table.Cell>
              <Table.Cell>
                <div className='flex items-center'>
                  {formatDate(tourJob.endDate)}
                </div>
              </Table.Cell>
              <Table.Cell>
                <div className='flex items-center'>
                  {formatDate(tourJob.expiredDate)}
                </div>
              </Table.Cell>
              <Table.Cell>
                <Badge color={getStatusColor(tourJob.status)}>
                  {tourJob.status}
                </Badge>
              </Table.Cell>
              <Table.Cell>
                <div className='flex space-x-2'>
                  <Link href={`/tourjobs/${tourJob.slug}`}>
                    <Button size='xs' color='yellow' title='View Details'>
                      <FaInfoCircle />
                    </Button>
                  </Link>
                  <Link href={`/tourjobs/update/${tourJob.id}`}>
                    <Button size='xs' color='blue' title='Update'>
                      <FaPencilAlt />
                    </Button>
                  </Link>
                  <DeleteButton
                    title={tourJob.title}
                    id={tourJob.id}
                    updateJobList={updateJobList}
                  />
                </div>
              </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>
    </div>
  )
}
