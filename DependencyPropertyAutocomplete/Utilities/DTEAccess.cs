using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace DependencyPropertyAutocomplete.Utilities
{
    internal static class DTEAccess
    {
        #region Constants
        private const string CLASS_PLACEHOLDER = "UNKNOWN_CLASS";
        #endregion

        #region Methods
        internal static string GetEnclosingClassForActiveWindow()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (!(Package.GetGlobalService(typeof(DTE)) is DTE2 dte))
            {
                return CLASS_PLACEHOLDER;
            }

            if (!(dte.ActiveWindow.Selection is TextSelection textSelection))
            {
                return CLASS_PLACEHOLDER;
            }

            if (!(textSelection.ActivePoint.CodeElement[vsCMElement.vsCMElementClass] is CodeClass codeClass))
            {
                return CLASS_PLACEHOLDER;
            }

            var className = codeClass.Name;
            return className;
        }
        #endregion
    }
}
