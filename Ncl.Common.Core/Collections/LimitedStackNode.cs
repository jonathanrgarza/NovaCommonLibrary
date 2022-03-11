namespace Ncl.Common.Core.Collections
{
    /// <summary>
    ///     A node for use with the <see cref="LimitedStack{T}" />.
    /// </summary>
    /// <typeparam name="T">The type for the value.</typeparam>
    internal sealed class LimitedStackNode<T>
    {
        internal LimitedStackNode<T> _next;
        internal LimitedStackNode<T> _prev;
        internal LimitedStack<T> _stack;
        private T _value;

        /// <summary>
        ///     Initializes a new instance of <see cref="LimitedStackNode{T}" />.
        /// </summary>
        /// <param name="stack">The parent stack.</param>
        /// <param name="value">The value for the node.</param>
        internal LimitedStackNode(LimitedStack<T> stack, T value)
        {
            _stack = stack;
            _value = value;
        }

        /// <summary>
        ///     Gets the next node.
        /// </summary>
        public LimitedStackNode<T> Next => _next == null || _next == _stack?._head ? null : _next;

        /// <summary>
        ///     Gets the previous node.
        /// </summary>
        public LimitedStackNode<T> Previous => _prev == null || _prev == _stack?._head ? null : _prev;

        /// <summary>
        ///     Gets the <see cref="LimitedStack{T}" /> this node is associated with.
        /// </summary>
        public LimitedStack<T> Stack => _stack;

        /// <summary>
        ///     Gets/Sets the value contained in the node.
        /// </summary>
        public T Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        ///     Gets the value contained in the node, by reference.
        /// </summary>
        public ref T ValueRef => ref _value;

        /// <summary>
        ///     Invalidates this node by setting all node references to <see langword="null" />.
        /// </summary>
        internal void Invalidate()
        {
            _stack = null;
            _next = null;
            _prev = null;
        }
    }
}