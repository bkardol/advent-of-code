import { Cell, asMatrix, asStringArray, countInMatrix } from "@aoc/common";

class Character extends Cell<Character, string> {}

export class Solution {
  public part1(input: string): number {
    const matrix = this.asXmasMatrix(input);
    const xmasFound = countInMatrix(matrix, (cell) => {
      if (cell.value !== "X") {
        return 0;
      }
      return [
        (c: Character) => c.right,
        (c: Character) => c.left,
        (c: Character) => c.top,
        (c: Character) => c.bottom,
        (c: Character) => c.topRight,
        (c: Character) => c.topLeft,
        (c: Character) => c.bottomRight,
        (c: Character) => c.bottomLeft,
      ].reduce(
        (acc, curr) => acc + (this.hasMasInDirection(cell, curr) ? 1 : 0),
        0
      );
    });
    return xmasFound;
  }

  public part2(input: string): number {
    const matrix = this.asXmasMatrix(input);
    const xmasFound = countInMatrix(matrix, (cell) => {
      if (
        cell.value === "A" &&
        this.hasMsDiagonally(cell.topLeft, cell.bottomRight) &&
        this.hasMsDiagonally(cell.topRight, cell.bottomLeft)
      ) {
        return 1;
      }
      return 0;
    });
    return xmasFound;
  }

  private asXmasMatrix(input: string) {
    return asMatrix(asStringArray(input), (value) => new Character(value));
  }

  private hasMasInDirection(
    cell: Character,
    direction: (cell: Character) => Character | undefined
  ) {
    return (
      cell.getInDirection(direction)?.value === "M" &&
      cell.getInDirection(direction)?.getInDirection(direction)?.value ===
        "A" &&
      cell
        .getInDirection(direction)
        ?.getInDirection(direction)
        ?.getInDirection(direction)?.value === "S"
    );
  }

  private hasMsDiagonally(
    diagonalCell: Character | undefined,
    oppositeDiagonalCell: Character | undefined
  ) {
    return (
      diagonalCell &&
      ["M", "S"].includes(diagonalCell.value) &&
      oppositeDiagonalCell &&
      ["M", "S"].includes(oppositeDiagonalCell.value) &&
      oppositeDiagonalCell.value !== diagonalCell.value
    );
  }
}
