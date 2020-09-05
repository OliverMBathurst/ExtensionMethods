namespace ExtensionClasses.Interfaces
{
    public interface IDoublyLinkedListNode<T>
    {
        IDoublyLinkedListNode<T> Previous { get; set; }

        IDoublyLinkedListNode<T> Next { get; set; }

        T Data { get; set; }
    }
}