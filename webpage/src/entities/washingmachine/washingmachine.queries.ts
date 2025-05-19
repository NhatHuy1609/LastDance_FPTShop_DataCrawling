import { washingMachineApis } from "@/services/api/washingmachine";
import { infiniteQueryOptions } from "@tanstack/react-query";
import { mapToPaginatedWashingMachines } from "./washingmachine.lib";
import { PaginatedWashingMachines, WashingMachinesQuery } from "./washingmachine.types";

export const WASHING_MACHINES_ROOT_QUERY_KEY = ['washingMachines'];

export const washingMachinesQueryOptions = (query?: WashingMachinesQuery) => {
  const { limit = 10, cursor = null } = query || {}

  return infiniteQueryOptions<PaginatedWashingMachines>({
    queryKey: [...WASHING_MACHINES_ROOT_QUERY_KEY, `limit-${limit}`],
    queryFn: async ({ pageParam, signal }) => {
      const response = await washingMachineApis.getWashingMachines({ 
        signal ,
        params: {
          limit,
          cursor: pageParam as string || undefined
        }
      })
      return mapToPaginatedWashingMachines(response.data)
    },
    initialPageParam: cursor ?? null,
    getNextPageParam: (data) => {
      console.log('NEXT CURSOR: ', data.nextCursor)
      return data.hasMore ? data.nextCursor : undefined
    }
  })
}