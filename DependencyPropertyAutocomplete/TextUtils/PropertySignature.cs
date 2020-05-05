using System;
using System.Text;

namespace DependencyPropertyAutocomplete.TextUtils
{
    public class PropertySignature
    {
        #region Constants
        public const string TYPEPLACEHOLDER = "UNKNOWN_TYPE";
        public const string DEFAULTACCESSMODIFIER = "public";
        #endregion

        #region Properties
        internal string AccessModifier { get; private set; }
        internal string Type { get; private set; }
        internal string Name { get; private set; }
        internal string PropertyName { get => $"{Name}Property"; }
        #endregion

        #region Constructor & Factories
        private PropertySignature(string rawSelection)
        {
            ParseRawSelection(rawSelection);
        }

        public static PropertySignature FromRawSelection(string rawSelection)
        {
            if (rawSelection == null)
            {
                throw new ArgumentNullException(nameof(rawSelection));
            }

            return new PropertySignature(rawSelection);
        }
        #endregion

        #region Methods
        private void ParseRawSelection(string rawSelection)
        {
            var tokens = rawSelection.Split(' ');

            AccessModifier = DEFAULTACCESSMODIFIER;
            Type = TYPEPLACEHOLDER;

            if (tokens.Length == 1)
            {
                Name = tokens[0];
            }
            else if (tokens.Length == 2)
            {
                Type = tokens[0];
                Name = tokens[1];
            }
            else if (tokens.Length == 3)
            {
                AccessModifier = tokens[0];
                Type = tokens[1];
                Name = tokens[2];
            }

            Name = Name.Replace("Property", "");
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{AccessModifier} {Type} {Name}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"\tget => ({Type})GetValue({PropertyName});");
            stringBuilder.AppendLine($"\tset => SetValue({PropertyName}, value);");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        #endregion
    }
}