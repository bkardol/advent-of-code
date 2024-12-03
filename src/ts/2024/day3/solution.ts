import { asNumberArray, asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): number {
    const matches =
      asStringArray(input)
        .join()
        .match(/mul\(\d+,\d+\)/g) ?? [];

    const parsed =
      matches.map((match) => match.match(/\d+/g)?.map(Number) ?? []) ?? [];

    return parsed.reduce((acc, curr) => acc + curr[0] * curr[1], 0);
  }

  public part2(input: string): number {
    const matches =
      asStringArray(input)
        .join()
        .match(/(mul\(\d+,\d+\))|(do\(\))|(don't\(\))/g) ?? [];

    let enabled = true;

    return matches.reduce((acc, curr) => {
      if (curr.startsWith("don't")) {
        enabled = false;
      } else if (curr.startsWith("do")) {
        enabled = true;
      } else {
        const numbers = curr.match(/\d+/g)?.map(Number) ?? [];
        return acc + (enabled ? numbers[0] * numbers[1] : 0);
      }
      return acc;
    }, 0);
  }
}
