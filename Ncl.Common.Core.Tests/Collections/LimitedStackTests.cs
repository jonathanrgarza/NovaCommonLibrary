using System;
using System.Collections.Generic;
using System.Linq;
using Ncl.Common.Core.Collections;
using Xunit;

namespace Ncl.Common.Core.Tests.Collections
{
    public class LimitedStackTests
    {
        private readonly IReadOnlyList<int> _defaultCollection = new List<int> { 1, 2, 3 }.AsReadOnly();

        [Fact]
        public void Constructor_WithDefault_ShouldCreateInstanceWithDefault()
        {
            // Act
            var instance = new LimitedStack<int>();

            // Assert
            Assert.Equal(LimitedStack<int>.DefaultMaxCapacity, instance.MaxCapacity);
        }

        [Fact]
        public void Constructor1_WithMaxCapacityValue_ShouldCreateInstanceWithValue()
        {
            //Arrange
            const int expected = 10;

            // Act
            var instance = new LimitedStack<int>(expected);

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
        }

        [Fact]
        public void Constructor1_WithUnlimitedValue_ShouldCreateInstanceWithUnlimitedCapacity()
        {
            //Arrange
            const int expected = LimitedStack<int>.UnlimitedCapacity;

            // Act
            var instance = new LimitedStack<int>(expected);

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
            Assert.True(instance.IsUnlimitedCapacity);
        }

        [Fact]
        public void Constructor1_WithNegativeMaxCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange

            static void TestCode()
            {
                // Act
                _ = new LimitedStack<int>(-10);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void Constructor2_WithDefaultCollection_ShouldCreateInstanceWithValues()
        {
            // Act
            var instance = new LimitedStack<int>(_defaultCollection, _defaultCollection.Count + 1);

            // Assert
            Assert.Equal(_defaultCollection.Count, instance.Count);
            Assert.Equal(_defaultCollection.Reverse(), instance);
        }

        [Fact]
        public void Constructor3_WithDefaultCollection_ShouldCreateInstanceWithValues()
        {
            // Act
            var instance = new LimitedStack<int>(new List<int>(_defaultCollection), _defaultCollection.Count + 1);

            // Assert
            Assert.Equal(_defaultCollection.Count, instance.Count);
            Assert.Equal(_defaultCollection.Reverse(), instance);
        }

        [Fact]
        public void IsEmpty_WithEmptyStack_ShouldBeTrue()
        {
            // Act
            var instance = new LimitedStack<int>();

            // Assert
            Assert.True(instance.IsEmpty);
        }

        [Fact]
        public void MaxCapacity_WithPositiveValue_ShouldSetValue()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>
            {
                // Act
                MaxCapacity = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
            Assert.False(instance.IsUnlimitedCapacity);
        }

        [Fact]
        public void MaxCapacity_WithSameValue_ShouldNoNothing()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>
            {
                MaxCapacity = expected
            };

            // Act
            instance.MaxCapacity = expected;

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
            Assert.False(instance.IsUnlimitedCapacity);
        }

