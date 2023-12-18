namespace Day17
{
    using System;
    using System.Collections.Generic;
    using Common.Matrix;

    internal class Node : Cell<Node, int>
    {
        public IDictionary<int, int> DirectionDistance { get; } = new Dictionary<int, int>();
        public char Character { get; set; } = 'O';

        public List<(Node, Direction, List<Node>, int, int)> VisitPart1(Direction sourceDirection, List<Node> path, int totalDistance, int currentAmountInDirection)
        {
            var nodeOptions = new List<(Node, Direction, List<Node>, int, int)>();
            Character = Value.ToString()[0];

            var addToOptions = (Node node, Direction sourceDirection, int amountInDirection) => AddToOptions(nodeOptions, node, sourceDirection, path, totalDistance, amountInDirection);

            switch (sourceDirection)
            {
                case Direction.Left:
                    addToOptions(Top, Direction.Bottom, 1);
                    addToOptions(Bottom, Direction.Top, 1);
                    if (currentAmountInDirection < 3)
                    {
                        addToOptions(Right, Direction.Left, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Right:
                    addToOptions(Top, Direction.Bottom, 1);
                    addToOptions(Bottom, Direction.Top, 1);
                    if (currentAmountInDirection < 3)
                    {
                        addToOptions(Left, Direction.Right, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Top:
                    addToOptions(Left, Direction.Right, 1);
                    addToOptions(Right, Direction.Left, 1);
                    if (currentAmountInDirection < 3)
                    {
                        addToOptions(Bottom, Direction.Top, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Bottom:
                    addToOptions(Left, Direction.Right, 1);
                    addToOptions(Right, Direction.Left, 1);
                    if (currentAmountInDirection < 3)
                    {
                        addToOptions(Top, Direction.Bottom, ++currentAmountInDirection);
                    }
                    break;
            }
            return nodeOptions;
        }

        public List<(Node, Direction, List<Node>, int, int)> VisitPart2(Direction sourceDirection, List<Node> path, int totalDistance, int currentAmountInDirection)
        {
            var nodeOptions = new List<(Node, Direction, List<Node>, int, int)>();
            Character = Value.ToString()[0];

            var addToOptions = (Node node, Direction sourceDirection, int amountInDirection) => AddToOptions(nodeOptions, node, sourceDirection, path, totalDistance, amountInDirection);

            switch (sourceDirection)
            {
                case Direction.Left:
                    if (currentAmountInDirection > 3)
                    {
                        addToOptions(Top, Direction.Bottom, 1);
                        addToOptions(Bottom, Direction.Top, 1);
                    }
                    else if (currentAmountInDirection < 10)
                    {
                        addToOptions(Right, Direction.Left, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Right:
                    if (currentAmountInDirection > 3)
                    {
                        addToOptions(Top, Direction.Bottom, 1);
                        addToOptions(Bottom, Direction.Top, 1);
                    }
                    else if (currentAmountInDirection < 10)
                    {
                        addToOptions(Left, Direction.Right, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Top:
                    if (currentAmountInDirection > 3)
                    {
                        addToOptions(Left, Direction.Right, 1);
                        addToOptions(Right, Direction.Left, 1);
                    }
                    else if (currentAmountInDirection < 10)
                    {
                        addToOptions(Bottom, Direction.Top, ++currentAmountInDirection);
                    }
                    break;
                case Direction.Bottom:
                    if (currentAmountInDirection > 3)
                    {
                        addToOptions(Left, Direction.Right, 1);
                        addToOptions(Right, Direction.Left, 1);
                    }
                    else if (currentAmountInDirection < 10)
                    {
                        addToOptions(Top, Direction.Bottom, ++currentAmountInDirection);
                    }
                    break;
            }
            return nodeOptions;
        }

        private void AddToOptions(List<(Node, Direction, List<Node>, int, int)> options, Node node, Direction sourceDirection, List<Node> path, int totalDistance, int amountInDirection)
        {
            if (node != null)
            {
                var newDistance = totalDistance + node.Value;
                if (!node.DirectionDistance.ContainsKey(amountInDirection) || newDistance < node.DirectionDistance[amountInDirection])
                {
                    node.DirectionDistance[amountInDirection] = newDistance;
                    var newPath = new List<Node>(path)
                    {
                        node
                    };
                    options.Add((node, sourceDirection, newPath, newDistance, amountInDirection));
                }
            }
        }
    }
}
