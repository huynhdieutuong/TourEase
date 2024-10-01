import { Application, TourJob } from '@/types'
import { formatCurrency, formatDate } from '@/utils'
import { Button, Card } from 'flowbite-react'
import {
  FaCalendar,
  FaClock,
  FaDollarSign,
  FaLanguage,
  FaUsers,
} from 'react-icons/fa'
import CardImage from '../CardImage'
import DestinationBadges from '../../components/DestinationBadges'
import Itineray from './Itineray'
import CountdownTimer from '../CountdownTimer'
import ApplicationList from './ApplicationList'
import ApplyButton from './ApplyButton'

type Props = {
  tourJob: TourJob
  applications: Application[]
}

const TourJobDetails = ({ tourJob, applications }: Props) => {
  return (
    <Card className='hover:shadow-xl transition-shadow duration-300'>
      <div className='grid grid-cols-2 gap-6 mt-3 h-96'>
        <div className='relative'>
          <div className='w-full bg-gray-200 aspect-h-10 aspect-w-16 rounded-lg overflow-hidden'>
            <CardImage imageUrl={tourJob.imageUrl} title={tourJob.title} />
          </div>
          <h2 className='absolute bottom-0 left-0 right-0 bg-black bg-opacity-50 text-white text-3xl font-bold p-4'>
            {tourJob.title}
          </h2>
          <div className='absolute top-2 right-2'>
            <CountdownTimer expireDate={tourJob.expiredDate} />
          </div>
        </div>

        <ApplicationList
          applications={applications}
          tourGuide={tourJob.tourGuide}
        />
      </div>

      <div className='p-6'>
        <div className='grid grid-cols-1 md:grid-cols-2 gap-4 mb-6'>
          <div className='flex items-center'>
            <FaCalendar className='text-blue-500 mr-2' />
            <span>
              {tourJob.days} days ({formatDate(tourJob.startDate)} -{' '}
              {formatDate(tourJob.endDate)})
            </span>
          </div>
          <div className='flex items-center'>
            <FaDollarSign className='text-green-500 mr-2' />
            <span>
              {formatCurrency(tourJob.currency, tourJob.salaryPerDay)} per day
            </span>
          </div>
          <div className='flex items-center'>
            <FaUsers className='text-purple-500 mr-2' />
            <span>{tourJob.participants} participants</span>
          </div>
          <div className='flex items-center'>
            <FaLanguage className='text-red-500 mr-2' />
            <span>{tourJob.languageSpoken}</span>
          </div>

          <DestinationBadges destinationIds={tourJob.destinationIds} showIcon />

          <div className='flex items-center'>
            <FaClock className='text-orange-500 mr-2' />
            <span>Expires on {formatDate(tourJob.expiredDate)}</span>
          </div>
        </div>

        <Itineray itinerary={tourJob.itinerary} />

        <div className='flex justify-between items-center'>
          <ApplyButton tourJobId={tourJob.id} />
          <Button color='light'>Save for Later</Button>
        </div>
      </div>
    </Card>
  )
}

export default TourJobDetails
