using System.Text.RegularExpressions;

namespace NpgsqlWithOwnedEntities
{
    public static class StringEx
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            Match startUnderscores = Regex.Match(input, @"^_+");
            string snakeCase = startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            //Console.WriteLine($"{input}->{snakeCase}");
            return snakeCase;
        }
    }
}