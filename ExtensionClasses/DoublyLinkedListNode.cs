using ExtensionClasses.Interfaces;

namespace ExtensionClasses
{
    public class DoublyLinkedListNode<T> : IDoublyLinkedListNode<T>
    {
        public IDoublyLinkedListNode<T> Previous { get; set; }

        public IDoublyLinkedListNode<T> Next { get; set; }

        public T Data { get; set; }
    }
}
