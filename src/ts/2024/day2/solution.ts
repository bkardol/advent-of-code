import { asStringArray } from "@aoc/common";

export class Solution {
  public part1(input: string): number {
    const reports = this.asReports(input);

    const checked = reports.map(
      (report) => this.checkReportSafety(report).safe
    );

    return checked.filter((report) => !!report).length;
  }

  public part2(input: string): number {
    const reports = this.asReports(input);

    const checkedReports = reports.map((report) =>
      this.checkReportSafety(report)
    );

    const checked = checkedReports.map((checkedReport) => {
      if (checkedReport.safe) {
        return true;
      }

      // Run check again and again with one value out
      let safe = false;
      for (let i = 0; i < checkedReport.report.length; i++) {
        const newReport = checkedReport.report.filter((_, j) => j !== i);
        if (this.checkReportSafety(newReport).safe) {
          safe = true;
          break;
        }
      }

      return safe;
    });

    return checked.filter((report) => !!report).length;
  }

  private asReports(input: string) {
    return asStringArray(input).map((report) =>
      report.split(" ").map((level) => Number(level))
    );
  }

  private checkReportSafety(report: number[]) {
    let prev = -1;
    let safe = true;
    let increasing = false;

    const isGradual = (level: number) => {
      const diff = Math.abs(prev - level);
      return diff < 4 && diff > 0;
    };
    const allIncreasing = (level: number) => increasing && level > prev;
    const allDecreasing = (level: number) => !increasing && level < prev;

    for (const level of report) {
      if (prev === -1) {
        increasing = report[1] > report[0];
        prev = level;
        continue;
      }

      if (isGradual(level) && (allIncreasing(level) || allDecreasing(level))) {
        prev = level;
      } else {
        safe = false;
        break;
      }
    }
    return {
      safe,
      report,
    };
  }
}
