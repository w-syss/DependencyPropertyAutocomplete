using DependencyPropertyAutocomplete.TextUtils;
using System.Text;
using Xunit;

namespace DependencyPropertyAutocompleteTests.TextUtils
{
    public class DependencyPropertySignatureTests
    {
        private string GenerateExpectedResult(string accessModifier, string type, string name, string propertyName, string enclosingClass)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{accessModifier} static readonly DependencyProperty {propertyName}");
            stringBuilder.Append($"\t= DependencyProperty.Register(nameof({name}), typeof({type}), typeof({enclosingClass}), new PropertyMetadata(default({type})));");
            return stringBuilder.ToString();
        }

        [Fact]
        public void GivenOnlyNameProducesCorrectOutput()
        {
            var rawSelection = "TestNameProperty";
            var property = DependencyPropertySignature.FromRawSelectionWithGivenClassName(rawSelection, nameof(DependencyPropertySignatureTests));

            var expected = GenerateExpectedResult(PropertySignature.DEFAULTACCESSMODIFIER, PropertySignature.TYPEPLACEHOLDER, "TestName", "TestNameProperty", nameof(DependencyPropertySignatureTests));
            var result = property.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenNameAndTypeProducesCorrectOutput()
        {
            var rawSelection = "int TestNameProperty";
            var property = DependencyPropertySignature.FromRawSelectionWithGivenClassName(rawSelection, nameof(DependencyPropertySignatureTests));

            var expected = GenerateExpectedResult(PropertySignature.DEFAULTACCESSMODIFIER, "int", "TestName", "TestNameProperty", nameof(DependencyPropertySignatureTests));
            var result = property.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenNameTypeAndAccessModifierProducesCorrectOutput()
        {
            var rawSelection = "private double TestNameProperty";
            var property = DependencyPropertySignature.FromRawSelectionWithGivenClassName(rawSelection, nameof(DependencyPropertySignatureTests));

            var expected = GenerateExpectedResult("private", "double", "TestName", "TestNameProperty", nameof(DependencyPropertySignatureTests));
            var result = property.ToString();

            Assert.Equal(expected, result);
        }
    }
}