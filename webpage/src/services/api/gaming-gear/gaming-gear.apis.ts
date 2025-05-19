import { httpGet } from '../_req'
import { GamingGearsDto, GamingGearsQueryDto } from './gaming-gear.types'

export function getGamingGears(options: { params?: GamingGearsQueryDto; signal?: AbortSignal }) {
  return httpGet<GamingGearsDto>('/GamingGear', options)
}
