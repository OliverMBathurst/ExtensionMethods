using ExtensionClasses.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionClasses.Collections
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public IEnumerable<IDoublyLinkedListNode<T>> Nodes { get; set; }
        
        public bool Has(T data)
        {
            if (Nodes == null || !Nodes.Any()) return false;

            return Nodes.FirstOrDefault(x => x.Data.Equals(data)) != null;
        }

        public T Get(T data) => Nodes.FirstOrDefault(x => x.Data.Equals(data)).Data;
    }
}
