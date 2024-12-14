import { Cell } from "./cell";

export const asMatrix = <TCell extends Cell<TCell, TValue>, TValue>(
  rows: string[],
  ctor: (value: string, position: { x: number; y: number }) => TCell,
  horizontalRepeat = false,
  verticalRepeat = false
) => {
  const matrix = rows.map((row, y) =>
    [...row].map((value, x) => {
      const cell = ctor(value, { x, y }) as TCell;
      cell.x = x;
      cell.y = y;
      return cell;
    })
  );
  linkMatrix(matrix, horizontalRepeat, verticalRepeat);
  return matrix;
};

export const findInMatrix = <TCell extends Cell<TCell, TValue>, TValue>(
  matrix: TCell[][],
  predicate: (cell: TCell) => boolean
): TCell | undefined => {
  for (const row of matrix) {
    for (const cell of row) {
      if (predicate(cell)) {
        return cell;
      }
    }
  }
};

export const filterInMatrix = <TCell extends Cell<TCell, TValue>, TValue>(
  matrix: TCell[][],
  predicate: (cell: TCell) => boolean
): TCell[] => {
  const result: TCell[] = [];
  for (const row of matrix) {
    for (const cell of row) {
      if (predicate(cell)) {
        result.push(cell);
      }
    }
  }
  return result;
};

export const countInMatrix = <TCell extends Cell<TCell, TValue>, TValue>(
  matrix: TCell[][],
  predicate: (cell: TCell) => number
): number => {
  let count = 0;
  for (const row of matrix) {
    for (const cell of row) {
      const value = predicate(cell);
      count += value;
    }
  }
  return count;
};

export const linkMatrix = <T extends Cell<T, TValue>, TValue>(
  matrix: T[][],
  horizontalRepeat = false,
  verticalRepeat = false
) => {
  for (let y = 0; y < matrix.length; y++) {
    const row = matrix[y];
    for (let x = 0; x < row.length; x++) {
      if (x > 0) {
        matrix[y][x].setLeft(row[x - 1]);
      }
      if (y > 0) {
        matrix[y][x].setTop(matrix[y - 1][x]);
      }
      if (horizontalRepeat && x == row.length - 1) {
        matrix[y][0].setLeft(row[x]);
      }
      if (verticalRepeat && y == matrix.length - 1) {
        matrix[0][x].setTop(row[x]);
      }
    }
  }
};
