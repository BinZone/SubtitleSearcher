namespace BinZone.SubtitleSearcher.Model
{
    class Subtitle : NotificationObject
    {
        private string _id;

        public const string IdPropertyName = "Id";

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }

        private string _name;

        public const string NamePropertyName = "Name";

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        private string _extension;

        public const string ExtensionPropertyName = "Extension";

        public string Extension
        {
            get { return _extension; }
            set
            {
                _extension = value;
                RaisePropertyChanged(ExtensionPropertyName);
            }
        }

        private string _language;

        public const string LanguagePropertyName = "Language";

        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                RaisePropertyChanged(LanguagePropertyName);
            }
        }


        private string _confidence;

        public const string ConfidencePropertyName = "Confidence";

        public string Confidence
        {
            get { return _confidence; }
            set
            {
                _confidence = string.Format("{0}%", double.Parse(value) * 100);
                RaisePropertyChanged(ConfidencePropertyName);
            }
        }


        private string _url;

        public const string UrlPropertyName = "Url";

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                RaisePropertyChanged(UrlPropertyName);
            }
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
