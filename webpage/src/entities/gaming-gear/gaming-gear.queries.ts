import { infiniteQueryOptions } from '@tanstack/react-query'
import { GamingGearsQuery, PaginatedGamingGears } from './gaming-gear.types'
import { gamingGearApis } from '@/services/api/gaming-gear'
import { mapToPaginatedGamingGears } from './gaming-gear.lib'

export const GAMING_GEARS_ROOT_QUERY_KEY = ['gaming-gears']

export const gamingGearsQueryOptions = (query?: GamingGearsQuery) => {
  const {
    limit = 10,
    cursor = null,
    name,
    category,
    minPrice,
    maxPrice,
    sortBy,
    isDescending
  } = query || {}

  return infiniteQueryOptions<PaginatedGamingGears>({
    queryKey: [...GAMING_GEARS_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await gamingGearApis.getGamingGears({
        signal,
        params: {
          limit,
          cursor: (pageParam as string) || undefined,
          name,
          category,
          minPrice,
          maxPrice,
          sortBy,
          isDescending
        }
      })
      return mapToPaginatedGamingGears(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}
