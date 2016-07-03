using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TestStack.White;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using System.Net;

namespace ModuleTest
{ 
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void delAllImages()
        {
            string _dir = @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug";
            string _del = @"*.jpg";
            string[] _files = Directory.GetFiles(_dir, _del);
            foreach (string fl in _files)
            {
                File.Delete(fl);
            }
        }


        [TestMethod]
        public void openPage()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("href"));
            storyboard.Click();
        }

        [ExpectedException(typeof(System.Net.WebException))]
       [TestMethod]
        public void downloadImageWrong()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("https://nytimes.com/image/2016/06/12/travel/12TIPS-security/12TIPS-articleLarge.jpg", AppDomain.CurrentDomain.BaseDirectory + "img.jpg");
            }
        }

        [TestMethod]
        public void downloadImageRight()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("https://nytimes.com/images/2016/06/12/travel/12TIPS-security/12TIPS-articleLarge.jpg", AppDomain.CurrentDomain.BaseDirectory + "img.jpg");
            }
        }

        [TestMethod]
        public void getValidImage()
        {
            News news = new News("test", "Noimage.jpeg", "test", "test", 1, "test", "test");
            var status = false;
            if(news.loadImage("Noimage.jpeg").GetType().ToString()== "System.Windows.Media.Imaging.BitmapImage")
            {
                status = true;
            }
            Assert.AreEqual(status, true);
        }

        [TestMethod]
        public void disposeHandler()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            CustomHandler handler = new CustomHandler();
            handler.Dispose();
        }

        [TestMethod]
        public void createHandler()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            CustomHandler handler=new CustomHandler();
    }

        [TestMethod]
        public void loadContent()
        {
            string request = " https://api.nytimes.com/svc/search/v2/articlesearch.json?api-key=a0e18d4e9a7942a087445ded8aece2d6&sort=newest&page=1";
            WebRequest req = WebRequest.Create(request);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            Assert.IsNotNull(Out);
        }
        
       [ExpectedException(typeof(System.IO.FileNotFoundException))]
       [TestMethod]
        public void createWrongNews()
        {
            News news = new News("test", "wrongImage.jpeg", "test", "test", 1, "test", "test");
        }

        [TestMethod]
        public void isImageArrowLoad()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
             var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("arrowleft"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isFonLoad()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("fon"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isImage1Load()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("image1"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isImage2Load()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("image2"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isImage3Load()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("image3"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isImage4Load()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("image4"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isImage5Load()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("image5"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }

        [TestMethod]
        public void isSingleImageLoad()
        {
            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug\MainProject.exe");
            var application = TestStack.White.Application.Launch(appPath);
            Assert.IsNotNull(application);
            var window = application.GetWindow("NewsScroll");
            var storyboard = window.Get<TestStack.White.UIItems.UIItem>(SearchCriteria.ByAutomationId("singleImage"));
            storyboard.Visible.ToString();
            Assert.IsNotNull(storyboard);
        }
    }
}
