namespace Desdiene
{
    public static class StringHelper
    {
        public static string RemoveBom(string str)
        {
            if (str == null) return null;

            return str
                .Replace('\uFEFF', ' ')
                .Trim();
        }
    }
}
