import { Currency, LanguageSpoken } from '@/types/enums'
import * as yup from 'yup'

export const tourJobSchema = yup.object({
  title: yup
    .string()
    .required('Title is required.')
    .min(5, 'Title must be at least 5 characters.')
    .max(255, 'Title must be less than or equal to 255 characters.'),

  currency: yup
    .mixed<Currency>()
    .test('is-valid-currency', 'Invalid currency type.', (value) => {
      return value !== undefined && Object.values(Currency).includes(value)
    })
    .required('Currency is required.'),

  salaryPerDay: yup
    .number()
    .required('Salary per day is required.')
    .positive('Salary per day must be greater than zero.')
    .test(
      'is-whole-number',
      'For VND, salary must not have decimal places.',
      (value, context) => {
        const { currency } = context.parent
        if (currency === Currency.VND && value % 1 !== 0) {
          return false
        }
        return true
      }
    ),

  languageSpoken: yup
    .mixed<LanguageSpoken>()
    .test('is-valid-language', 'Invalid language type.', (value) => {
      return (
        value !== undefined && Object.values(LanguageSpoken).includes(value)
      )
    })
    .required('LanguageSpoken is required.'),

  itinerary: yup
    .string()
    .required('Itinerary is required.')
    .min(5, 'Itinerary must be at least 5 characters.'),

  participants: yup
    .number()
    .required('Participants are required.')
    .min(10, 'Participants must be at least 10.'),

  expiredDate: yup
    .date()
    .required('Expired date is required.')
    .min(
      new Date(new Date().getTime() + 60 * 60 * 1000),
      'Expired date must be in the future.'
    ),

  startDate: yup
    .date()
    .required('Start date is required.')
    .when('expiredDate', (expiredDate, schema) =>
      expiredDate
        ? schema.test(
            'is-after-expiredDate',
            'Start date must be after the expired date.',
            function (value) {
              const expDate = Array.isArray(expiredDate)
                ? expiredDate[0]
                : expiredDate
              return value && expiredDate && new Date(value) > new Date(expDate)
            }
          )
        : schema
    ),

  endDate: yup
    .date()
    .required('End date is required')
    .when('startDate', (startDate, schema) =>
      startDate
        ? schema.test(
            'is-after-startDate',
            'End date must be after the start date.',
            function (value) {
              const sDate = Array.isArray(startDate) ? startDate[0] : startDate
              return value && startDate && new Date(value) > new Date(sDate)
            }
          )
        : schema
    ),
  countries: yup.array().required('Country are required.'),
  cities: yup.array().required('City are required.'),
})
