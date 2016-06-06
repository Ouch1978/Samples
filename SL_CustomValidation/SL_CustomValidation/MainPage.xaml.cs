using System.Windows;
using System.Windows.Controls;

namespace SL_CustomValidation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnValidate_Click( object sender , RoutedEventArgs e )
        {
            bool isPassedValidation = true;
            txtIntegerField.ClearValidationError();
            txtEmailField.ClearValidationError();
            txtPlainTextField.ClearValidationError();

            if( string.IsNullOrEmpty( txtIntegerField.Text ) || txtIntegerField.Text.IsNumberValid() == false )
            {
                txtIntegerField.SetValidation( "請輸入合法的整數" );
                txtIntegerField.RaiseValidationError();
                isPassedValidation = false;
            }

            if( string.IsNullOrEmpty( txtEmailField.Text ) || txtEmailField.Text.IsEmailValid() == false )
            {
                txtEmailField.SetValidation( "請輸入合法的Email信箱格式" );
                txtEmailField.RaiseValidationError();
                isPassedValidation = false;
            }

            if( string.IsNullOrEmpty( txtPlainTextField.Text ) || txtPlainTextField.Text.IsPlainTextValid() == false )
            {
                txtPlainTextField.SetValidation( "請輸入不包含任何符號的文字" );
                txtPlainTextField.RaiseValidationError();
                isPassedValidation = false;
            }

            if( isPassedValidation == true )
            {
                MessageBox.Show( "通過驗證!!" );
            }
        }
    }
}
