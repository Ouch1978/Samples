using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace SL_ReadConfigFile.Web
{
    public partial class SL_ReadConfigFile : System.Web.UI.Page
    {
        private string _seperator = ",";

        protected void Page_Load( object sender , EventArgs e )
        {
            //清除Cache，以避免讀取到Cache中的資料
            Response.Cache.SetCacheability( HttpCacheability.NoCache );

            WriteInitParams();
        }

        private void WriteInitParams()
        {
            NameValueCollection appSettings = WebConfigurationManager.AppSettings;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append( "<param name=\"InitParams\" value=\"" );

            string paramContent = string.Empty;

            for( int i = 0 ; i < appSettings.Count ; i++ )
            {
                if( paramContent.Length > 0 )
                {
                    paramContent += _seperator;
                }

                paramContent += string.Format( "{0}={1}" , appSettings.GetKey( i ) , appSettings[ i ] );
            }

            stringBuilder.Append( paramContent );

            stringBuilder.Append( "\" />" );

            this.litInitParams.Text = stringBuilder.ToString();
        }
    }
}