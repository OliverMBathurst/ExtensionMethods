using System.Collections.Generic;

namespace ExtensionClasses.Interfaces
{
    public interface IBinaryTree<T>
    {
        INode Root { get; set; }

        IEnumerable<INode> Nodes { get; }

        bool Has(T node);

        int Count { get; }
    }
}
