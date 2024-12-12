import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 12", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    test("part 1 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part1(input)).toBe(1930);
    });
    test("part 1 input", () => {
      const input = getInput(__dirname);
      expect(solution.part1(input)).toBe(1465968);
    });
  });

  describe("part 2", () => {
    test("part 2 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part2(input)).toBe(1206);
    });
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input)).toBe(897702);
    });
  });
});
