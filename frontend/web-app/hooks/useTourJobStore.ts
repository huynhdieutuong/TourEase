import { ApiPageResult, TourJob } from '@/types'
import { TourJobStatus } from '@/types/enums'
import { create } from 'zustand'

type State = {
  tourJobs: TourJob[]
  totalPages: number
}

type Actions = {
  setData: (data: ApiPageResult<TourJob[]>) => void
  setTotalApplicants: (tourJobId: string, totalApplicants: number) => void
  setTourJobStatus: (tourJobId: string, status: TourJobStatus) => void
}

const initialState: State = {
  tourJobs: [],
  totalPages: 0,
}

export const useTourJobStore = create<State & Actions>((set) => ({
  ...initialState,
  setData: (data: ApiPageResult<TourJob[]>) => {
    set({
      tourJobs: data.data,
      totalPages: data.metaData.totalPages,
    })
  },
  setTotalApplicants: (tourJobId: string, totalApplicants: number) => {
    set((state) => ({
      tourJobs: state.tourJobs.map((tourJob) =>
        tourJob.id === tourJobId ? { ...tourJob, totalApplicants } : tourJob
      ),
    }))
  },
  setTourJobStatus: (tourJobId: string, status: TourJobStatus) => {
    set((state) => ({
      tourJobs: state.tourJobs.map((tourJob) =>
        tourJob.id === tourJobId ? { ...tourJob, status } : tourJob
      ),
    }))
  },
}))
