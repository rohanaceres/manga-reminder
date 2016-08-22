using RedBeard.App.Logic.Operation;

namespace RedBeard.App.Logic.NavigablePage
{
	/// <summary>
	/// Abstract navigable page. Contains properties that all navigable pages
	/// should have.
	/// </summary>
	public abstract class AbstractNavigablePage
	{
        /// <summary>
        /// Service which will navigate through the pages.
        /// </summary>
        protected readonly NavigableService navigationService;
        /// <summary>
        /// Current page name.
        /// </summary>
        public abstract string ViewPageName { get; }

        /// <summary>
        /// Creates an instance of an specified page.
        /// </summary>
        /// <param name="navigationService">Service that will perform navigation.</param>
        public AbstractNavigablePage (NavigableService navigationService)
		{
			this.navigationService = navigationService;
		}
	}
}
