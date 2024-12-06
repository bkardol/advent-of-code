import { asMatrix, asStringArray, Direction } from "@aoc/common";
import { Location, LocationType } from "./Location";

export class Solution {
  public part1(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Location(this.resolveType(value))
    );

    this.walkThrough(map);

    return map.flatMap((row) => row).filter((location) => location.visited)
      .length;
  }

  public part2(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Location(this.resolveType(value))
    );

    this.walkThrough(map);

    const visited = map
      .flatMap((row) => row)
      .filter((location) => location.visited);

    let loopsCreated = 0;
    for (const location of visited) {
      const originalLocationValue = location.value;
      map.forEach((row) =>
        row.forEach((cell) => {
          cell.visited = false;
          cell.visitedFromDirection = [];
        })
      );

      try {
        location.value = LocationType.Obstacle;

        this.walkThrough(map);
      } catch (e) {
        loopsCreated++;
      }

      location.value = originalLocationValue;
    }

    return loopsCreated;
  }

  private walkThrough(map: Location[][]): void {
    let currentLocation = map
      .flatMap((row) => row)
      .find((location) => location.value === LocationType.Guard);

    let currentDirection = Direction.North;
    while (currentLocation) {
      const info = currentLocation.walkInDirection(currentDirection);
      if (!info.location) {
        break;
      }
      currentLocation = info.location;
      currentDirection = info.direction;
    }
  }

  private resolveType(type: string): LocationType {
    switch (type) {
      case LocationType.Empty:
        return LocationType.Empty;
      case LocationType.Obstacle:
        return LocationType.Obstacle;
      case LocationType.Guard:
        return LocationType.Guard;
      default:
        throw new Error(`Unknown location type: ${type}`);
    }
  }
}
