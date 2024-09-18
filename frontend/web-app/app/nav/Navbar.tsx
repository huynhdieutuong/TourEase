import { getCurrentUser } from '../actions/authActions'
import LoginButton from './LoginButton'
import Logo from './Logo'
import Search from './Search'
import UserActions from './UserActions'

export default async function Navbar() {
  const user = await getCurrentUser()

  return (
    <header className='sticky top-0 z-50 bg-yellow-400 flex justify-between items-center py-5 px-20 shadow-md'>
      <Logo />
      <Search />
      {user ? <UserActions user={user} /> : <LoginButton />}
    </header>
  )
}
