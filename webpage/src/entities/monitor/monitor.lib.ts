import { Monitor, PaginatedMonitors } from './monitor.types'
import { monitorTypesDto } from '@/services/api/monitor'

export function mapToMonitor(data: monitorTypesDto.MonitorDto): Monitor {
  return {
    ...data
  }
}

export function mapToPaginatedMonitors(data: monitorTypesDto.MonitorsDto): PaginatedMonitors {
  const { items, nextCursor, hasMore } = data

  return {
    items: items.map(mapToMonitor),
    nextCursor,
    hasMore
  }
}
