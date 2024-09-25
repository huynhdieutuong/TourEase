import { Label } from 'flowbite-react'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { FieldValues, UseControllerProps, useController } from 'react-hook-form'

type Props<T extends FieldValues> = {
  label?: string
  placeholder?: string
  dateFormat?: string
  showTimeSelect?: boolean
  showIcon?: boolean
} & UseControllerProps<T>

export default function FormDate<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <DatePicker
        {...props}
        {...field}
        onChange={(value) => field.onChange(value)}
        selected={field.value}
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
