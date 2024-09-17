import { FilterOption } from '@/types'
import { Dropdown } from 'flowbite-react'

type Props = {
  options: FilterOption[]
  label: string
  selectedValue: any
  onSelect: (value: any) => void
  disabled?: boolean
}

export default function FilterDropdown({
  options,
  label,
  selectedValue,
  onSelect,
  disabled,
}: Props) {
  return (
    <Dropdown
      color='warning'
      label={
        !selectedValue
          ? label
          : options.find((x) => x.value === selectedValue)?.label
      }
      disabled={disabled}
    >
      {options.map((opt) => (
        <Dropdown.Item
          key={opt.value}
          icon={opt.icon}
          onClick={() => onSelect(opt.value)}
        >
          {opt.label}
        </Dropdown.Item>
      ))}
    </Dropdown>
  )
}
