import { pathsToModuleNameMapper } from "ts-jest";
import { compilerOptions } from "./tsconfig";
import { defineConfig } from "jest";

export default defineConfig({
  testEnvironment: "node",
  transform: {
    "^.+.spec.ts?$": ["ts-jest", {}],
  },
  preset: "ts-jest",
  moduleNameMapper: pathsToModuleNameMapper(compilerOptions.paths, {
    prefix: "<rootDir>/",
  }),
});
