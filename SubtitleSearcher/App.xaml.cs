using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace BinZone.SubtitleSearcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLog(e.ExceptionObject as Exception);
            var result = System.Windows.MessageBox.Show("程序运行期间发生了严重的错误，即将退出,是否显示日志文件？", "错误"
                 , System.Windows.MessageBoxButton.YesNo
                 , System.Windows.MessageBoxImage.Error);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                Process.Start(LogFile);
            }

            Current.Shutdown();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            WriteLog(e.Exception);
            var result = System.Windows.MessageBox.Show("程序运行期间发生未知错误,是否继续？", "错误"
                 , System.Windows.MessageBoxButton.YesNo
                 , System.Windows.MessageBoxImage.Error);

            if (result == System.Windows.MessageBoxResult.No)
            {
                e.Handled = true;
                Current.Shutdown();
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteLog(Exception e)
        {
            using (var writer = new StreamWriter(LogFile, true))
            {
                if (!File.Exists(LogFile)) return;
                writer.WriteLine("====================================================================================");
                writer.Write(e);
                writer.WriteLine("\r\n");
            }
        }

        private static string LogFile
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, "Subtitle Searcher.log");
            }
        }
    }
}
