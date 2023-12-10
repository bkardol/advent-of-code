namespace Day10
{
    using System;
    using System.Linq;
    using Common.Matrix;

    internal class Pipe : Cell<Pipe, PipeType>
    {
        public void UpdateValue(PipeType newType) => this.Value = newType;

        internal void FillToBottomRight(PipeType newType)
        {
            UpdateValue(newType);
            var adjacent = new[] { this.Bottom, this.Right }.Where(p => p != null && p.Value == PipeType.Unknown);
            foreach (var adjacentItem in adjacent)
            {
                adjacentItem.UpdateValue(newType);
                adjacentItem.FillToBottomRight(newType);
            }
        }

        internal void FillToBottomLeft(PipeType newType)
        {
            UpdateValue(newType);
            var adjacent = new[] { this.Bottom, this.Left }.Where(p => p != null && p.Value == PipeType.Unknown);
            foreach (var adjacentItem in adjacent)
            {
                adjacentItem.UpdateValue(newType);
                adjacentItem.FillToBottomLeft(newType);
            }
        }

        internal void FillToTopLeft(PipeType newType)
        {
            UpdateValue(newType);
            var adjacent = new[] { this.Top, this.Left }.Where(p => p != null && p.Value == PipeType.Unknown);
            foreach (var adjacentItem in adjacent)
            {
                adjacentItem.UpdateValue(newType);
                adjacentItem.FillToTopLeft(newType);
            }
        }

        internal void FillToTopRight(PipeType newType)
        {
            UpdateValue(newType);
            var adjacent = new[] { this.Top, this.Right }.Where(p => p != null && p.Value == PipeType.Unknown);
            foreach (var adjacentItem in adjacent)
            {
                adjacentItem.UpdateValue(newType);
                adjacentItem.FillToTopRight(newType);
            }
        }
    }
}
