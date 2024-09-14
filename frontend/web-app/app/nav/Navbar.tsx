import Image from 'next/image'
import logo from '../logo.png'

export default function Navbar() {
  return (
    <header className='sticky top-0 z-50 flex justify-between items-center p-5 shadow-md text-2xl'>
      <div>Search</div>
      <div>
        <Image className='cursor-pointer' src={logo} alt='logo' width={100} />
      </div>
      <div>Login</div>
    </header>
  )
}
