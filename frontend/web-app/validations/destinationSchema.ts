import { DesType } from '@/types/enums'
import * as yup from 'yup'

export const destinationSchema = yup.object({
  name: yup
    .string()
    .required('Name is required.')
    .min(3, 'Name must be at least 3 characters.')
    .max(255, 'Name must be less than or equal to 255 characters.'),

  type: yup
    .mixed<DesType>()
    .test('is-valid-type', 'Invalid destination type.', (value) => {
      return value !== undefined && Object.values(DesType).includes(value)
    })
    .required('Type is required.'),

  parent: yup.object(),
})
