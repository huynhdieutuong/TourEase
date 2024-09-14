export type ApiResult<T> = {
  data: T[]
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
  totalItems: number
  hasPrevious: boolean
  hasNext: boolean
  firstRowOnPage: number
  lastRowOnPage: number
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
