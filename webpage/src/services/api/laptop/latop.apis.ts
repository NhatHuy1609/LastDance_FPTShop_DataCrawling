import { httpGet } from "../_req";
import { LaptopsDto, LaptopsQueryDto } from "./laptop.types";

export function getLaptops( options: { params?: LaptopsQueryDto,  signal?: AbortSignal } ) {
  return httpGet<LaptopsDto>("/Laptop", options);
}