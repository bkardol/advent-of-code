import { asMatrix, asStringArray } from "@aoc/common";
import { Location } from "./Location";

export class Solution {
  public part1(input: string): number {
    const map = asMatrix(asStringArray(input), (value) => new Location(value));

    this.processSignals(map, false);

    return this.countAntinodes(map);
  }

  public part2(input: string): number {
    const map = asMatrix(asStringArray(input), (value) => new Location(value));

    this.processSignals(map, true);

    return this.countAntinodes(map);
  }

  private getAllSignalLocations(map: Location[][]): Location[] {
    return map.reduce((acc: Location[], row) => {
      row.forEach((loc) => {
        if (loc.value !== ".") {
          acc.push(loc);
        }
      });
      return acc;
    }, []);
  }

  private processSignals(map: Location[][], anyGridPosition: boolean): void {
    const allSignalLocations = this.getAllSignalLocations(map);
    const uniqueSignals = new Set<string>(
      allSignalLocations.map((loc) => loc.value)
    );

    for (const signal of uniqueSignals) {
      const signalLocations = allSignalLocations.filter(
        (loc) => loc.value === signal
      );
      for (const loc of signalLocations) {
        for (const other of signalLocations) {
          if (loc === other) {
            continue;
          }
          this.addAntinode(map, loc, other, anyGridPosition);
        }
      }
    }
  }

  private addAntinode(
    map: Location[][],
    loc1: Location,
    loc2: Location,
    anyGridPosition: boolean
  ): void {
    if (anyGridPosition) {
      loc1.hasAntiNode = true;
    }

    const distanceY = loc1.y - loc2.y;
    const distanceX = loc1.x - loc2.x;

    let newY = loc1.y;
    let newX = loc1.x;
    do {
      newY += distanceY;
      newX += distanceX;

      if (newY < 0 || newX < 0 || newY >= map.length || newX >= map[0].length) {
        return;
      }
      map[newY][newX].hasAntiNode = true;
    } while (anyGridPosition);
  }

  private countAntinodes(map: Location[][]): number {
    return map.reduce(
      (acc, row) => acc + row.filter((loc) => loc.hasAntiNode).length,
      0
    );
  }
}
