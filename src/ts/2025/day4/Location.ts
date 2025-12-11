import { Cell } from "@aoc/common";

export enum LocationType {
  Empty = ".",
  RollOfPaper = "@",
}

export class Location extends Cell<Location, LocationType> {
  isAccessible(): boolean {
    const isAccessible =
      this.getAdjacent(true, (l) => l.value === LocationType.RollOfPaper)
        .length < 4;
    return isAccessible;
  }

  removeRollOfPaper(): boolean {
    if (this.value !== LocationType.RollOfPaper || !this.isAccessible()) {
      return false;
    }

    this.value = LocationType.Empty;
    return true;
  }
}
