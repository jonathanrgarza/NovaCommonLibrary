using System;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    /// Represents a generic factory that creates instances of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    public class GenericFactory<T> : IGenericFactory<T>
    {
        private readonly Func<T> _createMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFactory{T}"/> class.
        /// </summary>
        /// <param name="createMethod">The method used to create instances of type <typeparamref name="T"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="createMethod"/> is null.</exception>
        public GenericFactory(Func<T> createMethod)
        {
            _createMethod = createMethod ?? throw new ArgumentNullException(nameof(createMethod));
        }

        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The created instance of type <typeparamref name="T"/>.</returns>
        public T Create()
        {
            return _createMethod();
        }
    }
}