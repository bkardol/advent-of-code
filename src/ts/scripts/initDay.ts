import fs from "fs";
import path from "path";

function initDay(year: string, day: string): void {
  function copyDirectory(src: string, dest: string): void {
    if (!fs.existsSync(dest)) {
      fs.mkdirSync(dest, { recursive: true });
    }

    const entries = fs.readdirSync(src, { withFileTypes: true });

    for (const entry of entries) {
      const srcPath = path.join(src, entry.name);
      const destPath = path.join(dest, entry.name);

      if (entry.isDirectory()) {
        copyDirectory(srcPath, destPath);
      } else if (entry.isFile()) {
        fs.copyFileSync(srcPath, destPath);
      }
    }
  }

  function setDayInFiles(dir: string): void {
    const entries = fs.readdirSync(dir, { withFileTypes: true });

    for (const entry of entries) {
      const filePath = path.join(dir, entry.name);

      if (entry.isDirectory()) {
        setDayInFiles(filePath);
      } else if (entry.isFile()) {
        let content = fs.readFileSync(filePath, "utf-8");
        content = content.replace(/{{day}}/g, day);
        fs.writeFileSync(filePath, content, "utf-8");
      }
    }
  }

  try {
    copyDirectory("_template", `${year}/day${day}`);
    setDayInFiles(`${year}/day${day}`);
  } catch (error) {
    console.error(`Error: ${error}`);
  }
}

// Command-line arguments
const [year, day] = process.argv.slice(2);

if (!year || !day) {
  console.error("Usage: pnpm add:day <year> <day>");
  process.exit(1);
}

initDay(year, day);
