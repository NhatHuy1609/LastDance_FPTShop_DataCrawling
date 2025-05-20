import { televisionsQueryOptions } from "@/entities/television/television.queries";
import { TelevisionsQuery } from "@/entities/television/television.types";
import { useInfiniteQuery } from "@tanstack/react-query";

export function useTelevisions(query: TelevisionsQuery = {}) {
  return useInfiniteQuery(televisionsQueryOptions(query))
}