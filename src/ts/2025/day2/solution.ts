type Range = {
  first: number;
  last: number;
};

export class Solution {
  public part1(input: string): number {
    const ranges: Range[] = input.split(",").map((line) => {
      const [first, last] = line.split("-").map(Number);
      return { first, last };
    });

    const rangeCache: Map<Range, number[]> = new Map();

    for (const range of ranges) {
      const rangeInvalidIds: number[] = [];
      for (let id = range.first; id <= range.last; id++) {
        const idString = id.toString();
        if (idString.length % 2 !== 0) {
          continue;
        }

        const matchingRange = rangeCache
          .entries()
          .find(([r]) => r.first === range.first && r.last <= range.last);
        if (matchingRange) {
          rangeInvalidIds.push(...matchingRange[1]);
          id = matchingRange[0].last;
          continue;
        }

        const idPartLength = idString.length / 2;
        const firstHalf = idString.slice(0, idPartLength);
        const secondHalf = idString.slice(idPartLength);
        if (firstHalf === secondHalf) {
          rangeInvalidIds.push(id);
        }
      }
      rangeCache.set(range, rangeInvalidIds);
    }

    return rangeCache
      .values()
      .reduce((prev, curr) => [...prev, ...curr], [])
      .reduce((prev, curr) => prev + curr);
  }

  public part2(input: string): number {
    const ranges: Range[] = input.split(",").map((line) => {
      const [first, last] = line.split("-").map(Number);
      return { first, last };
    });

    const rangeCache: Map<Range, number[]> = new Map();

    for (const range of ranges) {
      const rangeInvalidIds: number[] = [];
      for (let id = range.first; id <= range.last; id++) {
        const idString = id.toString();

        const matchingRange = rangeCache
          .entries()
          .find(([r]) => r.first === range.first && r.last <= range.last);
        if (matchingRange) {
          rangeInvalidIds.push(...matchingRange[1]);
          id = matchingRange[0].last;
        }

        // Check if number is a repeating pattern
        const idLength = idString.length;
        for (
          let patternLength = 1;
          patternLength <= idLength / 2;
          patternLength++
        ) {
          if (idLength % patternLength !== 0) {
            continue;
          }
          const pattern = idString.slice(0, patternLength);
          let isRepeating = true;
          for (let pos = patternLength; pos < idLength; pos += patternLength) {
            if (idString.slice(pos, pos + patternLength) !== pattern) {
              isRepeating = false;
              break;
            }
          }
          if (isRepeating) {
            rangeInvalidIds.push(id);
            break;
          }
        }
      }
      rangeCache.set(range, rangeInvalidIds);
    }

    return rangeCache
      .values()
      .reduce((prev, curr) => [...prev, ...curr], [])
      .reduce((prev, curr) => prev + curr);
  }
}
