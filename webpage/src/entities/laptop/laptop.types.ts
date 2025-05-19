export type Laptop = {
  id: number
  name: string
  url: string
  imageUrl: string
  price: number
  priceDiscount: number
}

export type PaginatedLaptops = {
  laptops: Laptop[],
  nextCursor?: string
  hasMore: boolean
}

export type LaptopsQuery = {
  limit?: number
  cursor?: string
  name?: string
  category?: string
}