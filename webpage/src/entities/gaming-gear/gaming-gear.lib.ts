import { GamingGear, PaginatedGamingGears } from './gaming-gear.types'
import { gamingGearSchemasDto } from '@/services/api/gaming-gear'
import { z } from 'zod'

export function mapToGamingGear(
  data: z.infer<typeof gamingGearSchemasDto.GamingGearDtoSchema>
): GamingGear {
  return {
    ...data
  }
}

export function mapToPaginatedGamingGears(
  data: z.infer<typeof gamingGearSchemasDto.GamingGearsDtoSchema>
): PaginatedGamingGears {
  const { items, nextCursor, hasMore } = data

  return {
    items: items.map(mapToGamingGear),
    nextCursor,
    hasMore
  }
}
