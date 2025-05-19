import { washingMachinesQueryOptions } from "@/entities/washingmachine/washingmachine.queries";
import { useInfiniteQuery } from "@tanstack/react-query";

export function useWashingMachines() {
  return useInfiniteQuery(washingMachinesQueryOptions())
}

