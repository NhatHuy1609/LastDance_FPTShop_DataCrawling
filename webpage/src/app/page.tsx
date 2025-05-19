'use client'

import { ROUTE_PATHS } from '@/utils/route-path'
import { useRouter } from 'next/navigation'
import { useEffect } from 'react'

export default function Home() {
  const router = useRouter()

  useEffect(() => {
    router.push(ROUTE_PATHS.laptop)
  })

  return (
    <div className='w-full'>
      <>Hello</>
    </div>
  )
}
