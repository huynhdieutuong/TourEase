import { Color } from '@/app/components/CustomTheme'
import DestinationTable from './DestinationTable'

export default function List() {
  return (
    <div className='max-w-7xl mx-auto p-6'>
      <h1
        className={`text-3xl font-bold mb-8 text-center text-${Color.PRIMARY}`}
      >
        Destination Listings
      </h1>
      <DestinationTable />
    </div>
  )
}
