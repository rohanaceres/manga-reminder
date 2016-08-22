using System;
using System.Diagnostics;
using System.Windows.Input;

namespace RedBeard.App.Logic.Operation
{
	/// <summary>
	/// Commands for performing actions within the installation process.
	/// </summary>
	public class InstallerCommand : ICommand
	{
		/// <summary>
		/// Action to be performedq
		/// </summary>
		private readonly Action<object> action;
		/// <summary>
		/// Action to verify whether the command should be executed or not.
		/// </summary>
		private readonly Predicate<object> predicate;
#pragma warning disable 67
		/// <summary>
		/// Triggered when the status of the action has changed.
		/// </summary>
		public event EventHandler CanExecuteChanged;
#pragma warning restore 67

		/// <summary>
		/// Creates a command with it's <see cref="Action"/> and <see cref="predicate"/> (optional).
		/// </summary>
		/// <param name="action">Action to be made.</param>
		/// <param name="predicate">Action to verify wether the <see cref="action"/> can be made.</param>
		public InstallerCommand (Action<object> action, Predicate<object> predicate = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action), "You must specify an Action<T>.");
			}

			this.action = action;
			this.predicate = predicate;
		}

		/// <summary>
		/// Verify whether the action can be performed or not.
		/// </summary>
		/// <param name="parameter">Predicate parameter.</param>
		/// <returns>If the action is valid.</returns>
		public bool CanExecute (object parameter)
		{
			Debug.WriteLine("Can Execute?");
			if (predicate == null)
			{
				return true;
			}
			return predicate(parameter);
		}
		/// <summary>
		/// Execute the action.
		/// </summary>
		/// <param name="parameter">Action parameter.</param>
		public void Execute (object parameter = null)
		{
			Debug.WriteLine("Will execute!");
			action(parameter);
		}
	}
}
