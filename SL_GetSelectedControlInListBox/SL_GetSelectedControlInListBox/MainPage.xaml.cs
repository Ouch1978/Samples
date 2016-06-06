using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SL_GetSelectedControlInListBox
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void btnGetSelectedControl_Click( object sender , RoutedEventArgs e )
		{
			//直接去拿SelectedItem的話，只會拿到繫結的資料，而不是UI上的控制項
			var selectedItem = this.lstSampleData.SelectedItem;

			//得透過ItemContainerGenerator.ContainerFromItem才能真的拿到UI上的控制項
			ListBoxItem selectedListBoxItem = lstSampleData.ItemContainerGenerator.ContainerFromItem( selectedItem ) as ListBoxItem;

			//上面這幾行簡化成一行就會變
			//ListBoxItem selectedListBoxItem = lstSampleData.ItemContainerGenerator.ContainerFromItem( this.lstSampleData.SelectedItem ) as ListBoxItem;

			//也可以這樣寫
			//ListBoxItem selectedListBoxItem = lstSampleData.ItemContainerGenerator.ContainerFromIndex( this.lstSampleData.SelectedIndex ) as ListBoxItem;

			//如果已經拿得到Control，而想拿裡面繫結的值，則可以這樣拿
			var data = lstSampleData.ItemContainerGenerator.ItemFromContainer( selectedListBoxItem );

			//或是想透過Control拿到被選取到項目的Index，可以這樣寫
			int selectedItemIndex = lstSampleData.ItemContainerGenerator.IndexFromContainer( selectedListBoxItem );

			//拿得到UI上的控制項話，就可以改變UI上呈現的樣子啦~
			selectedListBoxItem.Foreground = new SolidColorBrush( Colors.Red );

		}
	}
}
