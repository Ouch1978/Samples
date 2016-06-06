using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace SL_CompareCollections
{
    public partial class MainPage : UserControl
    {
        private List<string> _collectionA = new List<string> { "2" , "Ouch" , "Visual Studio" , "Wei" , "Mango" , "WP7" , "3" , "Silverlight" , "RIA" };
        private List<string> _collectionB = new List<string> { "Mango" , "3" , "Windows" , "Ouch" , "WPF" , "RIA" , "Metro UI" , "Yuan" , "4" };

        public MainPage()
        {
            InitializeComponent();

            lstCollectionA.ItemsSource = _collectionA;

            lstCollectionB.ItemsSource = _collectionB;
        }

        private void ResetListBoxes()
        {
            _collectionA.ForEach
                ( item =>
                    {
                        ( lstCollectionA.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.White );
                    }
                );

            _collectionB.ForEach
                ( item =>
                    {
                        ( lstCollectionB.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.White );
                    }
                );
        }

        private void btnBoth_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            ResetListBoxes();

            //也可以這樣寫
            //List<string> result = _collectionA.Where( item => _collectionB.Contains( item ) ).ToList();

            List<string> result = _collectionA.Intersect( _collectionB ).ToList();


            result.ForEach
                ( item =>
                    {
                        ( lstCollectionA.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.Red );

                        ( lstCollectionB.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.Red );
                    }
                );
        }

        private void btnOnlyA_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            ResetListBoxes();

            //也可以這樣寫
            //List<string> result = _collectionA.Where( item => _collectionB.Contains( item ) == false ).ToList();

            List<string> result = _collectionA.Except( _collectionB ).ToList();

            result.ForEach
                ( item =>
                    {
                        ( lstCollectionA.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.Red );
                    }
                );
        }

        private void btnOnlyB_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            ResetListBoxes();

            //也可以這樣寫
            //List<string> result = _collectionB.Where( item => _collectionA.Contains( item ) == false ).ToList();

            List<string> result = _collectionB.Except( _collectionA ).ToList();

            result.ForEach
                ( item =>
                    {
                        ( lstCollectionB.ItemContainerGenerator.ContainerFromItem( item ) as ListBoxItem ).Foreground = new SolidColorBrush( Colors.Red );
                    }
                );

        }
    }
}
