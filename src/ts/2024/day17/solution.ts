import { asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): string {
    const { instructions, registers } = this.asInstructionsAndRegisters(input);
    return this.processInstructions(instructions, registers);
  }

  public part2(input: string): bigint {
    const { instructions, registers } = this.asInstructionsAndRegisters(input);
    const instructionsString = instructions.join(",");

    const repeatingFactor = 8n;

    for (let a = 1n; a < Number.MAX_VALUE; ) {
      const newRegisters = {
        ...registers,
        a,
      };

      const resultString = this.processInstructions(instructions, newRegisters);
      if (instructionsString === resultString) {
        return a;
      }

      if (instructionsString.endsWith(resultString)) {
        console.log(a);
        a *= repeatingFactor;
      } else {
        a++;
      }
    }

    return 0n;
  }

  private asInstructionsAndRegisters(input: string) {
    const inputArray = asStringArray(input);
    const registers = {
      a: BigInt(inputArray[0].split(": ")[1]),
      b: BigInt(inputArray[1].split(": ")[1]),
      c: BigInt(inputArray[2].split(": ")[1]),
    };
    const instructions = inputArray[4].split(": ")[1].split(",").map(BigInt);

    return { instructions, registers };
  }

  private processInstructions(
    instructions: bigint[],
    registers: { a: bigint; b: bigint; c: bigint }
  ) {
    const pointer = { instruction: 0 };
    const results: bigint[] = [];
    while (instructions[pointer.instruction] !== undefined) {
      const oldPointer = pointer.instruction;
      const result = this.processInstruction(
        instructions,
        pointer,
        instructions[pointer.instruction + 1],
        registers
      );

      if (result !== undefined) {
        results.push(result);
      }

      if (oldPointer === pointer.instruction) {
        pointer.instruction += 2;
      }
    }
    return results.join(",");
  }

  private processInstruction(
    instructions: bigint[],
    pointer: { instruction: number },
    operand: bigint,
    registers: { a: bigint; b: bigint; c: bigint }
  ): bigint | undefined {
    const instruction = instructions[pointer.instruction];
    switch (instruction) {
      case 0n:
        registers.a =
          registers.a / 2n ** this.getComboOperandValue(operand, registers);
        break;
      case 1n:
        registers.b = registers.b ^ operand;
        break;
      case 2n:
        registers.b = this.getComboOperandValue(operand, registers) % 8n;
        break;
      case 3n:
        if (registers.a !== 0n) {
          pointer.instruction = Number(operand);
        }
        break;
      case 4n:
        registers.b = registers.b ^ registers.c;
        break;
      case 5n:
        return this.getComboOperandValue(operand, registers) % 8n;
      case 6n:
        registers.b =
          registers.a / 2n ** this.getComboOperandValue(operand, registers);
        break;
      case 7n:
        registers.c =
          registers.a / 2n ** this.getComboOperandValue(operand, registers);
        break;
      default:
        throw new Error("Invalid instruction");
    }
  }

  private getComboOperandValue(
    comboOperand: bigint,
    registers: { a: bigint; b: bigint; c: bigint }
  ): bigint {
    switch (comboOperand) {
      case 0n:
      case 1n:
      case 2n:
      case 3n:
        return BigInt(comboOperand);
      case 4n:
        return registers.a;
      case 5n:
        return registers.b;
      case 6n:
        return registers.c;
      default:
        throw new Error("Invalid combo operand");
    }
  }
}
