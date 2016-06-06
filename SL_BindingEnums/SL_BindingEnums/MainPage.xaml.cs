using System;
using System.Windows;
using System.Windows.Controls;

namespace SL_BindingEnums
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.cmbVisibility.ItemsSource = EnumHelper.ToList<Visibility>();

            this.cmbDayOfWeek.ItemsSource = EnumHelper.ToList<DayOfWeek>();
        }
    }
}
