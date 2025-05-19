import { httpGet } from "../_req";
import { WashingMachinesDto, WashingMachineQueryDto } from "./washingmachine.types";

export function getWashingMachines( options: { params?: WashingMachineQueryDto,  signal?: AbortSignal } ) {
  return httpGet<WashingMachinesDto>("/WashingMachine", options);
}