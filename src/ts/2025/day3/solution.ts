import { asStringArray } from "../../common";

export class Solution {
  public part1(input: string): number {
    const batteryBanks = asStringArray(input).map((battery) =>
      battery.split("").map(Number)
    );
    let totalJoltage = 0;
    for (const bank of batteryBanks) {
      totalJoltage += this.getJoltageOutputForBank(bank, 2);
    }
    return totalJoltage;
  }

  public part2(input: string): number {
    const batteryBanks = asStringArray(input).map((battery) =>
      battery.split("").map(Number)
    );
    let totalJoltage = 0;
    for (const bank of batteryBanks) {
      totalJoltage += this.getJoltageOutputForBank(bank, 12);
    }
    return totalJoltage;
  }

  private getJoltageOutputForBank(
    bank: number[],
    joltageLength: number
  ): number {
    let bankJoltage = "";
    let fromIndex = 0;
    for (let i = joltageLength - 1; i >= 0; i--) {
      const { digit: maxDigit, index: indexOfMaxDigit } =
        this.highestDigitForBank(bank, fromIndex, i);
      bankJoltage += maxDigit.toString();
      fromIndex = indexOfMaxDigit + 1;
    }
    return Number(bankJoltage);
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
