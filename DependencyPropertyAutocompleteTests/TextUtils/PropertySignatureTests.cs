﻿using DependencyPropertyAutocomplete.TextUtils;
using System.Text;
using Xunit;

namespace DependencyPropertyAutocompleteTests.TextUtils
{
    public class PropertySignatureTests
    {

        private string GenerateExpectedResult(string accessModifier, string type, string name, string propertyName)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{accessModifier} {type} {name}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    get => ({type})GetValue({propertyName});");
            stringBuilder.AppendLine($"    set => SetValue({propertyName}, value);");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        private string GenerateExpectedResultForObjectReturnType(string accessModifier, string type, string name, string propertyName)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{accessModifier} {type} {name}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    get => GetValue({propertyName});");
            stringBuilder.AppendLine($"    set => SetValue({propertyName}, value);");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        [Fact]
        public void GivenOnlyNameProducesCorrectOutput()
        {
            var rawSelection = "TestNameProperty";
            var property = PropertySignature.FromRawSelection(rawSelection);

            var expected = GenerateExpectedResult(PropertySignature.DEFAULTACCESSMODIFIER, PropertySignature.TYPEPLACEHOLDER, "TestName", "TestNameProperty");
            var result = property.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenNameAndTypeProducesCorrectOutput()
        {
            var rawSelection = "string TestNameProperty";
            var property = PropertySignature.FromRawSelection(rawSelection);

            var expected = GenerateExpectedResult(PropertySignature.DEFAULTACCESSMODIFIER, "string", "TestName", "TestNameProperty");
            var result = property.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenNameTypeAndAccessModifierProducesCorrectOutput()
        {
            var rawSelection = "internal string TestNameProperty";
            var property = PropertySignature.FromRawSelection(rawSelection);

            var expected = GenerateExpectedResult("internal", "string", "TestName", "TestNameProperty");
            var result = property.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ObjectReturnTypeProducesCorrectOutput()
        {
            var rawSelection = "public object TestNameProperty";
            var property = PropertySignature.FromRawSelection(rawSelection);

            var expected = GenerateExpectedResultForObjectReturnType("public", "object", "TestName", "TestNameProperty");
            var result = property.ToString();

            Assert.Equal(expected, result);
        }
    }
}