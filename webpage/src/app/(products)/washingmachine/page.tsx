'use client'

import WashingMachinesDisplay from '@/app/_features/products-display/washingmachines'
import React from 'react'

function WashingMachinePage() {
  return (
    <div className='w-full p-4'>
      <h1 className='text-center text-2xl font-bold text-black text-sky-500'>Washing Machines</h1>
      <WashingMachinesDisplay />
    </div>
  )
}

export default WashingMachinePage
