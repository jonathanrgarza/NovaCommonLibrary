using System.ComponentModel;
using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Extensions.Tests
{
    public class EnumExtensionsTests
    {
        private const string HasAttributesValueEnumDesc = "1st value";
        private const string HasAttributesValueEnumAbbre = "1st";

        //Some test enum to be used with the following unit tests
        private enum TestEnum
        {
            None,
            [Description(HasAttributesValueEnumDesc)]
            [Abbreviation(HasAttributesValueEnumAbbre)]
            HasAttributesValue,
        }

        [Fact]
        public void GetAttributeOfType_PresentAttributeShouldReturnValue()
        {
            //Arrange
            const string expected = HasAttributesValueEnumDesc;

            //Act
            var actual = TestEnum.HasAttributesValue.GetAttributeOfType<DescriptionAttribute>()?.Description;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAttributeOfType_NotPresentAttributeShouldReturnNull()
        {
            //Act
            var actual = TestEnum.None.GetAttributeOfType<DescriptionAttribute>()?.Description;

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetDescription_PresentAttributeShouldReturnValue()
        {
            //Arrange
            const string expected = HasAttributesValueEnumDesc;

            //Act
            var actual = TestEnum.HasAttributesValue.GetDescription();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDescription_NotPresentAttributeShouldReturnName()
        {
            //Arrange
            var expected = TestEnum.None.ToString();

            //Act
            var actual = TestEnum.None.GetDescription();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDescription1_PresentAttributeShouldReturnTrue()
        {
            //Act
            var actual = TestEnum.HasAttributesValue.GetDescription(out _);

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void GetDescription1_PresentAttributeShouldReturnValue()
        {
            //Arrange
            const string expected = HasAttributesValueEnumDesc;

            //Act
            TestEnum.HasAttributesValue.GetDescription(out string actual);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDescription1_NotPresentAttributeShouldReturnFalse()
        {
            //Act
            var actual = TestEnum.None.GetDescription(out _);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void GetDescription1_NotPresentAttributeShouldReturnName()
        {
            //Arrange
            string expected = TestEnum.None.ToString();

            //Act
            TestEnum.None.GetDescription(out string actual);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAbbreviation_PresentAttributeShouldReturnValue()
        {
            //Arrange
            const string expected = HasAttributesValueEnumAbbre;

            //Act
            var actual = TestEnum.HasAttributesValue.GetAbbreviation();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAbbreviation_NotPresentAttributeShouldReturnName()
        {
            //Arrange
            string expected = TestEnum.None.ToString();

            //Act
            var actual = TestEnum.None.GetAbbreviation();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAbbreviation1_PresentAttributeShouldReturnTrue()
        {
            //Act
            var actual = TestEnum.HasAttributesValue.GetAbbreviation(out _);

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void GetAbbreviation1_PresentAttributeShouldReturnValue()
        {
            //Arrange
            const string expected = HasAttributesValueEnumAbbre;

            //Act
            TestEnum.HasAttributesValue.GetAbbreviation(out string actual);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAbbreviation1_NotPresentAttributeShouldReturnFalse()
        {
            //Act
            var actual = TestEnum.None.GetAbbreviation(out _);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void GetAbbreviation1_NotPresentAttributeShouldReturnName()
        {
            //Arrange
            string expected = TestEnum.None.ToString();

            //Act
            TestEnum.None.GetAbbreviation(out string actual);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}