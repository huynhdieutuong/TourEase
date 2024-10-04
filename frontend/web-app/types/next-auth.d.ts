import { type DefaultSession } from 'next-auth'
import 'next-auth/jwt'

declare module 'next-auth' {
  interface Session {
    user: {
      username: string
      roles: string[]
    } & DefaultSession['user']
    accessToken: string
    error: string
  }

  interface User {
    username: string
    roles: string[]
    error: string
  }
}

declare module 'next-auth/jwt' {
  interface JWT {
    username: string
    roles: string[]
    accessToken: string
    error: string
  }
}

declare module 'jwt-decode' {
  interface JwtPayload {
    username: string
    roles: string[]
  }
}
