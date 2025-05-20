import { httpGet } from "../_req";
import { TelevisionsDto, TelevisionQueryDto } from "./television.types";

export function getTelevisions(options: { params?: TelevisionQueryDto, signal?: AbortSignal }) {
  return httpGet<TelevisionsDto>("/Television", options);
}
