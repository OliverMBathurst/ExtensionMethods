using System.Collections.Generic;

namespace ExtensionClasses.Interfaces
{
    public interface IDoublyLinkedList<T>
    {
        IEnumerable<IDoublyLinkedListNode<T>> Nodes { get; set; }

        bool Has(T data);
    }
}
