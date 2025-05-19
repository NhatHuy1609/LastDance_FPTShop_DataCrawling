import { z } from 'zod'

export const WashingMachineDtoSchema = z.object({
  id: z.number(),
  name: z.string(),
  url: z.string(),
  imageUrl: z.string(),
  price: z.number(),
  priceDiscount: z.number()
})

// Query models
export const WashingMachineQueryDtoSchema = z.object({
  limit: z.number().optional(),
  cursor: z.string().optional(),
  name: z.string().optional(),
  category: z.string().optional(),
})