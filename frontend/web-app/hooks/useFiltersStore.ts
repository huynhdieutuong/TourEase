import { create } from 'zustand'

type State = {
  selectedCountry: string
  selectedCity: string
  selectedDuration: string
  selectedCurrency: string
  includeFinished: boolean
}

type Actions = {
  setFilters: (filters: Partial<State>) => void
  resetFilters: () => void
}

const initialState: State = {
  selectedCountry: '',
  selectedCity: '',
  selectedDuration: '',
  selectedCurrency: '',
  includeFinished: false,
}

export const useFiltersStore = create<State & Actions>((set) => ({
  ...initialState,
  setFilters: (newFilters: Partial<State>) => {
    set((state) => {
      return { ...state, ...newFilters }
    })
  },
  resetFilters: () => set(initialState),
}))
