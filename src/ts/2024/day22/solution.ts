import { asNumberArray, asStringArray } from "@aoc/common";

export class Solution {
  private readonly PRUNE_NUMBER = 16777216n;

  public part1(input: string): number {
    const secretNumbers = asStringArray(input).map(Number);

    const newSecretNumbers = secretNumbers.map((secretNumber) => {
      for (let i = 0; i < 2000; i++) {
        secretNumber = this.performSecretNumberSequence(secretNumber);
      }
      return secretNumber;
    });

    return newSecretNumbers.reduce((acc, n) => {
      return acc + n;
    }, 0);
  }

  public part2(input: string): number {
    const secretNumbers = asStringArray(input).map(Number);

    const buyerPrices = secretNumbers.map((secretNumber) => {
      const prices: number[] = [secretNumber % 10];
      for (let i = 0; i < 2000; i++) {
        const newSecretNumber = this.performSecretNumberSequence(secretNumber);
        prices.push(newSecretNumber % 10);
        secretNumber = newSecretNumber;
      }
      return prices;
    });

    const buyerDeltas = buyerPrices.map((prices) =>
      prices.map((price, i) => (i > 0 ? price - prices[i - 1] : 0))
    );

    let maxBananas = 0;
    const seenSequence = new Set<string>();
    for (let buyer = 0; buyer < buyerDeltas.length; buyer++) {
      console.log(buyer, "of", buyerDeltas.length);
      for (let i = 1; i < 2000 - 3; i++) {
        let bananaCount = buyerPrices[buyer][i + 3];
        const firstDelta = buyerDeltas[buyer][i];
        const secondDelta = buyerDeltas[buyer][i + 1];
        const thirdDelta = buyerDeltas[buyer][i + 2];
        const fourthDelta = buyerDeltas[buyer][i + 3];

        const sequence = `${firstDelta}${secondDelta}${thirdDelta}${fourthDelta}`;
        if (seenSequence.has(sequence)) {
          continue;
        }
        seenSequence.add(sequence);

        for (let j = buyer + 1; j < buyerDeltas.length; j++) {
          const deltas = buyerDeltas[j];
          const prices = buyerPrices[j];
          let matchingPrice = 0;
          const matchingDeltas = deltas.find((d, index) => {
            if (
              d === firstDelta &&
              deltas[index + 1] === secondDelta &&
              deltas[index + 2] === thirdDelta &&
              deltas[index + 3] === fourthDelta
            ) {
              matchingPrice = prices[index + 3];
              return true;
            }
          });
          if (matchingDeltas) {
            bananaCount += matchingPrice;
          }
        }

        maxBananas = Math.max(maxBananas, bananaCount);
      }
    }

    return maxBananas;
  }

  private performSecretNumberSequence(secretNumber: number): number {
    let newSecretNumber = BigInt(secretNumber);
    newSecretNumber =
      (BigInt(newSecretNumber * 64n) ^ newSecretNumber) % this.PRUNE_NUMBER;
    newSecretNumber =
      (BigInt(Math.round(Number(newSecretNumber / 32n))) ^ newSecretNumber) %
      this.PRUNE_NUMBER;
    newSecretNumber =
      ((newSecretNumber * 2048n) ^ newSecretNumber) % this.PRUNE_NUMBER;
    return Number(newSecretNumber);
  }
}
