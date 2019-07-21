using System.Windows;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class Error : Window
    {
        public Error()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TryAgain(object sender, RoutedEventArgs e)
        {
            CheckIntConection.CheckInternetConection();
            this.Close();
        }
    }
}
