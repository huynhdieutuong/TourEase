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

  salary: yup
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
    .min(5, 'Title must be at least 5 characters.'),

  participants: yup
    .number()
    .required('Participants are required.')
    .min(10, 'Participants must be at least 10.'),

  expiredDate: yup
    .date()
    .required('Expired date is required.')
    .min(new Date(), 'Expired date must be in the future.'),

  startDate: yup
    .date()
    .required('Start date is required.')
    .when('expiredDate', (expiredDate, schema) =>
      expiredDate
        ? schema.min(expiredDate, 'Start date must be after the expired date.')
        : schema
    ),

  endDate: yup
    .date()
    .required('End date is required')
    .when('startDate', (startDate, schema) =>
      startDate
        ? schema.min(startDate, 'End date must be after the start date.')
        : schema
    ),
  // destinationIds: yup
  //   .array()
  //   .of(yup.string().uuid('Invalid destination ID format.'))
  //   .required('Destination IDs are required.')
  //   .test(
  //     'all-exist',
  //     'One or more destination IDs are invalid.',
  //     async (destinationIds: string[] | undefined) => {
  //       if (!destinationIds) return false
  //       return await checkAllDestinationsExist(destinationIds)
  //     }
  //   ),
})

// Mock async validation for destination IDs
async function checkAllDestinationsExist(
  destinationIds: string[]
): Promise<boolean> {
  // Simulate API call to check destination IDs existence
  return true // Assume all destinations exist for now
}
