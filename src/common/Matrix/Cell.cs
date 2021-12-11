namespace Common.Matrix
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Cell
    {
        private Cell Left;
        private Cell Right;
        private Cell Top;
        private Cell Bottom;

        private Cell TopLeft;
        private Cell TopRight;
        private Cell BottomRight;
        private Cell BottomLeft;

        internal void SetLeft(Cell left)
        {
            Left = left;
            Left.Right = this;
        }
        internal void SetTop(Cell top, bool includeDiagonal)
        {
            Top = top;
            Top.Bottom = this;

            if (includeDiagonal)
            {
                TopLeft = top.Left;
                TopRight = top.Right;
                if (TopLeft != null)
                {
                    TopLeft.BottomRight = this;
                }

                if (TopRight != null)
                {
                    TopRight.BottomLeft = this;
                }
            }
        }

        protected IEnumerable<T> GetAdjacent<T>()
            where T : Cell =>
            new T[] { (T)Left, (T)Right, (T)Top, (T)Bottom, (T)TopLeft, (T)TopRight, (T)BottomLeft, (T)BottomRight }
            .Where(l => l != null);
    }

    public abstract class Cell<TCell, TValue> : Cell
        where TCell : Cell
    {
        public TValue Value { get; protected set; }

        internal void SetValue(TValue value) => Value = value;

        public IEnumerable<TCell> GetAdjacent() => GetAdjacent<TCell>();
    }
}
