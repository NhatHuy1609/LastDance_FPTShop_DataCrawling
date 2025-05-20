import { createContext, use } from 'react'
import {
  FetchNextPageOptions,
  InfiniteData,
  InfiniteQueryObserverResult
} from '@tanstack/react-query'
import { PaginatedLaptops } from '@/entities/laptop/laptop.types'

type LaptopsContextType = {
  filterLaptopsByName?: (name: string) => void
  filterLaptopsByCategory?: (category: string) => void
  filterLaptopsByPrice?: (minPrice?: number, maxPrice?: number) => void
  sortLaptops?: (sortBy: string, isDescending: boolean) => void
  fetchNextPage: (
    options?: FetchNextPageOptions | undefined
  ) => Promise<InfiniteQueryObserverResult<InfiniteData<PaginatedLaptops, unknown>, Error>>
  hasNextPage: boolean | undefined
  isFetchingNextPage: boolean
}

export const LaptopsContext = ({
  children,
  value
}: {
  children: React.ReactNode
  value: LaptopsContextType
}) => {
  return <LaptopsContextProvider.Provider value={value}>{children}</LaptopsContextProvider.Provider>
}

const LaptopsContextProvider = createContext<LaptopsContextType>({
  fetchNextPage: async () => {
    return {} as InfiniteQueryObserverResult<InfiniteData<PaginatedLaptops, unknown>, Error>
  },
  hasNextPage: false,
  isFetchingNextPage: false
})

export const useLaptopsContext = () => use(LaptopsContextProvider)
