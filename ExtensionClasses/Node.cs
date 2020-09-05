using ExtensionClasses.Interfaces;
using System.Collections.Generic;

namespace ExtensionClasses
{
    public class Node : INode
    {
        public INode Left { get; set; }

        public INode Right { get; set; }

        public INodeValue Value { get; set; }

        public IEnumerable<INode> Nodes
        {
            get
            {
                var list = new List<INode>
                {
                    Left,
                    Right
                };

                if (Left != null)
                {
                    list.AddRange(Left.Nodes);
                }

                if (Right != null)
                {
                    list.AddRange(Right.Nodes);
                }

                return list;
            }
        }
    }
}