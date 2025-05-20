export type Television = {
  id: number
  name: string
  url: string
  imageUrl: string
  price: number
  priceDiscount: number
}

export type PaginatedTelevisions = {
  televisions: Television[],
  nextCursor?: string
  hasMore: boolean
}

export type TelevisionsQuery = {
  limit?: number
  cursor?: string
  name?: string
  minPrice?: number
  maxPrice?: number
  sortBy?: string
  isDescending?: boolean
}
