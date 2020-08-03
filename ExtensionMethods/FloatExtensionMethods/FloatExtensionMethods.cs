namespace ExtensionMethods.FloatExtensionMethods
{
    public static class FloatExtensionMethods
    {
        public static bool IsInRange(this float current, float start, float end) => current >= start && end >= current;
    }
}