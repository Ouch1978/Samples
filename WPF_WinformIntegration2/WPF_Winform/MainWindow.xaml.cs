using System.Windows;
using WindowsFormsControlLibrary;
using System.Windows.Forms;

namespace WPF_Winform
{
    public partial class MainWindow : Window
    {
        private Form1 _form1 = new Form1();

        private Form2 _form2 = new Form2();

        public MainWindow()
        {
            InitializeComponent();

            _form1.TopLevel = false;

            windowsFormsHost1.Child = _form1;

            Panel panelCenter = new Panel { BackColor = System.Drawing.Color.Blue };

            panelCenter.Dock = DockStyle.Fill;

            _form1.Controls.Add( panelCenter );

            Panel panelTop = new Panel { BackColor = System.Drawing.Color.White , MinimumSize = new System.Drawing.Size { Height = 100 , Width = 0 } };

            panelTop.Dock = DockStyle.Top;

            panelCenter.Controls.Add( panelTop );

            _form2.TopLevel = false;

            windowsFormsHost2.Child = _form2;
        }
    }
}
