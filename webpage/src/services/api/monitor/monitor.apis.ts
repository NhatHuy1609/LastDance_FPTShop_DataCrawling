import { httpGet } from '../_req'
import { MonitorsDto, MonitorsQueryDto } from './monitor.types'

export function getMonitors(options: { params?: MonitorsQueryDto; signal?: AbortSignal }) {
  return httpGet<MonitorsDto>('/Monitor', options)
}
