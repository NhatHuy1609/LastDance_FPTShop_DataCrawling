import { createProductsDisplayContext } from '../context'

type WashingMachinesContextType = {
  filterWashingMachinesByName: (name: string) => void
  fetchNextPage: () => void
  hasNextPage: boolean
  isFetchingNextPage: boolean
}

export const { Context: WashingMachinesContext, useProductsContext: useWashingMachinesContext } =
  createProductsDisplayContext<WashingMachinesContextType>()
