import { Label, TextInput } from 'flowbite-react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  type?: string
  placeholder?: string
  required?: boolean
  fullwidth?: boolean
} & UseControllerProps<T>

export default function FormInput<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div className={`${props.fullwidth ? 'col-span-full' : ''}`}>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <TextInput
        {...field}
        type={props.type || 'text'}
        placeholder={props.placeholder || ''}
        color={
          fieldState.error ? 'failure' : !fieldState.isDirty ? '' : 'success'
        }
        helperText={fieldState.error?.message}
        required={props.required}
      />
    </div>
  )
}
