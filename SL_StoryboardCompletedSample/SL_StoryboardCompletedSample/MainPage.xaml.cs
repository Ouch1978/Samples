using System.Windows;
using System.Windows.Controls;

namespace SL_StoryboardCompletedSample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        private void Storyboard1_Completed( object sender , System.EventArgs e )
        {
            MessageBox.Show( "Storyboard1 播放完畢" );
        }

        private void Storyboard_Completed( object sender , System.EventArgs e )
        {
            MessageBox.Show( "Selected State 動畫播放完畢" );
        }
    }
}