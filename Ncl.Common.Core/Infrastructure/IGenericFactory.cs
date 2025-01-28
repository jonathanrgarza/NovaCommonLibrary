namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    /// Represents a generic factory that creates instances of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    public interface IGenericFactory<out T>
    {
        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The created instance of type <typeparamref name="T"/>.</returns>
        T Create();
    }
}