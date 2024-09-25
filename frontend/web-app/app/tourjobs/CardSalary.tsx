import React from 'react'
import { Color } from '../components/CustomTheme'
import { formatCurrency } from '@/utils'

type Props = {
  salary: number
  currency: string
}

export default function CardSalary({ salary, currency }: Props) {
  return (
    <div className={`text-${Color.TEXT} bg-white rounded-xl px-2 py-1`}>
      {formatCurrency(currency, salary)}
    </div>
  )
}
