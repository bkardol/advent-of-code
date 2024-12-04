import { Cell } from "./cell";

export const asMatrix = <TCell extends Cell<TCell, TValue>, TValue>(
  rows: string[],
  ctor: (value: string) => TCell
) => {
  const matrix = rows.map((row) =>
    [...row].map((value) => ctor(value) as TCell)
  );
  linkMatrix(matrix);
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

function linkMatrix<T extends Cell<T, TValue>, TValue>(matrix: T[][]) {
  for (let y = 0; y < matrix.length; y++) {
    const row = matrix[y];
    for (let x = 0; x < matrix.length; x++) {
      if (x > 0) {
        matrix[y][x].setLeft(row[x - 1]);
      }
      if (y > 0) {
        matrix[y][x].setTop(matrix[y - 1][x]);
      }
    }
  }
}
