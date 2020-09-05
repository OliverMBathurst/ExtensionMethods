namespace ExtensionClasses.Interfaces
{
    public interface INodeValue
    {
        bool HasValue { get; }

        object Value { get; set; }
    }
}
