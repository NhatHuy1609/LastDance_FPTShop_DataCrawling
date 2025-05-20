'use client'

import TelevisionsDisplay from '@/app/_features/products-display/televisions'
import React from 'react'

function TelevisionPage() {
  return (
    <div className='w-full p-4'>
      <h1 className='text-center text-2xl font-bold text-black text-sky-500'>Televisions</h1>
      <TelevisionsDisplay />
    </div>
  )
}

export default TelevisionPage
