export const asStringArray = (input: string): string[] => input.split("\n");

export const asNumberArray = (input: string): number[] =>
  input.match(/\d/g)?.map(Number) ?? [];

export const asNumber = (input: string): number =>
  Number(asNumberArray(input).join(""));
