using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Tests.Infrastructure
{
    public class EmptyTests
    {
        [Fact]
        public void Array_WithNothing_ShouldReturnEmptyArray()
        {
            // Act
            int[] actual = Empty<int>.Array();

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void Array_WithMultipleCalls_ShouldReturnSameEmptyArray()
        {
            // Act
            int[] first = Empty<int>.Array();
            int[] second = Empty<int>.Array();

            // Assert
            Assert.Same(first, second);
        }

        [Fact]
        public void List_WithNothing_ShouldReturnEmptyIList()
        {
            // Act
            IList<int> actual = Empty<int>.List();

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void List_WithMultipleCalls_ShouldReturnSameEmptyList()
        {
            // Act
            IList<int> first = Empty<int>.List();
            IList<int> second = Empty<int>.List();

            // Assert
            Assert.Same(first, second);
        }

        [Fact]
        public void Enumerable_WithNothing_ShouldReturnEmptyIEnumerable()
        {
            // Act
            IEnumerable<int> actual = Empty<int>.Enumerable();

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void Enumerable_WithMultipleCalls_ShouldReturnSameEmptyEnumerable()
        {
            // Act
            IEnumerable<int> first = Empty<int>.Enumerable();
            IEnumerable<int> second = Empty<int>.Enumerable();

            // Assert
            Assert.Same(first, second);
        }

        [Fact]
        public void ReadOnlyCollection_WithNothing_ShouldReturnEmptyReadOnlyCollection()
        {
            // Act
            ReadOnlyCollection<int> actual = Empty<int>.ReadOnlyCollection();

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadOnlyCollection_WithMultipleCalls_ShouldReturnSameEmptyReadOnlyCollection()
        {
            // Act
            ReadOnlyCollection<int> first = Empty<int>.ReadOnlyCollection();
            ReadOnlyCollection<int> second = Empty<int>.ReadOnlyCollection();

            // Assert
            Assert.Same(first, second);
        }

        [Fact]
        public void ReadOnlyDictionary_WithNothing_ShouldReturnEmptyDictionary()
        {
            // Act
            ReadOnlyDictionary<int, int> actual = Empty<int, int>.ReadOnlyDictionary();

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadOnlyDictionary_WithMultipleCalls_ShouldReturnSameEmptyDictionary()
        {
            // Act
            ReadOnlyDictionary<int, int> first = Empty<int, int>.ReadOnlyDictionary();
            ReadOnlyDictionary<int, int> second = Empty<int, int>.ReadOnlyDictionary();

            // Assert
            Assert.Same(first, second);
        }
    }
}
