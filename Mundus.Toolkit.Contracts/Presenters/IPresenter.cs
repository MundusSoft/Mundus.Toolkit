using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MToolkit.Presenters
{
    /// <summary>
    /// Interface that defines the contract for a Presenter.
    /// </summary>
    public interface IPresenter
    {
    }

    /// <summary>
    /// Interface that defines the contract for a presenter of a given <typeparamref name="TView"/> and <typeparamref name="TViewModel"/>
    /// </summary>
    /// <typeparam name="TView">The type of the view for this presenter.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model for this presenter.</typeparam>
    public interface IPresenter<TView, TViewModel> : IPresenter
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        TView View { get; set; }
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        TViewModel ViewModel { get; set; }
    }
}
