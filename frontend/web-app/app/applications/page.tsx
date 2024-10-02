import React from 'react'
import { Color } from '../components/CustomTheme'
import ApplicationTable from './ApplicationTable'
import { getMyApplications } from '../actions/applicationActions'

export default async function MyApplications() {
  const res = await getMyApplications()

  return (
    <div className='max-w-7xl mx-auto p-6'>
      <h1
        className={`text-3xl font-bold mb-8 text-center text-${Color.PRIMARY}`}
      >
        My Applications
      </h1>
      <ApplicationTable applicationsData={res.data} />
    </div>
  )
}
