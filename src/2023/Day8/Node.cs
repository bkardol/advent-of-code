namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Node
    {
        public string Name { get; }

        public string LeftName { get; }
        public string RightName { get; }

        public Node Left { get; private set; }
        public Node Right { get; private set; }

        public Node(string name, string leftName, string rightName)
        {
            this.Name = name;
            this.LeftName = leftName;
            this.RightName = rightName;
        }

        public void LinkNodes(Node left, Node right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
