export type Monitor = {
    id: number
    name: string
    url: string
    imageUrl: string
    price: number
    priceDiscount: number
    category: string
    isAvailable: string
}

export type PaginatedMonitors = {
    items: Monitor[]
    nextCursor?: string
    hasMore: boolean
}

export type MonitorsQuery = {
    limit?: number
    cursor?: string
    name?: string
    category?: string
    minPrice?: number
    maxPrice?: number
    sortBy?: string
    isDescending?: boolean
}
