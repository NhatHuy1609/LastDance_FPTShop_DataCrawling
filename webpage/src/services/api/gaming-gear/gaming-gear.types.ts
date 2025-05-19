import { z } from 'zod'
import { GamingGearDtoSchema, GamingGearsQueryDtoSchema } from './gaming-gear.schemas'
import { PaginatedResult } from '../_models/paginated-result'

export type GamingGearDto = z.infer<typeof GamingGearDtoSchema>
export type GamingGearsDto = PaginatedResult<GamingGearDto>
export type GamingGearsQueryDto = z.infer<typeof GamingGearsQueryDtoSchema>
