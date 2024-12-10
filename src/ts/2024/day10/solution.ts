import { asMatrix, asStringArray, Cell } from "@aoc/common";

export class Position extends Cell<Position, number> {}

export class Solution {
  public part1(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Position(Number(value))
    );

    const starts = map.flatMap((row) => row).filter((cell) => cell.value === 0);
    const result = starts.reduce(
      (acc, start) =>
        acc + this.walkTrail1(start, new Map<Position, Set<Position>>()).size,
      0
    );

    return result;
  }

  public part2(input: string): number {
    const map = asMatrix(
      asStringArray(input),
      (value) => new Position(Number(value))
    );

    const starts = map.flatMap((row) => row).filter((cell) => cell.value === 0);
    const result = starts.reduce(
      (acc, start) =>
        acc + this.walkTrail2(start, new Map<Position, Set<string>>()).size,
      0
    );

    return result;
  }

  private walkTrail1(
    position: Position,
    cache: Map<Position, Set<Position>>
  ): Set<Position> {
    const fromCache = cache.get(position);
    if (fromCache) {
      return fromCache;
    }

    const directions = position.getAdjacent(
      false,
      (cell) => cell.value === position.value + 1
    );

    const trail = new Set<Position>();
    for (const up of directions) {
      if (up.value === 9) {
        trail.add(up);
      } else {
        const upTrail = this.walkTrail1(up, cache);
        upTrail.forEach((cell) => trail.add(cell));
      }
    }
    cache.set(position, trail);
    return trail;
  }

  private walkTrail2(
    position: Position,
    cache: Map<Position, Set<string>>
  ): Set<string> {
    const fromCache = cache.get(position);
    if (fromCache) {
      return fromCache;
    }

    const directions = position.getAdjacent(
      false,
      (cell) => cell.value === position.value + 1
    );

    const trail = new Set<string>();
    for (const up of directions) {
      if (up.value === 9) {
        trail.add(`${up.y},${up.x}`);
      } else {
        const upTrail = this.walkTrail2(up, cache);
        upTrail.forEach((path) => trail.add(`${up.y},${up.x}|${path}`));
      }
    }

    cache.set(position, trail);
    return trail;
  }
}
