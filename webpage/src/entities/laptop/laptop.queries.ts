import { laptopApis } from "@/services/api/laptop";
import { infiniteQueryOptions, queryOptions } from "@tanstack/react-query";
import { mapToPaginatedLaptops } from "./laptop.lib";
import { LaptopsQuery, PaginatedLaptops } from "./laptop.types";

export const LAPTOPS_ROOT_QUERY_KEY = ['laptops'];

export const laptopsQueryOptions = (query?: LaptopsQuery) => {
  const { limit = 10, cursor = null } = query || {}

  return infiniteQueryOptions<PaginatedLaptops>({
    queryKey: [...LAPTOPS_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await laptopApis.getLaptops({ 
        signal ,
        params: {
          limit,
          cursor: pageParam as string || undefined
        }
      })
      return mapToPaginatedLaptops(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      console.log('NEXT CURSOR: ', data.nextCursor)
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}