using System;
using System.Collections;
using System.Collections.Generic;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Collections
{
    /// <summary>
    /// Represents a read-only stack.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    public interface IReadOnlyStack<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Copies the stack to an existing one-dimensional array, starting at the specified array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the stack.
        /// The array must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in the array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of elements in the source stack is greater than the
        /// available space from index to the end of the destination array.
        /// </exception>
        void CopyTo(T[] array, int index = 0);

        /// <summary>
        /// Determines whether an element is in the <see cref="IReadOnlyStack{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the stack. The value can be null for reference types.</param>
        /// <returns>true if the item is found in the stack; otherwise, false.</returns>
        bool Contains(T item);

        /// <summary>
        /// Returns the object at the top of the <see cref="IReadOnlyStack{T}"/> without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
        /// <returns>The object at the top of the stack.</returns>
        T Peek();

        /// <summary>
        /// Returns a value that indicates whether there is an object at the top of the <see cref="IReadOnlyStack{T}"/>,
        /// and if one is present, copies it to the <paramref name="result"/> parameter.
        /// The object is not removed from the stack.
        /// </summary>
        /// <param name="result">
        /// If present, the object at the top of the stack; otherwise, the default value of
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>true if there is an object at the top of the stack; false if the stack is empty.</returns>
        bool TryPeek(out T result);
    }

    /// <summary>
    /// Represents a read-only stack.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    public class ReadOnlyStack<T> : IReadOnlyStack<T>, ICollection
    {
        private readonly Stack<T> _stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyStack{T}"/> class.
        /// </summary>
        /// <param name="stack">The stack to wrap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stack"/> is null.</exception>
        public ReadOnlyStack(Stack<T> stack)
        {
            Guard.AgainstNullArgument(nameof(stack), stack);
            _stack = stack;
        }

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_stack).CopyTo(array, index);
        }

        /// <inheritdoc/>
        bool ICollection.IsSynchronized => false;

        /// <inheritdoc/>
        object ICollection.SyncRoot => _stack is ICollection coll ? coll.SyncRoot : this;

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return _stack.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc cref="IReadOnlyStack{T}"/>
        public int Count => _stack.Count;

        /// <summary>
        /// Determines whether an element is in the <see cref="ReadOnlyStack{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the stack. The value can be null for reference types.</param>
        /// <returns>true if the item is found in the stack; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return _stack.Contains(item);
        }

        /// <summary>
        /// Returns the object at the top of the <see cref="ReadOnlyStack{T}"/> without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
        /// <returns>The object at the top of the stack.</returns>
        public T Peek()
        {
            return _stack.Peek();
        }

        /// <summary>
        /// Returns a value that indicates whether there is an object at the top of the <see cref="ReadOnlyStack{T}"/>,
        /// and if one is present, copies it to the <paramref name="result"/> parameter.
        /// The object is not removed from the stack.
        /// </summary>
        /// <param name="result">
        /// If present, the object at the top of the stack; otherwise, the default value of
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>true if there is an object at the top of the stack; false if the stack is empty.</returns>
        public bool TryPeek(out T result)
        {
            if (_stack.Count <= 0)
            {
                result = default;
                return false;
            }

            result = _stack.Peek();
            return true;
        }

        /// <summary>
        /// Copies the stack to an existing one-dimensional array, starting at the specified array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the stack.
        /// The array must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in the array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of elements in the source stack is greater than the
        /// available space from index to the end of the destination array.
        /// </exception>
        public void CopyTo(T[] array, int index = 0)
        {
            _stack.CopyTo(array, index);
        }
    }
}