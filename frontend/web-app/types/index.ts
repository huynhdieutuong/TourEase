import { IconType } from 'react-icons'
import {
  ApplicationStatus,
  ApplicationTypes,
  DestinationType,
  TourJobStatus,
} from './enums'

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
  status: TourJobStatus
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
  isFinished: boolean
  owner: string
}

export type Destination = {
  id: string
  name: string
  slug: string
  type: DestinationType
  imageUrl?: string
  parentId?: string | null
  subDestinations: Destination[]
}

export type Application = {
  id: string
  tourJobId: string
  tourJob?: TourJob
  tourGuide: string
  comment: string
  appliedDate: string
  status: ApplicationStatus
}

export type FilterOption = {
  icon?: IconType
  label: string
  value: string
}

export type SelectOption = {
  value: string | number | null
  label: string
}

export type TotalApplicantsUpdatedMessage = {
  tourJobId: string
  totalApplicants: number
  type: ApplicationTypes
}

export type ApplicationActionMessage = {
  application: Application
  tourJob: TourJob
  type: ApplicationTypes
}

export type TourJobFinishedMessage = {
  tourJob: TourJob
  tourGuide: string
}
