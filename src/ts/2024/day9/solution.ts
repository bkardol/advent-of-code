interface File {
  id: number;
  size: number;
}

interface FreeSpace {
  size: number;
}

export class Solution {
  public part1(input: string): number {
    const disk = this.asDisk(input);

    const printed = this.printDisk(disk);
    for (let i = 0, j = printed.length - 1; i < j; i++) {
      if (Number.isNaN(printed[i])) {
        while (Number.isNaN(printed[j])) {
          j--;
        }
        printed[i] = printed[j];
        printed[j] = Number.NaN;
      }
    }

    return printed
      .filter((n) => !Number.isNaN(n))
      .reduce((acc, curr, i) => acc + curr * i, 0);
  }

  public part2(input: string): number {
    const disk = this.asDisk(input);

    const filesOnDisk = disk
      .filter((file): file is File => this.isFile(file))
      .reverse();
    for (const file of filesOnDisk) {
      for (let i = 0; i < disk.length; i++) {
        const freeSpace = disk[i];
        if (this.isFile(freeSpace) || freeSpace.size < file.size) {
          continue;
        }

        const removeIndex = disk.indexOf(file);
        if (removeIndex > i) {
          freeSpace.size -= file.size;

          disk.splice(removeIndex, 1, { size: file.size });
          disk.splice(i, 0, file);
        }
        break;
      }
    }

    return this.printDisk(disk).reduce(
      (acc, curr, i) => (Number.isNaN(curr) ? acc : acc + curr * i),
      0
    );
  }

  private asDisk(input: string): (File | FreeSpace)[] {
    const disk: (File | FreeSpace)[] = [];
    const inputArray = [...input];
    inputArray.forEach((character, i) => {
      if (i % 2 === 0) {
        disk.push({ id: i / 2, size: Number(character) });
      } else {
        disk.push({ size: Number(character) });
      }
    });
    return disk;
  }

  private isFile(some: File | FreeSpace): some is File {
    return "id" in some;
  }

  private printDisk(disk: (File | FreeSpace)[]): number[] {
    return disk.reduce((acc: number[], curr) => {
      for (let i = 0; i < curr.size; i++) {
        if ("id" in curr) {
          acc.push(curr.id);
        } else {
          acc.push(Number.NaN);
        }
      }
      return acc;
    }, []);
  }
}
