using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Collections
{
    /// <summary>
    ///     A stack (First-In-Last-Out) with a maximum capacity.
    ///     The oldest element in the stack is removed when adding a new element to a full stack.
    /// </summary>
    public sealed class LimitedStack<T> : IEnumerable<T>
    {
        /// <summary>
        ///     The default max capacity for the stack.
        /// </summary>
        public const int DefaultMaxCapacity = 25;

        /// <summary>
        ///     The value which represents an unlimited capacity for the stack.
        /// </summary>
        public const int UnlimitedCapacity = 0;

        internal LimitedStackNode<T> _head;
        internal int _version;

        private int _maxCapacity;

        /// <summary>
        ///     Initializes a new instance of <see cref="LimitedStack{T}" /> with
        ///     default max capacity and no entries.
        /// </summary>
        public LimitedStack()
        {
            Debug.Assert(DefaultMaxCapacity >= 0);
            _maxCapacity = DefaultMaxCapacity;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="LimitedStack{T}" /> with
        ///     a given max capacity and no entries.
        /// </summary>
        /// <param name="maxCapacity">The max capacity of the stack.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxCapacity" /> is negative.</exception>
        public LimitedStack(int maxCapacity)
        {
            Guard.AgainstNegativeArgument(nameof(maxCapacity), maxCapacity);
            _maxCapacity = maxCapacity;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="LimitedStack{T}" /> with
        ///     a given max capacity and entries from <paramref name="collection" />.
        ///     If the collection's count is greater than <paramref name="maxCapacity" />, only the last elements are taken.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxCapacity" /> is negative.</exception>
        public LimitedStack(ICollection<T> collection, int maxCapacity = DefaultMaxCapacity) : this(maxCapacity)
        {
            PushRange(collection);
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="LimitedStack{T}" /> with
        ///     a given max capacity and entries from <paramref name="enumerable" />.
        ///     If the enumerable's count is greater than <paramref name="maxCapacity" />, only the last elements are taken.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxCapacity" /> is negative.</exception>
        public LimitedStack(IEnumerable<T> enumerable, int maxCapacity = DefaultMaxCapacity) : this(maxCapacity)
        {
            PushRange(enumerable);
        }

        /// <summary>
        ///     Gets the current number of elements in the stack.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Gets if the stack is currently empty.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        ///     Gets if the stack has an unlimited capacity.
        /// </summary>
        public bool IsUnlimitedCapacity => _maxCapacity == UnlimitedCapacity;

        /// <summary>
        ///     Gets/Sets the max capacity of the stack.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value" /> is negative.</exception>
        public int MaxCapacity
        {
            get => _maxCapacity;
            set
            {
                if (_maxCapacity == value)
                    return;

                Guard.AgainstNegativeArgument(nameof(MaxCapacity), value);

                _maxCapacity = value;
                PerformResize();
            }
        }

        /// <summary>
        ///     Gets the last element in the stack.
        /// </summary>
        private LimitedStackNode<T> Last => _head?._prev;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Pushes the value onto the stack.
        ///     If the stack is at max capacity, the last element is removed from the stack.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Push(T value)
        {
            if (_head == null)
            {
                Debug.Assert(Count == 0);
                var head = new LimitedStackNode<T>(this, value);
                head._next = head;
                head._prev = head;
                _head = head;
                Count = 1;
                _version++;
                return;
            }

            int count = Count;
            Debug.Assert(count > 0);

            LimitedStackNode<T> currentHead = _head;
            LimitedStackNode<T> lastElement = currentHead._next;

            // Create the new head
            var newHead = new LimitedStackNode<T>(this, value)
            {
                _next = currentHead,
                _prev = lastElement
            };

            lastElement._next = newHead;
            currentHead._prev = newHead;

            if (count == 1)
            {
                //The head's previous element was pointing to itself
                currentHead._next = newHead;
            }

            _head = newHead;

            count++;
            _version++;

            int maxCapacity = MaxCapacity;
            if (maxCapacity != UnlimitedCapacity && count > maxCapacity)
            {
                RemoveLastNode();
                return;
            }

            Count = count;
        }

        /// <summary>
        ///     Pushes a range of elements onto the stack.
        ///     The last element of the range will become the top of the stack.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        public void PushRange(ICollection<T> collection)
        {
            Guard.AgainstNullArgument(nameof(collection), collection);

            if (collection.Count == 0)
                return;

            int maxCapacity = _maxCapacity;
            if (maxCapacity != UnlimitedCapacity && collection.Count >= maxCapacity)
            {
                Clear();
            }

            PushRange((IEnumerable<T>) collection);
        }

        /// <summary>
        ///     Pushes a range of elements onto the stack.
        ///     The last element of the range will become the top of the stack.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable" /> is <see langword="null" />.</exception>
        public void PushRange(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            GenerateNodesFromEnumerable(enumerable, out int count, out LimitedStackNode<T> newHead,
                out LimitedStackNode<T> currentNode);

            if (count == 0)
                return;

            Debug.Assert(newHead != null && currentNode != null);

            LimitedStackNode<T> oldHead = _head;
            if (oldHead == null)
            {
                _head = newHead;
                Count = count;
                _version++;
                return;
            }

            LimitedStackNode<T> lastNode = oldHead._next;

            int maxCapacity = MaxCapacity;
            if (!EnsureEnoughRoom(count))
            {
                oldHead = _head;
                Debug.Assert(oldHead != null);
                lastNode = oldHead._next;
            }

            oldHead._prev = currentNode;
            currentNode._next = oldHead;

            lastNode._next = newHead;
            newHead._prev = lastNode;

            _head = newHead;
            Count += count;
            _version++;
            Debug.Assert(IsUnlimitedCapacity || Count <= maxCapacity);
        }

        /// <summary>
        ///     Pops the top element of the stack.
        /// </summary>
        /// <returns>The top element.</returns>
        /// <exception cref="InvalidOperationException"><see cref="IsEmpty" /> is <see langword="true" />.</exception>
        public T Pop()
        {
            LimitedStackNode<T> poppedNode = PopHeadNode();

            if (poppedNode == null)
                throw new InvalidOperationException("No elements in the stack");

            return poppedNode.Value;
        }

        /// <summary>
        ///     Tries to pop the top element of the stack.
        /// </summary>
        /// <param name="value">Out: The top element of the stack or <see langword="default" /> if empty stack.</param>
        /// <returns><see langword="true" /> if the top element was popped, otherwise, <see langword="false" />.</returns>
        public bool TryPop(out T value)
        {
            LimitedStackNode<T> poppedNode = PopHeadNode();

            if (poppedNode == null)
            {
                value = default;
                return false;
            }

            value = poppedNode.Value;
            return true;
        }

        /// <summary>
        ///     Peeks at the top element of the stack without removing it.
        /// </summary>
        /// <returns>The top element.</returns>
        /// <exception cref="InvalidOperationException"><see cref="IsEmpty" /> is <see langword="true" />.</exception>
        public T Peek()
        {
            LimitedStackNode<T> headNode = _head;

            if (headNode == null)
                throw new InvalidOperationException("No elements in the stack");

            return headNode.Value;
        }

        /// <summary>
        ///     Tries to peek at pop the top element of the stack.
        /// </summary>
        /// <param name="value">Out: The top element of the stack or <see langword="default" /> if empty stack.</param>
        /// <returns><see langword="true" /> if not an empty stack, otherwise, <see langword="false" />.</returns>
        public bool TryPeek(out T value)
        {
            LimitedStackNode<T> headNode = _head;

            if (headNode == null)
            {
                value = default;
                return false;
            }

            value = headNode.Value;
            return true;
        }

        /// <summary>
        ///     Clears all elements from the stack.
        /// </summary>
        public void Clear()
        {
            LimitedStackNode<T> current = _head;
            if (current == null)
                return;

            while (current != null)
            {
                LimitedStackNode<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            _head = null;
            Count = 0;
            _version++;
        }

        /// <summary>
        ///     Pops the head node and decrements the count.
        ///     If the stack is empty, returns <see langword="null" />.
        /// </summary>
        /// <returns>The head node or <see langword="null" />.</returns>
        private LimitedStackNode<T> PopHeadNode()
        {
            if (_head == null)
            {
                Debug.Assert(Count == 0);
                return null;
            }

            LimitedStackNode<T> currentHead = _head;

            if (Count == 1)
            {
                Debug.Assert(currentHead._prev == currentHead._next);
                _head = null;
                currentHead.Invalidate();
                Count = 0;
                _version++;
                return currentHead;
            }

            LimitedStackNode<T> nextElement = currentHead._next;
            LimitedStackNode<T> lastElement = currentHead._prev;

            nextElement._prev = lastElement;
            lastElement._next = nextElement;

            currentHead.Invalidate();
            Count--;
            _version++;
            return currentHead;
        }

        private void RemoveLastNode()
        {
            if (_head == null)
            {
                Debug.Fail("_head should not be null when trying to remove last node");
                return;
            }

            LimitedStackNode<T> lastNode = Last;
            LimitedStackNode<T> nextLastNode = lastNode._prev;
            LimitedStackNode<T> headNode = lastNode._next;

            headNode._prev = nextLastNode;
            nextLastNode._next = headNode;

            lastNode.Invalidate();

            _version++;
        }

        private void RemoveLastNodes(int count)
        {
            Debug.Assert(count > 0, "count should be greater than zero when trying to remove last nodes");

            if (_head == null)
            {
                Debug.Fail("_head should not be null when trying to remove last nodes");
                return;
            }

            if (Count == 1)
            {
                _head.Invalidate();
                _head = null;
                Count = 0;
                _version++;
                return;
            }

            if (count >= Count)
            {
                Clear();
                return;
            }

            LimitedStackNode<T> lastNode = Last;
            for (int i = 0; i < count; i++)
            {
                LimitedStackNode<T> temp = lastNode;
                lastNode = temp._prev;
                temp.Invalidate();
            }

            LimitedStackNode<T> headNode = _head;
            headNode._prev = lastNode;
            lastNode._next = headNode;

            Count -= count;
            _version++;
            Debug.Assert(Count <= _maxCapacity);
        }

        private void PerformResize()
        {
            if (IsUnlimitedCapacity)
                return;

            int count = Count;
            int maxCapacity = MaxCapacity;
            if (count <= maxCapacity)
                return;

            //Resize
            RemoveLastNodes(count - maxCapacity);
        }

        private void GenerateNodesFromEnumerable(IEnumerable<T> enumerable, out int count, out LimitedStackNode<T> head,
            out LimitedStackNode<T> end)
        {
            Debug.Assert(enumerable != null);

            count = 0;
            head = null;
            end = null;

            int maxCapacity = MaxCapacity;

            IEnumerable<T> reversedEnumerable = enumerable.Reverse();

            foreach (T value in reversedEnumerable)
            {
                if (count == 0)
                {
                    head = new LimitedStackNode<T>(this, value);
                    end = head;
                    count++;

                    if (maxCapacity == 1)
                        break;

                    continue;
                }

                Debug.Assert(end != null);
                LimitedStackNode<T> previousNode = end;
                end = new LimitedStackNode<T>(this, value)
                {
                    _prev = previousNode
                };

                previousNode._next = end;
                count++;

                if (count >= maxCapacity)
                    break;
            }

            if (head == null)
                return;

            Debug.Assert(end != null);
            head._prev = end;
            end._next = head;
        }

        /// <summary>
        ///     Ensures there is enough room in the stack.
        /// </summary>
        /// <param name="additionalCount">The count being added to the stack.</param>
        /// <returns><see langword="false" /> if the stack was modified to add room, otherwise, <see langword="true" />.</returns>
        private bool EnsureEnoughRoom(int additionalCount)
        {
            if (additionalCount <= 0)
                return true;

            int maxCapacity = MaxCapacity;

            if (maxCapacity == UnlimitedCapacity)
                return true;

            int currentCount = Count;
            int totalCount = additionalCount + currentCount;
            if (totalCount <= maxCapacity)
                return true;

            RemoveLastNodes(totalCount - maxCapacity);
            return false;
        }

        /// <summary>
        ///     An enumerator for a <see cref="LimitedStackNode{T}" />.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly LimitedStack<T> _stack;
            private readonly int _version;
            private int _index;
            private LimitedStackNode<T> _currentElement;

            /// <summary>
            ///     Initializes a new instance of <see cref="Enumerator" />.
            /// </summary>
            /// <param name="stack">The stack.</param>
            internal Enumerator(LimitedStack<T> stack)
            {
                _stack = stack;
                _version = stack._version;
                _index = -2; // Not started status
                _currentElement = default;
            }

            /// <inheritdoc />
            public void Dispose()
            {
                _index = -1; //Ended status
            }

            /// <inheritdoc />
            public bool MoveNext()
            {
                bool anotherElementAvailable;
                if (_version != _stack._version)
                {
                    throw new InvalidOperationException(
                        @"Collection was modified after the enumerator was instantiated.");
                }

                switch (_index)
                {
                    case -2:
                    {
                        // First call to enumerator.
                        _index = _stack.Count - 1;
                        anotherElementAvailable = _index >= 0;
                        if (anotherElementAvailable)
                        {
                            _currentElement = _stack._head;
                        }

                        return anotherElementAvailable;
                    }
                    case -1:
                        // End of enumeration.
                        return false;
                }

                anotherElementAvailable = --_index >= 0;
                if (anotherElementAvailable)
                {
                    _currentElement = _currentElement._next;
                    return true;
                }

                _currentElement = default;
                return false;
            }

            /// <inheritdoc />
            public T Current
            {
                get
                {
                    if (_index < 0)
                        ThrowEnumerationNotStartedOrEnded(); //No braces on purpose

                    return _currentElement.Value;
                }
            }

            private void ThrowEnumerationNotStartedOrEnded()
            {
                Debug.Assert(_index == -1 || _index == -2);
                throw new InvalidOperationException(_index == -2
                    ? @"Enumeration has not started. Call MoveNext."
                    : @"Enumeration already finished.");
            }

            /// <inheritdoc />
            object IEnumerator.Current => Current;

            /// <inheritdoc />
            void IEnumerator.Reset()
            {
                if (_version != _stack._version)
                {
                    throw new InvalidOperationException(
                        @"Collection was modified after the enumerator was instantiated.");
                }

                _index = -2;
                _currentElement = default;
            }
        }
    }
}