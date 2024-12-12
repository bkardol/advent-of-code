import { Cell } from "@aoc/common";

export class Plot extends Cell<Plot, string> {}

export enum Direction {
  Top = "T",
  Bottom = "B",
  Left = "L",
  Right = "R",
}

export class Region {
  private readonly plots: Plot[] = [];
  private readonly sides: { plot: Plot; direction: Direction }[] = [];

  public get areaSize(): number {
    return this.plots.length;
  }

  public get perimeterSize(): number {
    return this.sides.length;
  }

  public get numberOfSides(): number {
    const handled: { plot: Plot; direction: Direction }[] = [];
    const sides = this.sides
      .sort((a, b) =>
        a.plot.y === b.plot.y ? a.plot.x - b.plot.x : a.plot.y - b.plot.y
      )
      .reduce((acc: Plot[], side) => {
        if (
          side.plot
            .getAdjacent(false)
            .every(
              (adjacent) =>
                !handled.some(
                  (h) => h.plot === adjacent && h.direction === side.direction
                )
            )
        ) {
          acc.push(side.plot);
        }
        handled.push(side);
        return acc;
      }, []);
    return sides.length;
  }

  public get fencePriceOfPerimeter(): number {
    return this.areaSize * this.perimeterSize;
  }

  public get fencePriceOfSides(): number {
    return this.areaSize * this.numberOfSides;
  }

  public add(plot: Plot) {
    this.plots.push(plot);
    this.sides.push(
      ...[
        { plot: plot.left, direction: Direction.Left },
        { plot: plot.right, direction: Direction.Right },
        { plot: plot.top, direction: Direction.Top },
        { plot: plot.bottom, direction: Direction.Bottom },
      ]
        .filter(
          (adjacent): adjacent is { plot: Plot; direction: Direction } =>
            !!adjacent.plot
        )
        .filter((adjacent) => adjacent.plot.value !== plot.value)
    );
  }

  public includes(plot: Plot) {
    return this.plots.includes(plot);
  }
}
