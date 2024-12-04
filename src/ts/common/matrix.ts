export const asMatrix = <
  T extends Cell<T>,
  Y extends new (value: string) => InstanceType<Y>,
>(
  rows: string[],
  ctor: Y
) => {
  const matrix = rows.map((row) =>
    [...row].map((value) => new ctor(value) as T)
  );
  linkMatrix(matrix);
  return matrix;
};

export class Cell<TCell extends Cell<TCell>> {
  public value!: string;

  public left?: TCell;
  public right?: TCell;
  public top?: TCell;
  public bottom?: TCell;

  public topLeft?: TCell;
  public topRight?: TCell;
  public bottomLeft?: TCell;
  public bottomRight?: TCell;

  constructor(value: string) {
    this.value = value;
  }

  public setLeft(left: TCell) {
    this.left = left;
    left.right = this as unknown as TCell;
  }

  public setTop(top: TCell) {
    this.top = top;
    top.bottom = this as unknown as TCell;

    this.topLeft = top.left;
    this.topRight = top.right;

    if (this.topLeft) {
      this.topLeft.bottomRight = this as unknown as TCell;
    }

    if (this.topRight) {
      this.topRight.bottomLeft = this as unknown as TCell;
    }
  }

  public getInDirection(
    direction: (TCell: TCell) => TCell | undefined
  ): TCell | undefined {
    return direction(this as unknown as TCell);
  }
}

function linkMatrix<T extends Cell<T>>(matrix: T[][]) {
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
