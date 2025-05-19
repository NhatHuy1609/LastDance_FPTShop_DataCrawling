'use client'

import LaptopsDisplay from '@/app/_features/products-display/laptops'
import React from 'react'

function LaptopPage() {
  return (
    <div className='w-full p-4'>
      <h1 className='text-center text-2xl font-bold text-black text-sky-500'>Laptops</h1>
      <LaptopsDisplay />
    </div>
  )
}

export default LaptopPage
