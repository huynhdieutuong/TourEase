'use client'

import { useApplicationStore } from '@/hooks/useApplicationStore'
import { useTourJobStore } from '@/hooks/useTourJobStore'
import {
  ApplicationActionMessage,
  TotalApplicantsUpdatedMessage,
} from '@/types'
import { ApplicationTypes } from '@/types/enums'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { ReactNode, useCallback, useEffect, useRef } from 'react'

type Props = {
  children: ReactNode
}

export default function SignalRProvider({ children }: Props) {
  const addApplication = useApplicationStore((state) => state.addApplication)
  const deleteApplication = useApplicationStore(
    (state) => state.deleteApplication
  )

  const connection = useRef<HubConnection | null>(null)
  const setTotalApplicants = useTourJobStore(
    (state) => state.setTotalApplicants
  )

  const handleTotalApplicantsUpdated = useCallback(
    (message: TotalApplicantsUpdatedMessage) => {
      setTotalApplicants(message.tourJobId, message.totalApplicants)
    },
    [setTotalApplicants]
  )

  const handleApplicationAction = useCallback(
    (message: ApplicationActionMessage) => {
      switch (message.type) {
        case ApplicationTypes.New:
          addApplication(message.application)
          break
        case ApplicationTypes.ReApply:
          addApplication(message.application)
          break
        case ApplicationTypes.Cancel:
          deleteApplication(message.application.id)
          break
        default:
          break
      }
    },
    []
  )

  useEffect(() => {
    if (!connection.current) {
      connection.current = new HubConnectionBuilder()
        .withUrl('http://localhost:6001/notifications')
        .withAutomaticReconnect()
        .build()

      connection.current
        .start()
        .then(() => 'Connected to notification hub')
        .catch((err) => console.log(err))
    }

    connection.current.on(
      'TotalApplicantsUpdated',
      handleTotalApplicantsUpdated
    )
    connection.current.on('ApplicationAction', handleApplicationAction)

    return () => {
      connection.current?.off(
        'TotalApplicantsUpdated',
        handleTotalApplicantsUpdated
      )
      connection.current?.off('ApplicationAction', handleApplicationAction)
    }
  }, [handleTotalApplicantsUpdated])

  return children
}
