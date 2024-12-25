import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 24", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    test("part 1 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part1(input)).toBe(4);
    });
    test("part 1 input", () => {
      const input = getInput(__dirname);
      expect(solution.part1(input)).toBe(63168299811048);
    });
  });

  describe("part 2", () => {
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input)).toBe("dwp,ffj,gjh,jdr,kfm,z08,z22,z31");
    });
  });
});
