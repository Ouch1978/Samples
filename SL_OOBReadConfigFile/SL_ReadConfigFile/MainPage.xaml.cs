using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SL_ReadConfigFile
{
    public partial class MainPage : UserControl
    {
        private IDictionary<string , string> _configurations;
        public MainPage()
        {
            InitializeComponent();

            _configurations =  ( Application.Current as App ).Configurations;

            if( _configurations.Count > 0 )
            {
                numericUpDown.Minimum = 1;
                numericUpDown.Maximum = _configurations.Count;
            }

        }

        private void btnBindConfig_Click( object sender , RoutedEventArgs e )
        {
            this.dgdConfigurations.ItemsSource = _configurations;
        }

        private void btnShowEntry_Click( object sender , RoutedEventArgs e )
        {
            string entryContent = string.Format( "{0} = {1}" , _configurations.ElementAt( ( int ) numericUpDown.Value - 1 ).Key , _configurations.ElementAt( ( int ) numericUpDown.Value - 1 ).Value );
            MessageBox.Show( entryContent );
        }
    }
}
