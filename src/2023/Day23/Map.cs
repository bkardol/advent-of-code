namespace Day23
{
    internal class Map
    {
        private readonly Tile[][] tiles;

        public Map(Tile[][] tiles, bool onlyDownhill)
        {
            foreach (var tile in tiles.SelectMany(t => t))
            {
                tile.Prepare(onlyDownhill);
            }
            this.tiles = tiles;
        }

        public int GetTouristicRoute()
        {
            var start = tiles[0][1];
            var end = tiles[^1][tiles[0].Length - 2];
            return GetTouristicRoute([], start, end);
        }

        private static int GetTouristicRoute(HashSet<Tile> path, Tile current, Tile end)
        {
            if (current == end)
            {
                return path.Count;
            }

            var routeLength = 0;
            foreach (var next in current.Walk())
            {
                if (!path.Add(next))
                {
                    continue;
                }

                routeLength = Math.Max(routeLength, GetTouristicRoute(path, next, end));
                path.Remove(next);
            }
            return routeLength;
        }
    }
}
