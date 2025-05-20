import { z } from 'zod'

export const TelevisionDtoSchema = z.object({
  id: z.number(),
  name: z.string(),
  url: z.string(),
  imageUrl: z.string(),
  price: z.number(),
  priceDiscount: z.number()
})

// Query models
export const TelevisionQueryDtoSchema = z.object({
  limit: z.number().optional(),
  cursor: z.string().optional(),
  name: z.string().optional(),
  minPrice: z.number().optional(),
  maxPrice: z.number().optional(),
  sortBy: z.string().optional(),
  isDescending: z.boolean().optional()
})
