using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Expression.Blend.SampleData.SampleDataSource;

namespace Wpf_ShowEmptyMessageInDataGrid
{
	public partial class Page1 : Page
	{
		public Page1()
		{
			InitializeComponent();

			( this.DataContext as SampleDataSource ).Collection.CollectionChanged += new NotifyCollectionChangedEventHandler( Collection_CollectionChanged );
		}

		void Collection_CollectionChanged( object sender , NotifyCollectionChangedEventArgs e )
		{
			var collection = sender as Expression.Blend.SampleData.SampleDataSource.ItemCollection;

			//如果已經沒資料可供繫結了，就產生一個假的項目以供RowDetails顯示(裡面放什麼就沒差了~所以我放了一個TextBlock)
			if( collection.Count == 0 )
			{
				//記得在對Items操作之前要先把資料繫結清除，不然會出Exception
				this.grdSample2.ItemsSource = null;

				this.grdSample2.Items.Add( new TextBlock { Text = "Empty" } );
			}
			//如果還是有資料，就維持原來的資料繫結
			else
			{
				this.grdSample2.ItemsSource = collection;
			}
		}

		private void btnDeleteLastRecord_Click( object sender , RoutedEventArgs e )
		{
			if( ( this.DataContext as SampleDataSource ).Collection.Count > 0 )
			{
				( this.DataContext as SampleDataSource ).Collection.RemoveAt( ( this.DataContext as SampleDataSource ).Collection.Count - 1 );
			}
			else
			{
				btnDeleteLastRecord.IsEnabled = false;
			}
		}
	}
}
