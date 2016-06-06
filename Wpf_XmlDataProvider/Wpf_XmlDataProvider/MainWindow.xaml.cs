using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Wpf_XmlDataProvider
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
            CultureInfo selected_culture = comboBox.SelectedItem as CultureInfo;

            CulturesHelper.ChangeCulture( selected_culture );
        }

    }
}
