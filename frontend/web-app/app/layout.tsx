import { Metadata } from 'next'
import './globals.css'
import Navbar from './nav/Navbar'
import ToasterProvider from './providers/ToasterProvider'
import WrapperProvider from './providers/WrapperProvider'
import { getDestinations } from './actions/destinationActions'
import CustomTheme from './components/CustomTheme'
import SignalRProvider from './providers/SignalRProvider'
import { getCurrentUser } from './actions/authActions'

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
  const user = await getCurrentUser()

  return (
    <CustomTheme>
      <html lang='en'>
        <body>
          <WrapperProvider destinations={res.data} user={user} />
          <ToasterProvider />
          <Navbar />
          <main className='container mx-auto px-5 py-10'>
            <SignalRProvider user={user}>{children}</SignalRProvider>
          </main>
        </body>
      </html>
    </CustomTheme>
  )
}
