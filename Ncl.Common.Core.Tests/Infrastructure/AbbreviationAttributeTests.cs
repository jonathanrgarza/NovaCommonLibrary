using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Tests.Infrastructure
{
    public class AbbreviationAttributeTests
    {
        [Fact]
        public void AbbreviationAttribute_ShouldCreateDefaultInstance()
        {
            //Act
            var instance = new AbbreviationAttribute();
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void AbbreviationAttribute_ShouldCreateInstance()
        {
            //Act
            var instance = new AbbreviationAttribute("Test");
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void IsDefaultAttribute_DefaultValueShouldReturnTrue()
        {
            //Arrange
            var instance = new AbbreviationAttribute();
            //Assert
            Assert.True(instance.IsDefaultAttribute());
        }

        [Fact]
        public void IsDefaultAttribute_NonDefaultValueShouldReturnFalse()
        {
            //Arrange
            var instance = new AbbreviationAttribute("Test");
            //Assert
            Assert.False(instance.IsDefaultAttribute());
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("", "")]
        public void EqualsTest_SameValuesShouldReturnTrue(string first, string second)
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute(first);
            object secondInstance = new AbbreviationAttribute(second); //Force as object to ensure test of Equals(object)

            //Act
            bool result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test", "test1")]
        [InlineData("", "test")]
        public void EqualsTest_DifferentValuesShouldReturnFalse(string first, string second)
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute(first);
            object secondInstance = new AbbreviationAttribute(second); //Force as object to ensure test of Equals(object)

            //Act
            bool result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsTest_DifferentTypesShouldReturnFalse()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute();
            string secondInstance = "";

            //Act
            // ReSharper disable once SuspiciousTypeConversion.Global
            bool result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("", "")]
        public void EqualsTest1_SameValuesShouldReturnTrue(string first, string second)
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute(first);
            var secondInstance = new AbbreviationAttribute(second); //Force as object to ensure test of Equals(object)

            //Act
            bool result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test", "test1")]
        [InlineData("", "test")]
        public void EqualsTest1_DifferentValuesShouldReturnFalse(string first, string second)
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute(first);
            var secondInstance = new AbbreviationAttribute(second);

            //Act
            bool result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsTest1_NullValueShouldReturnFalse()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute();

            //Act
            bool result = firstInstance.Equals(null);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_SameValuesShouldReturnSameValue()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute("test");
            var secondInstance = new AbbreviationAttribute("test");

            //Act
            int hashCode1 = firstInstance.GetHashCode();
            int hashCode2 = secondInstance.GetHashCode();

            //Assert
            Assert.True(hashCode1 == hashCode2);
        }

        [Fact]
        public void GetHashCode_DifferentValuesShouldReturnDifferentValues()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute("test");
            var secondInstance = new AbbreviationAttribute("t");

            //Act
            int hashCode1 = firstInstance.GetHashCode();
            int hashCode2 = secondInstance.GetHashCode();

            //Assert
            Assert.False(hashCode1 == hashCode2);
        }
    }
}