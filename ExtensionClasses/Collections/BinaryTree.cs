using ExtensionClasses.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionClasses.Collections
{
    public class BinaryTree<T> : IBinaryTree<T>
    {
        public INode Root { get; set; }

        public IEnumerable<INode> Nodes
        {
            get
            {
                var list = new List<INode> { Root };

                if (Root != null)
                {
                    list.AddRange(Root.Nodes);
                }
                
                return list;
            }
        }

        public bool Has(T obj) => Nodes.Any(x => x.Value.Equals(obj));

        public int Count => Nodes.Count();
    }
}