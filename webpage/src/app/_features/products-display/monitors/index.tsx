import { useMonitors } from '@/hooks/apis/monitor/useMonitors'
import React, { useEffect, useState, useCallback } from 'react'
import ProductsList from '../components/ProductsList'
import { MonitorsContext } from './context'
import { Monitor, MonitorsQuery } from '@/entities/monitor/monitor.types'
import LoadMoreButton from '../components/LoadMoreButton'
import MonitorsFilter from './MonitorsFilter'

function MonitorsDisplay() {
  const [queryParams, setQueryParams] = useState<MonitorsQuery>({
    limit: 10
  })

  const { fetchNextPage, hasNextPage, isFetchingNextPage, data, refetch } = useMonitors(queryParams)
  const [monitors, setMonitors] = useState<Monitor[]>([])

  // Update monitors state when data from API changes
  useEffect(() => {
    if (data) {
      const allMonitors = data.pages.flatMap((page) => page.items)
      setMonitors(allMonitors)
    }
  }, [data])

  const filterMonitorsByName = useCallback(
    (name: string) => {
      setQueryParams((prev) => ({ ...prev, name: name || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterMonitorsByCategory = useCallback(
    (category: string) => {
      setQueryParams((prev) => ({ ...prev, category: category || undefined, cursor: undefined }))
      refetch()
    },
    [refetch]
  )

  const filterMonitorsByPrice = useCallback(
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

  const sortMonitors = useCallback(
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

  const handleLoadMoreMonitors = () => {
    fetchNextPage()
  }

  return (
    <MonitorsContext
      value={{
        filterMonitorsByName,
        filterMonitorsByCategory,
        filterMonitorsByPrice,
        sortMonitors,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
      }}
    >
      <div className='relative flex flex-col md:flex-row w-full gap-6 p-4 md:p-6'>
        <div className='w-full md:w-1/4 lg:w-1/5 rounded-lg border border-zinc-200 shadow-sm bg-white'>
          <MonitorsFilter />
        </div>
        <div className='w-full md:w-3/4 lg:w-4/5'>
          <ProductsList data={monitors} />
          <div className='mt-8 flex w-full justify-center'>
            <LoadMoreButton loadMoreFn={handleLoadMoreMonitors} disable={!hasNextPage} />
          </div>
        </div>
      </div>
    </MonitorsContext>
  )
}

export default MonitorsDisplay
