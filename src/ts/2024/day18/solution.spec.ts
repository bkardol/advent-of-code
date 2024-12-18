import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 18", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    test("part 1 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part1(input, 7, 12)).toBe(22);
    });
    test("part 1 input", () => {
      const input = getInput(__dirname);
      expect(solution.part1(input, 71, 1024)).toBe(288);
    });
  });

  describe("part 2", () => {
    test("part 2 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part2(input, 7, 12)).toBe("6,1");
    });
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input, 71, 1024)).toBe("52,5");
    });
  });
});
