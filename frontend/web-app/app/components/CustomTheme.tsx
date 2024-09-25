import type { CustomFlowbiteTheme } from 'flowbite-react'
import { Flowbite } from 'flowbite-react'

export enum Color {
  PRIMARY = 'yellow-400',
  SECOND = 'yellow-100',
  TEXT = 'yellow-600',
}

const customTheme: CustomFlowbiteTheme = {
  pagination: {
    pages: {
      selector: {
        active: `bg-${Color.SECOND}`,
      },
    },
  },
  spinner: {
    color: {
      yellow: `fill-${Color.PRIMARY}`,
    },
  },
}

export default function CustomTheme({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return <Flowbite theme={{ theme: customTheme }}>{children}</Flowbite>
}
