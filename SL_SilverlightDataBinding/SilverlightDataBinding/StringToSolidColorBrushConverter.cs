using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SilverlightDataBinding
{
    public class StringToSolidColorBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            string colorString = value as string;

            if( String.IsNullOrEmpty( colorString ) )
            {
                return new SolidColorBrush( Color.FromArgb( 0 , 0 , 0 , 0 ) );
            }

            try
            {
                Color color = new Color();
                color.A = byte.Parse( colorString.Substring( 0 , 2 ) , NumberStyles.HexNumber );
                color.R = byte.Parse( colorString.Substring( 2 , 2 ) , NumberStyles.HexNumber );
                color.G = byte.Parse( colorString.Substring( 4 , 2 ) , NumberStyles.HexNumber );
                color.B = byte.Parse( colorString.Substring( 6 , 2 ) , NumberStyles.HexNumber );

                return new SolidColorBrush( color );
            }
            catch
            {
                return new SolidColorBrush( Color.FromArgb( 0 , 0 , 0 , 0 ) );
            }
        }

        public object ConvertBack( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            if( value is Color )
            {
                Color color = ( Color ) value;
                return color.ToString();
            }
            else
            {
                return "FFFFFFFF";
            }
        }

        #endregion
    }
}
