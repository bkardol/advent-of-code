import { asMatrix, asStringArray, Cell, Direction } from "@aoc/common";

export enum Type {
  START = "S",
  END = "E",
  WALL = "#",
}

export class Tile extends Cell<Tile, Type> {}

export class Solution {
  public part1(input: string): number {
    const matrix = asMatrix(
      asStringArray(input),
      (value) => new Tile(value as Type)
    );
    const start = matrix
      .flatMap((row) => row)
      .find((cell) => cell.value === Type.START);

    return this.findPathToEnd(start!).score;
  }

  public part2(input: string): number {
    const matrix = asMatrix(
      asStringArray(input),
      (value) => new Tile(value as Type)
    );
    const start = matrix
      .flatMap((row) => row)
      .find((cell) => cell.value === Type.START);

    return this.findPathToEnd(start!).tilesOnAnySimilarRoute.size + 1;
  }

  private findPathToEnd(start: Tile): {
    tilesOnAnySimilarRoute: Set<Tile>;
    score: number;
  } {
    let queue = [
      {
        current: start,
        turns: 0,
        pathSize: 0,
        tilesOnAnySimilarRoute: new Set<Tile>(),
        direction: Direction.East,
      },
    ];
    const visited = new Set<Tile>();

    const results: {
      tilesOnAnySimilarRoute: Set<Tile>;
      score: number;
    }[] = [];

    while (queue.length > 0) {
      const { current, turns, tilesOnAnySimilarRoute, direction, pathSize } =
        queue.shift()!;
      const score = 1000 * turns + pathSize;

      if (current.value === Type.END) {
        results.push({ tilesOnAnySimilarRoute, score });
        continue;
      }

      const neighbors = this.getNeighbors(current);
      if (visited.has(current)) {
        for (const neighbor of neighbors) {
          const neighborTurns =
            turns + (neighbor.direction !== direction ? 1 : 0);
          const queueMatch = queue.find(
            (q) =>
              q.current === neighbor.tile &&
              q.turns === neighborTurns &&
              q.pathSize === pathSize + 1
          );
          if (queueMatch) {
            tilesOnAnySimilarRoute.forEach((t) =>
              queueMatch.tilesOnAnySimilarRoute.add(t)
            );
          }
        }
        continue;
      }
      visited.add(current);

      for (const neighbor of neighbors) {
        queue.push({
          current: neighbor.tile,
          turns: turns + (neighbor.direction !== direction ? 1 : 0),
          tilesOnAnySimilarRoute: new Set([
            ...tilesOnAnySimilarRoute,
            neighbor.tile,
          ]),
          direction: neighbor.direction,
          pathSize: pathSize + 1,
        });
      }

      queue = queue.sort((a, b) =>
        a.turns === b.turns ? a.pathSize - b.pathSize : a.turns - b.turns
      );
    }

    if (results.length > 0) {
      const minScore = Math.min(...results.map((r) => r.score));
      return results.find((r) => r.score === minScore)!;
    }

    throw new Error("No path found");
  }

  private getNeighbors(tile: Tile) {
    return [
      { direction: Direction.North, tile: tile.top },
      { direction: Direction.East, tile: tile.right },
      { direction: Direction.South, tile: tile.bottom },
      { direction: Direction.West, tile: tile.left },
    ]
      .filter(
        (neighbor): neighbor is { direction: Direction; tile: Tile } =>
          !!neighbor.tile
      )
      .filter(({ tile }) => tile.value !== Type.WALL);
  }
}
