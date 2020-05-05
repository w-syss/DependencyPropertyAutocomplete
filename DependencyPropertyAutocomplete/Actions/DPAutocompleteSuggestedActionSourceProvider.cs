using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace DependencyPropertyAutocomplete.Actions
{
    [Export(typeof(ISuggestedActionsSourceProvider))]
    [Name("DepedencyPropertyAutocomplete Suggested Actions")]
    [ContentType("text")]
    internal class DPAutocompleteSuggestedActionSourceProvider : ISuggestedActionsSourceProvider
    {
        #region Imported Properties
        [Import(typeof(ITextStructureNavigatorSelectorService))]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }
        #endregion

        #region Implementation ISuggestedActionsSourceProvider
        public ISuggestedActionsSource CreateSuggestedActionsSource(ITextView textView, ITextBuffer textBuffer)
        {
            if (textBuffer == null || textView == null)
            {
                return null;
            }
            return new DPAutocompleteSuggestedActionSource(textView);
        }
        #endregion
    }
}
