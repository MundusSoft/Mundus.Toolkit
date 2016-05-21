using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Configuration
{
    /// <summary>
    /// Interface that resolves the settings for the application. 
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets a list with all the key names that exists.
        /// </summary>
        /// <returns><see cref="IList{T}"/> with all the key names that exists.</returns>
        IList<string> GetAllKeys();

        /// <summary>
        /// Check is a given <paramref name="key" /> exists within this
        /// application settings.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// <see langword="true" /> if the <paramref name="key"/> exists, <see langword="false"/> otherwise.
        /// </returns>
        bool Exists(string key);

        /// <summary>
        /// Adds or updates an entry with in the application settings for the given <paramref name="key"/> with the given <paramref name="value"/>. 
        /// </summary>
        /// <param name="key">The key to be added or updated.</param>
        /// <param name="value">The value to be set for the given key.</param>
        /// <typeparam name="T">The <see cref="Type"/> of the value.</typeparam>
        void Set<T>(string key, T value);

        /// <summary>
        /// <c>Get's</c> the given <paramref name="key"/> value as string. if no key exists then <see langword="null"/> should be return.
        /// </summary>
        /// <param name="key">The key whose value will be return as a string.</param>
        /// <returns><see cref="string"/> with the value of the <paramref name="key"/>.</returns>
        string GetString(string key);

        /// <summary>
        /// <c>Get's</c> the given <paramref name="key"/> value as <typeparamref name="T"/>. if no key exists then <paramref name="defaultValue"/> should be return.
        /// </summary>
        /// <param name="key">The key whose value will be return</param>
        /// <param name="defaultValue"> The value that will be return in case the given <paramref name="key"/> is not present.</param>
        /// <typeparam name="T">The <see cref="Type"/> of the value to be return.</typeparam>
        /// <returns>The value for the <paramref name="key"/> or <paramref name="defaultValue"/> if the key is not present.</returns>
        T Get<T>(string key, T defaultValue);
    }
}
