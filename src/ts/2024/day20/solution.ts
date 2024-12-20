import { asMatrix, asStringArray, Cell } from "@aoc/common";

export enum Type {
  START = "S",
  END = "E",
  WALL = "#",
  TRACK = ".",
}

export class Location extends Cell<Location, Type> {}

export class Solution {
  public part1(input: string): number {
    const matrix = asMatrix(
      asStringArray(input),
      (value) => new Location(value as Type)
    );
    const flattened = matrix.flat();
    const start = flattened.find((cell) => cell.value === Type.START)!;
    const end = flattened.find((cell) => cell.value === Type.END)!;
    const path = this.findPathToEnd(start, end);

    const cheats = this.findCheats(path);

    return [...cheats]
      .filter(([saved]) => saved >= 100)
      .reduce((acc, [, count]) => acc + count, 0);
  }

  public part2(input: string): number {
    const matrix = asMatrix(
      asStringArray(input),
      (value) => new Location(value as Type)
    );
    const flattened = matrix.flat();
    const end = flattened.find((cell) => cell.value === Type.END)!;

    const distancesToEnd = this.getDistancesToEnd(end);

    let cheats = 0;
    distancesToEnd.forEach((startDistance, start) => {
      distancesToEnd.forEach((endDistance, end) => {
        const dist = Math.abs(start.x - end.x) + Math.abs(start.y - end.y);

        if (dist <= 20 && startDistance - endDistance - dist >= 100) {
          cheats++;
        }
      });
    });

    return cheats;
  }

  private findCheats(path: Location[]): Map<number, number> {
    const cheats = new Map<number, number>();

    path.forEach((location, i) => {
      location
        .getAdjacent(false, (cheat1) => cheat1.value === Type.WALL)
        .forEach((cheat1) => {
          cheat1
            .getAdjacent(
              false,
              (cheat2) => cheat2.value !== Type.WALL && location !== cheat2
            )
            .forEach((cheat2) => {
              const saved = -(i - path.indexOf(cheat2) + 2);
              if (saved > 0) {
                cheats.set(saved, (cheats.get(saved) || 0) + 1);
              }
            });
        });
    });

    return cheats;
  }

  private findPathToEnd(start: Location, end: Location) {
    const queue = [{ tile: start, path: [start] }];
    const visited = new Set<Location>();

    while (queue.length > 0) {
      const { tile, path } = queue.shift()!;
      if (tile === end) {
        return path;
      }

      if (visited.has(tile)) {
        continue;
      }
      visited.add(tile);

      for (const neighbor of tile.getAdjacent(
        false,
        (location) => location.value !== Type.WALL && !visited.has(location)
      )) {
        queue.push({ tile: neighbor, path: [...path, neighbor] });
      }
    }

    throw new Error("No path found");
  }

  private getDistancesToEnd = (start: Location) => {
    const queue: { location: Location; steps: number }[] = [
      { location: start, steps: 0 },
    ];
    const distances: Map<Location, number> = new Map();

    distances.set(start, 0);

    while (queue.length !== 0) {
      const current = queue.shift();
      if (current === undefined) {
        break;
      }

      const { location, steps } = current;

      location
        .getAdjacent(false, (loc) => loc.value !== Type.WALL)
        .forEach((adjacent) => {
          const newDistance = steps + 1;
          const distance = distances.get(adjacent);
          if (distance === undefined || distance > newDistance) {
            queue.push({ location: adjacent, steps: steps + 1 });
            distances.set(adjacent, newDistance);
          }
        });
    }

    return distances;
  };
}
