'use client'

import MonitorsDisplay from '@/app/_features/products-display/monitors'
import React from 'react'

function MonitorPage() {
  return (
    <div className='w-full p-4'>
      <h1 className='text-center text-2xl font-bold text-sky-500'>Monitors</h1>
      <MonitorsDisplay />
    </div>
  )
}

export default MonitorPage
