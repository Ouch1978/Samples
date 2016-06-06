using System;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SL_FunWithColors
{
    public class XamlColorConverter
    {
        public static Color ConvertFromString( string colorString )
        {
            Color color;
            try
            {
                //透過XamlReader讀入一段由程Xaml描述的Line物件，並對其設定填色
                Line line = ( Line ) XamlReader.Load( "<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Fill=\"" + colorString + "\" />" );

                //反過來由Line物件取得填色屬性
                color = ( Color ) line.Fill.GetValue( SolidColorBrush.ColorProperty );
            }
            catch
            {
                //若輸入不合法的值的話會回傳預設的顏色(透明的黑色)
                color = new Color();
            }

            return color;
        }


    }
}
