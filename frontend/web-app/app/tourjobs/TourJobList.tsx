'use client'

import { MetaData, TourJob } from '@/types'
import { Spinner } from 'flowbite-react'
import queryString from 'query-string'
import { useEffect, useState } from 'react'
import { getTourJobs } from '../actions/tourJobActions'
import AppPagination from '../components/AppPagination'
import TourJobCard from './TourJobCard'
import { useParamsStore } from '@/hooks/useParamsStore'
import { useShallow } from 'zustand/shallow'
import EmptyFilter from './EmptyFilter'
import Filters from './Filters'
import { useDestinationStore } from '@/hooks/useDestinationStore'
import { getDestinations } from '../actions/destinationActions'

export default function TourJobList() {
  const [tourJobs, setTourJobs] = useState<TourJob[]>()
  const [metaData, setMetaData] = useState<MetaData>()

  const params = useParamsStore(
    useShallow((state) => ({
      pageIndex: state.pageIndex,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      destinationIds: state.destinationIds,
      duration: state.duration,
      currency: state.currency,
      includeFinished: state.includeFinished,
    }))
  )
  const setParams = useParamsStore((state) => state.setParams)
  const setDestinations = useDestinationStore((state) => state.setDestinations)
  const destinationsLoading = useDestinationStore((state) => state.loading)

  const query = queryString.stringify(params)

  useEffect(() => {
    getTourJobs(query).then((res) => {
      setTourJobs(res.data)
      setMetaData(res.metaData)
    })
  }, [query])

  useEffect(() => {
    getDestinations().then((res) => {
      setDestinations(res.data)
    })
  }, [])

  if (!tourJobs || destinationsLoading)
    return (
      <div className='text-center mt-5'>
        <Spinner size='xl' />
      </div>
    )

  if (tourJobs.length === 0) return <EmptyFilter />

  return (
    <>
      <Filters />
      <div className='grid grid-cols-4 gap-6'>
        {tourJobs.map((tourjob) => (
          <TourJobCard tourJob={tourjob} key={tourjob.id} />
        ))}
      </div>
      {metaData && (
        <AppPagination
          currentPage={params.pageIndex}
          totalPages={metaData?.totalPages}
          pageChange={(pageIndex) => setParams({ pageIndex })}
          pageSize={params.pageSize}
          sizeChange={(pageSize) => setParams({ pageSize })}
          showPageSize
        />
      )}
    </>
  )
}
