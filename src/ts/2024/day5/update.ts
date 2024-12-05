import { Rule } from "./rule";

export class Update {
  constructor(private readonly pages: number[]) {}

  public get middleNumber(): number {
    return this.pages[(this.pages.length - 1) / 2];
  }

  isCorrectlyOrdered(rules: Rule[]): boolean {
    return this.processRules(rules, (pageIndex, relatedPageIndex) => false);
  }

  reorder(rules: Rule[]) {
    while (!this.isCorrectlyOrdered(rules)) {
      this.processRules(rules, (pageIndex, relatedPageIndex) => {
        this.swap(pageIndex, relatedPageIndex);
        return true;
      });
    }
  }

  /**
   * Process the rules and call the onInvalidOrder function when a rule's order is not met
   * @param rules The rules to process
   * @param onInvalidOrder The action to perform on an invalid order, should return true if the processing should be continued, false otherwise
   * @returns true if the rules are correctly ordered, false otherwise
   */
  private processRules(
    rules: Rule[],
    onInvalidOrder: (pageIndex: number, relatedPageIndex: number) => boolean
  ) {
    for (let pageIndex = 0; pageIndex < this.pages.length - 1; pageIndex++) {
      const pagesBefore = pageIndex > 0 ? this.pages.slice(0, pageIndex) : [];
      const page = this.pages[pageIndex];
      const pagesAfter =
        pageIndex < this.pages.length - 1
          ? this.pages.slice(pageIndex + 1)
          : [];

      const beforeRules = rules.filter((rule) => rule.before === page);
      for (const beforeRule of beforeRules) {
        if (pagesBefore.includes(beforeRule.after)) {
          if (
            !onInvalidOrder(pageIndex, this.pages.indexOf(beforeRule.after))
          ) {
            return false;
          }
        }
      }

      const afterRules = rules.filter((rule) => rule.after === page);
      for (const afterRule of afterRules) {
        if (pagesAfter.includes(afterRule.before)) {
          if (
            !onInvalidOrder(pageIndex, this.pages.indexOf(afterRule.before))
          ) {
            return false;
          }
        }
      }
    }
    return true;
  }

  private swap(index1: number, index2: number) {
    const temp = this.pages[index1];
    this.pages[index1] = this.pages[index2];
    this.pages[index2] = temp;
  }
}
