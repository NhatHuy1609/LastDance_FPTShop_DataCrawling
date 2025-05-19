import { useLaptops } from '@/hooks/apis/laptop/useLaptops'
import React, { useEffect, useState } from 'react'
import ProductsList from '../components/ProductsList'
import { LaptopsContext } from './context'
import { Laptop } from '@/entities/laptop/laptop.types'
import LoadMoreButton from '../components/LoadMoreButton'
import LaptopsFilter from './LaptopsFilter'

function LaptopsDisplay() {
  const { fetchNextPage, hasNextPage, isFetchingNextPage, data, ...result } = useLaptops()
  const [laptops, setLaptops] = useState<Laptop[]>(
    () => data?.pages.flatMap((page) => page.laptops) || []
  )

  // Update laptops state when data from API changes
  useEffect(() => {
    setLaptops((prev) => [...(data?.pages.flatMap((page) => page.laptops) || [])])
  }, [data])

  const filterLaptopsByName = (name: string) => {
    const filteredLaptops = laptops.filter((laptop) =>
      laptop.name.toLowerCase().includes(name.toLowerCase())
    )
    setLaptops([...filteredLaptops])
  }

  const handleLoadMoreLaptops = () => {
    fetchNextPage()
  }

  return (
    <LaptopsContext
      value={{
        filterLaptopsByName,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
      }}
    >
      <div className='relative flex w-full gap-4 p-4'>
        <div className='w-1/4 rounded-md border border-black/60'>
          <LaptopsFilter />
        </div>
        <div className='w-full'>
          <ProductsList data={laptops} />
          <div className='mt-6 flex w-full justify-center'>
            <LoadMoreButton loadMoreFn={handleLoadMoreLaptops} disable={!hasNextPage} />
          </div>
        </div>
      </div>
    </LaptopsContext>
  )
}

export default LaptopsDisplay
