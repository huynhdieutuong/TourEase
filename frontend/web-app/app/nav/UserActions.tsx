'use client'

import { Role } from '@/types/enums'
import { Dropdown, DropdownDivider } from 'flowbite-react'
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import Link from 'next/link'
import { AiOutlineLogout } from 'react-icons/ai'
import { FaMapMarked, FaMapMarker } from 'react-icons/fa'
import { HiCog } from 'react-icons/hi2'
import { MdTour, MdWork, MdWorkOutline } from 'react-icons/md'

type Props = {
  user: User
}

export default function UserActions({ user }: Props) {
  return (
    <Dropdown inline label={`Welcome ${user.username}`}>
      {user.roles.includes(Role.ADMIN) && (
        <>
          <Dropdown.Item icon={FaMapMarked}>
            <Link href='/destinations/list'>Destinations</Link>
          </Dropdown.Item>
          <Dropdown.Item icon={FaMapMarker}>
            <Link href='/destinations/create'>Create Destination</Link>
          </Dropdown.Item>
        </>
      )}

      {user.roles.includes(Role.TRAVELAGENCY) && (
        <>
          <Dropdown.Item icon={MdWork}>
            <Link href='/tourjobs/list'>My TourJobs</Link>
          </Dropdown.Item>
          <Dropdown.Item icon={MdWorkOutline}>
            <Link href='/tourjobs/create'>Create TourJob</Link>
          </Dropdown.Item>
        </>
      )}

      {user.roles.includes(Role.TOURGUIDE) && (
        <Dropdown.Item icon={MdTour}>
          <Link href='/'>Applied TourJobs</Link>
        </Dropdown.Item>
      )}

      <Dropdown.Item icon={HiCog}>
        <Link href='/session'>Session (dev only!)</Link>
      </Dropdown.Item>
      <DropdownDivider />
      <Dropdown.Item
        icon={AiOutlineLogout}
        onClick={() => signOut({ callbackUrl: '/' })}
      >
        Sign out
      </Dropdown.Item>
    </Dropdown>
  )
}
