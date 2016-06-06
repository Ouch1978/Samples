using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SL_DynamicDataGridColumns
{
    public partial class MainPage : UserControl
    {
        private ObservableCollection<string> _columns = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();

            this.myListBox.ItemsSource = _columns;

            //如果DataGrid的欄位不是自動產生的(AutoGenerateColumns=False)，就可以在Loaded事件裡面抓取欄位。但如果是由DataGrid自動產生欄位(AutoGenerateColumns=True)，在Loaded中就會抓不到欄位喔!!
            //this.Loaded += new RoutedEventHandler( MainPage_Loaded );
        }

        private void MainPage_Loaded( object sender , RoutedEventArgs e )
        {
            foreach( DataGridColumn dataGridColumn in myDataGrid.Columns )
            {
                if( !_columns.Contains( dataGridColumn.Header.ToString() ) )
                {
                    _columns.Add( dataGridColumn.Header.ToString() );
                }
            }

            this.myListBox.SelectAll();
        }

        private void myButton_Click( object sender , RoutedEventArgs e )
        {
            for( int i = 0 ; i < _columns.Count ; i++ )
            {
                myDataGrid.Columns[ i ].Visibility = Visibility.Collapsed;

                if( ( myListBox.ItemContainerGenerator.ContainerFromIndex( i ) as ListBoxItem ).IsSelected == true )
                {
                    myDataGrid.Columns[ i ].Visibility = Visibility.Visible;
                }
            }
        }

        private void myDataGrid_AutoGeneratingColumn( object sender , DataGridAutoGeneratingColumnEventArgs e )
        {
            if( !_columns.Contains( e.PropertyName ) )
            {
                _columns.Add( e.PropertyName );
            }

            myListBox.SelectAll();
        }
    }
}
