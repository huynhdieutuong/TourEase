import { Metadata } from 'next'
import './globals.css'
import Navbar from './nav/Navbar'

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
    <html lang='en'>
      <body>
        <Navbar />
        <main className='container mx-auto px-5 py-10'>{children}</main>
      </body>
    </html>
  )
}
