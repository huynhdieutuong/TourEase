import * as yup from 'yup'

export const applicationSchema = yup.object({
  comment: yup
    .string()
    .required('Comment is required.')
    .max(500, 'Comment must be less than or equal to 500 characters.'),
})
