'use client'

import { useApplicationStore } from '@/hooks/useApplicationStore'
import { useTourJobStore } from '@/hooks/useTourJobStore'
import {
  ApplicationActionMessage,
  TotalApplicantsUpdatedMessage,
  TourJob,
  TourJobFinishedMessage,
} from '@/types'
import { ApplicationTypes, Role, TourJobStatus } from '@/types/enums'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { ReactNode, useCallback, useEffect, useRef } from 'react'
import toast from 'react-hot-toast'
import NotificationCard from '../components/NotificationCard'
import { User } from 'next-auth'
import { useParams } from 'next/navigation'

type Props = {
  children: ReactNode
  user: User | null
}

export default function SignalRProvider({ children, user }: Props) {
  const params = useParams<{ slug: string }>()
  const setCompletedJob = useApplicationStore((state) => state.setCompletedJob)
  const setTourJobStatus = useTourJobStore((state) => state.setTourJobStatus)
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

  const handleTourJobCreated = useCallback(
    (tourJob: TourJob) => {
      if (user?.roles.includes(Role.TOURGUIDE))
        toast(
          <NotificationCard
            linkUrl={`/tourjobs/${tourJob.slug}`}
            imageUrl={tourJob.imageUrl}
            message={`New TourJob! ${tourJob.title} has been added`}
          />
        )
    },
    [user?.roles]
  )

  const handleTourJobFinished = useCallback(
    (message: TourJobFinishedMessage) => {
      if (params.slug === message.tourJob.slug)
        setCompletedJob(message.tourGuide, message.tourJob.slug)
      setTourJobStatus(message.tourJob.id, TourJobStatus.Finished)
      if (user?.username === message.tourGuide) {
        toast(
          <NotificationCard
            linkUrl={`/tourjobs/${message.tourJob.slug}`}
            imageUrl={message.tourJob.imageUrl}
            message={`Congratulations on being accepted to ${message.tourJob.title} job.`}
          />
        )
      }
    },
    [params.slug, setCompletedJob, setTourJobStatus, user?.username]
  )

  const handleApplicationAction = useCallback(
    (message: ApplicationActionMessage) => {
      switch (message.type) {
        case ApplicationTypes.New:
          if (params.slug === message.tourJob.slug)
            addApplication(message.application)
          renderActionNotification(message, 'applied')
          break
        case ApplicationTypes.ReApply:
          if (params.slug === message.tourJob.slug)
            addApplication(message.application)
          renderActionNotification(message, 'reapplied')
          break
        case ApplicationTypes.Cancel:
          if (params.slug === message.tourJob.slug)
            deleteApplication(message.application.id)
          renderActionNotification(message, 'canceled')
          break
        default:
          break
      }
    },
    [addApplication, deleteApplication, params.slug, renderActionNotification]
  )

  // eslint-disable-next-line react-hooks/exhaustive-deps
  function renderActionNotification(
    message: ApplicationActionMessage,
    type: string
  ) {
    if (user?.username === message.tourJob.owner) {
      toast(
        <NotificationCard
          linkUrl={`/tourjobs/${message.tourJob.slug}`}
          imageUrl={message.tourJob.imageUrl}
          message={`${message.application.tourGuide} ${type} for ${message.tourJob.title} job`}
        />
      )
    }
  }

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
    connection.current.on('TourJobCreated', handleTourJobCreated)
    connection.current.on('ApplicationAction', handleApplicationAction)
    connection.current.on('TourJobFinished', handleTourJobFinished)

    return () => {
      connection.current?.off(
        'TotalApplicantsUpdated',
        handleTotalApplicantsUpdated
      )
      connection.current?.off('TourJobCreated', handleTourJobCreated)
      connection.current?.off('ApplicationAction', handleApplicationAction)
      connection.current?.off('TourJobFinished', handleTourJobFinished)
    }
  }, [
    handleApplicationAction,
    handleTotalApplicantsUpdated,
    handleTourJobCreated,
    handleTourJobFinished,
  ])

  return children
}
