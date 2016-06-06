using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SL_XboxAvatarImage
{
    public partial class MainPage : UserControl
    {
        private ObservableCollection<string> _gamerTags = new ObservableCollection<string> { "Ouch Liu" , "Yuan0716" , "Risky Liu" , "RighteousOhio51" , "WaviestToast236" , "DigitalCape9150" , "MinusPrawn8747" };

        public MainPage()
        {
            InitializeComponent();

            this.lstAvatarList.ItemsSource = _gamerTags;
        }

        private void btnAddGamerTag_Click( object sender , RoutedEventArgs e )
        {
            if( txtGamerTag.Text.Length > 0 && !_gamerTags.Contains( txtGamerTag.Text ) )
            {
                _gamerTags.Add( txtGamerTag.Text );
            }
        }
    }
}
