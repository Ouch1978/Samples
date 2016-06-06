using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wpf_TextBoxSelectAllEnhanced
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            EventManager.RegisterClassHandler( typeof( TextBox ) , UIElement.PreviewMouseLeftButtonDownEvent ,
               new MouseButtonEventHandler( TextBox_PreviewMouseLeftButtonDown ) , true );
            EventManager.RegisterClassHandler( typeof( TextBox ) , UIElement.GotKeyboardFocusEvent ,
              new RoutedEventHandler( SelectAllText ) , true );

            base.OnStartup( e );
        }

        private static void TextBox_PreviewMouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            var textbox = ( sender as TextBox );
            if( textbox != null && textbox.IsKeyboardFocusWithin != true )
            {
                if( e.OriginalSource.GetType().Name == "TextBoxView" )
                {
                    e.Handled = true;
                    textbox.Focus();
                }
            }
        }

        private static void SelectAllText( object sender , RoutedEventArgs e )
        {
            var textBox = e.OriginalSource as TextBox;
            if( textBox != null )
                textBox.SelectAll();
        } 

    }
}
