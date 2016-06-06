using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_ObjectDataProvider
{
    public partial class Page1 : Page
    {
        private ObjectDataProvider _objectDataProvider;

        public Page1()
        {
            InitializeComponent();

            _objectDataProvider = this.LayoutRoot.FindResource( "dataSource" ) as ObjectDataProvider;
        }

        private void cmbType_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            if( this.cmbMembers == null || this.cmbProducts == null )
            {
                return;
            }

            this.cmbMembers.Visibility = System.Windows.Visibility.Collapsed;
            this.cmbProducts.Visibility = System.Windows.Visibility.Collapsed;

            switch( (cmbType.SelectedItem as ComboBoxItem).Content.ToString() )
            {
                case "Members":
                    this.cmbMembers.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "Products":
                    this.cmbProducts.Visibility = System.Windows.Visibility.Visible;
                    break;
            }

        }

        private void btnUpdateDataSource_Click( object sender , RoutedEventArgs e )
        {
            switch( ( cmbType.SelectedItem as ComboBoxItem ).Content.ToString() )
            {
                case "Members":
                    _objectDataProvider.ObjectType = typeof( Members );
                    _objectDataProvider.MethodName = ( cmbMembers.SelectedValue as ComboBoxItem ).Content.ToString();
                    break;
                case "Products":
                    _objectDataProvider.ObjectType = typeof( Products );
                    _objectDataProvider.MethodName = ( cmbProducts.SelectedValue as ComboBoxItem ).Content.ToString();
                    break;
            }

            this._objectDataProvider.Refresh() ;

            if( _objectDataProvider.Error != null )
            {
                MessageBox.Show( _objectDataProvider.Error.Message );
            }

        }
    }
}
