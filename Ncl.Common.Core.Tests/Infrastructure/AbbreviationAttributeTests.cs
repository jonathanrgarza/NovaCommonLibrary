using Xunit;

namespace Ncl.Common.Core.Infrastructure.Tests
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
            var secondInstance = (object)new AbbreviationAttribute(second); //Force as object to ensure test of Equals(object)

            //Act
            var result = firstInstance.Equals(secondInstance);

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
            var secondInstance = (object)new AbbreviationAttribute(second); //Force as object to ensure test of Equals(object)

            //Act
            var result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsTest_DifferentTypesShouldReturnFalse()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute();
            var secondInstance = "";

            //Act
            var result = firstInstance.Equals(secondInstance);

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
            var result = firstInstance.Equals(secondInstance);

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
            var result = firstInstance.Equals(secondInstance);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsTest1_NullValueShouldReturnFalse()
        {
            //Arrange
            var firstInstance = new AbbreviationAttribute();

            //Act
            var result = firstInstance.Equals(null);

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
            var hashCode1 = firstInstance.GetHashCode();
            var hashCode2 = secondInstance.GetHashCode();

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
            var hashCode1 = firstInstance.GetHashCode();
            var hashCode2 = secondInstance.GetHashCode();

            //Assert
            Assert.False(hashCode1 == hashCode2);
        }
    }
}