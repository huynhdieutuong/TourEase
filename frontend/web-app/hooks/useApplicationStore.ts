import { Application } from '@/types'
import { create } from 'zustand'

type State = {
  applications: Application[]
  tourGuide?: string
  completedSlug?: string
}

type Actions = {
  setApplications: (applications: Application[]) => void
  addApplication: (application: Application) => void
  deleteApplication: (applicationId: string) => void
  setCompletedJob: (tourGuide?: string, completedSlug?: string) => void
}

const initialState: State = {
  applications: [],
}

export const useApplicationStore = create<State & Actions>((set) => ({
  ...initialState,
  setApplications: (applications: Application[]) => {
    set({ applications })
  },
  addApplication: (application: Application) => {
    set((state) => ({
      applications: [application, ...state.applications],
    }))
  },
  deleteApplication: (applicationId: string) => {
    set((state) => ({
      applications: state.applications.filter(
        (application) => application.id !== applicationId
      ),
    }))
  },
  setCompletedJob: (tourGuide?: string, completedSlug?: string) => {
    set({ tourGuide, completedSlug })
  },
}))
