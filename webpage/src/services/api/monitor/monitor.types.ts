import { z } from 'zod'
import { MonitorDtoSchema, MonitorsQueryDtoSchema } from './monitor.schemas'
import { PaginatedResult } from '../_models/paginated-result'

export type MonitorDto = z.infer<typeof MonitorDtoSchema>
export type MonitorsDto = PaginatedResult<MonitorDto>
export type MonitorsQueryDto = z.infer<typeof MonitorsQueryDtoSchema>
