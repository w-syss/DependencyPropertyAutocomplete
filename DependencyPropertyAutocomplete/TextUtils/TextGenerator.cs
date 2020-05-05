using System.Text;

namespace DependencyPropertyAutocomplete.TextUtils
{
    internal class TextGenerator
    {
        public static string GetNonIndentedCode(PropertySignature propertySignature, DependencyPropertySignature dependencyPropertySignature)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(propertySignature.ToString());
            stringBuilder.AppendLine();
            stringBuilder.Append(dependencyPropertySignature.ToString());
            return stringBuilder.ToString();
        }

        public static string GetIndentedCode(PropertySignature propertySignature, DependencyPropertySignature dependencyPropertySignature)
        {
            var nonIndentedCode = GetNonIndentedCode(propertySignature, dependencyPropertySignature);
            return IndentCode(nonIndentedCode);
        }

        public static string GetNonIndentedCodeWithRegion(PropertySignature propertySignature, DependencyPropertySignature dependencyPropertySignature)
        {
            var nonIndentedCode = GetNonIndentedCode(propertySignature, dependencyPropertySignature);
            return AddRegionToInput(propertySignature.PropertyName, nonIndentedCode);
        }

        public static string GetIndentedCodeWithRegion(PropertySignature propertySignature, DependencyPropertySignature dependencyPropertySignature)
        {
            var nonIndentedCodeWithRegion = GetNonIndentedCodeWithRegion(propertySignature, dependencyPropertySignature);
            return IndentCode(nonIndentedCodeWithRegion);
        }

        private static string AddRegionToInput(string propertyName, string input)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"#region DependencyProperty : {propertyName}");
            stringBuilder.AppendLine(input);
            stringBuilder.AppendLine($"#endregion");
            return stringBuilder.ToString();
        }
        private static string IndentCode(string input)
        {
            return input.Replace("\n", "\n\t\t");
        }
    }
}
