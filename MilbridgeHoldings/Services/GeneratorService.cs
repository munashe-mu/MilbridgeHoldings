namespace ModelLibrary.Services
{
    using System;
    using System.Linq;
    using System.Text;

    public class GeneratorService
    {
        public static string UniqueCode()
        {
            return "CM" + DateTime.Now.ToString("ddMMyyyy") + "." + DateTime.Now.ToString("HHmm") + "." + GetLetters(2) +
                  DateTime.Now.ToString("ss") + DateTime.Now.ToString("fff") + GetRandomChar();
        }

        public static string UniquePIN()
        {
            return new string(Enumerable.Repeat("0123456789", 4).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        private static string GetRandomChar()
        {
            const string chars = "CHIPABQRSTUXJKLMNOYZVWDEFG";
            return new string(Enumerable.Repeat(chars, 1).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        private static string GetLetters(int numberOfCharsToGenerate)
        {
            var random = new Random();
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            var sb = new StringBuilder();
            for (int i = 0; i < numberOfCharsToGenerate; i++)
            {
                int num = random.Next(0, chars.Length);
                sb.Append(chars[num]);
            }
            return sb.ToString();
        }

        public static int GeneratePin()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new();
            return _rdm.Next(_min, _max);
        }

        public static string GenerateVerifyCode()
        {
            int min = 100000;
            int max = 999999;
            Random rnd = new();
            return rnd.Next(min, max).ToString();
        }
    }
}
