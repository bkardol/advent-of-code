import { asMatrix, asStringArray, Cell } from "@aoc/common";

export enum Type {
  SAFE = ".",
  CORRUPTED = "#",
}

export class Location extends Cell<Location, Type> {}

export class Solution {
  public part1(input: string, size: number, bytes: number): number {
    const matrix = this.asMemoryMatrix(size);
    const corrupted = this.asCorruptedArray(input);
    this.corruptMatrix(matrix, corrupted.slice(0, bytes));

    const start = matrix[0][0];
    const end = matrix[size - 1][size - 1];

    const steps = this.findPathToEnd(start, end);

    return steps;
  }

  public part2(input: string, startSize: number, bytes: number): string {
    const matrix = this.asMemoryMatrix(startSize);
    const corrupted = this.asCorruptedArray(input);

    const start = matrix[0][0];
    const end = matrix[startSize - 1][startSize - 1];

    for (let i = bytes; i <= corrupted.length; i++) {
      this.corruptMatrix(matrix, corrupted.slice(0, i));
      const steps = this.findPathToEnd(start, end);
      if (steps === 0) {
        const blocker = corrupted[i - 1];
        return `${blocker.x},${blocker.y}`;
      }
    }

    throw new Error("No solution found");
  }

  private asCorruptedArray(input: string) {
    return asStringArray(input)
      .map((line) => line.split(","))
      .map(([x, y]) => ({ x: Number(x), y: Number(y) }));
  }

  private asMemoryMatrix(size: number) {
    return asMatrix(
      Array.from({ length: size }).map(() =>
        Array.from({ length: size + 1 }).join(".")
      ),
      (_, p) => new Location(Type.SAFE)
    );
  }

  private corruptMatrix(
    matrix: Location[][],
    corrupted: { x: number; y: number }[]
  ) {
    corrupted.forEach(({ x, y }) => (matrix[y][x].value = Type.CORRUPTED));
  }

  private findPathToEnd(start: Location, end: Location) {
    const queue = [{ tile: start, path: [start] }];
    const visited = new Set<Location>();

    while (queue.length > 0) {
      const { tile, path } = queue.shift()!;
      if (tile === end) {
        return path.length - 1;
      }

      if (visited.has(tile)) {
        continue;
      }
      visited.add(tile);

      for (const neighbor of tile.getAdjacent(
        false,
        (location) => location.value === Type.SAFE && !visited.has(location)
      )) {
        queue.push({ tile: neighbor, path: [...path, neighbor] });
      }
    }

    return 0;
  }
}
