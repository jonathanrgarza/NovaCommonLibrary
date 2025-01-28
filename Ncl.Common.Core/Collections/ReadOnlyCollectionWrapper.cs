using System;
using System.Collections;
using System.Collections.Generic;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Collections
{
    /// <summary>
    /// Interface for a readonly collection wrapper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T>
    {
        /// <summary>Determines whether the collection contains a specific value.</summary>
        /// <param name="item">The object to locate in the collection.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> is found in the collection; otherwise, <see langword="false"/>.
        /// </returns>
        bool Contains(T item);

        /// <summary>
        /// Copies the elements of the collection to an <see cref="T:System.Array"/>, starting at a particular
        /// <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied
        /// from collection. The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The number of elements in the source collection is greater than the
        /// available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// </exception>
        void CopyTo(T[] array, int index = 0);
    }

    /// <summary>
    /// Wraps a collection and exposes it as readonly.
    /// </summary>
    /// <typeparam name="T">The type held by the collection.</typeparam>
    public class ReadOnlyCollectionWrapper<T> : ICollection<T>, IReadOnlyCollectionWrapper<T>
    {
        private readonly ICollection<T> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyCollectionWrapper{T}"/> class.
        /// </summary>
        /// <param name="collection">The underlying collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        public ReadOnlyCollectionWrapper(ICollection<T> collection)
        {
            Guard.AgainstNullArgument(nameof(collection), collection);
            _collection = collection;
        }

        // <inheritdoc/>
        public bool IsReadOnly => true;

        // <inheritdoc/>
        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Can not add to a readonly collection");
        }

        // <inheritdoc/>
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("Can not clear to a readonly collection");
        }

        // <inheritdoc/>
        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        // <inheritdoc/>
        public void CopyTo(T[] array, int index = 0)
        {
            _collection.CopyTo(array, index);
        }

        // <inheritdoc/>
        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Can not remove to a readonly collection");
        }

        // <inheritdoc/>
        public int Count => _collection.Count;

        // <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        // <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}