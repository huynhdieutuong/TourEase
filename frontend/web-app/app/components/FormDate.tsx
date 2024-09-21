import { Label } from 'flowbite-react'
import DatePicker, { DatePickerProps } from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { FieldValues, UseControllerProps, useController } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  fullwidth?: boolean
  placeholder?: string
} & UseControllerProps<T> &
  Partial<DatePickerProps>

export default function FormDate<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div className={`block ${props.fullwidth ? 'col-span-full' : ''}`}>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <DatePicker
        {...field}
        onChange={(value) => field.onChange(value)}
        selected={field.value}
        placeholderText={props.placeholder || ''}
        showTimeSelect={props.showTimeSelect}
        className={`
                        rounded-lg w-[100%] flex flex-col
                        ${
                          fieldState.error
                            ? 'bg-red-50 border-red-500 text-red-900'
                            : !fieldState.invalid && fieldState.isDirty
                            ? 'bg-green-50 border-green-500 text-green-900'
                            : ''
                        }
                    `}
      />
      {fieldState.error && (
        <div className='text-red-500 text-sm'>{fieldState.error.message}</div>
      )}
    </div>
  )
}
