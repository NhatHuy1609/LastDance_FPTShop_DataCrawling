import { infiniteQueryOptions } from '@tanstack/react-query'
import { MonitorsQuery, PaginatedMonitors } from './monitor.types'
import { monitorApis } from '@/services/api/monitor'
import { mapToPaginatedMonitors } from './monitor.lib'

export const MONITORS_ROOT_QUERY_KEY = ['monitors']

export const monitorsQueryOptions = (query?: MonitorsQuery) => {
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

  return infiniteQueryOptions<PaginatedMonitors>({
    queryKey: [...MONITORS_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await monitorApis.getMonitors({
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
      return mapToPaginatedMonitors(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}
