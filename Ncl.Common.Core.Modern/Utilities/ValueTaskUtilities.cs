namespace Ncl.Common.Modern.Core.Utilities
{
    /// <summary>
    /// Provides utility methods for working with <see cref="ValueTask{T}"/> instances.
    /// </summary>
    public static class ValueTaskUtilities
    {
        /// <summary>
        /// Waits for all the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns an array of the results.
        /// </summary>
        /// <typeparam name="T">The type of the results.</typeparam>
        /// <param name="tasks">The array of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>An array containing the results of all the completed tasks.</returns>
        public static async ValueTask<T[]> WhenAll<T>(params ValueTask<T>[] tasks)
        {
            return await WhenAll((IReadOnlyList<ValueTask<T>>)tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Waits for all the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns an array of the results.
        /// </summary>
        /// <typeparam name="T">The type of the results.</typeparam>
        /// <param name="tasks">The collection of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>An array containing the results of all the completed tasks.</returns>
        public static ValueTask<T[]> WhenAll<T>(IEnumerable<ValueTask<T>> tasks)
        {
            return WhenAll(tasks.ToList());
        }

        /// <summary>
        /// Waits for all the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns an array of the results.
        /// </summary>
        /// <typeparam name="T">The type of the results.</typeparam>
        /// <param name="tasks">The read-only list of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>An array containing the results of all the completed tasks.</returns>
        public static async ValueTask<T[]> WhenAll<T>(IReadOnlyList<ValueTask<T>> tasks)
        {
            ArgumentNullException.ThrowIfNull(tasks);
            if (tasks.Count == 0)
                return [];

            // We don't allocate the list if no task throws
            List<Exception>? exceptions = null;

            var results = new T[tasks.Count];
            for (int i = 0; i < tasks.Count; i++)
                try
                {
                    results[i] = await tasks[i].ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>(tasks.Count);
                    exceptions.Add(ex);
                }

            return exceptions is null
                ? results
                : throw new AggregateException(exceptions);
        }

        /// <summary>
        /// Waits for any of the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns the result of the first completed task.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="tasks">The array of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>The result of the first completed task, or <c>null</c> if all tasks are canceled.</returns>
        public static async ValueTask<T> WhenAny<T>(params ValueTask<T>[] tasks)
        {
            return await WhenAny((IReadOnlyList<ValueTask<T>>)tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Waits for any of the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns the result of the first completed task.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="tasks">The collection of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>The result of the first completed task, or <c>null</c> if all tasks are canceled.</returns>
        public static ValueTask<T> WhenAny<T>(IEnumerable<ValueTask<T>> tasks)
        {
            return WhenAny(tasks.ToList());
        }

        /// <summary>
        /// Waits for any of the specified <see cref="ValueTask{T}"/> instances to complete execution, and returns the result of the first completed task.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="tasks">The read-only list of <see cref="ValueTask{T}"/> instances to wait for.</param>
        /// <returns>The result of the first completed task, or <c>null</c> if all tasks are canceled.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tasks"/> was <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="tasks"/> was empty.</exception>
        public static async ValueTask<T> WhenAny<T>(IReadOnlyList<ValueTask<T>> tasks)
        {
            ArgumentNullException.ThrowIfNull(tasks);
            if (tasks.Count == 0)
                throw new ArgumentException("The collection of tasks cannot be empty.", nameof(tasks));

            foreach (var task in tasks)
            {
                if (task.IsCompletedSuccessfully)
                    return task.Result;

                if (task.IsFaulted)
                    throw task.AsTask().Exception!;

                if (task.IsCanceled)
                    throw new TaskCanceledException(task.AsTask());
            }

            //Nothing completed synchronously, so we need to wait for one to complete
            var completedTask = await Task.WhenAny(tasks.Select(t => t.AsTask())).ConfigureAwait(false);

            if (completedTask.IsFaulted)
                throw completedTask.Exception!;

            if (completedTask.IsCanceled)
                throw new TaskCanceledException(completedTask);

            return completedTask.Result;
        }
    }
}
