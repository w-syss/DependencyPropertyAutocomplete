using DependencyPropertyAutocomplete.TextUtils;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DependencyPropertyAutocomplete.Actions
{
    internal class DPAutocompleteAction : ISuggestedAction
    {
        #region Properties
        public string DisplayText { get; }
        public ImageMoniker IconMoniker { get => default; }
        public bool HasActionSets { get => false; }
        public string IconAutomationText { get => null; }
        public string InputGestureText { get => null; }
        public bool HasPreview { get => true; }
        #endregion

        #region Fields
        private string indentedCode;
        private string nonIndentedCode;
        private readonly ITrackingSpan span;
        #endregion

        #region Constructor
        public DPAutocompleteAction(ITrackingSpan span)
        {
            this.span = span;
            DisplayText = GetDisplayText();

            SetCode();
        }
        #endregion

        #region Implementation : ISuggestedAction
        public Task<object> GetPreviewAsync(CancellationToken cancellationToken)
        {
            var textBlock = new TextBlock
            {
                Padding = new Thickness(5)
            };
            textBlock.Inlines.Add(new Run() { Text = nonIndentedCode });
            return Task.FromResult<object>(textBlock);
        }

        public Task<IEnumerable<SuggestedActionSet>> GetActionSetsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<SuggestedActionSet>>(null);
        }

        public void Invoke(CancellationToken cancellationToken)
        {
            span.TextBuffer.Replace(span.GetSpan(span.TextBuffer.CurrentSnapshot), indentedCode);
        }

        public void Dispose()
        {
            // No cleanup needed
        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
        #endregion

        #region Methods
        private string GetDisplayText()
        {
            return "Generate DependencyProperty";
        }

        private void SetCode()
        {
            var rawSelection = span.GetText(span.TextBuffer.CurrentSnapshot);
            var property = PropertySignature.FromRawSelection(rawSelection);
            var dependencyProperty = DependencyPropertySignature.FromRawSelection(rawSelection);

            nonIndentedCode = TextGenerator.GetNonIndentedCodeWithRegion(property, dependencyProperty);
            indentedCode = TextGenerator.GetIndentedCodeWithRegion(property, dependencyProperty);
        }
        #endregion
    }
}