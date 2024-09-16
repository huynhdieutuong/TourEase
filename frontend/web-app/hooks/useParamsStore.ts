import { create } from 'zustand'

type State = {
  pageIndex: number
  pageSize: number
  searchTerm: string
  searchValue: string
}

type Actions = {
  setParams: (params: Partial<State>) => void
  reset: () => void
  setSearchValue: (value: string) => void
}

const initialState: State = {
  pageIndex: 1,
  pageSize: 4,
  searchTerm: '',
  searchValue: '',
}

export const useParamsStore = create<State & Actions>((set) => ({
  ...initialState,
  setParams: (newParams: Partial<State>) => {
    set((state) => {
      if (newParams.pageIndex)
        return { ...state, pageIndex: newParams.pageIndex }

      return { ...state, ...newParams, pageIndex: 1 }
    })
  },
  reset: () => set(initialState),
  setSearchValue: (value: string) => {
    set({ searchValue: value })
  },
}))