        [Fact]
        public void MaxCapacity_WithSmallerValueAndFullStack_ShouldSetValueAndRemoveExtraValues()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });
            const int expected = 1;

            // Act
            instance.MaxCapacity = 1;
            int actual = instance.MaxCapacity;

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected, instance.Count);
            Assert.Equal(3, instance.Peek());
        }

        [Fact]
        public void MaxCapacity_WithUnlimitedValue_ShouldSetValue()
        {
            // Arrange
            const int expected = LimitedStack<int>.UnlimitedCapacity;
            var instance = new LimitedStack<int>
            {
                // Act
                MaxCapacity = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
            Assert.True(instance.IsUnlimitedCapacity);
        }

        [Fact]
        public void MaxCapacity_WithNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            void TestCode()
            {
                // Act
                instance.MaxCapacity = -1;
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void Push_WithValueAndEmptyStack_ShouldAddValue()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();

            // Act
            instance.Push(expected);

            // Assert
            int actual = instance.Peek();
            Assert.Equal(1, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Push_WithValueAndNonEmptyStack_ShouldAddValue()
        {
            // Arrange
            const int expected = 3;
            var instance = new LimitedStack<int>();
            instance.Push(1);

            // Act
            instance.Push(expected);

            // Assert
            int actual = instance.Peek();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Push_WithValueAndFullStack_ShouldAddValue()
        {
            // Arrange
            const int expected = 3;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.Push(expected);

            // Assert
            int actual = instance.Peek();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Push_WithValueAndFullStack_ShouldRemoveEnd()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(expected);

            // Act
            instance.Push(3);

            // Assert
            int actual = instance.Last();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Push_WithValueAndFullSingleValueStack_ShouldAddValue()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>(1);
            instance.Push(1);

            // Act
            instance.Push(expected);

            // Assert
            int actual = instance.Peek();
            Assert.Equal(1, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Push_WithValueAndFullSingleValueStack_ShouldRemoveEnd()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>(1);
            instance.Push(1);

            // Act
            instance.Push(expected);

            // Assert
            int actual = instance.Last();
            Assert.Equal(1, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndEmptyStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();

            // Act
            instance.PushRange(new[] { 1, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndNonEmptyStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 3;
            var instance = new LimitedStack<int>();
            instance.Push(1);

            // Act
            instance.PushRange(new[] { 2, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(3, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndUnlimitedNonEmptyStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 3;
            var instance = new LimitedStack<int>(LimitedStack<int>.UnlimitedCapacity);
            instance.Push(1);

            // Act
            instance.PushRange(new[] { 2, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(3, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndAlmostFullStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 4;
            var instance = new LimitedStack<int>(3);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.PushRange(new[] { 3, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(3, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndAlmostFullStack_ShouldRemoveEnd()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>(3);
            instance.Push(1);
            instance.Push(expected);

            // Act
            instance.PushRange(new[] { 3, 4 });

            // Assert
            int actual = instance.Last();
            Assert.Equal(3, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndFullStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 4;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.PushRange(new[] { 3, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndFullStack_ShouldRemoveEnd()
        {
            // Arrange
            const int expected = 3;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.PushRange(new[] { expected, 4 });

            // Assert
            int actual = instance.Last();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithOversizedNumberOfValuesAndFullStack_ShouldAddValues()
        {
            // Arrange
            const int expected = 6;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.PushRange(new[] { 3, 4, 5, expected });

            // Assert
            int actual = instance.Peek();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithOversizedNumberOfValuesAndFullStack_ShouldRemoveEnd()
        {
            // Arrange
            const int expected = 5;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);
            instance.Push(2);

            // Act
            instance.PushRange(new[] { 3, 4, expected, 6 });

            // Assert
            int actual = instance.Last();
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithMultipleValuesAndSingleValueStack_ShouldLastValueOnly()
        {
            // Arrange
            const int expected = 5;
            var instance = new LimitedStack<int>(1);
            instance.Push(1);

            // Act
            instance.PushRange(new[] { 3, 4, expected });

            // Assert
            int actual = instance.Last();
            Assert.Equal(1, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PushRange_WithEmptyListValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 1;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);

            // Act
            instance.PushRange(Array.Empty<int>());

            // Assert
            Assert.Equal(expected, instance.Count);
        }

        [Fact]
        public void PushRange_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var instance = new LimitedStack<int>(2);

            void TestCode()
            {
                // Act
                instance.PushRange(null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void PushRange_WithEmptyEnumerableValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 1;
            var instance = new LimitedStack<int>(2);
            instance.Push(1);

            // Act
            instance.PushRange(Enumerable.Empty<int>());

            // Assert
            Assert.Equal(expected, instance.Count);
        }

        [Fact]
        public void PushRange1_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var instance = new LimitedStack<int>(2);

            void TestCode()
            {
                // Act
                instance.PushRange((IEnumerable<int>) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Pop_WithNonEmptyStack_ShouldRemoveFirstValueAndReturnIt()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(expected);

            // Act
            int actual = instance.Pop();

            // Assert
            Assert.Equal(1, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Pop_WithNearEmptyStack_ShouldRemoveFirstValueAndReturnIt()
        {
            // Arrange
            const int expected = 1;
            var instance = new LimitedStack<int>();
            instance.Push(expected);

            // Act
            int actual = instance.Pop();

            // Assert
            Assert.Equal(0, instance.Count);
            Assert.Equal(expected, actual);
            Assert.Empty(instance);
        }

        [Fact]
        public void Pop_WithEmptyStack_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            // Act
            void TestCode()
            {
                _ = instance.Pop();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public void TryPop_WithNonEmptyStack_ShouldRemoveFirstValueAndReturnTrue()
        {
            // Arrange
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(2);

            // Act
            bool actual = instance.TryPop(out _);

            // Assert
            Assert.Equal(1, instance.Count);
            Assert.True(actual);
        }

        [Fact]
        public void TryPop_WithNonEmptyStack_ShouldSetOutValue()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(expected);

            // Act
            _ = instance.TryPop(out int actual);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryPop_WithEmptyStack_ShouldReturnFalse()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            // Act
            bool actual = instance.TryPop(out _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Peek_WithNonEmptyStack_ShouldReturnHeadValueWithoutRemovingIt()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(expected);

            // Act
            int actual = instance.Peek();

            // Assert
            Assert.Equal(2, instance.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Peek_WithEmptyStack_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            // Act
            void TestCode()
            {
                _ = instance.Peek();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public void TryPeek_WithNonEmptyStack_ShouldReturnTrueWithoutRemovingHeadValue()
        {
            // Arrange
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(2);

            // Act
            bool actual = instance.TryPeek(out _);

            // Assert
            Assert.Equal(2, instance.Count);
            Assert.True(actual);
        }

        [Fact]
        public void TryPeek_WithNonEmptyStack_ShouldSetOutValue()
        {
            // Arrange
            const int expected = 2;
            var instance = new LimitedStack<int>();
            instance.Push(1);
            instance.Push(expected);

            // Act
            _ = instance.TryPeek(out int actual);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryPeek_WithEmptyStack_ShouldReturnFalse()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            // Act
            bool actual = instance.TryPeek(out _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Clear_WithNonEmptyStack_ShouldRemoveAllElements()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            // Act
            instance.Clear();

            // Assert
            Assert.True(instance.IsEmpty);
            Assert.Empty(instance);
        }

        [Fact]
        public void Clear_WithEmptyStack_ShouldDoNothing()
        {
            // Arrange
            var instance = new LimitedStack<int>();

            // Act
            instance.Clear();

            // Assert
            Assert.True(instance.IsEmpty);
            Assert.Empty(instance);
        }

        [Fact]
        public void GetEnumerator_WithNonEmptyStack_ShouldEnumerateValues()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            // Act
            using IEnumerator<int> enumerator = instance.GetEnumerator();

            // Assert
            Assert.True(enumerator.MoveNext());
            Assert.Equal(3, enumerator.Current);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(2, enumerator.Current);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(1, enumerator.Current);

            Assert.False(enumerator.MoveNext());
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void GetEnumerator_MoveNext_WithNonEmptyStackAndChangedStack_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            using IEnumerator<int> enumerator = instance.GetEnumerator();
            instance.Push(4);

            void TestCode()
            {
                // Act
                enumerator.MoveNext();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public void GetEnumerator_Reset_WithNonEmptyStack_ShouldRestartEnumerableValues()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            using IEnumerator<int> enumerator = instance.GetEnumerator();
            Assert.True(enumerator.MoveNext());
            Assert.Equal(3, enumerator.Current);

            // Act
            enumerator.Reset();

            // Assert
            Assert.True(enumerator.MoveNext());
            Assert.Equal(3, enumerator.Current);
        }

        [Fact]
        public void GetEnumerator_Reset_WithNonEmptyStackAndChangedStack_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            using IEnumerator<int> enumerator = instance.GetEnumerator();
            instance.Push(4);

            void TestCode()
            {
                // Act
                enumerator.Reset();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public void GetEnumerator_Current_WithNotStartedEnumerator_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var instance = new LimitedStack<int>(new[] { 1, 2, 3 });

            using IEnumerator<int> enumerator = instance.GetEnumerator();

            void TestCode()
            {
                // Act
                _ = enumerator.Current;
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }
    }
}