using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media;

namespace SL_FunWithColors
{
    public class ColorConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            string color = value.ToString();

            Dictionary<string , byte> colorInfo = new Dictionary<string , byte> 
                    { 
                        { "Alpha" , System.Convert.ToByte( "ff" , 16 ) } , 
                        { "Red" , System.Convert.ToByte( "00" , 16 ) } , 
                        { "Green" , System.Convert.ToByte( "00" , 16 ) } , 
                        { "Blue" , System.Convert.ToByte( "00" , 16 ) } 
                    };

            try
            {
                //判斷輸入值是否為Hex色碼，如果不是#開頭，則當作為輸入的值是色票名稱
                if( color.IndexOf( "#" ) == 0 )
                {
                    color = color.Replace( "#" , string.Empty );

                    bool hasAlphaChannel = ( color.Length > 6 ) ? true : false;

                    for( int position = 0 ; position < colorInfo.Count ; position++ )
                    {
                        colorInfo[ colorInfo.Keys.ElementAt( ( hasAlphaChannel == true ) ? position : position + 1 ) ] = System.Convert.ToByte( color.Substring( position * 2 , 2 ) , 16 );
                    }
                }
                else
                {
                    //透過Reflection去翻出System.Windows.Media.Colors的色票名稱
                    Type colorType = ( typeof( System.Windows.Media.Colors ) );

                    if( colorType.GetProperty( color ) != null )
                    {
                        object colorObject = colorType.InvokeMember( color , BindingFlags.GetProperty , null , null , null );
                        if( colorObject != null )
                        {
                            return ( Color ) colorObject;
                        }
                    }
                }
            }
            catch
            {
            }

            return Color.FromArgb( colorInfo[ "Alpha" ] , colorInfo[ "Red" ] , colorInfo[ "Green" ] , colorInfo[ "Blue" ] );

        }

        public object ConvertBack( object value , Type targetType , object parameter , System.Globalization.CultureInfo culture )
        {
            if( value is Color )
            {
                Color color = ( Color ) value;

                return string.Format( "#{0}{1}{2}{3}" , color.A , color.R , color.G , color.B );
            }
            else
            {
                //若輸入值不是Color型別，則回傳透明的黑色Hex字串值
                return "#00000000";
            }
        }

    }
}
