import { asStringArray } from "@aoc/common";

export class Computer {
  public connections: Set<Computer> = new Set();

  constructor(public readonly id: string) {}

  public connect(computer: Computer) {
    this.connections.add(computer);
    computer.connections.add(this);
  }
}

export class Solution {
  private readonly interconnectedSize = 3;

  public part1(input: string): number {
    const computers = this.asComputers(input);
    const interconnectedComputers = this.getInterconnectedComputers(computers);

    const combinations = new Set();
    for (const connected of interconnectedComputers) {
      if (connected.size >= this.interconnectedSize) {
        for (const comb of this.getCombinations(
          [...connected].sort((a, b) => a.id.localeCompare(b.id)),
          this.interconnectedSize
        )) {
          if (comb.some((comp) => comp.id.startsWith("t"))) {
            combinations.add(comb.map((comp) => comp.id).join(","));
          }
        }
      }
    }

    return combinations.size;
  }

  public part2(input: string): string {
    const computers = this.asComputers(input);
    const interconnectedComputers = this.getInterconnectedComputers(computers);

    const maxConnected = interconnectedComputers.reduce(
      (acc: Set<Computer>, connected) =>
        connected.size > acc.size ? connected : acc,
      new Set<Computer>()
    );

    return [...maxConnected.values()]
      .map((comp) => comp.id)
      .sort()
      .join(",");
  }

  private asComputers(input: string): Computer[] {
    const computers = new Map<string, Computer>();
    asStringArray(input).forEach((connection) => {
      const [from, to] = connection.split("-");
      if (!computers.has(from)) {
        computers.set(from, new Computer(from));
      }
      if (!computers.has(to)) {
        computers.set(to, new Computer(to));
      }
      computers.get(from)!.connect(computers.get(to)!);
    });
    return [...computers.values()];
  }

  private getInterconnectedComputers(computers: Computer[]) {
    return computers
      .map((computer) => {
        const linked = [new Set([computer])];
        for (const connected of computer.connections) {
          for (const linkedSet of linked) {
            if (
              [...linkedSet].every((comp) => connected.connections.has(comp))
            ) {
              linkedSet.add(connected);
              break;
            }
          }
          linked.push(new Set([computer, connected]));
        }
        return linked;
      })
      .flat();
  }

  private getCombinations(
    computers: Computer[],
    setSize: number,
    result: Computer[] = []
  ) {
    if (setSize === 0) {
      return [result];
    }
    const combinations: Computer[][] = [];
    for (let i = 0; i <= computers.length - setSize; i++) {
      combinations.push(
        ...this.getCombinations(computers.slice(i + 1), setSize - 1, [
          ...result,
          computers[i],
        ])
      );
    }
    return combinations;
  }
}
