'use client'

import { getMyTourJob } from '@/app/actions/tourJobActions'
import AppSpinner from '@/app/components/AppSpinner'
import { Color } from '@/app/components/CustomTheme'
import { useDestinationStore } from '@/hooks/useDestinationStore'
import { TourJob } from '@/types'
import { formatCurrency, formatDate } from '@/utils'
import { Badge, Button, Table } from 'flowbite-react'
import Link from 'next/link'
import { useEffect, useState } from 'react'
import toast from 'react-hot-toast'
import { FaInfoCircle, FaPencilAlt, FaTrashAlt } from 'react-icons/fa'

export function MyTourJobList() {
  const [tourJobs, setTourJobs] = useState<TourJob[]>([])
  const [loading, setLoading] = useState(true)
  const destinations = useDestinationStore((state) => state.destinations)

  useEffect(() => {
    getMyTourJob()
      .then((res) => setTourJobs(res.data))
      .catch((err) => toast.error(err.message))
      .finally(() => setLoading(false))
  }, [])

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

  function getDestinationsName(destinationIds: string[]) {
    return destinations
      ?.filter((destination) => destinationIds.includes(destination.id))
      .map((des) => des.name)
      .join(', ')
  }

  if (loading) return <AppSpinner />

  return (
    <div className='max-w-7xl mx-auto p-6'>
      <h1
        className={`text-3xl font-bold mb-8 text-center text-${Color.PRIMARY}`}
      >
        Tour Job Listings
      </h1>
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
            {tourJobs.map((job) => (
              <Table.Row key={job.id} className='hover:bg-yellow-50'>
                <Table.Cell>{job.title}</Table.Cell>
                <Table.Cell>{job.days} days</Table.Cell>
                <Table.Cell>
                  <div className='flex items-center'>
                    {formatCurrency(job.currency, job.salaryPerDay)}/day
                  </div>
                </Table.Cell>
                <Table.Cell>
                  <div className='flex items-center'>
                    {getDestinationsName(job.destinationIds)}
                  </div>
                </Table.Cell>
                <Table.Cell>
                  <div className='flex items-center'>
                    {formatDate(job.startDate)}
                  </div>
                </Table.Cell>
                <Table.Cell>
                  <div className='flex items-center'>
                    {formatDate(job.endDate)}
                  </div>
                </Table.Cell>
                <Table.Cell>
                  <div className='flex items-center'>
                    {formatDate(job.expiredDate)}
                  </div>
                </Table.Cell>
                <Table.Cell>
                  <Badge color={getStatusColor(job.status)}>{job.status}</Badge>
                </Table.Cell>
                <Table.Cell>
                  <div className='flex space-x-2'>
                    <Link href={`/tourjobs/${job.slug}`}>
                      <Button size='xs' color='yellow' title='View Details'>
                        <FaInfoCircle />
                      </Button>
                    </Link>
                    <Button size='xs' color='blue' title='Update'>
                      <FaPencilAlt />
                    </Button>
                    <Button size='xs' color='red' title='Delete'>
                      <FaTrashAlt />
                    </Button>
                  </div>
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table>
      </div>
    </div>
  )
}
