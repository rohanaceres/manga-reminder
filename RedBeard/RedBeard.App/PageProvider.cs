using RedBeard.App.Logic.NavigablePage;

namespace RedBeard.App
{
    /// <summary>
	/// Provides instances for all pages business logic.
	/// </summary>
	public class PageProvider
    {
        public ConfigurationPageLogic ConfigurationPageLogic => new ConfigurationPageLogic(App.Navigation);
        public RegisterPageLogic RegisterPageLogic = new RegisterPageLogic(App.Navigation);
    }
}
