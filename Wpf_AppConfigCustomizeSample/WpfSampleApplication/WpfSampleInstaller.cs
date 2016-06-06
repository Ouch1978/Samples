using System.ComponentModel;
using System.Configuration;

namespace WpfSampleApplication
{
    [RunInstaller( true )]
    public partial class WpfSampleInstaller : System.Configuration.Install.Installer
    {
        public WpfSampleInstaller()
        {
            InitializeComponent();
        }

        //我們得override掉原來的Install()方法
        public override void Install( System.Collections.IDictionary stateSaver )
        {
            base.Install( stateSaver );

            //取得使用者輸入的參數值(這邊的Parameter名稱很重要喔，後面的Custom Action還要用到)
            string param1 = Context.Parameters[ "Param1" ];

            string param2 = Context.Parameters[ "Param2" ];

            string param3 = Context.Parameters[ "Param3" ];

            //尋找安裝路徑中的App.config檔
            var exeConfigurationFileMap = new ExeConfigurationFileMap();

            exeConfigurationFileMap.ExeConfigFilename = Context.Parameters[ "assemblypath" ] + ".config";

            var config = ConfigurationManager.OpenMappedExeConfiguration( exeConfigurationFileMap , ConfigurationUserLevel.None );

            //將參數值寫回App.config檔
            config.AppSettings.Settings[ "Config1" ].Value = param1;

            config.AppSettings.Settings[ "Config2" ].Value = param2;

            config.AppSettings.Settings[ "Config3" ].Value = param3;


            config.Save();
        }
    }
}
