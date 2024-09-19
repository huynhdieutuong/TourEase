import NextAuth, { type DefaultSession } from 'next-auth'
import { JWT } from 'next-auth/jwt'

declare module 'next-auth' {
  interface Session {
    user: {
      username: string
      roles: string[]
    } & DefaultSession['user']
    accessToken: string
  }

  interface User {
    username: string
    roles: string[]
  }
}

declare module 'next-auth/jwt' {
  interface JWT {
    username: string
    roles: string[]
    accessToken: string
  }
}

declare module 'jwt-decode' {
  interface JwtPayload {
    username: string
    roles: string[]
  }
}
