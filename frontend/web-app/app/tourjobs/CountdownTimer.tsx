'use client'

import { useApplicationStore } from '@/hooks/useApplicationStore'
import { useParams } from 'next/navigation'
import { useEffect, useState } from 'react'
import Countdown, { zeroPad } from 'react-countdown'

type Props = {
  expireDate: string
  forceCompleted?: boolean
}

type Date = {
  days: number
  hours: number
  minutes: number
  seconds: number
  completed: boolean
}

export default function CountdownTimer({ expireDate, forceCompleted }: Props) {
  const [isCompleted, setIsCompleted] = useState<boolean | undefined>(false)
  const params = useParams<{ slug: string }>()
  const completedSlug = useApplicationStore((state) => state.completedSlug)

  useEffect(() => {
    setIsCompleted(forceCompleted)
  }, [forceCompleted])

  useEffect(() => {
    if (params.slug && params.slug === completedSlug) setIsCompleted(true)
  }, [completedSlug, params.slug])

  function renderer({ days, hours, minutes, seconds, completed }: Date) {
    const finalCompleted = completed || isCompleted
    const backgroundColor = finalCompleted
      ? 'bg-red-600'
      : days === 0 && hours < 10
      ? 'bg-amber-600'
      : 'bg-green-600'

    return (
      <div
        className={`border-2 border-white text-white py-1 px-2 rounded-lg flex justify-center ${backgroundColor}`}
      >
        {finalCompleted ? (
          <span>Completed</span>
        ) : (
          <span suppressHydrationWarning={true}>
            {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:
            {zeroPad(seconds)}
          </span>
        )}
      </div>
    )
  }

  return <Countdown date={expireDate} renderer={renderer} />
}
