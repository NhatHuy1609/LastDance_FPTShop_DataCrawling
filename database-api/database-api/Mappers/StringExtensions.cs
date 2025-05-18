using System.Globalization;
using System.Text;

namespace database_api.Mappers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Xóa dấu tiếng Việt (e.g "màn hình" -> "man hinh")
        /// </summary>
        /// <param name="text">The input string.</param>
        /// <returns>The string without diacritics.</returns>
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}