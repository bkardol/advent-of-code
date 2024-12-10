export class Cell<TCell extends Cell<TCell, TValue>, TValue> {
  public value!: TValue;

  public x!: number;
  public y!: number;

  public left?: TCell;
  public right?: TCell;
  public top?: TCell;
  public bottom?: TCell;

  public topLeft?: TCell;
  public topRight?: TCell;
  public bottomLeft?: TCell;
  public bottomRight?: TCell;

  constructor(value: TValue) {
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

  public getAdjacent(
    includeDiagonal: boolean,
    condition?: (cell: TCell) => boolean
  ): TCell[] {
    return [
      this.left,
      this.right,
      this.top,
      this.bottom,
      ...(includeDiagonal
        ? [this.topLeft, this.topRight, this.bottomLeft, this.bottomRight]
        : []),
    ].filter(
      (cell): cell is TCell => !!cell && (!condition || condition(cell))
    );
  }
}
