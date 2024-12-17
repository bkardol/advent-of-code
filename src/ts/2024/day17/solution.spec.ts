import { getExampleInput, getInput } from "@aoc/common";
import { Solution } from "./solution";

describe("Day 17", () => {
  let solution: Solution;

  beforeEach(() => {
    solution = new Solution();
  });

  describe("part 1", () => {
    // test("part 1 example", () => {
    //   const input = getExampleInput(__dirname);
    //   expect(solution.part1(input)).toBe("4,6,3,5,6,3,5,2,1,0");
    // });
    test("part 1 input", () => {
      const input = getInput(__dirname);
      expect(solution.part1(input)).toBe("3,6,7,0,5,7,3,1,4");
    });
  });

  describe("part 2", () => {
    test("part 2 example", () => {
      const input = getExampleInput(__dirname);
      expect(solution.part2(input)).toBe(117440n);
    });
    test("part 2 input", () => {
      const input = getInput(__dirname);
      expect(solution.part2(input)).toBe(164278496489149n);
    });
  });
});
