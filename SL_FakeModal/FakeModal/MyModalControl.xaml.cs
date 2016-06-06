using System.Windows.Controls;

namespace FakeModal
{
	public partial class MyModalControl : UserControl
	{
		public MyModalControl()
		{
			InitializeComponent();
		}

		private void btnYes_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//you can do something here.
			(this.Parent as Panel).Children.Remove(this);
		}

		private void btnNo_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//you can do something here.
			(this.Parent as Panel).Children.Remove(this);
		}
	}
}