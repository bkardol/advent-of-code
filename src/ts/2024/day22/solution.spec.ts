import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 22", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    test("part 1 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part1(input)).toEqual(37327623);
    });
    test("part 1 input", async () => {
      const input = getInput(__dirname);
      expect(solution.part1(input)).toBe(14869099597);
    });
  });

  describe("part 2", () => {
    test("part 2 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part2(input)).toBe(23);
    });
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input)).toBe(1717);
    });
  });
});
