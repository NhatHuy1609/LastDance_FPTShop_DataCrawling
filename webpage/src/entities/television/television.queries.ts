import { televisionApis } from "@/services/api/television";
import { infiniteQueryOptions } from "@tanstack/react-query";
import { mapToPaginatedTelevisions } from "./television.lib";
import { TelevisionsQuery, PaginatedTelevisions } from "./television.types";

export const TELEVISIONS_ROOT_QUERY_KEY = ['televisions'];

export const televisionsQueryOptions = (query?: TelevisionsQuery) => {
  const {
    limit = 10,
    cursor = null,
    name,
    minPrice,
    maxPrice,
    sortBy,
    isDescending
  } = query || {}

  return infiniteQueryOptions<PaginatedTelevisions>({
    queryKey: [...TELEVISIONS_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await televisionApis.getTelevisions({ 
        signal,
        params: {
          limit,
          cursor: (pageParam as string) || undefined,
          name,
          minPrice,
          maxPrice,
          sortBy,
          isDescending
        }
      })

      return mapToPaginatedTelevisions(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}
