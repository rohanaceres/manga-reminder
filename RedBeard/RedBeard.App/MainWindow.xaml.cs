using RedBeard.App.Logic.Job;
using RedBeard.Model;
using System.Windows;

namespace RedBeard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAddEntry(object sender, RoutedEventArgs e)
        {

        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            PaniniAlertEntry entry1 = new PaniniAlertEntry();
            entry1.Email = "ceres.rohana@gmail.com";
            entry1.Links.Add(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055");
            entry1.Links.Add(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=251459");
            entry1.Links.Add(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=251452");

            PaniniAlertEntry entry2 = new PaniniAlertEntry();
            entry2.Email = "Leaolucylia@gmail.com";
            entry2.Links.Add(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055");

            PaniniCrawlerJob.Run(new PaniniAlertEntry[] { entry1, entry2 });
        }
    }
}
