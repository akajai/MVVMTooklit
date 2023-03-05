using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ConnectionString()
        {
            string connectionString=ConfigurationManager.AppSettings["ConnectionString"];
            return connectionString;
        }

    }
}
