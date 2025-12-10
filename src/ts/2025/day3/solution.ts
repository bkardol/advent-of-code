import { asStringArray } from "../../common";

export class Solution {
  public part1(input: string): number {
    const batteryBanks = asStringArray(input).map((battery) =>
      battery.split("").map(Number)
    );
    let totalJoltage = 0;
    for (const bank of batteryBanks) {
      const { digit: maxDigit, index: indexOfMaxDigit } =
        this.highestDigitForBank(bank, 0, 1);
      const { digit: maxRightDigit } = this.highestDigitForBank(
        bank,
        indexOfMaxDigit + 1,
        0
      );
      const joltage = `${maxDigit}${maxRightDigit}`;
      totalJoltage += Number(joltage);
    }
    return totalJoltage;
  }

  public part2(input: string): number {
    const batteryBanks = asStringArray(input).map((battery) =>
      battery.split("").map(Number)
    );
    let totalJoltage = 0;
    for (const bank of batteryBanks) {
      let bankJoltage = "";
      let fromIndex = 0;
      for (let i = 11; i >= 0; i--) {
        const { digit: maxDigit, index: indexOfMaxDigit } =
          this.highestDigitForBank(bank, fromIndex, i);
        bankJoltage += maxDigit.toString();
        fromIndex = indexOfMaxDigit + 1;
      }
      totalJoltage += Number(bankJoltage);
    }
    return totalJoltage;
  }

  private highestDigitForBank(
    bank: number[],
    fromIndex: number,
    remainingLength: number
  ): { digit: number; index: number } {
    const bankPart =
      remainingLength === 0
        ? bank.slice(fromIndex)
        : bank.slice(fromIndex, -remainingLength);
    const maxDigit = Math.max(...bankPart);
    const indexOfMaxDigit = bankPart.indexOf(maxDigit) + fromIndex;
    return { digit: maxDigit, index: indexOfMaxDigit };
  }
}
