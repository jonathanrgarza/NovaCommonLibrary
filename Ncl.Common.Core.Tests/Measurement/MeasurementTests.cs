using System;
using Ncl.Common.Core.Extensions;
using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Measurement.Tests
{
    public class MeasurementTests
    {
        private const string firstAbbr = "1st";
        private const string secondAbbr = "2nd";

        private enum MockUoM
        {
            [Abbreviation(firstAbbr)]
            First,
            [Abbreviation(secondAbbr)]
            Second
        }

        private class MockMeasurement : Measurement<MockMeasurement, MockUoM>
        {
            public MockMeasurement() : base()
            {
            }

            public MockMeasurement(double value, MockUoM unit) : base(value, unit)
            {
            }

            public MockMeasurement(MockMeasurement instance) : base(instance)
            {
            }

            public override MockMeasurement Convert(MockUoM newUnit)
            {
                //Simple conversion logic
                var curUnit = Unit;
                if (curUnit == newUnit)
                    return this;

                double convertedValue;
                if (newUnit == MockUoM.First)
                {
                    convertedValue = Value * 10.0;
                }
                else
                {
                    convertedValue = Value / 10.0;
                }

                return new MockMeasurement(convertedValue, newUnit);
            }

            protected override MockMeasurement NewInstance()
            {
                return new MockMeasurement();
            }

            protected override MockMeasurement NewInstance(double value, MockUoM unit)
            {
                return new MockMeasurement(value, unit);
            }
        }

        [Fact]
        public void Measurement_ShouldCreateDefaultInstance()
        {
            //Act
            var instance = new MockMeasurement();
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Measurement1_ShouldCreateInstance()
        {
            //Act
            var instance = new MockMeasurement(12.5, MockUoM.First);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(MockUoM.First, instance.Unit);
        }

        [Fact]
        public void Measurement2_ShouldCreateCopyInstance()
        {
            //Arrange
            var original = new MockMeasurement(12.5, MockUoM.First);
            //Act
            var instance = new MockMeasurement(original);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(MockUoM.First, instance.Unit);
        }

        [Fact]
        public void Measurement2_NullArgument_ShouldThrowException()
        {
            //Arrange
            MockMeasurement original = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var instance = new MockMeasurement(original);
            });
        }

        [Fact]
        public void HasValue_DefaultInstance_ShouldReturnTrue()
        {
            //Act
            var instance = new MockMeasurement();
            //Assert
            Assert.NotNull(instance);
            Assert.True(instance.HasValue);
        }

        [Fact]
        public void HasValue_NonDefaultInstance_ShouldReturnFalse()
        {
            //Act
            var instance = new MockMeasurement(1.5, MockUoM.First);
            //Assert
            Assert.NotNull(instance);
            Assert.False(instance.HasValue);
        }

        [Fact]
        public void Equals_CompareToNull_ShouldReturnFalse()
        {
            var mock = new MockMeasurement();

            var actual = mock.Equals(null);

            Assert.False(actual);
        }

        [Fact]
        public void Equals_CompareToDifferentType_ShouldReturnFalse()
        {
            var mock = new MockMeasurement();

            var actual = mock.Equals("test string");

            Assert.False(actual);
        }

        [Fact]
        public void Equals_CompareToSameTypeDifferentValue_ShouldReturnFalse()
        {
            var mock = new MockMeasurement();
            var mock2 = (object)new MockMeasurement(1.0, MockUoM.First);

            var actual = mock.Equals(mock2);

            Assert.False(actual);
        }

        [Fact]
        public void Equals_CompareToSameTypeSameValue_ShouldReturnTrue()
        {
            var mock = new MockMeasurement(1.0, MockUoM.First);
            var mock2 = (object)new MockMeasurement(1.0, MockUoM.First);

            var actual = mock.Equals(mock2);

            Assert.True(actual);
        }

        [Fact]
        public void Equals1_CompareToDifferentValue_ShouldReturnFalse()
        {
            var mock = new MockMeasurement(2.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.First);

            var actual = mock.Equals(mock2);

            Assert.False(actual);
        }

        [Fact]
        public void Equals1_CompareToSameValueDifferentUoM_ShouldReturnFalse()
        {
            var mock = new MockMeasurement(1.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.Second);

            var actual = mock.Equals(mock2);

            Assert.False(actual);
        }

        [Fact]
        public void Equals1_CompareToSameValueSameUoM_ShouldReturnTrue()
        {
            var mock = new MockMeasurement(1.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.First);

            var actual = mock.Equals(mock2);

            Assert.True(actual);
        }

        [Fact]
        public void GetHashCode_SameValueSameUom_ShouldReturnEqualValue()
        {
            var mock = new MockMeasurement(1.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.First);

            var firstHash = mock.GetHashCode();
            var secondHash = mock2.GetHashCode();

            Assert.Equal(firstHash, secondHash);
        }

        [Fact]
        public void GetHashCode_DifferentValueSameUom_ShouldReturnDifferentValue()
        {
            var mock = new MockMeasurement(5.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.First);

            var firstHash = mock.GetHashCode();
            var secondHash = mock2.GetHashCode();

            Assert.NotEqual(firstHash, secondHash);
        }

        [Fact]
        public void GetHashCode_SameValueDifferentUom_ShouldReturnDifferentValue()
        {
            var mock = new MockMeasurement(1.0, MockUoM.First);
            var mock2 = new MockMeasurement(1.0, MockUoM.Second);

            var firstHash = mock.GetHashCode();
            var secondHash = mock2.GetHashCode();

            Assert.NotEqual(firstHash, secondHash);
        }

        [Fact]
        public void ToString_SetValueAndUoM_ShouldReturnStringRep()
        {
            var expected = $"{1} {MockUoM.First.GetAbbreviation()}";
            var mock = new MockMeasurement(1.0, MockUoM.First);

            var actual = mock.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString1_SetValueAndUoM_ShouldReturnRoundedStringRep()
        {
            const int precision = 2;
            string format = $"F{precision}";
            var expected = $"{2.123456789.ToString(format)} {MockUoM.First.GetAbbreviation()}";
            var mock = new MockMeasurement(2.123456789, MockUoM.First);

            var actual = mock.ToString(precision);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_NullOther_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                mock.Add(mock2);
            });
        }

        [Fact]
        public void Add_SameUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Add(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Add_DifferentUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Add(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Add1_SameUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Add(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Add1_DifferentUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Add(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorAdd_NullLeft_ShouldThrowException()
        {
            MockMeasurement mock = null;
            MockMeasurement mock2 = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock + mock2;
            });
        }

        [Fact]
        public void OperatorAdd_NullRight_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock + mock2;
            });
        }

        [Fact]
        public void OperatorAdd_SameUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock + mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorAdd_DifferentUom_ShouldReturnAdditionResult()
        {
            const double expectedValue = 5.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock + mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Subtract_NullOther_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                mock.Subtract(mock2);
            });
        }

        [Fact]
        public void Subtract_SameUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Subtract(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Subtract_DifferentUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Subtract(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Subtract1_SameUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Subtract(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Subtract1_DifferentUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Subtract(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorSubtract_NullLeft_ShouldThrowException()
        {
            MockMeasurement mock = null;
            MockMeasurement mock2 = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock - mock2;
            });
        }

        [Fact]
        public void OperatorSubtract_NullRight_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock - mock2;
            });
        }

        [Fact]
        public void OperatorSubtract_SameUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock - mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorSubtract_DifferentUom_ShouldReturnSubtractiveResult()
        {
            const double expectedValue = 2.0;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock - mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Multiply_NullOther_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                mock.Multiply(mock2);
            });
        }

        [Fact]
        public void Multiply_SameUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Multiply(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Multiply_DifferentUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Multiply(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Multiply1_SameUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Multiply(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Multiply1_DifferentUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Multiply(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorMultiply_NullLeft_ShouldThrowException()
        {
            MockMeasurement mock = null;
            MockMeasurement mock2 = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock * mock2;
            });
        }

        [Fact]
        public void OperatorMultiply_NullRight_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock * mock2;
            });
        }

        [Fact]
        public void OperatorMultiply_SameUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock * mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorMultiply_DifferentUom_ShouldReturnMultiplicativeResult()
        {
            const double expectedValue = 5.25;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock * mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Divide_NullOther_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                mock.Divide(mock2);
            });
        }

        [Fact]
        public void Divide_DivideByZero_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = new MockMeasurement(-0.0, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock.Divide(mock2);
            });
        }

        [Fact]
        public void Divide_SameUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Divide(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Divide_DifferentUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Divide(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Divide1_DivideByZero_ShouldThrowException()
        {
            double otherValue = 0.0;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock.Divide(otherValue, otherUnit);
            });
        }

        [Fact]
        public void Divide1_SameUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Divide(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Divide1_DifferentUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Divide(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorDivide_NullLeft_ShouldThrowException()
        {
            MockMeasurement mock = null;
            MockMeasurement mock2 = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock / mock2;
            });
        }

        [Fact]
        public void OperatorDivide_NullRight_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock / mock2;
            });
        }

        [Fact]
        public void OperatorDivide_DivideByZero_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = new MockMeasurement(0.0, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock / mock2;
            });
        }

        [Fact]
        public void OperatorDivide_SameUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock / mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorDivide_DifferentUom_ShouldReturnDivisionResult()
        {
            const double expectedValue = 2.33;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock / mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Modulus_NullOther_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                mock.Modulus(mock2);
            });
        }

        [Fact]
        public void Modulus_DivideByZero_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = new MockMeasurement(0.0, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock.Modulus(mock2);
            });
        }

        [Fact]
        public void Modulus_SameUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Modulus(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Modulus_DifferentUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock.Modulus(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Modulus1_DivideByZero_ShouldThrowException()
        {
            double otherValue = 0.0;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock.Modulus(otherValue, otherUnit);
            });
        }

        [Fact]
        public void Modulus1_SameUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Modulus(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void Modulus1_DifferentUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);


            var actual = mock.Modulus(otherValue, otherUnit);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorModulus_NullLeft_ShouldThrowException()
        {
            MockMeasurement mock = null;
            MockMeasurement mock2 = new MockMeasurement(3.5, MockUoM.First);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock % mock2;
            });
        }

        [Fact]
        public void OperatorModulus_NullRight_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = mock % mock2;
            });
        }

        [Fact]
        public void OperatorModulus_DivideByZero_ShouldThrowException()
        {
            var mock = new MockMeasurement(3.5, MockUoM.First);
            MockMeasurement mock2 = new MockMeasurement(0.0, MockUoM.First);

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = mock % mock2;
            });
        }

        [Fact]
        public void OperatorModulus_SameUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 1.5;
            MockUoM otherUnit = MockUoM.First;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock % mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void OperatorModulus_DifferentUom_ShouldReturnRemainderResult()
        {
            const double expectedValue = 0.5;
            const MockUoM expectedUoM = MockUoM.First;

            double otherValue = 0.15;
            MockUoM otherUnit = MockUoM.Second;
            var mock = new MockMeasurement(3.5, MockUoM.First);
            var mock2 = new MockMeasurement(otherValue, otherUnit);

            var actual = mock % mock2;

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }
    }
}