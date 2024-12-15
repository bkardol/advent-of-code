import { Cell } from "@aoc/common";
import { Direction } from "./Direction";

export enum Type {
  Wall = "#",
  Empty = ".",
  Box = "O",
  Robot = "@",
  BoxLeft = "[",
  BoxRight = "]",
}

export class Location extends Cell<Location, Type> {
  private readonly moveMap: Record<
    Direction,
    (location: Location) => Location | undefined
  > = {
    [Direction.Up]: (location) => location.top,
    [Direction.Down]: (location) => location.bottom,
    [Direction.Left]: (location) => location.left,
    [Direction.Right]: (location) => location.right,
  };

  public moveRobot(direction: Direction): Location {
    if (this.value !== Type.Robot) {
      throw new Error("Cannot move a non-robot cell.");
    }

    let nextCellFn = this.moveMap[direction];
    let adjacentCell = nextCellFn(this);

    switch (adjacentCell?.value) {
      case Type.Empty:
        this.value = Type.Empty;
        adjacentCell.value = Type.Robot;
        return adjacentCell;
      case Type.Box:
        let passedWall = false;
        const adjacentEmptyCell = this.getInDirection(nextCellFn, (cell) => {
          passedWall = passedWall || cell.value === Type.Wall;
          return cell.value === Type.Empty;
        });
        if (!passedWall && adjacentEmptyCell) {
          adjacentEmptyCell.value = Type.Box;
          adjacentCell.value = Type.Robot;
          this.value = Type.Empty;
          return adjacentCell;
        }
        break;
      case Type.BoxLeft:
      case Type.BoxRight:
        const isHorizontal = [Direction.Left, Direction.Right].includes(
          direction
        );
        if (
          (isHorizontal && adjacentCell.moveBoxHorizontal(direction)) ||
          (!isHorizontal && adjacentCell.moveBoxVertical(direction, false))
        ) {
          if (!isHorizontal) {
            adjacentCell.moveBoxVertical(direction, true);
          }
          this.value = Type.Empty;
          adjacentCell.value = Type.Robot;
          return adjacentCell;
        }
    }

    return this;
  }

  private moveBoxHorizontal(direction: Direction) {
    const nextCell = this.moveMap[direction](this);
    if (!nextCell || nextCell.value === Type.Wall) {
      return false;
    }

    if (
      nextCell.value === Type.Empty ||
      ([Type.BoxLeft, Type.BoxRight].includes(nextCell.value) &&
        nextCell.moveBoxHorizontal(direction))
    ) {
      nextCell.value = this.value;
      this.value = Type.Empty;
      return true;
    }

    return false;
  }

  private moveBoxVertical(direction: Direction, moveBoxes: boolean): boolean {
    const boxLeft = this.value === Type.BoxLeft ? this : this.left;
    const boxRight = this.value === Type.BoxRight ? this : this.right;

    if (!boxLeft || !boxRight) {
      throw new Error("Box is not properly placed.");
    }

    const nextCellFn = this.moveMap[direction];
    const nextLeft = nextCellFn(boxLeft);
    const nextRight = nextCellFn(boxRight);

    if (
      !nextLeft ||
      !nextRight ||
      nextLeft.value === Type.Wall ||
      nextRight.value === Type.Wall
    ) {
      return false;
    }

    if (
      (nextLeft.value === Type.Empty ||
        nextLeft.moveBoxVertical(direction, moveBoxes)) &&
      (nextRight.value === Type.Empty ||
        nextRight.moveBoxVertical(direction, moveBoxes))
    ) {
      if (moveBoxes) {
        boxLeft.value = boxRight.value = Type.Empty;
        nextLeft.value = Type.BoxLeft;
        nextRight.value = Type.BoxRight;
      }
      return true;
    }

    return false;
  }
}
