import { monitorsQueryOptions } from "@/entities/monitor/monitor.queries";
import { MonitorsQuery } from "@/entities/monitor/monitor.types";
import { useInfiniteQuery } from "@tanstack/react-query";

export function useMonitors(query: MonitorsQuery = {}) {
  return useInfiniteQuery(monitorsQueryOptions(query))
} 