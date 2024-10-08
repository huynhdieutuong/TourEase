import { create } from 'zustand'

type State = {
  pageIndex: number
  pageSize: number
  searchTerm: string
  searchValue: string
  orderBy: string
  destinationIds: string
  duration: string
  currency: string
  includeFinished: boolean
}

type Actions = {
  setParams: (params: Partial<State>) => void
  resetParams: () => void
  setSearchValue: (value: string) => void
  resetSearch: () => void
}

const initialState: State = {
  pageIndex: 1,
  pageSize: 12,
  searchTerm: '',
  searchValue: '',
  orderBy: '',
  destinationIds: '',
  duration: '',
  currency: '',
  includeFinished: false,
}

export const useParamsStore = create<State & Actions>((set) => ({
  ...initialState,
  setParams: (newParams: Partial<State>) => {
    set((state) => {
      if (newParams.searchTerm)
        return {
          ...initialState,
          searchValue: state.searchValue,
          searchTerm: newParams.searchTerm,
        }
      if (newParams.pageIndex)
        return { ...state, pageIndex: newParams.pageIndex }

      return { ...state, ...newParams, pageIndex: 1 }
    })
  },
  resetParams: () => set(initialState),
  setSearchValue: (value: string) => set({ searchValue: value }),
  resetSearch: () =>
    set((state) => {
      return { ...state, searchTerm: '', searchValue: '' }
    }),
}))
