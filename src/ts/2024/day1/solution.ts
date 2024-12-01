import { asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): number {
    const { left, right } = this.asLocationIds(input);

    const output = left
      .map((left, i) => Math.abs(left - right[i]))
      .reduce((prev, curr) => prev + curr, 0);

    return output;
  }

  public part2(input: string): number {
    const { left, right } = this.asLocationIds(input);

    const output = left
      .map((left) => left * right.filter((value) => value === left).length)
      .reduce((prev, curr) => prev + curr, 0);
    return output;
  }

  private asLocationIds(input: string) {
    const values = asStringArray(input).map((line) =>
      line.split("   ").map((value) => Number(value))
    );
    const left = values.sort((a, b) => a[0] - b[0]).map(([left]) => left);
    const right = values.sort((a, b) => a[1] - b[1]).map(([, right]) => right);

    return { left, right };
  }
}
