import { gamingGearsQueryOptions } from '@/entities/gaming-gear/gaming-gear.queries'
import { GamingGearsQuery } from '@/entities/gaming-gear/gaming-gear.types'
import { useInfiniteQuery } from '@tanstack/react-query'

export function useGamingGears(query: GamingGearsQuery = {}) {
  return useInfiniteQuery(gamingGearsQueryOptions(query))
}
