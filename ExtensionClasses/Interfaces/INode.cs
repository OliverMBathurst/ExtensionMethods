using System.Collections.Generic;

namespace ExtensionClasses.Interfaces
{
    public interface INode
    {
        INode Left { get; set; }

        INode Right { get; set; }

        INodeValue Value { get; set; }

        IEnumerable<INode> Nodes { get; }
    }
}
