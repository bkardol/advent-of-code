import { asMatrix, asStringArray, Cell } from "@aoc/common";

export class Tile extends Cell<Tile, string> {}

export class Robot {
  public tile!: Tile;

  public constructor(
    public readonly position: { x: number; y: number },
    public readonly velocity: { x: number; y: number }
  ) {}
}

export class Solution {
  public part1(input: string): number {
    const robots = this.asRobots(input);

    const maxX = this.getMax(robots, "x") + 1;
    const maxY = this.getMax(robots, "y") + 1;
    const halfX = (maxX - 1) / 2;
    const halfY = (maxY - 1) / 2;

    const quadrants = [
      {
        minX: -1,
        minY: -1,
        maxX: halfX,
        maxY: halfY,
        robots: 0,
      },
      { minX: halfX, minY: -1, maxX, maxY: halfY, robots: 0 },
      { minX: -1, minY: halfY, maxX: halfX, maxY, robots: 0 },
      {
        minX: halfX,
        minY: halfY,
        maxX: maxX,
        maxY,
        robots: 0,
      },
    ];

    robots.forEach((robot) => {
      const { x, y } = this.moveRobot(robot, 100, maxX, maxY);

      const quadrant = quadrants.find(
        (q) => x > q.minX && y > q.minY && x < q.maxX && y < q.maxY
      );

      if (quadrant) {
        quadrant.robots++;
      }
    });

    return quadrants.reduce((acc, quadrant) => acc * quadrant.robots, 1);
  }

  public part2(input: string): number {
    const robots = this.asRobots(input);

    const maxX = this.getMax(robots, "x") + 1;
    const maxY = this.getMax(robots, "y") + 1;

    for (let i = 0; i < 9999; i += 1) {
      const coords = robots.map((robot) =>
        this.moveRobot(robot, i, maxX, maxY)
      );

      for (const coord of coords.filter((c) => c.x === 70)) {
        const possibleChristmasTree = Array.from({ length: 20 }).every((_, j) =>
          coords.some((c) => c.x === coord.x && c.y === coord.y + j)
        );

        if (possibleChristmasTree) {
          return i;
        }
      }
    }

    throw new Error("No Christmas tree found");
  }

  private moveRobot(
    robot: Robot,
    iterations: number,
    maxX: number,
    maxY: number
  ) {
    let newX = (robot.position.x + iterations * robot.velocity.x) % maxX;
    let newY = (robot.position.y + iterations * robot.velocity.y) % maxY;
    if (newX < 0) {
      newX = maxX + newX;
    }
    if (newY < 0) {
      newY = maxY + newY;
    }

    return { x: newX, y: newY };
  }

  asRobots(input: string) {
    return asStringArray(input).map((line) => {
      const [pos, vel] = line.split(" ");
      const [posX, posY] = pos.slice(2).split(",");
      const [velX, velY] = vel.slice(2).split(",");
      return new Robot(
        { x: Number(posX), y: Number(posY) },
        { x: Number(velX), y: Number(velY) }
      );
    });
  }

  getMax(robots: Robot[], axis: "x" | "y") {
    return robots.reduce(
      (max, robot) => Math.max(max, robot.position[axis]),
      0
    );
  }
}
