using System;
using System.Reflection;
using System.Windows.Controls;
using System.Linq;

namespace RedBeard.App.Logic.Operation
{
	/// <summary>
	/// Navigable service during the installation.
	/// </summary>
	public class NavigableService
	{
		/// <summary>
		/// Frame to contain the current page during the navigation.
		/// </summary>
		protected readonly Frame Frame;

		/// <summary>
		/// Create an instance with the specified <see cref="Frame"/>.
		/// </summary>
		/// <param name="frame">Responsible for containing the current page during the navigation.</param>
		public NavigableService (Frame frame)
		{
			this.Frame = frame;
		}

		/// <summary>
		/// Goes back to the previous page.
		/// </summary>
		public void GoBack ()
		{
			this.Frame.GoBack();
		}
		/// <summary>
		/// Goes forward to the next page.
		/// </summary>
		public void GoForward ()
		{
			this.Frame.GoForward();
		}
		/// <summary>
		/// Navigate to a specific page by it's generic type.
		/// </summary>
		/// <typeparam name="T">Generic page type to be navigated to.</typeparam>
		/// <param name="parameter">Navigation parameter.</param>
		/// <returns>Whether the navigation was succeeded or not.</returns>
		public bool Navigate<T> (object parameter = null)
		{
			var type = typeof(T);
			return Navigate(type, parameter);
		}
		/// <summary>
		/// Navigate to a specific page by it's type.
		/// </summary>
		/// <typeparam name="T">Generic page type to be navigated to.</typeparam>
		/// <param name="parameter">Navigation parameter.</param>
		/// <returns>Whether the navigation was succeeded or not.</returns>
		public bool Navigate (Type source, object parameter = null)
		{
			var src = Activator.CreateInstance(source);
			return this.Frame.Navigate(src, parameter);
		}
		/// <summary>
		/// Navigate to a specific page by it's reflected type name.
		/// </summary>
		/// <param name="page">Page reflected name.</param>
		/// <returns>Whether the navigation was succeeded or not.</returns>
		public bool Navigate (string page)
		{
			var type =  Assembly.GetExecutingAssembly()
								.GetTypes()
								.SingleOrDefault(a => a.Name.Equals(page));

			if (type == null) { return false; }

			var src = Activator.CreateInstance(type);
			return this.Frame.Navigate(src);
		}
	}
}
