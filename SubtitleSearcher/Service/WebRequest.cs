using BinZone.SubtitleSearcher.Model;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinZone.SubtitleSearcher.Service
{
    class WebRequest
    {
        //http://sub.makedie.me/sub/?searchword=
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static Task<string> GetSubtitleListAsync(string fileName, uint videoLength = 0)
        {
            var searchUrl = videoLength <= 0
                ? string.Format("http://subtitle.kankan.xunlei.com:8000/search.json/mname={0}", fileName)
                : string.Format("http://subtitle.kankan.xunlei.com:8000/search.json/mname={0}&videolength={1}",
                    fileName, videoLength);

            var client = new HttpClient();
            return client.GetStringAsync(searchUrl);
        }

        /// <summary>
        /// 异步方式下载字幕
        /// </summary>
        /// <param name="sub">字幕对象</param>
        /// <param name="path">下载文件保存路径</param>
        /// <param name="videoName">不包含文件扩展名的视频名称</param>
        /// <param name="rename">是否和视频保持同名，可能会覆盖之前的下载</param>
        /// <returns></returns>
        public static Task DownloadSubtitleAsync(Subtitle sub, string path, string videoName, bool? rename = false)
        {
            var fullFileName/*当前需要保存的字幕名称*/ = rename == true
                             ? Path.Combine(path, videoName + "." + sub.Extension)//与原视频名称相同的字幕名称
                             : Path.Combine(path, sub.ToString());//原始的字幕名称

            return Task.Run(() =>
            {
                //根据url获取远程文件流
                var request = System.Net.WebRequest.Create(sub.Url) as HttpWebRequest;

                if (request == null) return;
                var response = request.GetResponse() as HttpWebResponse;
                if (response == null) return;
                using (var sr = response.GetResponseStream())
                {
                    //创建本地文件写入流
                    using (var sw = new FileStream(fullFileName, FileMode.Create))
                    {
                        long totalDownloadedByte = 0;
                        var by = new byte[1024];
                        var osize = sr.Read(by, 0, by.Length);
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            sw.Write(by, 0, osize);
                            osize = sr.Read(by, 0, by.Length);
                        }
                    }
                }
            });
        }
    }
}
