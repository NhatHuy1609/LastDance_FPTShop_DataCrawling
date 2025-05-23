import { laptopApis } from "@/services/api/laptop";
import { infiniteQueryOptions } from "@tanstack/react-query";
import { mapToPaginatedLaptops } from "./laptop.lib";
import { LaptopsQuery, PaginatedLaptops } from "./laptop.types";

export const LAPTOPS_ROOT_QUERY_KEY = ['laptops'];

export const laptopsQueryOptions = (query?: LaptopsQuery) => {
  const {
    limit = 10,
    cursor = null,
    name,
    minPrice,
    maxPrice,
    sortBy,
    isDescending
  } = query || {}

  return infiniteQueryOptions<PaginatedLaptops>({
    queryKey: [...LAPTOPS_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await laptopApis.getLaptops({ 
        signal ,
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
      return mapToPaginatedLaptops(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}