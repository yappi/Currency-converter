using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IEnumerable<Rate> Rates;
        public MainWindow()
        {                      
            InitializeComponent();            
            CheckIntConection.CheckInternetConection();            
            SynchronizationContext uiContext = SynchronizationContext.Current;
            Thread thread = new Thread(Run);
            thread.Start(uiContext);
        } 

        private void InputValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetCurrency getCurrency = new GetCurrency();
            double.TryParse(InputValue.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double uah);
            UsdOutputValue.Text = getCurrency.GetCur(Rates, "USD", uah);
            EurOutputValue.Text = getCurrency.GetCur(Rates, "EUR", uah);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Run(object state)
        {
            SynchronizationContext uiContext = state as SynchronizationContext;
            GetXml getXml = new GetXml();
            getXml.TryGetXml(ref Rates);
            uiContext.Post(UpdateUI, state);
        }

        private void UpdateUI(object state)
        { 
            InputValue.IsReadOnly = false;
            LabelStatus.Foreground = new SolidColorBrush(Colors.Green);
            LabelStatus.Content = "Ready";
        }
    }
}
