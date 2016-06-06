using System;
using System.Windows.Data;

namespace Wpf_DataTemplateWithTrigger
{
    public class ScoreConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            int score = ( int ) value;
            int passScore = ( int ) parameter;

            if( score >= passScore )
            {
                return true;
            }

            return false;
        }

        public object ConvertBack( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            return null;
        }
    }
}
