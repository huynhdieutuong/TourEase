import { Metadata } from 'next'
import './globals.css'
import Navbar from './nav/Navbar'
import ToasterProvider from './providers/ToasterProvider'
import WrapperProvider from './providers/WrapperProvider'
import { getDestinations } from './actions/destinationActions'
import CustomTheme from './components/CustomTheme'

export const metadata: Metadata = {
  title: 'TourEase',
  description: 'Making Tour Management Effortless',
}

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  const res = await getDestinations()

  return (
    <CustomTheme>
      <html lang='en'>
        <body>
          <WrapperProvider destinations={res.data} />
          <ToasterProvider />
          <Navbar />
          <main className='container mx-auto px-5 py-10'>{children}</main>
        </body>
      </html>
    </CustomTheme>
  )
}
