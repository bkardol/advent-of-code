import { asStringArray } from "@aoc/common";

interface Equation {
  testValue: number;
  numbers: number[];
}

enum Operator {
  ADDITION = "+",
  MULTIPLICATION = "*",
  CONCATENATION = "[]",
}

export class Solution {
  private readonly operatorsPart1 = [
    Operator.ADDITION,
    Operator.MULTIPLICATION,
  ];
  private readonly operatorsPart2 = [
    Operator.ADDITION,
    Operator.MULTIPLICATION,
    Operator.CONCATENATION,
  ];

  public part1(input: string): number {
    const equations: Equation[] = this.asEquations(input);

    const totalCalibrationResult = this.calculateCalibrationResult(
      equations,
      this.operatorsPart1
    );

    return totalCalibrationResult;
  }

  public part2(input: string): number {
    const equations: Equation[] = this.asEquations(input);

    const totalCalibrationResult = this.calculateCalibrationResult(
      equations,
      this.operatorsPart2
    );

    return totalCalibrationResult;
  }

  private asEquations(input: string): Equation[] {
    return asStringArray(input).map((line) => {
      const splitted = line.split(": ");
      return {
        testValue: Number(splitted[0]),
        numbers: splitted[1].split(" ").map(Number),
      };
    });
  }

  private calculateCalibrationResult(
    equations: Equation[],
    operators: string[]
  ) {
    return equations.reduce(
      (acc, equation) =>
        acc +
        (this.trySolveEquation(
          operators,
          equation,
          1,
          equation.numbers[0],
          Operator.ADDITION
        )
          ? equation.testValue
          : 0),
      0
    );
  }

  private trySolveEquation(
    operators: string[],
    equation: Equation,
    index: number,
    currentValue: number,
    previousOperator: string
  ): boolean {
    if (index === equation.numbers.length) {
      return currentValue === equation.testValue;
    }

    const number = equation.numbers[index];
    for (const operator of operators) {
      let newValue = currentValue;
      switch (operator) {
        case Operator.ADDITION:
          newValue = currentValue + number;
          break;
        case Operator.MULTIPLICATION:
          newValue = currentValue * number;
          break;
        case Operator.CONCATENATION:
          newValue = Number(`${currentValue}${number}`);
          break;
      }

      if (
        newValue <= equation.testValue &&
        this.trySolveEquation(
          operators,
          equation,
          index + 1,
          newValue,
          previousOperator
        )
      ) {
        return true;
      }
    }

    return false;
  }
}
