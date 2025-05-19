import { laptopsQueryOptions } from "@/entities/laptop/laptop.queries";
import { useInfiniteQuery } from "@tanstack/react-query";

export function useLaptops() {
  return useInfiniteQuery(laptopsQueryOptions())
}