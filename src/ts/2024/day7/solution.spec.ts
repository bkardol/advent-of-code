import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 7", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    test("part 1 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part1(input)).toBe(3749);
    });
    test("part 1 input", () => {
      const input = getInput(__dirname);
      expect(solution.part1(input)).toBe(538191549061);
    });
  });

  describe("part 2", () => {
    test("part 2 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part2(input)).toBe(11387);
    });
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input)).toBe(34612812972206);
    });
  });
});
