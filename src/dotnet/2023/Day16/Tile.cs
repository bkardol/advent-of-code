namespace Day16
{
    using System.Collections.Generic;
    using Common.Matrix;

    internal class Tile : Cell<Tile, TileType>
    {
        public List<Direction> Energized { get; } = new List<Direction>();

        internal void ProcessBeam(Direction sourceDirection)
        {
            if (this.Energized.Contains(sourceDirection))
                return;

            this.Energized.Add(sourceDirection);

            switch (this.Value)
            {
                case TileType.EmptySpace:
                    MoveThroughEmptySpace(sourceDirection);
                    break;
                case TileType.ForwardMirror:
                    ReflectOnForwardMirror(sourceDirection);
                    break;
                case TileType.BackMirror:
                    ReflectOnBackMirror(sourceDirection);
                    break;
                case TileType.HorizontalSplitter:
                    HitHorizontalSplitter(sourceDirection);
                    break;
                case TileType.VerticalSplitter:
                    HitVerticalSplitter(sourceDirection);
                    break;
            }
        }

        internal void Clear() => this.Energized.Clear();

        private void MoveThroughEmptySpace(Direction sourceDirection)
        {
            switch(sourceDirection)
            {
                case Direction.Left:
                    Right?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Top:
                    Bottom?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Right:
                    Left?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Bottom:
                    Top?.ProcessBeam(sourceDirection);
                    break;
            }
        }

        private void ReflectOnForwardMirror(Direction sourceDirection)
        {
            switch (sourceDirection)
            {
                case Direction.Left:
                    Top?.ProcessBeam(Direction.Bottom);
                    break;
                case Direction.Top:
                    Left?.ProcessBeam(Direction.Right);
                    break;
                case Direction.Right:
                    Bottom?.ProcessBeam(Direction.Top);
                    break;
                case Direction.Bottom:
                    Right?.ProcessBeam(Direction.Left);
                    break;
            }
        }

        private void ReflectOnBackMirror(Direction sourceDirection)
        {
            switch (sourceDirection)
            {
                case Direction.Left:
                    Bottom?.ProcessBeam(Direction.Top);
                    break;
                case Direction.Top:
                    Right?.ProcessBeam(Direction.Left);
                    break;
                case Direction.Right:
                    Top?.ProcessBeam(Direction.Bottom);
                    break;
                case Direction.Bottom:
                    Left?.ProcessBeam(Direction.Right);
                    break;
            }
        }

        private void HitHorizontalSplitter(Direction sourceDirection)
        {
            switch (sourceDirection)
            {
                case Direction.Left:
                    Right?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Top:
                    Left?.ProcessBeam(Direction.Right);
                    Right?.ProcessBeam(Direction.Left);
                    break;
                case Direction.Right:
                    Left?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Bottom:
                    Left?.ProcessBeam(Direction.Right);
                    Right?.ProcessBeam(Direction.Left);
                    break;
            }
        }

        private void HitVerticalSplitter(Direction sourceDirection)
        {
            switch (sourceDirection)
            {
                case Direction.Left:
                    Top?.ProcessBeam(Direction.Bottom);
                    Bottom?.ProcessBeam(Direction.Top);
                    break;
                case Direction.Top:
                    Bottom?.ProcessBeam(sourceDirection);
                    break;
                case Direction.Right:
                    Top?.ProcessBeam(Direction.Bottom);
                    Bottom?.ProcessBeam(Direction.Top);
                    break;
                case Direction.Bottom:
                    Top?.ProcessBeam(sourceDirection);
                    break;
            }
        }
    }
}
