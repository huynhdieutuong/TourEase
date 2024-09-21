import React from 'react'
import { Color } from '../components/CustomTheme'

type Currency = {
  locale: string
  name: string
}

const currencies: { [key: string]: Currency } = {
  $: { locale: 'en-US', name: 'USD' },
  đ: { locale: 'vi-VN', name: 'VND' },
}

type Props = {
  salary: number
  currency: string
}

export default function CardSalary({ salary, currency }: Props) {
  const value = new Intl.NumberFormat(currencies[currency].locale, {
    style: 'currency',
    currency: currencies[currency].name,
  }).format(salary)

  return (
    <div className={`text-${Color.TEXT} bg-white rounded-xl px-2 py-1`}>
      {value}
    </div>
  )
}
