using DependencyPropertyAutocomplete.Utilities;
using Microsoft.VisualStudio.Shell;
using System;
using System.Text;

namespace DependencyPropertyAutocomplete.TextUtils
{
    public class DependencyPropertySignature
    {
        #region Fields
        private readonly PropertySignature property;
        private string enclosingClass;
        #endregion

        #region Constructor & Factories
        private DependencyPropertySignature(string rawSelection)
        {
            property = PropertySignature.FromRawSelection(rawSelection);

            ThreadHelper.ThrowIfNotOnUIThread();
            LoadClassName();
        }

        private DependencyPropertySignature(string rawSelection, string className)
        {
            property = PropertySignature.FromRawSelection(rawSelection);
            enclosingClass = className;
        }

        public static DependencyPropertySignature FromRawSelection(string rawSelection)
        {
            if (rawSelection == null)
            {
                throw new ArgumentNullException(nameof(rawSelection));
            }

            return new DependencyPropertySignature(rawSelection);
        }

        public static DependencyPropertySignature FromRawSelectionWithGivenClassName(string rawSelection, string className)
        {
            if (rawSelection == null)
            {
                throw new ArgumentNullException(nameof(rawSelection));
            }

            if (className == null)
            {
                throw new ArgumentNullException(nameof(className));
            }

            return new DependencyPropertySignature(rawSelection, className);
        }
        #endregion

        #region Methods

        protected void LoadClassName()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            enclosingClass = DTEAccess.GetEnclosingClassForActiveWindow();
        }

        public static bool IsCandidate(string selection)
        {
            if (selection == null)
            {
                return false;
            }

            var tokenCount = selection.Split(' ').Length;

            return (tokenCount <= 3) && selection.EndsWith("Property", StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{property.AccessModifier} static readonly DependencyProperty {property.PropertyName}");
            stringBuilder.Append($"\t= DependencyProperty.Register(nameof({property.Name}), typeof({property.Type}), typeof({enclosingClass}), new PropertyMetadata(default({property.Type})));");
            return stringBuilder.ToString();
        }
        #endregion
    }
}
