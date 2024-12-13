import { asStringArray } from "@aoc/common";
import { Button, Machine } from "./machine";

export class Solution {
  public part1(input: string): number {
    const machines: Machine[] = [];
    const inputArray = asStringArray(input);

    while (inputArray.length > 0) {
      const buttonALine = inputArray.shift();
      const buttonBLine = inputArray.shift();
      const prizeLine = inputArray.shift();
      inputArray.shift();

      if (!buttonALine || !buttonBLine || !prizeLine) {
        continue;
      }

      const machine = new Machine(
        this.asButton(buttonALine),
        this.asButton(buttonBLine),
        this.asPrize(prizeLine)
      );
      machines.push(machine);
    }

    return machines
      .map((m) => m.pressButtonsBetter())
      .filter((value) => value !== 0)
      .reduce((acc, value) => acc + value, 0);
  }

  public part2(input: string): number {
    const machines: Machine[] = [];
    const inputArray = asStringArray(input);

    while (inputArray.length > 0) {
      const buttonALine = inputArray.shift();
      const buttonBLine = inputArray.shift();
      const prizeLine = inputArray.shift();
      inputArray.shift();

      if (!buttonALine || !buttonBLine || !prizeLine) {
        continue;
      }

      const prize = this.asPrize(prizeLine);
      prize.x += 10000000000000;
      prize.y += 10000000000000;
      const machine = new Machine(
        this.asButton(buttonALine),
        this.asButton(buttonBLine),
        prize
      );
      machines.push(machine);
    }

    return machines
      .map((m) => m.pressButtonsBetter())
      .filter((value) => value !== 0)
      .reduce((acc, value) => acc + value, 0);
  }

  private asButton(line: string): Button {
    const [first, second] = line.split(": ");
    const [, id] = first.split(" ");
    const [x, y] = second.split(", ");
    const xValue = x.split("+")[1];
    const yValue = y.split("+")[1];
    return new Button(id, Number(xValue), Number(yValue));
  }

  private asPrize(line: string): { x: number; y: number } {
    const [, second] = line.split(": ");
    const [x, y] = second.split(", ");
    const xValue = x.split("=")[1];
    const yValue = y.split("=")[1];
    return { x: Number(xValue), y: Number(yValue) };
  }
}
