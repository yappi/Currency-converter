using System.Windows;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for ErrorURL.xaml
    /// </summary>
    public partial class ErrorURL : Window
    {
        public ErrorURL()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
