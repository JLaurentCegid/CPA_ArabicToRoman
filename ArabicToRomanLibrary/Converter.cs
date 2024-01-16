using System.Globalization;
using System.Text;

namespace ArabicToRomanLibrary
{
    public class Converter
    {
        public const int MIN_VALUE = 0;
        public const int MAX_VALUE = 3999;

        public string ToRoman(float arabic)
        {
            if (arabic < MIN_VALUE || arabic > MAX_VALUE)
            {
                throw new ArgumentOutOfRangeException(nameof(arabic), VALUE_ERROR_MESSAGE);
            }

            int integer = (int)arabic;

            StringBuilder builder = new();
            string temp = integer.ToString("G", CultureInfo.InvariantCulture).PadLeft(MAX_LENGTH, PADDING_CHARACTER);

            for (int index = 0; index < MAX_LENGTH; index++)
            {
                builder.Append(ConvertToRoman(temp[index], index));
            }

            return builder.ToString();
        }

        private string ConvertToRoman(char character, int index)
        {
            Dictionary<char, string> data = new()
            {
                { PADDING_CHARACTER, string.Empty },
                { '1', characters[index][0] },
                { '2', characters[index][0] + characters[index][0] },
                { '3', characters[index][0] + characters[index][0] + characters[index][0] },
                { '4', characters[index][0] + characters[index][1] },
                { '5', characters[index][1] },
                { '6', characters[index][1] + characters[index][0] },
                { '7', characters[index][1] + characters[index][0] + characters[index][0] },
                { '8', characters[index][1] + characters[index][0] + characters[index][0] + characters[index][0] },
                { '9', characters[index][0] + characters[index][2] }
            };

            return data[character];
        }

        private const int MAX_LENGTH = 4;
        private const char PADDING_CHARACTER = '0';
        private const string VALUE_ERROR_MESSAGE = "Value must be in the range 0 - 3999";

        private readonly string[][] characters =
        {
            new string[] { "M", "", "" },
            new string[] { "C", "D", "M" },
            new string[] { "X", "L", "C" },
            new string[] { "I", "V", "X" }
        };
    }
}
