namespace Common.Array
{
    using System.Linq;

    public static class CommonArray
    {
        public static T[] GetColumn<T>(T[][] source, int index) => source.Select(row => row[index]).ToArray();
    }
}
