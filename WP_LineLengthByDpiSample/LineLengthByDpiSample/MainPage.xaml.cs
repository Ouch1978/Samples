using Microsoft.Phone.Controls;
using ScreenSizeSupport;

namespace LineLengthByDpiSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            var ppi = (int) DisplayInformationEx.Default.ViewPixelsPerInch;

            txtPpi.Text = ppi.ToString();
        }
    }
}