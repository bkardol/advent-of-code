export class Solution {
  public part1(input: string): number {
    const stones = input.split(" ").map(Number);
    const blinkCount = 25;

    const cache = new Map<string, number>();
    return stones.reduce(
      (acc, stone) =>
        acc + this.calculateAmountOfStones(stone, blinkCount, cache),
      0
    );
  }

  public part2(input: string): number {
    const stones = input.split(" ").map(Number);
    const blinkCount = 75;

    const cache = new Map<string, number>();
    return stones.reduce(
      (acc, stone) =>
        acc + this.calculateAmountOfStones(stone, blinkCount, cache),
      0
    );
  }

  private calculateAmountOfStones(
    stone: number,
    i: number,
    cache: Map<string, number>
  ): number {
    if (i === 0) {
      return 1;
    }

    const cacheKey = `${stone}-${i}`;
    const cachedValue = cache.get(cacheKey);
    if (cachedValue) {
      return cachedValue;
    }

    if (stone === 0) {
      const value = this.calculateAmountOfStones(1, i - 1, cache);
      cache.set(cacheKey, value);
      return value;
    }

    const stoneString = stone.toString();
    if (stoneString.length % 2 === 0) {
      const firstStone = Number(
        stoneString.slice(0, stoneString.length / 2).replace(/^0+/, "")
      );
      let secondStone = Number(
        stoneString.slice(stoneString.length / 2).replace(/^0+/, "")
      );
      const value =
        this.calculateAmountOfStones(firstStone, i - 1, cache) +
        this.calculateAmountOfStones(secondStone, i - 1, cache);
      cache.set(cacheKey, value);
      return value;
    }

    const value = this.calculateAmountOfStones(stone * 2024, i - 1, cache);
    cache.set(cacheKey, value);
    return value;
  }
}
