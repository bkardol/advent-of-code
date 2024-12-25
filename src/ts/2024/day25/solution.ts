import { asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): number {
    const keys: number[][] = [];
    const locks: number[][] = [];
    const maxHeight = 6;
    input.split("\n\n").forEach((data) => {
      const dataArray = asStringArray(data);
      const isLock = dataArray[0] === "#####";
      const sizes: number[] = [];
      const height = maxHeight;
      for (let i = 0; i < height; i++) {
        for (let j = 1; j < dataArray.length; j++) {
          const row = isLock ? j : dataArray.length - 1 - j;
          if (dataArray[row][i] === ".") {
            sizes.push(j - 1);
            break;
          }
        }
      }
      if (isLock) {
        locks.push(sizes);
      } else {
        keys.push(sizes);
      }
    });

    return locks.reduce(
      (acc, lock) =>
        acc +
        keys.filter((key) => key.every((size, i) => size + lock[i] < maxHeight))
          .length,
      0
    );
  }
}
