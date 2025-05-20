import { z } from "zod";
import { TelevisionDtoSchema, TelevisionQueryDtoSchema } from "./television.schemas";
import { PaginatedResult } from "../_models/paginated-result";

export type TelevisionDto = z.infer<typeof TelevisionDtoSchema>
export type TelevisionsDto = PaginatedResult<TelevisionDto>
export type TelevisionQueryDto = z.infer<typeof TelevisionQueryDtoSchema>
