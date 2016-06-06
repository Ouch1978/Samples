using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_ObjectDataSource
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

            if( Properties.Resources.Culture != null && !Properties.Resources.Culture.Equals( selected_culture ) )
            {
                CulturesHelper.ChangeCulture( selected_culture );
            }
        }

    }
}
