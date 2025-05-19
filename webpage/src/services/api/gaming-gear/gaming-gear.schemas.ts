import { z } from 'zod'

export const GamingGearDtoSchema = z.object({
  id: z.number(),
  name: z.string(),
  url: z.string(),
  imageUrl: z.string(),
  price: z.number(),
  priceDiscount: z.number(),
  category: z.string(),
  isAvailable: z.string()
})

export const GamingGearsDtoSchema = z.object({
  items: z.array(GamingGearDtoSchema),
  nextCursor: z.string().optional(),
  hasMore: z.boolean()
})

// Query models
export const GamingGearsQueryDtoSchema = z.object({
  limit: z.number().optional(),
  cursor: z.string().optional(),
  name: z.string().optional(),
  category: z.string().optional(),
  minPrice: z.number().optional(),
  maxPrice: z.number().optional(),
  sortBy: z.string().optional(),
  isDescending: z.boolean().optional()
})
