

namespace Wpf_CustomCursor
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Win32.SafeHandles;

    public class CursorHelper
    {
        private static class NativeMethods
        {
            //參考 http://msdn.microsoft.com/en-us/library/ms648052(v=vs.85).aspx ，做出一樣的Struct，用來複寫系統的游標。
            public struct IconInfo
            {
                public bool fIcon;
                public int xHotspot;
                public int yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            [DllImport( "user32.dll" )]
            public static extern SafeIconHandle CreateIconIndirect( ref IconInfo icon );

            [DllImport( "user32.dll" )]
            public static extern bool DestroyIcon( IntPtr hIcon );

            [DllImport( "user32.dll" )]
            [return: MarshalAs( UnmanagedType.Bool )]
            public static extern bool GetIconInfo( IntPtr hIcon , ref IconInfo pIconInfo );
        }

        [SecurityPermission( SecurityAction.LinkDemand , UnmanagedCode = true )]
        private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base( true )
            {
            }

            override protected bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon( handle );
            }
        }

        /// <summary>
        /// 透過Bitmap建立圖形游標
        /// </summary>
        /// <param name="bitmap">要當成游標的Bitmap</param>
        /// <param name="xHotSpot">游標頂點的X軸位移</param>
        /// <param name="yHotSpot">游標頂點的Y軸位移</param>
        /// <returns>自訂的游標物件</returns>
        private static Cursor InternalCreateCursor( System.Drawing.Bitmap bitmap , int xHotSpot , int yHotSpot )
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo( bitmap.GetHicon() , ref iconInfo );

            iconInfo.xHotspot = xHotSpot;
            iconInfo.yHotspot = yHotSpot;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect( ref iconInfo );
            return CursorInteropHelper.Create( cursorHandle );
        }

        /// <summary>
        /// 用來以UIElement建立自定游標
        /// </summary>
        /// <param name="element">要當成游標的UIElement</param>
        /// <param name="xHotSpot">游標頂點的X軸位移</param>
        /// <param name="yHotSpot">游標頂點的Y軸位移</param>
        /// <returns>自訂的游標物件</returns>
        public static Cursor CreateCursor( UIElement element , int xHotSpot = 0 , int yHotSpot = 0 )
        {
            element.Measure( new Size( double.PositiveInfinity , double.PositiveInfinity ) );
            element.Arrange( new Rect( new Point() , element.DesiredSize ) );

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                ( int ) element.DesiredSize.Width , ( int ) element.DesiredSize.Height ,
                96 , 96 , PixelFormats.Pbgra32 );

            renderTargetBitmap.Render( element );

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add( BitmapFrame.Create( renderTargetBitmap ) );

            using( var memoryStream = new MemoryStream() )
            {
                encoder.Save( memoryStream );
                using( var bitmap = new System.Drawing.Bitmap( memoryStream ) )
                {
                    return InternalCreateCursor( bitmap , xHotSpot , yHotSpot );
                }
            }
        }
    }
}
