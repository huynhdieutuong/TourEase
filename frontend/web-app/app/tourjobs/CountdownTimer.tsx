'use client'

import Countdown, { zeroPad } from 'react-countdown'

type Props = {
  expireDate: string
}

type Date = {
  days: number
  hours: number
  minutes: number
  seconds: number
  completed: boolean
}

const renderer = ({ days, hours, minutes, seconds, completed }: Date) => {
  const backgroundColor = completed
    ? 'bg-red-600'
    : days === 0 && hours < 10
    ? 'bg-amber-600'
    : 'bg-green-600'

  return (
    <div
      className={`border-2 border-white text-white py-1 px-2 rounded-lg flex justify-center ${backgroundColor}`}
    >
      {completed ? (
        <span>Completed</span>
      ) : (
        <span suppressHydrationWarning={true}>
          {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
        </span>
      )}
    </div>
  )
}

export default function CountdownTimer({ expireDate }: Props) {
  return <Countdown date={expireDate} renderer={renderer} />
}
