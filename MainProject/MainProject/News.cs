using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Leap;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MainProject
{
   public class News
    {
       
        public string title { get; set; }
        public BitmapImage imgUrl { get; set; }
        public string context { get; set; }
        public string shortContext { get; set; }
        public int id { get; set; }
        public string category { get; set; }
        public string href { get; set; }
        public News(string title, string imgUrl, string context,string shortContext,int id, string category, string href)
        {
            this.title = title;
            this.context = context;
            this.imgUrl = loadImage(imgUrl);
            this.shortContext = shortContext;
            this.id = id;
            this.category = category;
            this.href = href;
        }
        public BitmapImage loadImage(string imgUrl)
        {
            var uriSource = new Uri(@imgUrl, UriKind.Relative);
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = uriSource;
            imageSource.CacheOption = BitmapCacheOption.OnLoad;
            imageSource.EndInit();
            return imageSource;
        }
    }
}
