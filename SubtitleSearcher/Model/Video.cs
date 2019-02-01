using System;
using System.IO;

namespace BinZone.SubtitleSearcher.Model
{
    public class Video : NotificationObject
    {
        #region Property and Filed

        public const string LengthPropertyName = "Length";

        /// <summary>
        /// 视频播放时长，以毫秒为单位
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        public const string FileNamePropertyName = "FileName";

        /// <summary>
        /// 不包含文件扩展名的视频文件名称
        /// </summary>
        public string Name { get; private set; }

        public const string ExtensionPropertyName = "Extension";

        /// <summary>
        /// 视频文件扩展名
        /// </summary>
        public string Extension { get; private set; }

        public const string FilePathPropertyName = "FullPath";

        /// <summary>
        /// 视频文件所在位置的绝对路径
        /// </summary>
        public string Directory { get; private set; }

        #endregion

        public Video()
        {
            Directory = string.Empty;
            Name = string.Empty;
            Extension = string.Empty;
            Length = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">视频文件绝对路径</param>        
        public Video(string fullPath)
        {
            Split(fullPath);
            Length = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">视频文件绝对路径</param>
        /// <param name="videoLength">视频文件播放长度</param>
        public Video(string fullPath, int videoLength)
        {
            Split(fullPath);
            if (videoLength <= 0)
            {
                throw new ArgumentOutOfRangeException("视频播放时间应该大于0");
            }
            Length = videoLength;
        }

        public void UpdateInfo(string fullPath)
        {
            Split(fullPath);
            Length = -1;
        }

        public void UpdateInfo(string fullPath, int videoLength)
        {
            Split(fullPath);
            if (videoLength <= 0)
            {
                throw new ArgumentOutOfRangeException("视频播放时间应该大于0");
            }
            Length = videoLength;
        }

        private void Split(string fullPath)
        {
            Extension = Path.GetExtension(fullPath).Remove(0, 1);
            Name = Path.GetFileNameWithoutExtension(fullPath);
            Directory = Path.GetDirectoryName(fullPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>包含文件扩展名的文件名称</returns>
        public override string ToString()
        {
            return Name + "." + Extension;
        }
    }
}
