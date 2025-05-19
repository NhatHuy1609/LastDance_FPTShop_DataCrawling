export type GamingGear = {
  id: number
  name: string
  url: string
  imageUrl: string
  price: number
  priceDiscount: number
  category: string
  isAvailable: string
}

export type PaginatedGamingGears = {
  items: GamingGear[]
  nextCursor?: string
  hasMore: boolean
}

export type GamingGearsQuery = {
  limit?: number
  cursor?: string
  name?: string
  category?: string
  minPrice?: number
  maxPrice?: number
  sortBy?: string
  isDescending?: boolean
}
