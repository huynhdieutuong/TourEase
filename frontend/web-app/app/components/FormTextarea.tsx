import { Label, Textarea } from 'flowbite-react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  placeholder?: string
  rows?: number
} & UseControllerProps<T>

export default function FormTextarea<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <Textarea
        {...props}
        {...field}
        color={
          fieldState.error ? 'failure' : !fieldState.isDirty ? '' : 'success'
        }
        helperText={fieldState.error?.message}
        rows={props.rows || 4}
      />
    </div>
  )
}
