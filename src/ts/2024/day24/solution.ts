import { asStringArray } from "@aoc/common";

enum Operation {
  AND = "AND",
  OR = "OR",
  XOR = "XOR",
}

class Logic {
  constructor(
    public readonly input1: string,
    public readonly operation: Operation,
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

    switch (this.operation) {
      case Operation.AND:
        gateOutput.value = gate1Enabled && gate2Enabled;
        break;
      case Operation.OR:
        gateOutput.value = gate1Enabled || gate2Enabled;
        break;
      case Operation.XOR:
        gateOutput.value = gate1Enabled !== gate2Enabled;
        break;
    }
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
          new Logic(input1, op as Operation, input2, output)
      );
    logic
      .map(({ input1, input2, output }) => [input1, input2, output])
      .flat()
      .forEach((gate) =>
        gates.set(gate, gates.get(gate) ?? { value: undefined })
      );

    const zGates = [...gates.entries()]
      .filter(([gate]) => gate.startsWith("z"))
      .sort(([gateA], [gateB]) => gateB.localeCompare(gateA));
    while (!zGates.every(([_, { value }]) => value !== undefined)) {
      logic.forEach((logic) => logic.process(gates));
    }

    const binary = zGates.map(([_, { value }]) => (value ? "1" : "0")).join("");
    return parseInt(binary, 2);
  }

  public part2(input: string): number {
    return 0;
  }
}
