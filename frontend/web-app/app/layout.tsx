import { Metadata } from 'next'
import './globals.css'
import Navbar from './nav/Navbar'
import CustomTheme from './components/CustomTheme'
import ToasterProvider from './providers/ToasterProvider'

export const metadata: Metadata = {
  title: 'TourEase',
  description: 'Making Tour Management Effortless',
}

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <CustomTheme>
      <html lang='en'>
        <body>
          <ToasterProvider />
          <Navbar />
          <main className='container mx-auto px-5 py-10'>{children}</main>
        </body>
      </html>
    </CustomTheme>
  )
}
