import { Destination } from '@/types'
import { create } from 'zustand'

type State = {
  loading: boolean
  destinations: Destination[]
  destinationsTree: Destination[]
}

type Actions = {
  setDestinations: (destinations: Destination[]) => void
}

const initialState: State = {
  loading: true,
  destinations: [],
  destinationsTree: [],
}

function convertListToTree(destinations: Destination[]) {
  const cloneDestinations = structuredClone(destinations)
  return recursive(cloneDestinations, null)
}

function recursive(destinations: Destination[], parentId: string | null) {
  const parents = destinations.filter((des) => des.parentId === parentId)
  parents.forEach((parent) => {
    const children = recursive(destinations, parent.id)
    parent.subDestinations = children
  })
  return parents
}

export const useDestinationStore = create<State & Actions>((set) => ({
  ...initialState,
  setDestinations: (destinations) =>
    set({
      destinations,
      destinationsTree: convertListToTree(destinations),
      loading: false,
    }),
}))
