import { createContext, useContext } from 'react'
import {
  FetchNextPageOptions,
  InfiniteData,
  InfiniteQueryObserverResult
} from '@tanstack/react-query'
import { PaginatedMonitors } from '@/entities/monitor/monitor.types'

type MonitorsContextType = {
  filterMonitorsByName?: (name: string) => void
  filterMonitorsByCategory?: (category: string) => void
  filterMonitorsByPrice?: (minPrice?: number, maxPrice?: number) => void
  sortMonitors?: (sortBy: string, isDescending: boolean) => void
  fetchNextPage: (
    options?: FetchNextPageOptions | undefined
  ) => Promise<InfiniteQueryObserverResult<InfiniteData<PaginatedMonitors, unknown>, Error>>
  hasNextPage: boolean | undefined
  isFetchingNextPage: boolean
}

export const MonitorsContext = ({
  children,
  value
}: {
  children: React.ReactNode
  value: MonitorsContextType
}) => {
  return (
    <MonitorsContextProvider.Provider value={value}>{children}</MonitorsContextProvider.Provider>
  )
}

const MonitorsContextProvider = createContext<MonitorsContextType>({
  fetchNextPage: async () => {
    return {} as InfiniteQueryObserverResult<InfiniteData<PaginatedMonitors, unknown>, Error>
  },
  hasNextPage: false,
  isFetchingNextPage: false
})

export const useMonitorsContext = () => useContext(MonitorsContextProvider)
