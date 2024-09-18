import React from 'react'

type Props = {
  days: number
}

export default function CardDays({ days }: Props) {
  return (
    <div
      className='w-10 h-10 rounded-full
      flex flex-col justify-center items-center p-1 
    bg-white border-2 border-yellow-600'
    >
      <span className='text-2xl leading-3 font-bold'>{days}</span>
      <span className='lowercase text-xs'>days</span>
    </div>
  )
}
