using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace DependencyPropertyAutocomplete.TextUtils
{
    internal class TextHelper
    {
        #region Fields
        private readonly ITextView textView;
        #endregion

        #region Constructor
        internal TextHelper(ITextView textView)
        {
            this.textView = textView;
        }
        #endregion

        #region Methods
        internal bool TryGetDependencyPropertySpan(out SnapshotSpan propertySpan)
        {
            propertySpan = default;

            if (TryGetSelectedSpan(out SnapshotSpan span))
            {
                var text = span.GetText();
                if (DependencyPropertySignature.IsCandidate(text))
                {
                    propertySpan = span;
                    return true;
                }
            }
            return false;
        }

        private bool TryGetSelectedSpan(out SnapshotSpan selectedSpan)
        {
            selectedSpan = default;

            var selectedSpans = textView.Selection.SelectedSpans;

            if (selectedSpans.Count != 1)
            {
                return false;
            }

            var span = selectedSpans[0];
            if (span.IsEmpty)
            {
                return false;
            }

            selectedSpan = span;
            return true;
        }
        #endregion
    }
}