export class Button {
  constructor(
    private readonly id: string,
    public readonly xPlus: number,
    public readonly yPlus: number
  ) {}

  press(x: number, y: number) {
    return { x: x + this.xPlus, y: y + this.yPlus };
  }
}

export class Machine {
  constructor(
    private readonly buttonA: Button,
    private readonly buttonB: Button,
    private readonly prize: { x: number; y: number }
  ) {}

  public pressButtons(): number {
    const order = [{ xCurrent: 0, yCurrent: 0, presses: [0, 0] }];
    const visited = new Set<string>();

    while (order.length > 0) {
      const { xCurrent, yCurrent, presses } = order.shift()!;

      if (xCurrent === this.prize.x && yCurrent === this.prize.y) {
        return presses[0] * 3 + presses[1];
      }

      if (xCurrent > this.prize.x || yCurrent > this.prize.y) {
        continue;
      }

      const key = `${xCurrent},${yCurrent}`;
      if (visited.has(key)) continue;
      visited.add(key);

      [this.buttonA, this.buttonB]
        .filter((_, i) => presses[i] < 100)
        .forEach((button, i) => {
          const { x, y } = button.press(xCurrent, yCurrent);
          const newPresses = [...presses];
          newPresses[i]++;
          order.push({ xCurrent: x, yCurrent: y, presses: newPresses });
        });
    }

    return 0;
  }

  public pressButtonsBetter(): number {
    const determinant =
      this.buttonA.xPlus * this.buttonB.yPlus -
      this.buttonA.yPlus * this.buttonB.xPlus;
    const aPresses =
      (this.prize.x * this.buttonB.yPlus - this.prize.y * this.buttonB.xPlus) /
      determinant;
    const bPresses =
      (this.prize.y * this.buttonA.xPlus - this.prize.x * this.buttonA.yPlus) /
      determinant;

    if (Number.isInteger(aPresses) && Number.isInteger(bPresses)) {
      return aPresses * 3 + bPresses;
    }

    return 0;
  }
}
