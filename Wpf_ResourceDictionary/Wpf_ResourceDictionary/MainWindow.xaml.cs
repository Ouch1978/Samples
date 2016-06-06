using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_ResourceDictionary
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

        private void comboBox_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            CulturesHelper.ChangeCulture( ( comboBox.SelectedItem as CultureInfo ) );
        }
    }
}
