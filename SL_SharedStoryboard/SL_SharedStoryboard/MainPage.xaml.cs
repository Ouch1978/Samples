using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SL_SharedStoryboard
{
    public partial class MainPage : UserControl
    {
        private Storyboard _storyboard;

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnPlayAnimation_Click( object sender , RoutedEventArgs e )
        {
            if( cmbTargetName.SelectedIndex > -1 )
            {
                //取得定義在App.xaml中的Storyboard資源
                _storyboard = Application.Current.Resources[ "moveBallStoryboard" ] as Storyboard;

                //記得要先把之前的Storyboard停止，才能針對它重新賦與Property
                _storyboard.Stop();

                //取得Storyboard的目標
                UIElement uiElement = this.FindName( ( cmbTargetName.SelectedItem as ComboBoxItem ).Content.ToString() ) as UIElement;

                //因為Storyboard中使用了CompositeTransform，所以必需幫目標的RenderTransform加上這個屬性才行
                uiElement.RenderTransform = new CompositeTransform();

                //利用Storyboard.SetTarget重設_storyboard的目標
                Storyboard.SetTarget( _storyboard , uiElement );

                _storyboard.Begin();
            }
        }
    }
}