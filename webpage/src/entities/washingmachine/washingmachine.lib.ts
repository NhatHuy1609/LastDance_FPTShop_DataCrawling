import { washingMachineTypesDto } from "@/services/api/washingmachine";
import { WashingMachine, PaginatedWashingMachines } from "./washingmachine.types";

export function mapToWashingMachine(
  data: washingMachineTypesDto.WashingMachineDto
): WashingMachine {
  return {
    ...data
  }
}

export function mapToPaginatedWashingMachines(
  data: washingMachineTypesDto.WashingMachinesDto
) : PaginatedWashingMachines {
  const { items, nextCursor, hasMore } = data

  return {
    washingMachines: items.map(mapToWashingMachine),
    nextCursor,
    hasMore
  }
}
