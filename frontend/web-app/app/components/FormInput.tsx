import { Label, TextInput } from 'flowbite-react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'
import { IconType } from 'react-icons'

type Props<T extends FieldValues> = {
  label?: string
  type?: string
  placeholder?: string
  icon?: IconType
  rightIcon?: IconType
} & UseControllerProps<T>

export default function FormInput<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <TextInput
        {...props}
        {...field}
        type={props.type || 'text'}
        color={
          fieldState.error ? 'failure' : !fieldState.isDirty ? '' : 'success'
        }
        helperText={fieldState.error?.message}
      />
    </div>
  )
}
