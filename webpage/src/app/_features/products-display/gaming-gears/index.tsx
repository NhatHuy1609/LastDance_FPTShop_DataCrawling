import { useGamingGears } from '@/hooks/apis/gaming-gear/useGamingGears'
import React, { useEffect, useState, useCallback } from 'react'
import ProductsList from '../components/ProductsList'
import { GamingGearsContext } from './context'
import { GamingGear, GamingGearsQuery } from '@/entities/gaming-gear/gaming-gear.types'
import LoadMoreButton from '../components/LoadMoreButton'
import GamingGearsFilter from './GamingGearsFilter'

function GamingGearsDisplay() {
  const [queryParams, setQueryParams] = useState<GamingGearsQuery>({
    limit: 10
  })

  const { fetchNextPage, hasNextPage, data, refetch } = useGamingGears(queryParams)
  const [gamingGears, setGamingGears] = useState<GamingGear[]>([])

  // Update gamingGears state when data from API changes
  useEffect(() => {
    if (data) {
      const allGamingGears = data.pages.flatMap((page) => page.items)
      setGamingGears(allGamingGears)
    }
  }, [data])

  const filterGamingGearsByName = useCallback(
    (name: string) => {
      setQueryParams((prev) => ({ ...prev, name: name || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterGamingGearsByCategory = useCallback(
    (category: string) => {
      setQueryParams((prev) => ({ ...prev, category: category || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterGamingGearsByPrice = useCallback(
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

  const sortGamingGears = useCallback(
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

  return (
    <GamingGearsContext.Provider
      value={{
        gamingGears,
        filterGamingGearsByName,
        filterGamingGearsByCategory,
        filterGamingGearsByPrice,
        sortGamingGears
      }}
    >
      <div className='flex flex-col gap-6 lg:flex-row'>
        <div className='w-full lg:w-1/4'>
          <GamingGearsFilter />
        </div>
        <div className='w-full lg:w-3/4'>
          <ProductsList data={gamingGears} />
          {hasNextPage && (
            <div className='mt-8 flex justify-center'>
              <LoadMoreButton loadMoreFn={() => fetchNextPage()} disable={!hasNextPage} />
            </div>
          )}
        </div>
      </div>
    </GamingGearsContext.Provider>
  )
}

export default GamingGearsDisplay
