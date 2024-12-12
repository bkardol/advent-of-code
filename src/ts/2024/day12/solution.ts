import { asMatrix, asStringArray } from "@aoc/common";
import { Plot, Region } from "./Region";

export class Solution {
  public part1(input: string): number {
    return this.calculateFencePrice(
      input,
      (region) => region.fencePriceOfPerimeter
    );
  }

  public part2(input: string): number {
    return this.calculateFencePrice(
      input,
      (region) => region.fencePriceOfSides
    );
  }

  private calculateFencePrice(
    input: string,
    fencePrice: (region: Region) => number
  ) {
    let inputArray = asStringArray(input);
    inputArray.splice(0, 0, ".".repeat(inputArray[0].length));
    inputArray.splice(inputArray.length, 0, ".".repeat(inputArray[0].length));
    inputArray = inputArray.map((row) => "." + row + ".");

    const matrix = asMatrix(inputArray, (value) => new Plot(value));
    const regions: Region[] = [];
    for (let row of matrix) {
      for (let cell of row.filter((cell) => cell.value !== ".")) {
        if (regions.some((region) => region.includes(cell))) {
          continue;
        }
        const region = new Region();
        this.fillRegion(cell, region);
        regions.push(region);
      }
    }

    return regions.reduce((acc, region) => acc + fencePrice(region), 0);
  }

  private fillRegion(cell: Plot, region: Region) {
    if (region.includes(cell)) {
      return;
    }
    region.add(cell);

    cell
      .getAdjacent(false, (adjacent) => adjacent.value === cell.value)
      .forEach((adjacent) => this.fillRegion(adjacent, region));
  }
}
