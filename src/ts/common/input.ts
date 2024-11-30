import * as fs from "fs";
import * as path from "path";

const getInputData = (directory: string, inputType: string) =>
  fs.readFileSync(path.resolve(directory, `./${inputType}.aoc`)).toString();

export const getExampleInput = (directory: string) =>
  getInputData(directory, "example");

export const getInput = (directory: string) => getInputData(directory, "input");
