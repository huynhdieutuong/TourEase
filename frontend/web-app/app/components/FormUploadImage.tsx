import React, { useState } from 'react'
import { FaUpload } from 'react-icons/fa'
import { Color } from './CustomTheme'
import { useController, UseControllerProps } from 'react-hook-form'
import { Label } from 'flowbite-react'

type Props = {
  label?: string
  fullwidth?: boolean
  placeholder?: string
} & UseControllerProps

export default function FormUploadImage(props: Props) {
  const { fieldState, field } = useController({ ...props, defaultValue: '' })
  const [previewImage, setPreviewImage] = useState(null)

  return (
    <div className='mb-4 col-span-full'>
      {props.label && (
        <div className='mb-2 block'>
          <Label htmlFor={field.name} value={props.label} />
        </div>
      )}
      <div className='relative'>
        <input
          type='file'
          id='image'
          name='image'
          // onChange={handleImageUpload}
          className='hidden'
          accept='image/*'
        />
        <label
          htmlFor='image'
          className={`flex items-center justify-center w-full py-2 px-4 bg-white text-${Color.TEXT} rounded-lg shadow-lg tracking-wide uppercase border border-yellow cursor-pointer hover:bg-yellow-400 hover:text-white `} //${errors.image ? 'border-red-500' : 'border-blue-500'}
        >
          <FaUpload className='mr-2' />
          <span className='text-base leading-normal'>Select an image</span>
        </label>
      </div>
      {previewImage && (
        <div className='mt-4'>
          <img
            src={previewImage}
            alt='Preview'
            className='max-w-full h-auto rounded-lg'
          />
        </div>
      )}
      {/* {errors.image && <p className="text-red-500 text-xs italic">{errors.image}</p>} */}
    </div>
  )
}
