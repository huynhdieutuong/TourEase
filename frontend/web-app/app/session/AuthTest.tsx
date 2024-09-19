'use client'

import React, { useState } from 'react'
import { authTourJobTest } from '../actions/tourJobActions'
import { Button } from 'flowbite-react'

export default function AuthTest() {
  const [loading, setLoading] = useState(false)
  const [result, setResult] = useState()

  function doAuthGet() {
    setResult(undefined)
    setLoading(true)
    authTourJobTest()
      .then((res) => setResult(res))
      .catch((err) => setResult(err))
      .finally(() => setLoading(false))
  }

  return (
    <div className='flex items-center gap-4'>
      <Button outline isProcessing={loading} onClick={doAuthGet}>
        Test auth
      </Button>
      <div>{JSON.stringify(result, null, 2)}</div>
    </div>
  )
}
