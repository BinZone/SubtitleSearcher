using System.ComponentModel;

namespace BinZone.SubtitleSearcher.Model
{
    /// <summary>
    /// 实现INotifyPropertyChanged，对属性改变事件进行抽象封装
    /// </summary>
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
