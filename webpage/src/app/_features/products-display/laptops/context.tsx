import { createProductsDisplayContext } from '../context'

type LaptopsContextType = {
  filterLaptopsByName: (name: string) => void
  fetchNextPage: () => void
  hasNextPage: boolean
  isFetchingNextPage: boolean
}

export const { Context: LaptopsContext, useProductsContext: useLaptopsContext } =
  createProductsDisplayContext<LaptopsContextType>()
