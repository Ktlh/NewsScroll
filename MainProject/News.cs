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
        public bool start;
        public bool fin;
        public string title { get; set; }
        public BitmapImage imgUrl { get; set; }
        public string context { get; set; }
        public string shortContext { get; set; }
        public int id { get; set; }
        public string category { get; set; }
       public News(string title, string imgUrl, string context,string shortContext,int id, string category,bool start,bool fin)
        {
            this.title = title;
            this.context = context;
            var uriSource = new Uri(@imgUrl, UriKind.Relative);
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = uriSource;
            imageSource.CacheOption = BitmapCacheOption.OnLoad;
            imageSource.EndInit();
            this.imgUrl = imageSource;
            this.shortContext = shortContext;
            this.id = id;
            this.category = category;
            this.start = start;
            this.fin = fin;
        }
    }
}
