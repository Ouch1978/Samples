using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Expression.Blend.SampleData.SampleDataSource;

namespace SL_ListBoxItemWithAnimation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click( object sender , RoutedEventArgs e )
        {
            ( listBox.DataContext as SampleDataSource ).Collection.Insert( 0 , new Item { Name = string.Format( "NewItem{0}" , ( listBox.DataContext as SampleDataSource ).Collection.Count ) , Image = ( listBox.DataContext as SampleDataSource ).Collection.ElementAt( 0 ).Image } );

            if( listBox.Items.Count > 1 )
            {
                btnRemoveItem.IsEnabled = true;
            }

        }

        private void btnRemoveItem_Click( object sender , RoutedEventArgs e )
        {
            if( ( listBox.DataContext as SampleDataSource ).Collection.Count > 0 )
            {
                ( listBox.DataContext as SampleDataSource ).Collection.RemoveAt( 0 );
            }

            if( listBox.Items.Count <= 1 )
            {
                btnRemoveItem.IsEnabled = false;
            }

        }
    }
}
