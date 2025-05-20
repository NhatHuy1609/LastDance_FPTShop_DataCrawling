import { laptopsQueryOptions } from "@/entities/laptop/laptop.queries";
import { LaptopsQuery } from "@/entities/laptop/laptop.types";
import { useInfiniteQuery } from "@tanstack/react-query";

export function useLaptops(query: LaptopsQuery = {}) {
  return useInfiniteQuery(laptopsQueryOptions(query))
}