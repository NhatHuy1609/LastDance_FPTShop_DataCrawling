import { GamingGear } from '@/entities/gaming-gear/gaming-gear.types'
import { createContext } from 'react'

interface GamingGearsContextType {
  gamingGears: GamingGear[]
  filterGamingGearsByName: (name: string) => void
  filterGamingGearsByCategory: (category: string) => void
  filterGamingGearsByPrice: (minPrice?: number, maxPrice?: number) => void
  sortGamingGears: (sortBy: string, isDescending: boolean) => void
}

export const GamingGearsContext = createContext<GamingGearsContextType>({
  gamingGears: [],
  filterGamingGearsByName: () => {},
  filterGamingGearsByCategory: () => {},
  filterGamingGearsByPrice: () => {},
  sortGamingGears: () => {}
})
