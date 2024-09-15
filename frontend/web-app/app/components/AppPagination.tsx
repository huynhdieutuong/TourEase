'use client'

import { Dropdown, Pagination } from 'flowbite-react'

type Props = {
  currentPage: number
  totalPages: number
  pageChange: (page: number) => void

  pageSize: number
  sizeChange: (size: number) => void
  showPageSize?: boolean
}

const pageSizes = [4, 8, 12]

export default function AppPagination({
  currentPage,
  totalPages,
  pageChange,
  pageSize,
  sizeChange,
  showPageSize,
}: Props) {
  return (
    <div className='flex justify-center items-center mt-5'>
      <Pagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={(page) => pageChange(page)}
        showIcons
      />
      {showPageSize && (
        <div className='flex items-center ml-10'>
          <span className='uppercase text-md text-gray-500 mr-2'>
            Page size
          </span>
          <Dropdown label={`${pageSize}`} inline>
            {pageSizes.map((value, i) => (
              <Dropdown.Item key={i} onClick={() => sizeChange(value)}>
                {value}
              </Dropdown.Item>
            ))}
          </Dropdown>
        </div>
      )}
    </div>
  )
}
