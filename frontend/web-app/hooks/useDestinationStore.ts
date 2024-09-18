import { Destination } from '@/types'
import { create } from 'zustand'

type State = {
  loading: boolean
  destinations: Destination[]
}

type Actions = {
  setDestinations: (destinations: Destination[]) => void
}

const initialState: State = {
  loading: true,
  destinations: [],
}

export const useDestinationStore = create<State & Actions>((set) => ({
  ...initialState,
  setDestinations: (destinations) => set({ destinations, loading: false }),
}))
