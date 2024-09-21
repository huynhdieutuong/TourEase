import { Label, Textarea } from 'flowbite-react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  placeholder?: string
  rows?: number
  required?: boolean
  fullwidth?: boolean
} & UseControllerProps<T>

export default function FormTextarea<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div className={`${props.fullwidth ? 'col-span-full' : ''}`}>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <Textarea
        {...field}
        placeholder={props.placeholder || ''}
        color={
          fieldState.error ? 'failure' : !fieldState.isDirty ? '' : 'success'
        }
        helperText={fieldState.error?.message}
        rows={props.rows || 4}
        required={props.required}
      />
    </div>
  )
}
