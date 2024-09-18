import { jwtDecode } from 'jwt-decode'
import NextAuth, { Profile } from 'next-auth'
import { OIDCConfig } from 'next-auth/providers'
import DuendeIDS6Provider from 'next-auth/providers/duende-identity-server6'

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
    async jwt({ token, account }) {
      if (account && account.access_token) {
        const decoded = jwtDecode(account.access_token)
        token.username = decoded.username
        token.roles = decoded.roles
      }
      return token
    },
    async session({ session, token }) {
      session.user.username = token.username
      session.user.roles = token.roles
      return session
    },
  },
})
