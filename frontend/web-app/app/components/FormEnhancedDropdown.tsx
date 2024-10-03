import { SelectOption } from '@/types'
import clsx from 'clsx'
import { Label } from 'flowbite-react'
import React from 'react'
import { FieldValues, useController, UseControllerProps } from 'react-hook-form'
import Select from 'react-select'

const controlStyles = {
  base: 'border rounded-lg bg-white hover:cursor-pointer',
  focus: 'border-yellow-400 ring-1 ring-yellow-300',
  nonFocus: 'border-gray-500 hover:border-gray-600',
  disabled: 'opacity-50',
}
const placeholderStyles = 'text-gray-500 pl-1 py-0.5'
const selectInputStyles = 'pl-1 py-0.5'
const valueContainerStyles = 'p-1 gap-1'
const singleValueStyles = 'leading-7 ml-1'
const multiValueStyles =
  'bg-gray-100 rounded items-center py-0.5 pl-2 pr-1 gap-1.5'
const multiValueLabelStyles = 'leading-6 py-0.5'
const multiValueRemoveStyles =
  'border border-gray-200 bg-white hover:bg-red-50 hover:text-red-800 text-gray-500 hover:border-red-300 rounded-md'
const indicatorsContainerStyles = 'p-1 gap-1'
const clearIndicatorStyles =
  'text-gray-500 p-1 rounded-md hover:bg-red-50 hover:text-red-800'
const indicatorSeparatorStyles = 'bg-gray-300'
const dropdownIndicatorStyles =
  'p-1 hover:bg-gray-100 text-gray-500 rounded-md hover:text-black'
const menuStyles = 'p-1 mt-2 border border-gray-200 bg-white rounded-lg'
const groupHeadingStyles = 'ml-3 mt-2 mb-1 text-gray-500 text-sm'
const optionStyles = {
  base: 'hover:cursor-pointer px-3 py-2 rounded',
  focus: 'bg-gray-100 active:bg-gray-200',
  selected: "after:content-['✔'] after:ml-2 after:text-green-500 text-gray-500",
}
const noOptionsMessageStyles =
  'text-gray-500 p-2 bg-gray-50 border border-dashed border-gray-200 rounded-sm'

type Props<T extends FieldValues> = {
  options: SelectOption[]
  label?: string
  placeholder?: string
  isClearable?: boolean
  isSearchable?: boolean
  isMulti?: boolean
  closeMenuOnSelect?: boolean
  isDisabled?: boolean
} & UseControllerProps<T>

export default function FormEnhancedDropdown<T extends FieldValues>(
  props: Props<T>
) {
  const { fieldState, field } = useController({ ...props })

  return (
    <div>
      {props.label && (
        <div className='mb-2 block'>
          <Label
            htmlFor={field.name}
            value={`${props.label} ${props.isDisabled ? '(Disabled)' : ''}`}
          />
        </div>
      )}
      <Select<SelectOption>
        {...props}
        {...field}
        unstyled
        //@ts-expect-error
        isMulti={props.isMulti}
        styles={{
          input: (base) => ({
            ...base,
            'input:focus': {
              boxShadow: 'none',
            },
          }),
          // On mobile, the label will truncate automatically, so we want to
          // override that behaviour.
          multiValueLabel: (base) => ({
            ...base,
            whiteSpace: 'normal',
            overflow: 'visible',
          }),
          control: (base) => ({
            ...base,
            transition: 'none',
          }),
        }}
        classNames={{
          control: ({ isFocused, isDisabled }) =>
            clsx(
              isFocused ? controlStyles.focus : controlStyles.nonFocus,
              isDisabled && controlStyles.disabled,
              controlStyles.base
            ),
          placeholder: () => placeholderStyles,
          input: () => selectInputStyles,
          valueContainer: () => valueContainerStyles,
          singleValue: () => singleValueStyles,
          multiValue: () => multiValueStyles,
          multiValueLabel: () => multiValueLabelStyles,
          multiValueRemove: () => multiValueRemoveStyles,
          indicatorsContainer: () => indicatorsContainerStyles,
          clearIndicator: () => clearIndicatorStyles,
          indicatorSeparator: () => indicatorSeparatorStyles,
          dropdownIndicator: () => dropdownIndicatorStyles,
          menu: () => menuStyles,
          groupHeading: () => groupHeadingStyles,
          option: ({ isFocused, isSelected }) =>
            clsx(
              isFocused && optionStyles.focus,
              isSelected && optionStyles.selected,
              optionStyles.base
            ),
          noOptionsMessage: () => noOptionsMessageStyles,
        }}
      />
      {fieldState.error && (
        <div className='text-red-500 text-sm'>{fieldState.error.message}</div>
      )}
    </div>
  )
}
