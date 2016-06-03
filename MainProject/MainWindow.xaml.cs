using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Leap;
using System.Threading;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Windows.Navigation;

namespace MainProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int start = 1;
        private static int fin = 4;
        private CustomHandler handler;
        private Leap.Frame fram;
        private IList<System.Windows.Controls.Image> bugs = new List<System.Windows.Controls.Image>();
        private IList<News> newslist = new List<News>();
        private IList<News> fooList = new List<News>();
        private IList<News> newsListSorted = new List<News>();
        private IList<string> categoryList = new List<string>();
        private bool Sort = false;
        private bool ShtorkaBool = false;
        News SelectedNews = null;
        public MainWindow()
        {
            handler = new CustomHandler();
            handler.Listener.OnFingersRegistered += ListenerOnOnFingersRegistered;
            handler.Listener.OnGestureMade += Listener_OnGestureMade;
            handler.Listener.onFrameDetected += Listener_OnFrameDetected;
            // InitializeComponent();
        }

        private void Listener_OnFrameDetected(Leap.Frame obj)
        {
            fram = obj;
        }

        private void Listener_OnGestureMade(GestureList gestures)
        {
            foreach (var gestur in gestures)
            {

                if (LeapGestures.GestureTypesLookup[gestur.Type] == "Tap gesture")
                {
                    LeapAsMouse.EnableClick(fram);
                }
                if (LeapGestures.GestureTypesLookup[gestur.Type] == "Circle gesture")
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (ShtorkaBool == true)
                        {
                            var storyboard4 = (System.Windows.Media.Animation.Storyboard)TryFindResource("hideGridSingle");
                            storyboard4.Begin();
                            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaDown");
                            storyboard5.Begin();
                            ShtorkaBool = false;
                            this.Shtorka.Visibility = Visibility.Hidden;
                        }
                    });
                }
                if (LeapGestures.GestureTypesLookup[gestur.Type] == "Swipe gesture")
                {
                    SwipeGesture swipe = new SwipeGesture(gestur);
                    Console.WriteLine("Swipe");
                    Dispatcher.Invoke(() =>
                    {
                        Leap.Vector swipeStart = swipe.StartPosition;
                        float startPoint = swipeStart.x;
                        Leap.Vector currentSwipePosition = swipe.Position;
                        float endPoint = currentSwipePosition.x;
                        //float currentSwipeSpeed = swipe.Speed;
                        if (endPoint > startPoint)
                        {
                            Console.WriteLine("Swipe left");
                            if (start != 1)
                            {
                                Canvas.SetLeft(this.ControlsGrid1, -190);
                                Canvas.SetLeft(this.ControlsGrid2, 48);
                                Canvas.SetLeft(this.ControlsGrid3, 282);
                                Canvas.SetLeft(this.ControlsGrid4, 523);
                                Canvas.SetLeft(this.ControlsGrid5, 764);
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == start - 1)
                                    {
                                        this.ShotContextGrid1.Text = elem.shortContext;
                                        this.TitleGrid1.Text = elem.title;
                                        this.ImGrid1.Source = elem.imgUrl;
                                    }
                                }
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == start)
                                    {
                                        this.ShotContextGrid2.Text = elem.shortContext;
                                        this.TitleGrid2.Text = elem.title;
                                        this.ImGrid2.Source = elem.imgUrl;
                                    }
                                }
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == start + 1)
                                    {
                                        this.ShotContextGrid3.Text = elem.shortContext;
                                        this.TitleGrid3.Text = elem.title;
                                        this.ImGrid3.Source = elem.imgUrl;
                                    }
                                }
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == start + 2)
                                    {
                                        this.ShotContextGrid4.Text = elem.shortContext;
                                        this.TitleGrid4.Text = elem.title;
                                        this.ImGrid4.Source = elem.imgUrl;
                                    }
                                }
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == start + 3)
                                    {
                                        this.ShotContextGrid5.Text = elem.shortContext;
                                        this.TitleGrid5.Text = elem.title;
                                        this.ImGrid5.Source = elem.imgUrl;
                                    }
                                }


                                var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("left-swipe");
                                storyboard.Completed += new EventHandler((x, y) =>
                                {
                                    storyboard.Remove();
                                    Canvas.SetLeft(this.ControlsGrid1, -190);
                                    Canvas.SetLeft(this.ControlsGrid2, 48);
                                    Canvas.SetLeft(this.ControlsGrid3, 282);
                                    Canvas.SetLeft(this.ControlsGrid4, 523);
                                    Canvas.SetLeft(this.ControlsGrid5, 764);


                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start)
                                        {
                                            this.ShotContextGrid2.Text = elem.shortContext;
                                            this.TitleGrid2.Text = elem.title;
                                            this.ImGrid2.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 1)
                                        {
                                            this.ShotContextGrid3.Text = elem.shortContext;
                                            this.TitleGrid3.Text = elem.title;
                                            this.ImGrid3.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 2)
                                        {
                                            this.ShotContextGrid4.Text = elem.shortContext;
                                            this.TitleGrid4.Text = elem.title;
                                            this.ImGrid4.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 3)
                                        {
                                            this.ShotContextGrid5.Text = elem.shortContext;
                                            this.TitleGrid5.Text = elem.title;
                                            this.ImGrid5.Source = elem.imgUrl;
                                        }
                                    }
                                    this.ShotContextGrid1.Text = null;
                                    this.TitleGrid1.Text = null;
                                    this.ImGrid1.Source = null;

                                });
                                storyboard.Begin();
                                start = start - 1;
                                fin = fin - 1;
                            }


                        }
                        else
                        {
                            if (fin != 10)
                            {
                                foreach (News elem in newslist)
                                {
                                    if (elem.id == fin + 1)
                                    {
                                        this.ShotContextGrid5.Text = elem.shortContext;
                                        this.TitleGrid5.Text = elem.title;
                                        this.ImGrid5.Source = elem.imgUrl;
                                    }
                                }
                                var storyboard2 = (System.Windows.Media.Animation.Storyboard)TryFindResource("right-swipe");
                                storyboard2.Completed += new EventHandler((x, y) =>
                                {
                                    storyboard2.Remove();
                                    Canvas.SetLeft(this.ControlsGrid1, 48);
                                    Canvas.SetLeft(this.ControlsGrid2, 282);
                                    Canvas.SetLeft(this.ControlsGrid3, 523);
                                    Canvas.SetLeft(this.ControlsGrid4, 764);
                                    Canvas.SetLeft(this.ControlsGrid5, 996);
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start)
                                        {
                                            this.ShotContextGrid1.Text = elem.shortContext;
                                            this.TitleGrid1.Text = elem.title;
                                            this.ImGrid1.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 1)
                                        {
                                            this.ShotContextGrid2.Text = elem.shortContext;
                                            this.TitleGrid2.Text = elem.title;
                                            this.ImGrid2.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 2)
                                        {
                                            this.ShotContextGrid3.Text = elem.shortContext;
                                            this.TitleGrid3.Text = elem.title;
                                            this.ImGrid3.Source = elem.imgUrl;
                                        }
                                    }
                                    foreach (News elem in newslist)
                                    {
                                        if (elem.id == start + 3)
                                        {
                                            this.ShotContextGrid4.Text = elem.shortContext;
                                            this.TitleGrid4.Text = elem.title;
                                            this.ImGrid4.Source = elem.imgUrl;
                                        }
                                    }
                                    this.ShotContextGrid5.Text = null;
                                    this.TitleGrid5.Text = null;
                                    this.ImGrid5.Source = null;
                                });
                                storyboard2.Begin();
                                start = start + 1;
                                fin = fin + 1;
                            }

                        }
                        Thread.Sleep(100);
                    });


                }

            }
        }

        private void ListenerOnOnFingersRegistered(Leap.Vector obj)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();

        }

        private void ImGrid2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempImage = this.ImGrid2.Source;
            var tempTitle = this.TitleGrid2.Text;

            Console.WriteLine(tempTitle);
        }



        private void ControlsGrid1_MouseEnter(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid1U");
            storyboard.Begin();
            this.ShotContextGrid1.Visibility = Visibility.Visible;
        }

        private void ControlsGrid1_MouseLeave(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid1D");
            storyboard.Begin();
            this.ShotContextGrid1.Visibility = Visibility.Hidden;
        }

        private void ControlsGrid2_MouseEnter(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid2U");
            storyboard.Begin();
            this.ShotContextGrid2.Visibility = Visibility.Visible;
        }

        private void ControlsGrid2_MouseLeave(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid2D");
            storyboard.Begin();
            this.ShotContextGrid2.Visibility = Visibility.Hidden;
        }

        private void ControlsGrid3_MouseEnter(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid3U");
            storyboard.Begin();
            this.ShotContextGrid3.Visibility = Visibility.Visible;
        }

        private void ControlsGrid3_MouseLeave(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid3D");
            storyboard.Begin();
            this.ShotContextGrid3.Visibility = Visibility.Hidden;
        }

        private void ControlsGrid4_MouseEnter(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid4U");
            storyboard.Begin();
            this.ShotContextGrid4.Visibility = Visibility.Visible;
        }

        private void ControlsGrid4_MouseLeave(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid4D");
            storyboard.Begin();
            this.ShotContextGrid4.Visibility = Visibility.Hidden;
        }

        private void ControlsGrid5_MouseEnter(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid5U");
            storyboard.Begin();
            this.ShotContextGrid5.Visibility = Visibility.Visible;
        }

        private void ControlsGrid5_MouseLeave(object sender, MouseEventArgs e)
        {
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGrid5D");
            storyboard.Begin();
            this.TitleGrid5.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            handler.Dispose();
            string _dir = @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug";
            string _del = @"*.jpg";
            string[] _files = Directory.GetFiles(_dir, _del);
            foreach (string fl in _files)
            {
                File.Delete(fl);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            String baseURL = "https://api.nytimes.com/svc/search/v2/articlesearch.json";
            String apiKey = "a0e18d4e9a7942a087445ded8aece2d6";
            //InitializeComponent();
            string request = " https://api.nytimes.com/svc/search/v2/articlesearch.json?api-key=a0e18d4e9a7942a087445ded8aece2d6&sort=newest&page=1";
            WebRequest req = WebRequest.Create(request);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            JObject news = JsonConvert.DeserializeObject<JObject>(Out);
            string imgUrl;
            string imgSrc;
            string shortCont;
            News[] newsarray = new News[10];
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    imgUrl = (string)"https://nytimes.com/" + news["response"]["docs"][i]["multimedia"][1]["url"];

                    using (WebClient client = new WebClient())


                    {
                        client.DownloadFile(imgUrl, AppDomain.CurrentDomain.BaseDirectory + "img" + i + ".jpg");
                        imgSrc = "img" + i + ".jpg";
                        shortCont = news["response"]["docs"][i]["snippet"].ToString();
                        shortCont = shortCont.Substring(0, 35);
                        shortCont += "...";
                        newsarray[i] = new News(news["response"]["docs"][i]["headline"]["main"].ToString(), imgSrc, news["response"]["docs"][i]["snippet"].ToString(), shortCont, i + 1, news["response"]["docs"][i]["section_name"].ToString(), news["response"]["docs"][i]["web_url"].ToString());
                        newslist.Add(newsarray[i]);

                        categoryList.Add(news["response"]["docs"][i]["section_name"].ToString());
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    try {
                    Console.WriteLine("wasted");
                    imgSrc = "Noimage.jpeg";
                    shortCont = news["response"]["docs"][i]["snippet"].ToString();
                    shortCont = shortCont.Substring(0, 35);
                    shortCont += "...";
                    newsarray[i] = new News(news["response"]["docs"][i]["headline"]["main"].ToString(), imgSrc, news["response"]["docs"][i]["snippet"].ToString(), shortCont, i + 1, news["response"]["docs"][i]["section_name"].ToString(), news["response"]["docs"][i]["web_url"].ToString());
                    newslist.Add(newsarray[i]);
                       
                        categoryList.Add(news["response"]["docs"][i]["section_name"].ToString());
                    }
                    catch (System.ArgumentOutOfRangeException exe) { }
                }
                
            }

         


            /*   News news1 = new News("Столетний фильм о Шерлоке Холмсе представят в Одессе", "im1.jpg", "Во время 7-го Одесского международного кинофестиваля, на Потемкинской лестнице будет презентована уникальная кинолента Шерлок Холмс 1916 года. Показ состоится в сопровождении симфонического оркестра, сообщает сайт ОМКФ.На Потемкинской лестнице традиционно состоится один из крупнейших open-air показов Европы. Киноперформанс на легендарных одесской лестнице в сопровождении симфонического оркестра по праву считается одним из самых масштабных и ярких событий фестиваля и ежегодно собирает более 15 тысяч зрителей. На этот раз под открытым небом Одессы посетители увидят немую киноленту Шерлок Холмс, которая в 2016 году празднует столетний юбилей, -говорится в сообщении.Фильм Шерлок Холмс был создан американским режиссером Артуром Бертелетом.Главную роль в фильме исполнил театральный актер Уильям Жиллетт.", "Во время 7-го Одесского международного кинофестиваля, на Потемкинской лестнице будет презентована уникальная кинолента Шерлок Холмс 1916 года...", 1, "movies",true,false);
               news.Add(news1);
                News news2 = new News("7 автомобилей, которые стали киногероями", "im2.jpg", "Режиссеры сняли ряд фильмов, где автомобиль играет более важную роль, чем его владелец.Иногда автомобили создают специально для фильмов. Такие машины не имеют аналогов.Чаще серийные автомобили приобретаютпопулярность у покупателей благодаря фильмам.Mustang Shelby GT500 EleanorGazeta.ua предлагает ТОП - 7 автомобилей, которые прославились в кино.Среди всех классических масл - каров особой популярностью пользуется Ford Mustang Shelby GT500 Eleanor 1967 года выпуска.Эта машина стала известной на весь мир благодаря художественному фильму Угнать за 60 секунд.Cadillac Miller - Meteor(ECTO 1)Cadillac ECTO 1 1959 года использовали как карету скорой медицинской помощи и катафалк.После того, как машина снялась в фильме Охотники за привидениями, автомобиль стал известным как эктомобиль.", "Режиссеры сняли ряд фильмов, где автомобиль играет более важную роль, чем его владелец...", 2, "Cars",false,false);
               news.Add(news2);
               News news3 = new News("Жюри Каннского кинофестиваля определило победителей", "im3.jpg", "Главный приз 69-го Каннского кинофестиваля - Золотую пальмовую ветвь - получил фильм Я, Дэниэл Блэйк британского режиссера Кена Лоуча.Об этом сообщает официальный Twitter - аккаунт фестиваля.Лента посвящена социальным проблемам в Европе.Ее герой - плотник Дэниел Блейк - серьезно заболел и пытается получить помощь.Гран - при достался фильму Это всего лишь конец света канадского режиссера Ксавье Долана.Приз жюри получила британский режиссер Андреа Арнольд за фильм Американская милашка, в которой речь идет о девушке - подростке, которая присоединяется к группе коммивояжеров.Лучшими режиссерами назвали сразу двух - француза Оливье Ассаяса и румына Кристиана Мунджиу.Лучшим актером назвали Шахаба Хоссеини из Ирана - за роль в фильме Коммивояжер.", "Главный приз 69-го Каннского кинофестиваля - Золотую пальмовую ветвь - получил фильм...", 3, "movies", false, false);
               news.Add(news3);
               News news4 = new News("В Киев привезли новую версию Славуты", "im4.jpg", "Сегодня с Запорожского автомобильного завода в Киев привезли новую версию Славуты. Ее официально презентуют завтра на Столичном автошоу.Автомобиль разработали на АвтоЗАЗе.Он имеет 5 - дверный кузов хэтчбек.Очень похож на китайский автомобиль Chery Riich G2.Салон выполнен в сером цвете с серебристыми вставками на торпеде.", "Сегодня с Запорожского автомобильного завода в Киев привезли новую версию Славуты. Ее официально презентуют завтра на Столичном автошоу...", 4, "Cars", false, true);
               news.Add(news4);
               News news5 = new News("Десятки новых моделей показали  в Киеве", "im5.jpg", "Сегодня началось 11 Столичное автошоу. Мероприятие проводят на территории дилерского центра Укравто на Столичном шоссе.Туда привезли более 200 новых автомобилей более 15 брендов.Среди них - Mercedes - Benz, Maserati, KIA, Opel, Chevrolet, Renault, Nissan, Chery, ЗАЗ и другие.Также мотоциклы марок Ducati и Harley - Davidson.Первой сегодня состоялась презентация концептуального отечественного легкового ЗАЗ Slavuta Нова, который в ближайшее время может стать серийным.Он сделан по образцу китайского Chery Riich G2.Сейчас на ЗАЗе в конструкцию модели вносят изменения и адаптируют его к отечественным потребностям.Когда именно новинка станет серийной, пока неизвестно.Однако руководство завода уже планирует локализовать в Украине производство более 50 % деталей и комплектующих к этой модели.", "Сегодня началось 11 Столичное автошоу. Мероприятие проводят на территории дилерского центра Укравто на Столичном шоссе...", 5, "Cars", false, false);
               news.Add(news5);
               News news6 = new News("Подача-важный элемент в игре против Серены-Свитолина", "im6.jpg", "Пресс-конференция первой ракетки Украины Элины Свитолиной после матча третьего круга на Ролан Гаррос.Напомним, украинка обыграла Ану Иванович из Сербии и вышла в 1/8 финала.Иванович - очень опытная теннисистка, и это чувствовалось. Не могу сказать, что подготовка к проходила не так, как к предыдущим играм. Все было довольно обычно.- сказала Свитолина.", "Пресс-конференция первой ракетки Украины Элины Свитолиной после матча третьего круга на Ролан Гаррос...", 6, "Sport", false, false);
               news.Add(news6);
               News news7 = new News("Шахтер хочет обменять Бойко на Нема - СМИ", "im7.jpg", "Вратарь Бешикташа Денис Бойко может продолжить карьеру в Шахтере.Как сообщает Fotospor, донецкий клуб готов предложить за 28 летнего футболиста бразильского полузащитника Веллингтона Нема.По информации издания, в трансфере заинтересован Мирон Маркевич, который работал с Бойко в Днепре, а сейчас может возглавить горняков.", "Вратарь Бешикташа Денис Бойко может продолжить карьеру в Шахтере...", 7, "Sport", false, false);
               news.Add(news7);
               News news8 = new News("Антонов запустит производство крупнейшего в мире самолета", "im8.jpg", "Государственное предприятие Антонов может запустить серийное производство самолета Ан-225 Мрия.Мрию считают крупнейшим в мире самолетом.Об этом рассказал вице - президент Антонова Александр Коцюба, пишет Дело. По скромным оценкам, создание и запуск такого самолета в серийное производство оцинюетья в 3 - 4 миллиарда долларов, -сказал он.", "Государственное предприятие Антонов может запустить серийное производство самолета Ан-225 Мрия.", 8, "Ukraine", false, false);
               news.Add(news8);
               News news9 = new News("Товарищеский матч. Румыния - Украина - 3:4", "im9.jpg", "Приветствуем все болельщиков сборной Украины! Сегодня национальная команда сыграет товарищеский матч против Румынии. Встреча пройдет в Турине на поле Стадио Гранде Олимпико Торино. Начало поединка в 20:30 по киевскому времени.Накануне украинцы провели тренировку в Турине.Занятие длилось около часа и в нем приняли участие 29 футболистов.В том числе Александр Зинченко, который последним присоединился к команде.", "Приветствуем все болельщиков сборной Украины! Сегодня национальная команда сыграет товарищеский матч против Румынии...", 9, "Sport", false, false);
               news.Add(news9);
               */


            this.ShotContextGrid1.Text = newsarray[0].shortContext;
            this.TitleGrid1.Text = newsarray[0].title;
            this.ImGrid1.Source = newsarray[0].imgUrl;

            this.ShotContextGrid2.Text = newsarray[1].shortContext;
            this.TitleGrid2.Text = newsarray[1].title;
            this.ImGrid2.Source = newsarray[1].imgUrl;

            this.ShotContextGrid3.Text = newsarray[2].shortContext;
            this.TitleGrid3.Text = newsarray[2].title;
            this.ImGrid3.Source = newsarray[2].imgUrl;

            this.ShotContextGrid4.Text = newsarray[3].shortContext;
            this.TitleGrid4.Text = newsarray[3].title;
            this.ImGrid4.Source = newsarray[3].imgUrl;

            /* this.ShotContextGrid5.Text = news5.shortContext;
             this.TitleGrid5.Text = news5.title;
             this.ImGrid5.Source = news5.imgUrl;*/
            string[] compareArray = new string[10];
            int j = 0;
            foreach (string elem in categoryList)
            {
                if (elem == "" | compareArray.Contains(elem))
                {

                }

                else
                {
                    this.listBox.Items.Add(elem);
                    compareArray[j] = elem;
                    j++;
                }

            }
            // https://api.nytimes.com/svc/search/v2/articlesearch.json?api-key=a0e18d4e9a…section_name%3AArts&sort=newest&facet_field=section_name&facet_filter=true

        }



        private void button_Click_1(object sender, RoutedEventArgs e)
        {



        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  Console.WriteLine(listBox.SelectedItem.ToString());
            if (this.listBox.SelectedItem != null)
            {
                string _dir = @"C:\Users\Peter\Documents\Visual Studio 2015\Projects\MainProject\MainProject\bin\Debug";
                string _del = @"*.jpg";
                string[] _files = Directory.GetFiles(_dir, _del);
                foreach (string fl in _files)
                {
                    File.Delete(fl);
                }

                string request = "https://api.nytimes.com/svc/search/v2/articlesearch.json?api-key=a0e18d4e9a7942a087445ded8aece2d6&fq=section_name%3A" + listBox.SelectedItem.ToString() + "&sort=newest&facet_field=section_name&facet_filter=true";
                WebRequest req = WebRequest.Create(request);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                JObject news = JsonConvert.DeserializeObject<JObject>(Out);
                string imgUrl;
                string imgSrc;
                string shortCont;
                News[] newsarray = new News[10];
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        imgUrl = (string)"https://nytimes.com/" + news["response"]["docs"][i]["multimedia"][1]["url"];

                        using (WebClient client = new WebClient())


                        {
                            client.DownloadFile(imgUrl, AppDomain.CurrentDomain.BaseDirectory + "img" + i + ".jpg");
                            imgSrc = "img" + i + ".jpg";
                            shortCont = news["response"]["docs"][i]["snippet"].ToString();
                            shortCont = shortCont.Substring(0, 35);
                            shortCont += "...";
                            newsarray[i] = new News(news["response"]["docs"][i]["headline"]["main"].ToString(), imgSrc, news["response"]["docs"][i]["snippet"].ToString(), shortCont, i + 1, news["response"]["docs"][i]["section_name"].ToString(), news["response"]["docs"][i]["web_url"].ToString());
                            newsListSorted.Add(newsarray[i]);

                        }
                    }
                    catch (System.ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("wasted");
                        imgSrc = "Noimage.jpeg";
                        shortCont = news["response"]["docs"][i]["snippet"].ToString();
                        shortCont = shortCont.Substring(0, 35);
                        shortCont += "...";
                        newsarray[i] = new News(news["response"]["docs"][i]["headline"]["main"].ToString(), imgSrc, news["response"]["docs"][i]["snippet"].ToString(), shortCont, i + 1, news["response"]["docs"][i]["section_name"].ToString(), news["response"]["docs"][i]["web_url"].ToString());
                        newsListSorted.Add(newsarray[i]);

                    }
                }
                foreach (News elem in newslist)
                {
                    Console.WriteLine(elem.title);
                }
                Console.WriteLine("//////////////////");
                Sort = true;
                fooList.Clear();
                fooList = newslist;
                newslist.Clear();
                newslist = newsListSorted;
                foreach (News elem1 in newslist)
                {
                    Console.WriteLine(elem1.title);
                }
                start = 1;
                fin = 4;
                this.ShotContextGrid1.Text = newsarray[1].shortContext;
                this.TitleGrid1.Text = newsarray[1].title;
                this.ImGrid1.Source = newsarray[1].imgUrl;

                this.ShotContextGrid2.Text = newsarray[2].shortContext;
                this.TitleGrid2.Text = newsarray[2].title;
                this.ImGrid2.Source = newsarray[2].imgUrl;

                this.ShotContextGrid3.Text = newsarray[3].shortContext;
                this.TitleGrid3.Text = newsarray[3].title;
                this.ImGrid3.Source = newsarray[3].imgUrl;

                this.ShotContextGrid4.Text = newsarray[4].shortContext;
                this.TitleGrid4.Text = newsarray[4].title;
                this.ImGrid4.Source = newsarray[4].imgUrl;
            }
        }

        private void listBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void ControlsGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempTitle = this.TitleGrid1.Text;
            foreach (News elem in newslist)
            {
                if (elem.title == tempTitle)
                {
                    SelectedNews = elem;
                    break;
                }
            }
            this.SingleImage.Source = SelectedNews.imgUrl;
            this.SingleTextTitle.Text = SelectedNews.title;
            this.SingleTextContext.Text = SelectedNews.context;
            Uri href = new Uri(SelectedNews.href);
            this.SingleHref.NavigateUri = href;
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();
            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaUp");
            storyboard5.Begin();
            ShtorkaBool = true;
            this.Shtorka.Visibility = Visibility.Visible;
        }

        private void ControlsGrid2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempTitle = this.TitleGrid2.Text;
            foreach (News elem in newslist)
            {
                if (elem.title == tempTitle)
                {
                    SelectedNews = elem;
                    break;
                }
            }
            this.SingleImage.Source = SelectedNews.imgUrl;
            this.SingleTextTitle.Text = SelectedNews.title;
            this.SingleTextContext.Text = SelectedNews.context;
            Uri href = new Uri(SelectedNews.href);
            this.SingleHref.NavigateUri = href;
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();
            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaUp");
            storyboard5.Begin();
            ShtorkaBool = true;
            this.Shtorka.Visibility = Visibility.Visible;
        }

        private void ControlsGrid3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempTitle = this.TitleGrid3.Text;
            foreach (News elem in newslist)
            {
                if (elem.title == tempTitle)
                {
                    SelectedNews = elem;
                    break;
                }
            }
            this.SingleImage.Source = SelectedNews.imgUrl;
            this.SingleTextTitle.Text = SelectedNews.title;
            this.SingleTextContext.Text = SelectedNews.context;
            Uri href = new Uri(SelectedNews.href);
            this.SingleHref.NavigateUri = href;
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();
            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaUp");
            storyboard5.Begin();
            ShtorkaBool = true;
            this.Shtorka.Visibility = Visibility.Visible;
        }

        private void ControlsGrid4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempTitle = this.TitleGrid4.Text;
            foreach (News elem in newslist)
            {
                if (elem.title == tempTitle)
                {
                    SelectedNews = elem;
                    break;
                }
            }
            this.SingleImage.Source = SelectedNews.imgUrl;
            this.SingleTextTitle.Text = SelectedNews.title;
            this.SingleTextContext.Text = SelectedNews.context;
            Uri href = new Uri(SelectedNews.href);
            this.SingleHref.NavigateUri = href;
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();
            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaUp");
            storyboard5.Begin();
            ShtorkaBool = true;
            this.Shtorka.Visibility = Visibility.Visible;
        }

        private void ControlsGrid5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tempTitle = this.TitleGrid5.Text;
            foreach (News elem in newslist)
            {
                if (elem.title == tempTitle)
                {
                    SelectedNews = elem;
                    break;
                }
            }
            this.SingleImage.Source = SelectedNews.imgUrl;
            this.SingleTextTitle.Text = SelectedNews.title;
            this.SingleTextContext.Text = SelectedNews.context;
            Uri href = new Uri(SelectedNews.href);
            this.SingleHref.NavigateUri = href;
            var storyboard = (System.Windows.Media.Animation.Storyboard)TryFindResource("visibleGridSingle");
            storyboard.Begin();
            var storyboard5 = (System.Windows.Media.Animation.Storyboard)TryFindResource("ShtorkaUp");
            storyboard5.Begin();
            ShtorkaBool = true;
            this.Shtorka.Visibility = Visibility.Visible;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
