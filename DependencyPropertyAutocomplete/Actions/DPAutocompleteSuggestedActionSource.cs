using DependencyPropertyAutocomplete.TextUtils;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DependencyPropertyAutocomplete.Actions
{
    internal class DPAutocompleteSuggestedActionSource : ISuggestedActionsSource
    {
        #region Events
        public event EventHandler<EventArgs> SuggestedActionsChanged;
        #endregion

        #region Fields
        private readonly TextHelper textHelper;
        #endregion

        #region Constructor
        public DPAutocompleteSuggestedActionSource(ITextView textView)
        {
            textHelper = new TextHelper(textView);
        }
        #endregion

        #region Implementation : ISuggestedActionsSource
        public void Dispose()
        {
            // No cleanup needed
        }

        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            if (textHelper.TryGetDependencyPropertySpan(out SnapshotSpan span))
            {
                var trackingSpan = range.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);

                var action = new DPAutocompleteAction(trackingSpan);
                var actionSet = new SuggestedActionSet(null, new ISuggestedAction[] { action });
                return new SuggestedActionSet[] { actionSet };
            }

            return Enumerable.Empty<SuggestedActionSet>();
        }

        public Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
                {
                    return textHelper.TryGetDependencyPropertySpan(out SnapshotSpan _);
                },
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);
        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
        #endregion
    }
}