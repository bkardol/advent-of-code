import { asMatrix, asStringArray, Cell } from "@aoc/common";

export class Position extends Cell<Position, string> {}

export class Solution {
  private readonly numericKeyMatrix: Position[][];
  private readonly directionalKeyMatrix: Position[][];

  constructor() {
    this.numericKeyMatrix = asMatrix(
      ["789", "456", "123", " 0A"],
      (value) => new Position(value)
    );
    this.directionalKeyMatrix = asMatrix(
      [" ^A", "<v>"],
      (value) => new Position(value)
    );
  }

  public part1(input: string): number {
    const numberOfRobots = 2;
    const cache = this.createEmptyCache(numberOfRobots);
    return asStringArray(input).reduce(
      (acc, keyCode) =>
        acc +
        parseInt(keyCode) *
          this.getAmountOfKeyPresses(
            this.numericKeyMatrix,
            keyCode,
            numberOfRobots,
            cache
          ),
      0
    );
  }

  public part2(input: string): number {
    const numberOfRobots = 25;
    const cache = this.createEmptyCache(numberOfRobots);
    return asStringArray(input).reduce(
      (acc, keyCode) =>
        acc +
        parseInt(keyCode) *
          this.getAmountOfKeyPresses(
            this.numericKeyMatrix,
            keyCode,
            numberOfRobots,
            cache
          ),
      0
    );
  }

  private createEmptyCache(numberOfRobots: number) {
    const cache = new Map<number, Map<string, number>>();
    for (let i = 0; i <= numberOfRobots; i++) {
      cache.set(i, new Map<string, number>());
    }
    return cache;
  }

  private getMoves(from: Position, to: Position) {
    const queue = [{ position: from, path: "" }];
    const distances: { [key: string]: number } = {};

    if (from === to) return ["A"];

    let allPaths: string[] = [];
    while (queue.length) {
      const current = queue.shift();
      if (current === undefined) break;

      if (current.position === to) {
        allPaths.push(current.path + "A");
      }

      this.getAdjacent(current.position).forEach(({ direction, position }) => {
        const newPath = current.path + direction;
        if (
          distances[position.value] === undefined ||
          distances[position.value] >= newPath.length
        ) {
          queue.push({ position, path: newPath });
          distances[position.value] = newPath.length;
        }
      });
    }

    return allPaths.sort((a, b) => a.length - b.length);
  }

  private getAdjacent(position: Position) {
    return [
      { direction: "^", position: position.top },
      { direction: ">", position: position.right },
      { direction: "v", position: position.bottom },
      { direction: "<", position: position.left },
    ].filter(
      (
        adjacentDirection
      ): adjacentDirection is { direction: string; position: Position } =>
        !!adjacentDirection.position && adjacentDirection.position.value !== " "
    );
  }

  private fillMapFromPosition(
    position: Position,
    map: Map<string, number>,
    steps: number
  ) {
    position
      .getAdjacent(
        false,
        (p) =>
          p.value !== " " && (!map.has(p.value) || map.get(p.value)! > steps)
      )
      .forEach((adj) => {
        map.set(adj.value, steps);
        this.fillMapFromPosition(adj, map, steps + 1);
      });
  }

  private getAmountOfKeyPresses(
    keypad: Position[][],
    keyCode: string,
    robot: number,
    cache: Map<number, Map<string, number>>
  ): number {
    const roboCache = cache.get(robot)!;
    const cached = roboCache.get(keyCode);
    if (cached !== undefined) {
      return cached;
    }

    const flattenedKeypad = keypad.flat();
    let current = "A";
    let length = 0;
    for (let i = 0; i < keyCode.length; i++) {
      const moves = this.getMoves(
        flattenedKeypad.find((p) => p.value === current)!,
        flattenedKeypad.find((p) => p.value === keyCode[i])!
      );

      if (robot === 0) {
        length += moves[0].length;
      } else {
        length += Math.min(
          ...moves.map((move) =>
            this.getAmountOfKeyPresses(
              this.directionalKeyMatrix,
              move,
              robot - 1,
              cache
            )
          )
        );
      }
      current = keyCode[i];
    }

    roboCache.set(keyCode, length);

    return length;
  }
}
