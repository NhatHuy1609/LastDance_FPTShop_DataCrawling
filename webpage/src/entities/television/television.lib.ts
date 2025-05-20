import { televisionTypesDto } from "@/services/api/television";
import { Television, PaginatedTelevisions } from "./television.types";

export function mapToTelevision(
  data: televisionTypesDto.TelevisionDto
): Television {
  return {
    ...data
  }
}

export function mapToPaginatedTelevisions(
  data: televisionTypesDto.TelevisionsDto
) : PaginatedTelevisions {
  const { items, nextCursor, hasMore } = data

  return {
    televisions: items.map(mapToTelevision),
    nextCursor,
    hasMore
  }
}
