using System.Threading.Tasks;

namespace MToolkit.Navigation
{
    /// <summary>
    /// An interface defining how navigation between presenters should be performed asynchronous.
    /// </summary>
  public  interface INavigationAsync
    {
        /// <summary>
        /// Discards the current presenter from the navigation stack and shows the previous one if any exist asynchronous.
        /// </summary>
        Task PopAsync();

        /// <summary>
        /// Pushes the <typeparam name="T">presenter</typeparam> to the top of the navigations stack asynchronous.
        /// </summary>
        /// <typeparam name="T">The presenter to be pushed to the top of the navigation stack</typeparam>
        /// <returns>The <typeparam name="T">presenter</typeparam> that got push.</returns>
        Task<T> PushAsync<T>();

        /// <summary>
        /// Pushes the <typeparam name="T">presenter</typeparam> to the top of the navigations stack asynchronous.
        /// </summary>
        /// <typeparam name="T">The presenter to be pushed to the top of the navigation stack</typeparam>
        /// <param name="withAnimation">if set to <c>true</c> then an animation is shown.</param>
        /// <returns>The <typeparam name="T">presenter</typeparam> that got push.</returns>
        Task<T> PushAsync<T>(bool withAnimation);
    }
}
