import { asStringArray } from "@aoc/common";
import { Rule } from "./rule";
import { Update } from "./update";

export class Solution {
  public part1(input: string): number {
    const { rules, updates } = this.asRulesAndUpdates(input);

    return updates
      .filter((update) => update.isCorrectlyOrdered(rules))
      .reduce((acc, update) => acc + update.middleNumber, 0);
  }

  public part2(input: string): number {
    const { rules, updates } = this.asRulesAndUpdates(input);

    const incorrectUpdates = updates.filter(
      (u) => !u.isCorrectlyOrdered(rules)
    );
    incorrectUpdates.forEach((update) => update.reorder(rules));
    return incorrectUpdates.reduce(
      (acc, update) => acc + update.middleNumber,
      0
    );
  }

  private asRulesAndUpdates(input: string) {
    const lines = asStringArray(input);
    const rules: Rule[] = [];
    const updates: Update[] = [];

    let processedRules = false;
    for (const line of lines) {
      if (!processedRules) {
        const [first, second] = line.split("|");
        rules.push(new Rule(parseInt(first), parseInt(second)));
        if (line === "") {
          processedRules = true;
        }
      } else {
        const numbers = line.split(",").map((n) => parseInt(n));
        updates.push(new Update(numbers));
      }
    }

    return { rules, updates };
  }
}
