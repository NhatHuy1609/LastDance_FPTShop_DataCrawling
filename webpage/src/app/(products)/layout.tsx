'use client'

import React, { useState } from 'react'
import Link from 'next/link'
import { ROUTE_PATHS } from '@/utils/route-path'
import { cn } from '@/lib/utils'

function ProductsDisplayLayout({ children }: { children: React.ReactNode }) {
  const pages = [
    {
      name: 'Laptop',
      path: ROUTE_PATHS.laptop
    },
    {
      name: 'Monitor',
      path: ROUTE_PATHS.monitor
    },
    {
      name: 'Gaming gear',
      path: ROUTE_PATHS.gamingGear
    },
    {
      name: 'Washing machine',
      path: ROUTE_PATHS.washingMachine
    },
    {
      name: 'Television',
      path: ROUTE_PATHS.television
    }
  ]

  const [activePageIndex, setActivePageIndex] = useState(0)
  const handleClick = (pageIndex: number) => {
    setActivePageIndex(pageIndex)
  }

  return (
    <div className='flex size-full flex-col items-center'>
      <ul className='flex gap-4 rounded-md bg-gray-100 p-4 shadow-sm'>
        {pages.map((page, index) => {
          const isLinkActive = activePageIndex === index
          return (
            <li key={page.name}>
              <Link
                onClick={() => handleClick(index)}
                href={page.path}
                className={cn(
                  'size-full rounded-md px-4 py-2 text-sm font-semibold transition-all hover:bg-white hover:text-sky-500',
                  {
                    'bg-white text-sky-500': isLinkActive
                  }
                )}
              >
                {page.name}
              </Link>
            </li>
          )
        })}
      </ul>
      <div className='mt-6 w-full border'>{children}</div>
    </div>
  )
}

export default ProductsDisplayLayout
