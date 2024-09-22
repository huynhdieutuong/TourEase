import { format, parseISO } from 'date-fns'

export function formatCurrency(currency: string, salary: number) {
  const currencies: { [key: string]: { locale: string; name: string } } = {
    $: { locale: 'en-US', name: 'USD' },
    đ: { locale: 'vi-VN', name: 'VND' },
  }

  const value = new Intl.NumberFormat(currencies[currency].locale, {
    style: 'currency',
    currency: currencies[currency].name,
  }).format(salary)

  return value
}

export function formatDate(
  dateString: string,
  dateFormat: string = 'MMMM d, yyyy'
) {
  return format(parseISO(dateString), dateFormat)
}
