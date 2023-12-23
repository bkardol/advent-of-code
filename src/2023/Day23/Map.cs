namespace Day23
{
    internal class Map
    {
        private readonly Tile[][] tiles;

        public Map(Tile[][] tiles, bool onlyDownhill)
        {
            var flatTiles = tiles.SelectMany(t => t).ToArray();
            tiles[0][1].IsFirst = true;
            foreach (var tile in flatTiles)
            {
                tile.SetWalkableAdjacent(onlyDownhill);
            }
            foreach (var tile in flatTiles)
            {
                tile.SetWalkableAdjacentCrossroads();
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

            // ".Jump()" can be replaced with ".Walk()", but will go way slower :)
            foreach (var next in current.Jump())
            {
                if (!path.Add(next.Key))
                {
                    continue;
                }
                foreach (var tile in next.Value)
                {
                    path.Add(tile);
                }

                routeLength = Math.Max(routeLength, GetTouristicRoute(path, next.Key, end));
                foreach (var tile in next.Value)
                {
                    path.Remove(tile);
                }
            }
            return routeLength;
        }
    }
}
