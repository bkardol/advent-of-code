import { Cell, Direction } from "@aoc/common";

export enum LocationType {
  Empty = ".",
  Obstacle = "#",
  Guard = "^",
}

export class Location extends Cell<Location, LocationType> {
  public visited = false;
  public visitedFromDirection: Direction[] = [];

  walkInDirection(direction: Direction): {
    location: Location | undefined;
    direction: Direction;
  } {
    let location: Location | undefined = this;
    while (location === this) {
      switch (direction) {
        case Direction.North:
          location = this.top;
          break;
        case Direction.East:
          location = this.right;
          break;
        case Direction.South:
          location = this.bottom;
          break;
        case Direction.West:
          location = this.left;
          break;
      }

      if (location?.value === LocationType.Obstacle) {
        direction = this.turnRight(direction);
        location = this;
      }
      if (location?.visitedFromDirection.includes(direction)) {
        throw new Error("Loop detected");
      }
      location?.visitedFromDirection.push(direction);
    }

    location?.visit();

    return { location, direction };
  }

  private turnRight(direction: Direction): Direction {
    switch (direction) {
      case Direction.North:
        return Direction.East;
      case Direction.East:
        return Direction.South;
      case Direction.South:
        return Direction.West;
      case Direction.West:
        return Direction.North;
    }
  }

  private visit(): Location | undefined {
    this.visited = true;
    return this;
  }
}
