export { auth as middleware } from '@/auth'

export const config = {
  // protect routes
  matcher: ['/session'],

  // override pages
  pages: { signIn: '/api/auth/signin' },
}
