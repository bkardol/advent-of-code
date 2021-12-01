using System.IO;
using System.Reflection;

namespace InputService
{
    public static class InputService
    {
        public static string[] GetExample() => GetFileLines("example");

        public static string[] GetInput() => GetFileLines("input");

        private static string[] GetFileLines(string fileName) => File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{fileName}.aoc"));
    }
}
