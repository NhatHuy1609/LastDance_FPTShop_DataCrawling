export type WashingMachine = {
  id: number
  name: string
  url: string
  imageUrl: string
  price: number
  priceDiscount: number
}

export type PaginatedWashingMachines = {
  washingMachines: WashingMachine[],
  nextCursor?: string
  hasMore: boolean
}

export type WashingMachinesQuery = {
  limit?: number
  cursor?: string
  name?: string
  category?: string
}