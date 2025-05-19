'use client'

import React from 'react'
import { QueryClientProvider } from '@/providers/QueryClientProvider'

const RootLayoutComp = ({ children }: { children: React.ReactNode }) => {
  return (
    <QueryClientProvider>
      <div className='size-full'>
        {children}
      </div>
    </QueryClientProvider>
  )
}

export default RootLayoutComp
