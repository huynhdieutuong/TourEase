import { IconType } from 'react-icons'
import { DestinationType } from './enums'

export type ApiResult<T> = {
  data: T
  message: string
  isSucceeded: boolean
}

export type ApiPageResult<T> = ApiResult<T> & {
  metaData: MetaData
}

export type MetaData = {
  currentPage: number
  totalPages: number
  pageSize: number
}

export type TourJob = {
  id: string
  title: string
  slug: string
  days: number
  salaryPerDay: number
  salary: number
  currency: string
  tourGuide?: string
  totalApplicants?: number
  expiredDate: string
  status: string
  itinerary: string
  imageUrl?: string
  participants: number
  languageSpoken: string
  startDate: string
  endDate: string
  destinationIds: string[]
  createdDate: string
  updatedDate: string
  createdBy: string
  updatedBy?: string
  isDeleted: boolean
  deletedDate?: string
}

export type Destination = {
  id: string
  name: string
  slug: string
  type: DestinationType
  imageUrl?: string
  parentId?: string
}

export type FilterOption = {
  icon?: IconType
  label: string
  value: string
}

export type SelectOption = {
  value: string | number
  label: string
}
