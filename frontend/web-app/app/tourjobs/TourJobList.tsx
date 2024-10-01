'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import queryString from 'query-string'
import { useEffect, useState } from 'react'
import { useShallow } from 'zustand/shallow'
import { getTourJobs } from '../actions/tourJobActions'
import AppPagination from '../components/AppPagination'
import EmptyFilter from './EmptyFilter'
import TourJobCard from './TourJobCard'
import TourJobFilter from './TourJobFilter'
import TourJobOrder from './TourJobOrder'
import AppSpinner from '../components/AppSpinner'
import { useTourJobStore } from '@/hooks/useTourJobStore'

export default function TourJobList() {
  const [loading, setLoading] = useState(true)
  const setData = useTourJobStore((state) => state.setData)
  const data = useTourJobStore(
    useShallow((state) => ({
      tourJobs: state.tourJobs,
      totalPages: state.totalPages,
    }))
  )

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
  const query = queryString.stringify(params)

  useEffect(() => {
    getTourJobs(query).then((res) => {
      setData(res)
      setLoading(false)
    })
  }, [query])

  if (loading) return <AppSpinner />

  function renderTourJobs() {
    if (!data.tourJobs || data.tourJobs.length === 0) return <EmptyFilter />

    return (
      <>
        <div className='grid grid-cols-4 gap-6'>
          {data.tourJobs.map((tourjob) => (
            <TourJobCard tourJob={tourjob} key={tourjob.id} />
          ))}
        </div>
        {data.totalPages && (
          <AppPagination
            currentPage={params.pageIndex}
            totalPages={data.totalPages}
            pageChange={(pageIndex) => setParams({ pageIndex })}
            pageSize={params.pageSize}
            sizeChange={(pageSize) => setParams({ pageSize })}
            showPageSize
          />
        )}
      </>
    )
  }

  return (
    <>
      <div className='flex justify-between mb-6'>
        <TourJobFilter />
        <TourJobOrder />
      </div>
      {renderTourJobs()}
    </>
  )
}
