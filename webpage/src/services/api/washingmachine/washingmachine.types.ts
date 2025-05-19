import { z } from "zod";
import { WashingMachineDtoSchema, WashingMachineQueryDtoSchema } from "./washingmachine.schemas";
import { PaginatedResult } from "../_models/paginated-result";

export type WashingMachineDto = z.infer<typeof WashingMachineDtoSchema>
export type WashingMachinesDto = PaginatedResult<WashingMachineDto>
export type WashingMachineQueryDto = z.infer<typeof WashingMachineQueryDtoSchema>