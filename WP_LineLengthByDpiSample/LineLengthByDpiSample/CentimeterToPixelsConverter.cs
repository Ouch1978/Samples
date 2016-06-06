using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ScreenSizeSupport;

namespace LineLengthByDpiSample
{
    public class CentimeterToPixelsConverter : IValueConverter
    {
        private const double CentimeterToInchFactor = 2.54;

        public object Convert( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            double pixelsForOneCentimeter = DisplayInformationEx.Default.ViewPixelsPerInch / CentimeterToInchFactor;

            double actualPixels = double.Parse( value.ToString() ) * pixelsForOneCentimeter;

            return actualPixels;
        }

        public object ConvertBack( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
