
namespace SamDriver.Util
{
    public static class StringExtensionMethods
    {
        public static string RemoveAllWhitespace(this string input)
        {
            char[] rebuilt = new char[input.Length];
            int j = 0;

            foreach (var character in input.ToCharArray())
            {
                if (char.IsWhiteSpace(character)) continue;

                rebuilt[j++] = character;
            }

            return new string(rebuilt, 0, j);
        }
    }
}
