using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Wpf_ReadXmlByLinqToXml
{

    public partial class MainWindow : Window
    {
        private string _xmlPath = @"List.xml";
        private XDocument _xDocument;

        public MainWindow()
        {
            InitializeComponent();

            BindXmlToDataGrid();

        }

        private void BindXmlToDataGrid()
        {
            _xDocument = XDocument.Load( _xmlPath );

            var items = from book in _xDocument.Root.Elements( "Book" ) select book;

            this.dataGrid1.ItemsSource = items;
        }

        private void Window_Closing( object sender , System.ComponentModel.CancelEventArgs e )
        {

            MessageBoxResult result = MessageBox.Show( "Save values?" , "Closing" , MessageBoxButton.YesNoCancel , MessageBoxImage.Question , MessageBoxResult.Cancel );

            switch( result )
            {
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    _xDocument.Save( _xmlPath );
                    break;
                default:
                    break;
            }
        }
    }
}
