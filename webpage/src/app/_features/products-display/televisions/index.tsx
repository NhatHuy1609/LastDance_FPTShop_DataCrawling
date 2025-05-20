import { useTelevisions } from '@/hooks/apis/television/useTelevisions'
import React, { useCallback, useEffect, useState } from 'react'
import ProductsList from '../components/ProductsList'
import { Television, TelevisionsQuery } from '@/entities/television/television.types'
import LoadMoreButton from '../components/LoadMoreButton'
import TelevisionsFilter from './TelevisionsFilter'
import { TelevisionsContext } from './context'

function TelevisionsDisplay() {
  const [queryParams, setQueryParams] = useState<TelevisionsQuery>({
    limit: 10
  })

  const { fetchNextPage, hasNextPage, isFetchingNextPage, data, refetch } =
    useTelevisions(queryParams)
  const [televisions, setTelevisions] = useState<Television[]>([])

  // Update televisions state when data from API changes
  useEffect(() => {
    if (data) {
      const allTelevisions = data.pages.flatMap((page) => page.televisions)
      setTelevisions(allTelevisions)
    }
  }, [data])

  const filterTelevisionsByName = useCallback(
    (name: string) => {
      setQueryParams((prev) => ({ ...prev, name: name || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterTelevisionsByPrice = useCallback(
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

  const sortTelevisions = useCallback(
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

  const handleLoadMoreTelevisions = () => {
    fetchNextPage()
  }

  return (
    <TelevisionsContext
      value={{
        sortTelevisions,
        filterTelevisionsByPrice,
        filterTelevisionsByName,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
      }}
    >
      <div className='relative flex w-full gap-4 p-4'>
        <div className='w-1/4 rounded-md border border-black/60'>
          <TelevisionsFilter />
        </div>
        <div className='w-full'>
          <ProductsList data={televisions} />
          <div className='mt-6 flex w-full justify-center'>
            <LoadMoreButton loadMoreFn={handleLoadMoreTelevisions} disable={!hasNextPage} />
          </div>
        </div>
      </div>
    </TelevisionsContext>
  )
}

export default TelevisionsDisplay
