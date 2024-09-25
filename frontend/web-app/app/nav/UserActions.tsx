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
          <Link href='/destinations/list'>
            <Dropdown.Item icon={FaMapMarked}>Destinations</Dropdown.Item>
          </Link>
          <Link href='/destinations/create'>
            <Dropdown.Item icon={FaMapMarker}>Create Destination</Dropdown.Item>
          </Link>
        </>
      )}

      {user.roles.includes(Role.TRAVELAGENCY) && (
        <>
          <Link href='/tourjobs/list'>
            <Dropdown.Item icon={MdWork}>My TourJobs</Dropdown.Item>
          </Link>
          <Link href='/tourjobs/create'>
            <Dropdown.Item icon={MdWorkOutline}>Create TourJob</Dropdown.Item>
          </Link>
        </>
      )}

      {user.roles.includes(Role.TOURGUIDE) && (
        <Link href='/'>
          <Dropdown.Item icon={MdTour}>Applied TourJobs</Dropdown.Item>
        </Link>
      )}

      <Link href='/session'>
        <Dropdown.Item icon={HiCog}>Session (dev only!)</Dropdown.Item>
      </Link>
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
