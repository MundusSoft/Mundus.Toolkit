using MyMvvm.Presenters;

namespace MyMvvm.Navigation
{
    /// <summary>
    /// An interface defining how navigation between views should be performed.
    /// </summary>
  public  interface INavigation
    {
        /// <summary>
        /// Discards the current presenter from the navigation stack and shows the previous one if any exists.
        /// </summary>
        void Pop();

        /// <summary>
        /// Pushes the <typeparam name="T">presenter</typeparam> to the top of the navigations stack.
        /// </summary>
        /// <typeparam name="T">The presenter to be pushed to the top of the navigation stack</typeparam>
        /// <returns>The <typeparam name="T">presenter</typeparam> that got push.</returns>
        T Push<T>() where T: IPresenter;

        /// <summary>
        /// Pushes the <typeparam name="T">presenter</typeparam> to the top of the navigations stack.
        /// </summary>
        /// <typeparam name="T">The presenter to be pushed to the top of the navigation stack</typeparam>
        /// <param name="withAnimation">if set to <c>true</c> then an animation is shown.</param>
        /// <returns>The <typeparam name="T">presenter</typeparam> that got push.</returns>
        T Push<T>(bool withAnimation) where T : IPresenter;
    }
}
