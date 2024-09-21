import { SelectOption } from '@/types'
import { Label, Select } from 'flowbite-react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  required?: boolean
  options: SelectOption[]
  fullwidth?: boolean
} & UseControllerProps<T>

export default function FormDropdown<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  const handleValueChange = (value: string) => {
    const firstOptionValue = props.options[0]?.value
    const isNumeric = typeof firstOptionValue === 'number'

    field.onChange(isNumeric ? Number(value) : value)
  }

  return (
    <div className={`${props.fullwidth ? 'col-span-full' : ''}`}>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <Select
        color={
          fieldState.error
            ? 'failure'
            : !fieldState.isDirty
            ? 'yellow'
            : 'success'
        }
        {...field}
        required={props.required}
        onChange={(e) => handleValueChange(e.target.value)}
      >
        <option value=''>Choose an option</option>
        {props.options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </Select>
      {fieldState.error && (
        <div className='text-red-500 text-sm'>{fieldState.error.message}</div>
      )}
    </div>
  )
}
