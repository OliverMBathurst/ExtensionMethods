using ExtensionClasses.Interfaces;

namespace ExtensionClasses
{
    public class NodeValue : INodeValue
    {
        public bool HasValue => Value != null;

        public object Value { get; set; }
    }
}
