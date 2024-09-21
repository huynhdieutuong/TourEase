import TourJobForm from '../TourJobForm'

export default function Create() {
  return (
    <>
      <div className='max-w-[75%] mx-auto p-10 shadow-md rounded-md'>
        <h2 className='text-3xl font-bold mb-6 text-center text-yellow-600'>
          Create Tour Job
        </h2>
        <TourJobForm />
      </div>
    </>
  )
}
