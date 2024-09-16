import { useParamsStore } from '@/hooks/useParamsStore'

export default function EmptyFilter() {
  const reset = useParamsStore((state) => state.reset)

  return (
    <div className='h-[40vh] border-2 rounded-md shadow-md flex justify-center items-center flex-col'>
      <h3 className='text-2xl font-bold'>No matches for this filter</h3>
      <span className='mt-2'>Try changing or resetting the filter</span>
      <button
        onClick={reset}
        className='mt-4 border-2 border-yellow-600 rounded-md py-2 px-4 text-sm font-bold
                        hover:bg-yellow-600 hover:text-white transition duration-300 ease-in-out'
      >
        Remove Filters
      </button>
    </div>
  )
}
