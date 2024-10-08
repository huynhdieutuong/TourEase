import { jwtDecode } from 'jwt-decode'
import NextAuth, { Profile } from 'next-auth'
import { OIDCConfig } from 'next-auth/providers'
import DuendeIDS6Provider from 'next-auth/providers/duende-identity-server6'
import { Role } from './types/enums'
import { NextResponse } from 'next/server'

export const { handlers, signIn, signOut, auth } = NextAuth({
  session: {
    strategy: 'jwt',
  },
  providers: [
    DuendeIDS6Provider({
      id: 'id-server',
      clientId: 'nextApp',
      clientSecret: 'secret',
      issuer: 'http://localhost:5001',
      authorization: { params: { scope: 'openid profile tourEaseApp' } },
      idToken: true,
    } as OIDCConfig<Profile>),
  ],
  callbacks: {
    authorized({ auth, request }) {
      const { pathname } = request.nextUrl

      if (!auth) return false

      if (
        pathname === '/tourjobs/list' ||
        pathname === '/tourjobs/create' ||
        pathname.startsWith('/tourjobs/update/')
      ) {
        if (!auth?.user?.roles.includes(Role.TRAVELAGENCY)) {
          return NextResponse.redirect(new URL('/api/403', request.url))
        }
      }

      if (pathname === '/destinations/list') {
        if (!auth?.user?.roles.includes(Role.ADMIN)) {
          return NextResponse.redirect(new URL('/api/403', request.url))
        }
      }

      if (pathname === '/applications') {
        if (!auth?.user?.roles.includes(Role.TOURGUIDE)) {
          return NextResponse.redirect(new URL('/api/403', request.url))
        }
      }

      return true
    },
    async jwt({ token, account }) {
      if (account && account.access_token) {
        const decoded = jwtDecode(account.access_token)
        token.username = decoded.username
        token.roles = decoded.roles
        token.accessToken = account.access_token
        token.expiresAt = decoded.exp
      }

      // Check if the token has expired, force re-authentication
      if (token.expiresAt && Date.now() > Number(token.expiresAt) * 1000) {
        token.error = 'TokenExpired'
      }

      return token
    },
    async session({ session, token }) {
      if (token) {
        session.user.username = token.username
        session.user.roles = token.roles
        session.user.error = token.error
        session.accessToken = token.accessToken
      }
      return session
    },
  },
})
