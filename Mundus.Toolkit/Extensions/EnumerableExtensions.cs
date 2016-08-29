using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MToolkit
{
    /// <summary>
    ///     Class holding extension methods for <see cref="IEnumerable" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Gets the index of <paramref name="item" /> within the items enumeration.
        /// </summary>
        /// <param name="items">The <paramref name="items" /> enumeration.</param>
        /// <param name="item">The <paramref name="item" /> to get the index.</param>
        /// <returns>
        ///     the <paramref name="item" /> index within the enumeration if the item belongs to that enumeration, -1 otherwise or
        ///     if the enumeration is null.
        /// </returns>
        public static int IndexOf(this IEnumerable items, object item)
        {
            if (items == null)
            {
                return -1;
            }

            var itemsList = items as IList;
            if (itemsList != null)
            {
                return itemsList.IndexOf(item);
            }

            var enumerator = items.GetEnumerator();
            for (var i = 0;; i++)
            {
                if (!enumerator.MoveNext())
                {
                    return -1;
                }

                if (enumerator.Current == item)
                {
                    return i;
                }
            }
        }

        /// <summary>
        ///     Executes the <paramref name="action" /> with each item in <paramref name="items" />.
        /// </summary>
        /// <typeparam name="T">The enumerable item's type.</typeparam>
        /// <param name="items">The elements to enumerate.</param>
        /// <param name="action">The action to apply to each item in the <paramref name="items" />.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
        /// <summary>
        /// Executes the <paramref name="body" /> function for each element in the <paramref name="source" /> enumeration, asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of objects with in the enumeration</typeparam>
        /// <param name="source">The enumeration to iterate.</param>
        /// <param name="body">The function to be executed when iterating.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="Task"/> that can be awaitable.</returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body, CancellationToken token)
        {
            await ForEachAsync(source, Math.Max(1, Environment.ProcessorCount - 1), body, token);
        }

        /// <summary>
        /// Executes the <paramref name="body" /> function for each element in the <paramref name="source" /> enumeration, asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of objects with in the enumeration</typeparam>
        /// <param name="source">The enumeration to iterate.</param>
        /// <param name="body">The function to be executed when iterating.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="Task"/> that can be awaitable.</returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, CancellationToken, Task> body, CancellationToken token)
        {
            await ForEachAsync(source, Math.Max(1, Environment.ProcessorCount - 1), body, token);
        }

        /// <summary>
        /// Executes the <paramref name="body"/> function for each element in the <paramref name="source"/> enumeration, asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of objects with in the enumeration</typeparam>
        /// <param name="source">The enumeration to iterate.</param>
        /// <param name="parallelCount">how many iterations runs at the same time.</param>
        /// <param name="body">The function to be executed when iterating.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="Task"/> that can be awaitable.</returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, int parallelCount, Func<T, Task> body, CancellationToken token)
        {
            //return await Task.WhenAll(
            var enumerable = Partitioner.Create(source)
                                        .GetPartitions(parallelCount)
                                        .Select(partition =>
                                                Task.Run(async () =>
                                                {
                                                    using (partition)
                                                    {
                                                        while (partition.MoveNext())
                                                        {
                                                            await body(partition.Current);
                                                        }
                                                    }
                                                }, token));
            await Task.WhenAll(enumerable);
        }

        /// <summary>
        /// Executes the <paramref name="body"/> function for each element in the <paramref name="source"/> enumeration, asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of objects with in the enumeration</typeparam>
        /// <param name="source">The enumeration to iterate.</param>
        /// <param name="parallelCount">how many iterations runs at the same time.</param>
        /// <param name="body">The function to be executed when iterating.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="Task"/> that can be awaitable.</returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, int parallelCount, Func<T, CancellationToken, Task> body, CancellationToken token)
        {
            //return await Task.WhenAll(
            var enumerable = Partitioner.Create(source)
                                        .GetPartitions(parallelCount)
                                        .Select(partition =>
                                                Task.Run(async () =>
                                                {
                                                    using (partition)
                                                    {
                                                        while (partition.MoveNext())
                                                        {
                                                            await body(partition.Current, token);
                                                        }
                                                    }
                                                }, token));
            await Task.WhenAll(enumerable);
        }
    }
}
