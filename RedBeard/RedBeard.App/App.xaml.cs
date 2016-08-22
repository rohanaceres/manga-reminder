using RedBeard.App.Logic.Operation;
using RedBeard.App.Pages;
using System.Windows;

namespace RedBeard.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static NavigableService Navigation;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Show main window:
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Go to first page - ReadInformationPage
            Navigation = new NavigableService(mainWindow.MyFrame);
            Navigation.Navigate<RegisterPage>();
        }
    }
}
