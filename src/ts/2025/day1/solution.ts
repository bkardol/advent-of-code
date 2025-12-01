import { asStringArray } from "@aoc/common";

export class Solution {
  private readonly dialStart = 50;

  public part1(input: string): number {
    let rotatedToZero = 0;
    asStringArray(input)
      .map((line) => this.toDialRotationNumber(line))
      .reduce((prev, curr) => {
        let sum = prev + curr;
        while (sum >= 100) {
          sum -= 100;
        }
        while (sum < 0) {
          sum += 100;
        }
        if (sum === 0) {
          ++rotatedToZero;
        }
        return sum;
      }, this.dialStart);

    return rotatedToZero;
  }

  public part2(input: string): number {
    let rotatedToZero = 0;
    asStringArray(input)
      .map((line) => this.toDialRotationNumber(line))
      .reduce((prev, curr) => {
        let sum = prev + curr;
        while (sum >= 100) {
          sum -= 100;
          ++rotatedToZero;
        }
        while (sum < 0) {
          sum += 100;
          ++rotatedToZero;
        }
        if (sum === 0 && curr < -100) {
          --rotatedToZero;
        }
        if (sum === 0 && curr > 100) {
          ++rotatedToZero;
        }
        return sum;
      }, this.dialStart);

    return rotatedToZero;
  }

  private toDialRotationNumber(dialRotation: string): number {
    if (dialRotation[0] === "L") {
      return -Number(dialRotation.slice(1));
    } else {
      return Number(dialRotation.slice(1));
    }
  }
}
