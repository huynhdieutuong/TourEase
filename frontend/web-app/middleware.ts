export { auth as middleware } from '@/auth'

export const config = {
  // protect routes
  matcher: [
    '/session',

    '/tourjobs/list',
    '/tourjobs/create',
    '/tourjobs/update/:path*',

    '/destinations/list',
    '/destinations/create',
    '/destinations/update/:path*',
  ],

  // override pages
  pages: { signIn: '/api/auth/signin' },
}
