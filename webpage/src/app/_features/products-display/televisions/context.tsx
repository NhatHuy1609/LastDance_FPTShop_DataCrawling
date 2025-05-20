import { createContext, use } from 'react'
import {
  FetchNextPageOptions,
  InfiniteData,
  InfiniteQueryObserverResult
} from '@tanstack/react-query'
import { PaginatedTelevisions } from '@/entities/television/television.types'

type TelevisionsContextType = {
  filterTelevisionsByName?: (name: string) => void
  filterTelevisionsByCategory?: (category: string) => void
  filterTelevisionsByPrice?: (minPrice?: number, maxPrice?: number) => void
  sortTelevisions?: (sortBy: string, isDescending: boolean) => void
  fetchNextPage: (
    options?: FetchNextPageOptions | undefined
  ) => Promise<InfiniteQueryObserverResult<InfiniteData<PaginatedTelevisions, unknown>, Error>>
  hasNextPage: boolean | undefined
  isFetchingNextPage: boolean
}

export const TelevisionsContext = ({
  children,
  value
}: {
  children: React.ReactNode
  value: TelevisionsContextType
}) => {
  return (
    <TelevisionsContextProvider.Provider value={value}>
      {children}
    </TelevisionsContextProvider.Provider>
  )
}

const TelevisionsContextProvider = createContext<TelevisionsContextType>({
  fetchNextPage: async () => {
    return {} as InfiniteQueryObserverResult<InfiniteData<PaginatedTelevisions, unknown>, Error>
  },
  hasNextPage: false,
  isFetchingNextPage: false
})

export const useTelevisionsContext = () => use(TelevisionsContextProvider)
