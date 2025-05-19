import { laptopTypesDto } from "@/services/api/laptop";
import { Laptop, PaginatedLaptops } from "./laptop.types";

export function mapToLaptop(
  data: laptopTypesDto.LaptopDto
): Laptop {
  return {
    ...data
  }
}

export function mapToPaginatedLaptops(
  data: laptopTypesDto.LaptopsDto
) : PaginatedLaptops {
  const { items, nextCursor, hasMore } = data

  return {
    laptops: items.map(mapToLaptop),
    nextCursor,
    hasMore
  }
}
