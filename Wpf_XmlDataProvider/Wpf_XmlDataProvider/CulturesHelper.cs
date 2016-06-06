using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Xml;

namespace Wpf_XmlDataProvider
{
    public class CulturesHelper
    {
        private static bool _isFoundInstalledCultures = false;

        private static string _resourcePrefix = "Language";

        private static string _culturesFolder = "Cultures";

        private static XmlDataProvider _xmlDataProvider;

        private static List<CultureInfo> _supportedCultures = new List<CultureInfo>();

        public static List<CultureInfo> SupportedCultures
        {
            get
            {
                return _supportedCultures;
            }
        }

        public CulturesHelper()
        {
            if( !_isFoundInstalledCultures )
            {

                CultureInfo cultureInfo = new CultureInfo( "" );

                List<string> files = Directory.GetFiles( string.Format( "{0}\\{1}" , System.Windows.Forms.Application.StartupPath , _culturesFolder ) ).Where( s => s.Contains( _resourcePrefix ) && s.ToLower().EndsWith( "xml" ) ).ToList();

                foreach( string file in files )
                {
                    try
                    {
                        string cultureName = file.Substring( file.IndexOf( "." ) + 1 ).Replace( ".xml" , "" );

                        cultureInfo = CultureInfo.GetCultureInfo( cultureName );


                        if( cultureInfo != null )
                        {
                            _supportedCultures.Add( cultureInfo );

                        }
                    }
                    catch( ArgumentException )
                    {
                    }

                }

                if( _supportedCultures.Count > 0 && Properties.Settings.Default.DefaultCulture != null )
                {
                    ChangeCulture( Properties.Settings.Default.DefaultCulture );
                }

                _isFoundInstalledCultures = true;
            }
        }

        public static XmlDataProvider LanguageProvider
        {
            get
            {
                if( _xmlDataProvider == null )
                {
                    _xmlDataProvider = ( XmlDataProvider ) App.Current.FindResource( "xmlLanguageProvider" );
                }

                return _xmlDataProvider;
            }
        }

        public static void ChangeCulture( CultureInfo culture )
        {
            if( _supportedCultures.Contains( culture ) )
            {
                string LoadedFileName = string.Format( "{0}\\{1}\\{2}.{3}.xml" , System.Windows.Forms.Application.StartupPath , _culturesFolder , _resourcePrefix , culture.Name );

                FileStream fileStream = new FileStream( @LoadedFileName , FileMode.Open );

                LanguageProvider.Document = new XmlDocument();

                LanguageProvider.Document.Load( fileStream );

                Properties.Settings.Default.DefaultCulture = culture;
                Properties.Settings.Default.Save();

                LanguageProvider.Refresh();
            }
        }

    }
}
