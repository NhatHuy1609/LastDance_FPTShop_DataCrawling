import { useLaptops } from '@/hooks/apis/laptop/useLaptops'
import React, { useCallback, useEffect, useState } from 'react'
import ProductsList from '../components/ProductsList'
import { LaptopsContext } from './context'
import { Laptop, LaptopsQuery } from '@/entities/laptop/laptop.types'
import LoadMoreButton from '../components/LoadMoreButton'
import LaptopsFilter from './LaptopsFilter'

function LaptopsDisplay() {
  const [queryParams, setQueryParams] = useState<LaptopsQuery>({
    limit: 10
  })

  const { fetchNextPage, hasNextPage, isFetchingNextPage, data, refetch } = useLaptops(queryParams)
  const [laptops, setLaptops] = useState<Laptop[]>([])

  // Update laptops state when data from API changes
  useEffect(() => {
    if (data) {
      const allLaptops = data.pages.flatMap((page) => page.laptops)
      setLaptops(allLaptops)
    }
  }, [data])

  const filterLaptopsByName = useCallback(
    (name: string) => {
      setQueryParams((prev) => ({ ...prev, name: name || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterLaptopsByPrice = useCallback(
    (minPrice?: number, maxPrice?: number) => {
      setQueryParams((prev) => ({
        ...prev,
        minPrice: minPrice || undefined,
        maxPrice: maxPrice || undefined,
        cursor: undefined
      }))
      refetch()
    },
    [refetch]
  )

  const sortLaptops = useCallback(
    (sortBy: string, isDescending: boolean) => {
      setQueryParams((prev) => ({
        ...prev,
        sortBy: sortBy || undefined,
        isDescending,
        cursor: undefined
      }))
      refetch()
    },
    [refetch]
  )

  const handleLoadMoreLaptops = () => {
    fetchNextPage()
  }

  return (
    <LaptopsContext
      value={{
        sortLaptops,
        filterLaptopsByPrice,
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
