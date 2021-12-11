namespace Common.Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Cell<TCell, TValue>
        where TCell : Cell<TCell, TValue>
    {
        public TValue Value { get; protected set; }

        public TCell Left { get; private set; }
        public TCell Right { get; private set; }
        public TCell Top { get; private set; }
        public TCell Bottom { get; private set; }

        public TCell TopLeft { get; private set; }
        public TCell TopRight { get; private set; }
        public TCell BottomRight { get; private set; }
        public TCell BottomLeft { get; private set; }

        public TCell PositionsLeft(int positions) => GetFromDirection(positions, cell => cell.Left);
        public TCell PositionsRight(int positions) => GetFromDirection(positions, cell => cell.Right);
        public TCell PositionsTop(int positions) => GetFromDirection(positions, cell => cell.Top);
        public TCell PositionsBottom(int positions) => GetFromDirection(positions, cell => cell.Bottom);

        internal void SetValue(TValue value) => Value = value;

        internal void SetLeft(TCell left)
        {
            Left = left;
            Left.Right = this as TCell;
        }

        internal void SetTop(TCell top, bool includeDiagonal)
        {
            Top = top;
            Top.Bottom = this as TCell;

            if (includeDiagonal)
            {
                TopLeft = top.Left;
                TopRight = top.Right;

                if (TopLeft != null)
                {
                    TopLeft.BottomRight = this as TCell;
                }

                if (TopRight != null)
                {
                    TopRight.BottomLeft = this as TCell;
                }
            }
        }

        protected IEnumerable<TCell> GetAdjacent() =>
            new TCell[] { Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight }
            .Where(l => l != null);

        private TCell GetFromDirection(int positions, Func<TCell, TCell> getInDirection)
        {
            var cell = this as TCell;
            while (--positions >= 0 && cell != null)
            {
                cell = getInDirection(cell);
            }
            return cell;
        }
    }
}
