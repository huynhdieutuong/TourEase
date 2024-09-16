'use client'

import { MetaData, TourJob } from '@/types'
import { Spinner } from 'flowbite-react'
import queryString from 'query-string'
import { useEffect, useState } from 'react'
import { getData } from '../actions/tourJobActions'
import AppPagination from '../components/AppPagination'
import TourJobCard from './TourJobCard'
import { useParamsStore } from '@/hooks/useParamsStore'
import { useShallow } from 'zustand/shallow'

export default function TourJobList() {
  const [tourJobs, setTourJobs] = useState<TourJob[]>([])
  const [metaData, setMetaData] = useState<MetaData>()

  const params = useParamsStore(
    useShallow((state) => ({
      pageIndex: state.pageIndex,
      pageSize: state.pageSize,
    }))
  )
  const setParams = useParamsStore((state) => state.setParams)

  const query = queryString.stringify(params)

  useEffect(() => {
    const fetchData = async () => {
      try {
        const res = await getData(query)
        setTourJobs(res.data)
        setMetaData(res.metaData)
      } catch (error) {}
    }
    fetchData()
  }, [query])

  if (tourJobs.length === 0)
    return (
      <div className='text-center mt-5'>
        <Spinner size='xl' />
      </div>
    )

  return (
    <>
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
