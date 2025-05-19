'use client'

import GamingGearsDisplay from '@/app/_features/products-display/gaming-gears'
import React from 'react'

function GamingGearPage() {
  return (
    <div className='min-h-screen bg-gradient-to-b from-slate-900 to-slate-800'>
      <div className='container mx-auto px-4 py-8'>
        <div className='mb-8 text-center'>
          <h1 className='mb-2 text-4xl font-bold text-white'>Gaming Gear</h1>
          <p className='text-lg text-slate-300'>
            Khám phá bộ sưu tập phụ kiện gaming chất lượng cao
          </p>
        </div>
        <div className='rounded-xl bg-white/5 p-6 backdrop-blur-sm'>
          <GamingGearsDisplay />
        </div>
      </div>
    </div>
  )
}

export default GamingGearPage
