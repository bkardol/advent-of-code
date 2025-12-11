import { asMatrix, asStringArray, countInMatrix } from "../../common";
import { Location, LocationType } from "./Location";

export class Solution {
  public part1(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Location(value as LocationType)
    );

    return countInMatrix(map, (cell) =>
      cell.value === LocationType.RollOfPaper && cell.isAccessible() ? 1 : 0
    );
  }

  public part2(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Location(value as LocationType)
    );

    let removedRolls = 0;
    let totalRemovedRolls = 0;
    do {
      removedRolls = countInMatrix(map, (cell) =>
        cell.removeRollOfPaper() ? 1 : 0
      );
      totalRemovedRolls += removedRolls;
    } while (removedRolls > 0);
    return totalRemovedRolls;
  }
}
