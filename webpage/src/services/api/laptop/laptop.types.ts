import { z } from "zod";
import { LaptopDtoSchema, LaptopsQueryDtoSchema } from "./latop.schemas";
import { PaginatedResult } from "../_models/paginated-result";

export type LaptopDto = z.infer<typeof LaptopDtoSchema>
export type LaptopsDto = PaginatedResult<LaptopDto>
export type LaptopsQueryDto = z.infer<typeof LaptopsQueryDtoSchema>