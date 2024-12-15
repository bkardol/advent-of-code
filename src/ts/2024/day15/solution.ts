import { asMatrix, asStringArray } from "@aoc/common";
import { Direction } from "./Direction";
import { Location, Type } from "./Location";

export class Solution {
  public part1(input: string): number {
    const { warehouse, instructions } = this.asWarehouseAndInstructions(
      input,
      false
    );
    let robot = this.findRobot(warehouse);

    for (const instruction of instructions) {
      robot = robot.moveRobot(instruction);
    }

    const boxes = warehouse
      .flatMap((row) => row)
      .filter((cell) => cell.value === Type.Box)
      .reduce((acc, box) => acc + box.y * 100 + box.x, 0);

    console.log(
      warehouse.map((row) => row.map((cell) => cell.value).join("")).join("\n")
    );
    return boxes;
  }

  public part2(input: string): number {
    const { warehouse, instructions } = this.asWarehouseAndInstructions(
      input,
      true
    );
    let robot = this.findRobot(warehouse);

    for (const instruction of instructions) {
      robot = robot.moveRobot(instruction);
    }

    const boxes = warehouse
      .flatMap((row) => row)
      .filter((cell) => cell.value === Type.BoxLeft)
      .reduce((acc, box) => acc + box.y * 100 + box.x, 0);

    return boxes;
  }

  private asWarehouseAndInstructions(
    input: string,
    widen: boolean
  ): { warehouse: Location[][]; instructions: Direction[] } {
    let instructions = [];
    let stringArray = asStringArray(input);
    while (true) {
      const line = stringArray.pop();
      if (!line) {
        break;
      }

      instructions.push(...[...line].reverse().map((c) => c as Direction));
    }
    instructions = instructions.reverse();

    if (widen) {
      stringArray = stringArray.map((row) =>
        row
          .replaceAll("#", "##")
          .replaceAll("O", "[]")
          .replaceAll(".", "..")
          .replace("@", "@.")
      );
    }

    const warehouse = asMatrix(stringArray, (c) => new Location(c as Type));

    return { warehouse, instructions };
  }

  private findRobot(warehouse: Location[][]): Location {
    let robot = warehouse
      .flatMap((row) => row)
      .find((cell) => cell.value === Type.Robot);

    if (!robot) {
      throw new Error("Robot not found.");
    }

    return robot;
  }
}
