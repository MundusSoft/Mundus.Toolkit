using System;
using System.Collections;
using System.Collections.Generic;

namespace MyMvvm
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
        ///     Adds all the items of the collection to the <paramref name="source" /> collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="itemsToAdd">The items to add.</param>
        public static void AddRange<T>(this IList<T> source, IList<T> itemsToAdd)
        {
            if (source == null || itemsToAdd == null)
            {
                return;
            }
            foreach (var item in itemsToAdd)
            {
                source.Add(item);
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
    }
}
