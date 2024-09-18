import Logo from './Logo'
import Search from './Search'

export default function Navbar() {
  return (
    <header className='sticky top-0 z-50 bg-yellow-400 flex justify-between items-center py-5 px-20 shadow-md text-2xl'>
      <Logo />
      <Search />

      <div>Login</div>
    </header>
  )
}
