import { Cell } from "@aoc/common";

export class Location extends Cell<Location, string> {
  public hasAntiNode: boolean = false;
}
