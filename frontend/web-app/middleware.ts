import { NextRequest, NextResponse } from 'next/server'
import { getCurrentUser } from './app/actions/authActions'
import { Role } from './types/enums'
import { getToken } from 'next-auth/jwt'

export { auth as middleware } from '@/auth'

// export default middleware((req: NextRequest) => {
//   const url = req.nextUrl.pathname
//   const token = getToken()

//   if (url.startsWith('/destinations') && user?.roles.includes(Role.ADMIN)) {
//     return NextResponse.redirect(new URL('/403', req.url))
//   }

//   if (url.startsWith('/tourjobs') && user?.roles.includes(Role.TRAVELAGENCY)) {
//     return NextResponse.redirect(new URL('/403', req.url))
//   }

//   return NextResponse.next()
// })

export const config = {
  // protect routes
  matcher: [
    '/session',

    '/tourjobs/list',
    '/tourjobs/create',
    '/tourjobs/details/:path*',
    '/tourjobs/update/:path*',

    '/destinations/list',
    '/destinations/create',
    '/destinations/details/:path*',
    '/destinations/update/:path*',
  ],

  // override pages
  pages: { signIn: '/api/auth/signin' },
}
