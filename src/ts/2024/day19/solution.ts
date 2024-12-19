import { asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): number {
    const inputLines = asStringArray(input);
    const availablePatterns = inputLines[0].split(", ");
    const designs = inputLines.slice(2);

    return designs.filter(
      (design) =>
        this.checkDesign(design, availablePatterns, new Map<string, number>()) >
        0
    ).length;
  }

  public part2(input: string): number {
    const inputLines = asStringArray(input);
    const availablePatterns = inputLines[0].split(", ");
    const designs = inputLines.slice(2);

    return designs.reduce(
      (acc, design) =>
        acc +
        this.checkDesign(design, availablePatterns, new Map<string, number>()),
      0
    );
  }

  private checkDesign(
    design: string,
    patterns: string[],
    cache: Map<string, number>
  ): number {
    if (design.length === 0) {
      return 1;
    }

    const cacheResult = cache.get(design);
    if (cacheResult !== undefined) {
      return cacheResult;
    }

    const result = patterns
      .filter((pattern) => design.startsWith(pattern))
      .reduce(
        (acc, pattern) =>
          acc + this.checkDesign(design.slice(pattern.length), patterns, cache),
        0
      );

    cache.set(design, result);

    return result;
  }
}
