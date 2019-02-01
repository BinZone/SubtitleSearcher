using BinZone.SubtitleSearcher.Model;
using BinZone.SubtitleSearcher.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BinZone.SubtitleSearcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Video = new Video();
        }

        public Video Video { get; private set; }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {

            Video.UpdateInfo(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());

            //获得文件名后的操作...  
            ShowProgress("开始查找...");
            GetSubtitleList();
        }

        private void ShowProgress(string message)
        {
            Progress.Visibility = Visibility.Visible;
            StatusTips.Content = message;
        }

        private Task HideProgress(string message)
        {
            StatusTips.Content = message;
            return Task.Run(() =>
            {
                ThreadPool.QueueUserWorkItem(o => Progress.Dispatcher.Invoke(
                    () =>
                    {
                        Progress.Visibility = Visibility.Hidden;
                    }));
            });
        }

        private async void GetSubtitleList()
        {
            var response = await WebRequest.GetSubtitleListAsync(fileName: Video.Name + "." + Video.Extension);
            SubtitleList.ItemsSource = JsonHelper.Deserialize(response);
            await HideProgress(string.Format("搜索完成，一共找到条{0}记录", SubtitleList.Items.Count));
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
                        ? DragDropEffects.Link : DragDropEffects.None;
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProgress("开始下载字幕文件...");

            if (SubtitleList.SelectedItem == null || SubtitleList.SelectedIndex == -1) return;
            var sub = SubtitleList.SelectedItem as Subtitle;
            if (sub == null) return;

            await WebRequest.DownloadSubtitleAsync(sub, Video.Directory, Video.Name, CanRename.IsChecked);
            await HideProgress(string.Format("字幕文件{0}下载完成...", sub.Name));
        }
    }
}
