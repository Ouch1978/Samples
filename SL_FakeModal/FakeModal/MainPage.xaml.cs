using System.Windows.Controls;

namespace FakeModal
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void btnShowModal_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			this.LayoutRoot.Children.Add( new MyModalControl());
		}
	}
}