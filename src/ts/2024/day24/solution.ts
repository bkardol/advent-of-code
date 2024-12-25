import { asStringArray } from "@aoc/common";

enum Operator {
  AND = "AND",
  OR = "OR",
  XOR = "XOR",
}

class Logic {
  constructor(
    public readonly input1: string,
    public readonly operator: Operator,
    public readonly input2: string,
    public readonly output: string
  ) {}

  process(gates: Map<string, { value: boolean | undefined }>) {
    const gate1 = gates.get(this.input1)!;
    const gate2 = gates.get(this.input2)!;
    const gateOutput = gates.get(this.output)!;
    if (gate1.value === undefined || gate2.value === undefined) {
      gateOutput.value = undefined;
      return;
    }

    const gate1Enabled = gate1.value === true;
    const gate2Enabled = gate2.value === true;

    switch (this.operator) {
      case Operator.AND:
        gateOutput.value = gate1Enabled && gate2Enabled;
        break;
      case Operator.OR:
        gateOutput.value = gate1Enabled || gate2Enabled;
        break;
      case Operator.XOR:
        gateOutput.value = gate1Enabled !== gate2Enabled;
        break;
    }

    return gateOutput.value;
  }
}

export class Solution {
  public part1(input: string): number {
    const [gatesString, logicString] = input.split("\n\n");
    const gates = new Map<string, { value: boolean | undefined }>(
      asStringArray(gatesString)
        .map((gate) => gate.split(": "))
        .map(([name, value]) => [name, { value: value === "1" }])
    );
    const logic = asStringArray(logicString)
      .map((gate) => gate.split(" "))
      .map(
        ([input1, op, input2, _, output]) =>
          new Logic(input1, op as Operator, input2, output)
      );
    logic
      .map(({ input1, input2, output }) => [input1, input2, output])
      .flat()
      .forEach((gate) =>
        gates.set(gate, gates.get(gate) ?? { value: undefined })
      );

    const zGates = [...gates.entries()]
      .filter(([gate]) => this.isZGate(gate))
      .sort(([gateA], [gateB]) => gateB.localeCompare(gateA));
    while (!zGates.every(([_, { value }]) => value !== undefined)) {
      logic.forEach((logic) => logic.process(gates));
    }

    const binary = zGates.map(([_, { value }]) => (value ? "1" : "0")).join("");
    return parseInt(binary, 2);
  }

  public part2(input: string): string {
    const [gatesString, logicString] = input.split("\n\n");
    const gates = new Map<string, { value: boolean | undefined }>(
      asStringArray(gatesString)
        .map((gate) => gate.split(": "))
        .map(([name, value]) => [name, { value: value === "1" }])
    );
    const logic = asStringArray(logicString)
      .map((gate) => gate.split(" "))
      .map(
        ([input1, op, input2, _, output]) =>
          new Logic(input1, op as Operator, input2, output)
      );
    logic
      .map(({ input1, input2, output }) => [input1, input2, output])
      .flat()
      .forEach((gate) =>
        gates.set(gate, gates.get(gate) ?? { value: undefined })
      );

    const zGates = [...gates.entries()]
      .filter(([gate]) => this.isZGate(gate))
      .sort(([gateA], [gateB]) => gateB.localeCompare(gateA));
    const zMin = Number(zGates[zGates.length - 1][0].substring(1));
    const zMax = Number(zGates[0][0].substring(1));

    const answerp2: Set<string> = new Set();
    while (!zGates.every(([_, { value }]) => value !== undefined)) {
      for (let i = 0; i < logic.length; i++) {
        const l = logic[i];
        if (l.process(gates) === undefined) {
          continue;
        }

        if (
          this.isInvalidNonXor(l, zMax) ||
          this.isInvalidXor(l) ||
          this.isInvalidXY(l, logic, Operator.XOR, Operator.XOR, zMin) ||
          this.isInvalidXY(l, logic, Operator.AND, Operator.OR, zMin)
        ) {
          answerp2.add(l.output);
        }
      }
    }

    return [...answerp2].sort().join(",");
  }

  private isInvalidNonXor(l: Logic, zMax: number) {
    return (
      l.operator !== Operator.XOR &&
      this.isZGate(l.output) &&
      this.isGateLower(l.output, zMax)
    );
  }

  private isInvalidXor(l: Logic) {
    return (
      l.operator === Operator.XOR &&
      !this.isZGate(l.output) &&
      !this.isXYGate(l.input1) &&
      !this.isXYGate(l.input2)
    );
  }

  private isInvalidXY(
    l: Logic,
    logic: Logic[],
    currentOperator: Operator,
    nextOperator: Operator,
    zMin: number
  ) {
    return (
      this.isXYGate(l.input1) &&
      this.isGateHigher(l.input1, zMin) &&
      this.isXYGate(l.input2) &&
      this.isGateHigher(l.input2, zMin) &&
      l.operator === currentOperator &&
      !logic.some(
        (next) =>
          next.operator === nextOperator &&
          (next.input1 === l.output || next.input2 === l.output)
      )
    );
  }

  private isGateHigher(gate: string, zMin: number) {
    return Number(gate.substring(1)) > zMin;
  }

  private isGateLower(gate: string, zMax: number) {
    return Number(gate.substring(1)) < zMax;
  }

  private isZGate(gate: string) {
    return gate.startsWith("z");
  }

  private isXYGate(gate: string) {
    return gate.startsWith("x") || gate.startsWith("y");
  }
}
