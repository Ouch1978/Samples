using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Wpf_StoryboardInDataTemplate
{
	/// <summary>
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class Page1 : Page
	{
		List<Border> borders;

		public Page1()
		{
			InitializeComponent();

			this.Loaded += new RoutedEventHandler( Page1_Loaded );
		}

		void Page1_Loaded( object sender , RoutedEventArgs e )
		{
			borders = FindVisualChildren<Border>( lstSampleData , "border" );
		}

		private void btnBeginStoryboard_Click( object sender , RoutedEventArgs e )
		{
			Storyboard bright = lstSampleData.ItemTemplate.Resources[ "Bright" ] as Storyboard;

			ListBoxItem selectedItem = lstSampleData.ItemContainerGenerator.ContainerFromItem( lstSampleData.SelectedItem ) as ListBoxItem;

			bright.Begin( borders.ElementAt( lstSampleData.SelectedIndex ) );
		}

		public static List<T> FindVisualChildren<T>( DependencyObject depObj , string name = "" ) where T : DependencyObject
		{
			List<T> list = new List<T>();
			if( depObj != null )
			{
				for( int i = 0 ; i < VisualTreeHelper.GetChildrenCount( depObj ) ; i++ )
				{
					DependencyObject child = VisualTreeHelper.GetChild( depObj , i );
					if( child != null && child is T )
					{
						if( string.IsNullOrEmpty( name ) == true || ( child as FrameworkElement ).Name == name )
						{
							list.Add( ( T ) child );
						}
					}

					List<T> childItems = FindVisualChildren<T>( child , name );

					if( childItems != null && childItems.Count() > 0 )
					{
						foreach( var item in childItems )
						{
							list.Add( item );
						}
					}
				}
			}
			return list;
		}

	}
}
