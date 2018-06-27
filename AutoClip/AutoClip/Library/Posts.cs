using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class Posts
    {
        public int STT;
        public string Title;
        public string Text_Content;
        public string Tag = "";
        public string Image = "";
        public string url = "";
        public string Time = "";
        public int Ngay = 0;

        //Login
        public void Login(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            HtmlNode Text = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='post-body entry-content']");
            Text_Content = Text.InnerText.Trim();


        }
        //crwal blogspot
        public void blog(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {
                for (int i = 1; i < 31; i++)
                {
                    string url_html = "";
                    HtmlNode _Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='post-body entry-content']/ol/li[{0}]/div/div/div[1]/div[2]/div[1]/div[1]", i));
                    url_html = _Content.InnerHtml;
                    int html = url_html.IndexOf("class");
                    url_html = url_html.Remove(html - 2);
                    int http = url_html.IndexOf("id=");
                    url_html = url_html.Remove(0, http + 3);
                    url += url_html + ",";

                }

            }
            catch (Exception)
            {


            }
            StreamWriter sw = new StreamWriter(@"C:\RACC\videoId.txt");
            string[] videoidnow = url.Split(',');
            foreach (var item in videoidnow)
            {
                sw.WriteLine(item);
            }
            sw.Close();



        }

        ////ltn.com.tw
        public void Handle(string k, string Text_Content, string Title, string Tag, string Image)
        {
            bool key = true;
            // in file vào file txt
            if (!File.Exists(string.Format(@"C:\RACC\Data\Video" + k + @"\Input.txt", k)))
            {

                //  MessageBox.Show("Số Text nhiều hơn số Folder | Vui lòng tạo thêm folder");

                key = false;
            }
            if (key)
            {
                String dulieunhap = Text_Content;
                String filepath = @"C:\RACC\Data\Video" + k + @"\Input.txt";// đường dẫn của file muốn tạo
                FileStream fs = new FileStream(filepath, FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
                sWriter.WriteLine(dulieunhap);
                sWriter.Flush();
                fs.Close();


                String filepath1 = @"C:\RACC\Data\Video" + k + @"\Title.txt";// đường dẫn của file muốn tạo
                FileStream fs1 = new FileStream(filepath1, FileMode.Create);
                StreamWriter sWriter1 = new StreamWriter(fs1, Encoding.UTF8);//fs là 1 FileStream 
                sWriter1.WriteLine(Title);
                sWriter1.Flush();
                fs1.Close();

                String filepath2 = @"C:\RACC\Data\Video" + k + @"\TagVideo.txt";// đường dẫn của file muốn tạo
                FileStream fs2 = new FileStream(filepath2, FileMode.Create);
                StreamWriter sWriter2 = new StreamWriter(fs2, Encoding.UTF8);//fs là 1 FileStream 
                sWriter2.WriteLine(Tag);
                sWriter2.Flush();
                fs2.Close();




            }
            // download ảnh
            try
            {
                WebClient Wc = new WebClient();
                String txtSaveFile = @"C:\RACC\Data\Video" + k + @"\Image\";

                Uri FileUrl = new Uri(Image);//Uri để tạo đầu vào cho Wc tải về, Trim để xóa kí tự rỗng ở 2 đầu


                Wc.DownloadFileAsync(FileUrl, txtSaveFile + "image.jpg");
                while (Wc.IsBusy)
                {

                }


                if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\image.jpg", k)))
                {

                    Uri FileUrl1 = new Uri(Image);//Uri để tạo đầu vào cho Wc tải về, Trim để xóa kí tự rỗng ở 2 đầu


                    Wc.DownloadFileAsync(FileUrl1, txtSaveFile + "image.jpeg");
                    while (Wc.IsBusy)
                    {

                    }
                }
                if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\image.jpeg", k))
                    && !File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\image.jpg", k)))
                {

                    Uri FileUrl2 = new Uri(Image);//Uri để tạo đầu vào cho Wc tải về, Trim để xóa kí tự rỗng ở 2 đầu


                    Wc.DownloadFileAsync(FileUrl2, txtSaveFile + "image.png");
                    while (Wc.IsBusy)
                    {

                    }
                }




            }
            catch (Exception)
            {

            }
        }


        public void Crawl(string Link)
        {
            try
            {
                Title = "";
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='whitecon articlebody']/h1");
                //$x(".//*[@class='whitecon articlebody']/h1")[0].innerText
                Title = pTitle.InnerText.Trim();

                // ok  title
                Text_Content = "";
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='text']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }
                // 2 trường hợp trong phần style
                try
                {
                    try
                    {
                        /// Image crawl
                        HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='pic750']");
                        Image = image.InnerHtml;
                        if (Image.Contains("jpg"))
                        {
                            int jpg = Image.IndexOf("jpg");
                            Image = Image.Remove(jpg + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("png");
                            Image = Image.Remove(png + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("jpeg");
                            Image = Image.Remove(png + 4);
                        }

                        int http = Image.IndexOf("http");
                        Image = Image.Remove(0, http);
                        // string Image
                        /// Done Crawl Image
                        /// 

                    }
                    catch (Exception)
                    {
                        HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='pic300 boxTitle']/li[1]");
                        Image = image.InnerHtml;
                        if (Image.Contains("jpg"))
                        {
                            int jpg = Image.IndexOf("jpg");
                            Image = Image.Remove(jpg + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("png");
                            Image = Image.Remove(png + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("jpeg");
                            Image = Image.Remove(png + 4);
                        }

                        int http = Image.IndexOf("http");
                        Image = Image.Remove(0, http);
                    }
                }
                catch (Exception)
                {

                }


                Tag = "";
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='keyword boxTitle']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";

                    }

                }
                catch (Exception)
                {


                }
                // time
                try
                {
                    HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='text']/span"));
                    Time = Time1.InnerText;
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

            }


        }
        public void Crawl_Entertainment(string Link)
        {
            try
            {


                Title = "";
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='news_content']/h1");
                string _Title = pTitle.InnerText.Trim();

                Title = _Title;
                // ok  title
                Text_Content = "";
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='news_content']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }
                // 2 trường hợp trong phần style
                try
                {
                    /// Image crawl
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='ph_i']");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 

                }
                catch (Exception)
                {

                }
                try
                {
                    HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='date']"));
                    Time = Time1.InnerText;
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {


                }
            }
            catch
            {


            }

        }
        public void Crawl_Sports(string Link)
        {
            try
            {

          
            Title = "";
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='news_content']/h1");
            string _Title = pTitle.InnerText.Trim();

            Title = _Title;
            // ok  title
            Text_Content = "";
            //HtmlNode Content33 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='news_content']/div[2]/div/p");
            //string asfsdf = Content33.InnerHtml;
            try
            {
                for (int i = 1; i < 100; i++)
                {
                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='news_content']/div[2]/div/p[{0}]", i));
                    Text_Content += Content.InnerText;
                    Text_Content += "\r\n" + "\r\n";

                }

            }
            catch (Exception)
            {


            }
            // 2 trường hợp trong phần style
            try
            {
                /// Image crawl
                HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='ph_i']");
                Image = image.InnerHtml;
                if (Image.Contains("jpg"))
                {
                    int jpg = Image.IndexOf("jpg");
                    Image = Image.Remove(jpg + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("png");
                    Image = Image.Remove(png + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("jpeg");
                    Image = Image.Remove(png + 4);
                }

                int http = Image.IndexOf("http");
                Image = Image.Remove(0, http);
                // string Image
                /// Done Crawl Image
                /// 

            }
            catch (Exception)
            {

            }

            try
            {
                HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='c_time']"));
                Time = Time1.InnerText;
                string pattern = @"\d{2}";
                Regex reg = new Regex(pattern);
                MatchCollection m = reg.Matches(Time);
                string ngay = "";
                foreach (var item in m)
                {
                    ngay += item.ToString();
                }
                Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
            }
            catch (Exception)
            {


            }
            }
            catch
            {


            }


        }
        public void Crawl_Style(string Link)
        {
            try
            {

            Title = "";
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='caption']/h2");
            string _Title = pTitle.InnerText.Trim();
            Title = _Title;
            // ok  title
            Text_Content = "";
            try
            {
                for (int i = 1; i < 100; i++)
                {
                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='boxTitle']/p[{0}]", i));
                    Text_Content += Content.InnerText;
                    Text_Content += "\r\n" + "\r\n";

                }

            }
            catch (Exception)
            {


            }
            // 2 trường hợp trong phần style
            try
            {
                /// Image crawl
                HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='ph_i']");
                Image = image.InnerHtml;
                if (Image.Contains("jpg"))
                {
                    int jpg = Image.IndexOf("jpg");
                    Image = Image.Remove(jpg + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("png");
                    Image = Image.Remove(png + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("jpeg");
                    Image = Image.Remove(png + 4);
                }

                int http = Image.IndexOf("http");
                Image = Image.Remove(0, http);
                // string Image
                /// Done Crawl Image
                /// 

            }
            catch (Exception)
            {

            }

            }
            catch
            {


            }



        }


        public void Crawl_Sohu_Society(string Link)
        {
            try
            {


                Title = "";
                string _URL = "http://www.sohu.com/a/166767737_737391?loc=1&focus_pic=0&_f=index_chan43news_6";
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='text-title']");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // ok  title
                Text_Content = "";
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='article']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }
                // 2 trường hợp trong phần style
                try
                {
                    /// Image crawl
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='pic750']");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 

                }
                catch (Exception)
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='pic300 boxTitle']/li[1]");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                }

                Tag = "";
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='keyword boxTitle']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";

                    }

                }
                catch (Exception)
                {


                }

            }
            catch (Exception)
            {

            }
        }
        public void GetLink_Sohu(string Link)
        {
            try
            {
                //   string _URL = string.Format("http://www.aboluowang.com/{0}/roll/", node);
                string _URL = "http://news.sohu.com/scroll/";
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int i = 1; i < 10; i++)
                {
                    for (int ii = 1; ii < 12; ii++)
                    {
                        try
                        {
                            HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='newsList']/div[1]/ul/li[1]/em", ii, i));
                            string url_html = URL.InnerHtml;
                            int data = url_html.LastIndexOf("target");
                            url_html = url_html.Remove(data - 2);
                            int http = url_html.LastIndexOf("href");
                            url_html = url_html.Remove(0, http);
                            url += url_html + ",";
                            ii++;

                        }
                        catch (Exception)
                        {

                        }

                    }
                }


            }
            catch (Exception)
            {
            }

        }
        public void GetLink(string Link, int Limit)
        {

            try
            {
                for (int ii = 1; ii < Limit; ii++)
                {
                    string _URL = Link + "/" + ii;
                    HtmlWeb _htmlWeb = new HtmlWeb();
                    _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                    for (int i = 0; i < 20; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='list imm']/li[{0}]", i + 1));
                        string url_html = URL.InnerHtml;
                        int data = url_html.IndexOf("data-desc");
                        url_html = url_html.Remove(data - 2);
                        int http = url_html.IndexOf("http");
                        url_html = url_html.Remove(0, http);
                        url += url_html + ",";


                    }
                }
            }
            catch (Exception)
            {


            }





        }
        ////Chinatime.com
        public void Crawl_Chinatimes_Opinion(string Link)
        {
            try
            {


                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='bigpicbox']/header/hgroup/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title;
                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='arttext marbotm clear-fix']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {


                    }
                    // download image
                    try
                    {

                        HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='picbox2']/img");
                        Image = image.Attributes["src"].Value;


                    }
                    catch (Exception)
                    {
                        if (Image == "")
                        {
                            HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='picbox2']/a");
                            Image = image.Attributes["href"].Value;
                        }

                    }
                }
                catch (Exception)
                {

                }
                // TIME
                try
                {
                    //reporter
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='reporter']/time");
                    Time = time1.InnerText.Trim();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());

                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {


            }

        }
        public void GetLink_Chinatimes_Opinion(string Link, string key)
        {
            try
            {

                // test link : http://opinion.chinatimes.com/20180220002304-262105
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int i = 0; i < 30; i++)
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='news-list']/ul/li[{0}]", i + 1));
                    string url_html = URL.InnerHtml;
                    Regex reg = new Regex(@"\d{14}.\d{6}");
                    Match match = reg.Match(url_html);

                    if (key == "opinion")
                    {
                        url += "http://opinion.chinatimes.com/" + match.ToString() + ",";
                    }
                    else if (key == "world")
                    {
                        url += "http://www.chinatimes.com/realtimenews/" + match.ToString() + ",";
                    }
                    else if (key == "china")
                    {
                        url += "http://www.chinatimes.com/realtimenews/" + match.ToString() + ",";
                    }
                    else if (key == "armament")
                    {
                        url += "http://www.chinatimes.com/realtimenews/" + match.ToString() + ",";
                    }
                    else if (key == "armament")
                    {
                        url += "http://www.chinatimes.com/realtimenews/" + match.ToString() + ",";
                    }
                    else if (key == "sports")
                    {
                        url += "http://www.chinatimes.com/realtimenews/" + match.ToString() + ",";
                    }

                }


            }
            catch (Exception)
            {


            }

        }

        public void GetLink_DWnews(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);

            try
            {
                for (int i = 1; i < 100; i++)
                {
                    string url_html = "";
                    HtmlNode _Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='main-pbl humanityMore']/ul/li[{0}]/div", i));
                    url_html = _Content.InnerHtml;
                    int html = url_html.IndexOf("html");
                    url_html = url_html.Remove(html + 4);
                    int http = url_html.IndexOf("http");
                    url_html = url_html.Remove(0, http);
                    url += url_html + ",";

                }

            }
            catch (Exception)
            {


            }


        }
        public void Crawl_DWnews(string Link)
        {
            try
            {
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='box']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                try
                {
                    HtmlNode component_2_0 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='component_2_0']/p"));
                    Text_Content += component_2_0.InnerText;
                    Text_Content += "\r\n" + "\r\n";

                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='component_2_1']/p"));
                    Text_Content += Content.InnerText;
                    Text_Content += "\r\n" + "\r\n";



                }
                catch (Exception)
                {


                }
                // download image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='component_3_0']/a");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

            }


        }
        // // udn.com
        public void Get_udn(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {
                for (int ii = 1; ii < 8; ii++)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='area category_box_list'][{0}]/dl/dt[{1}]", ii, i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.IndexOf("target");
                        url_html = url_html.Remove(data - 2);
                        int http = url_html.IndexOf("/news/");
                        url_html = url_html.Remove(0, http);
                        url_html = "https://udn.com" + url_html;
                        url += url_html + ",";


                    }
                }

            }
            catch (Exception)
            {


            }


        }
        public void Get_udn_Breaknews(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            for (int i = 1; i < 61; i++)
            {
                try
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='breaknews_body']/dl/dt[{0}]/h2", i));
                    string url_html = URL.InnerHtml;
                    int data = url_html.IndexOf("target");
                    url_html = url_html.Remove(data - 2);
                    int http = url_html.IndexOf("/news/");
                    url_html = url_html.Remove(0, http);
                    url_html = "https://udn.com" + url_html;
                    url += url_html + ",";
                }
                catch
                {

                    Console.WriteLine("Crawl false");
                }



            }



        }
        public void Crawl_UDN_Sports(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='story_body_content']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // download content
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@id='story_body_content']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }

                Text_Content = Text_Content.Replace("<!-- /.photo_pop -->", "");
                Text_Content = Text_Content.Replace("<!-- /.photo -->", "");
                Text_Content = Text_Content.Replace("facebook", "");

                //  download image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='photo_center photo-story']/a[1]");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("src");
                    Image = Image.Remove(src - 2);

                }
                catch (Exception)
                {

                }


            }
            catch (Exception)
            {

            }
            try
            {
                HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='story_bady_info_author']/span");
                Time = time1.InnerText;
                string pattern = @"\d{2}";
                Regex reg = new Regex(pattern);
                MatchCollection m = reg.Matches(Time);
                string ngay = "";
                foreach (var item in m)
                {
                    ngay += item.ToString();
                }
                Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
            }
            catch (Exception)
            {
            }
            //tag
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@id='story_tags']/a[{0}]", i));
                    Tag += Tag1.InnerText;
                    Tag += ",";
                }
            }
            catch (Exception)
            {
            }


        }
        //sina.com.cn
        public void Get_sina(string Link)
        {

            try
            {
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int ii = 1; ii < 30; ii++)
                {
                    try
                    {
                        for (int i = 1; i < 30; i++)
                        {
                            HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='subShowContent1_news{0}']/div[{1}]/h2", ii, i));
                            string url_html = URL.InnerHtml;
                            int data = url_html.IndexOf("target");
                            url_html = url_html.Remove(data - 2);
                            int http = url_html.IndexOf("http");
                            url_html = url_html.Remove(0, http);

                            url += url_html + ",";

                        }

                    }
                    catch (Exception)
                    {


                    }




                }
            }
            catch (Exception)
            {


            }

        }
        public void Crawl_sina(string Link)
        {
            try
            {
                Title = "";
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='main-title']");
                string _Title = pTitle.InnerText.Trim();

                Title = _Title;
                // ok  title
                Text_Content = "";
                //HtmlNode Content33 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='news_content']/div[2]/div/p");
                //string asfsdf = Content33.InnerHtml;
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='article']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }
                }
                catch (Exception)
                {

                }
                // 2 trường hợp trong phần style
                try
                {
                    /// Image crawl
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='img_wrapper']");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 

                }
                catch (Exception)
                {
                }
                try
                {
                    HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='date-source']"));
                    Time = Time1.InnerText;
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {

                }
                //tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='article-keywords']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }

            }
            catch (Exception)
            {
            }

        }
        public void Get_News_Sina(string Link)
        {
            try
            {
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int ii = 1; ii < 90; ii++)
                {

                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='list_009']/li[{0}]", ii));
                    string url_html = URL.InnerHtml;
                    int data = url_html.IndexOf("target");
                    url_html = url_html.Remove(data - 2);
                    int http = url_html.IndexOf("http");
                    url_html = url_html.Remove(0, http);

                    url += url_html + ",";


                }
            }
            catch (Exception)
            {


            }
        }
        public void Get_Military(string Link)
        {
            try
            {
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int ii = 1; ii < 30; ii++)
                {
                    try
                    {
                        for (int i = 1; i < 30; i++)
                        {
                            HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='fixList']/ul[{0}]/li[{1}]", ii, i));
                            string url_html = URL.InnerHtml;
                            int data = url_html.IndexOf("target");
                            url_html = url_html.Remove(data - 2);
                            int http = url_html.IndexOf("http");
                            url_html = url_html.Remove(0, http);

                            url += url_html + ",";

                        }

                    }
                    catch (Exception)
                    {


                    }


                }
            }
            catch (Exception)
            {


            }

        }
        public void Crawl_Military(string Link)
        {
            try
            {
                Title = "";
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='main-content w1240']/h1");
                string _Title = pTitle.InnerText.Trim();

                Title = _Title;
                // ok  title
                Text_Content = "";
                //HtmlNode Content33 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='news_content']/div[2]/div/p");
                //string asfsdf = Content33.InnerHtml;
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='article']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }
                Text_Content = Text_Content.Replace("&nbsp;", "");
                // 2 trường hợp trong phần style
                try
                {
                    /// Image crawl
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='img_wrapper']");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 

                }
                catch (Exception)
                {

                }

                try
                {
                    HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='date-source']/span"));
                    Time = Time1.InnerText;
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='keywords']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
            }


        }
        public void Crawl_Military_International(string Link)
        {
            try
            {
                Title = "";
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='main-content w1240']/h1");
                string _Title = pTitle.InnerText.Trim();

                Title = _Title;
                // ok  title
                Text_Content = "";
                //HtmlNode Content33 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='news_content']/div[2]/div/p");
                //string asfsdf = Content33.InnerHtml;
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='article']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }
                Text_Content = Text_Content.ConvertHTML();
                // 2 trường hợp trong phần style
                try
                {
                    /// Image crawl
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='img_wrapper']");
                    Image = image.InnerHtml;
                    if (Image.Contains("jpg"))
                    {
                        int jpg = Image.IndexOf("jpg");
                        Image = Image.Remove(jpg + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("png");
                        Image = Image.Remove(png + 3);
                    }
                    else if (Image.Contains("png"))
                    {
                        int png = Image.IndexOf("jpeg");
                        Image = Image.Remove(png + 4);
                    }

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    // string Image
                    /// Done Crawl Image
                    /// 

                }
                catch (Exception)
                {

                }

                try
                {
                    HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='date-source']"));
                    Time = Time1.InnerText;
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='art_keywords']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
            }


        }
        //sports sina.com.vn
        public void Get_Sport_Sina(string Link, string node)
        {
            try
            {
                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                int a = 0;
                if (node == "sportsother")// node đầu tiên là ads
                {
                    a = 1;
                }
                for (int i1 = 1 + a; i1 < 15; i1++)
                {
                    for (int i2 = 1; i2 < 5; i2++)
                    {
                        for (int i3 = 1; i3 < 10; i3++)
                        {
                            try
                            {
                                HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@aside='{3}']/div[1]/div[1]/div[{0}]/figure[{1}]/ul/li[{2}]", i1, i2, i3, node));
                                string url_html = URL.InnerHtml;
                                int data = url_html.IndexOf("target");
                                url_html = url_html.Remove(data - 2);
                                int http = url_html.IndexOf("http");
                                url_html = url_html.Remove(0, http);
                                url += url_html + ",";
                            }
                            catch (Exception)
                            {


                            }


                        }
                    }
                }
            }
            catch (Exception)
            {


            }

        }
        public void Crawl_Sport_Sina(string Link)
        {
            try
            {


                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {
                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='j_title']");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title;
                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='artibody']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {
                    }
                    Text_Content = Text_Content.Replace("<!--StartFragment -->", "");
                    // download image
                    try
                    {
                        HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-a__figure']");
                        Image = image.InnerHtml;
                        if (Image.Contains("jpg"))
                        {
                            int jpg = Image.IndexOf("jpg");
                            Image = Image.Remove(jpg + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("png");
                            Image = Image.Remove(png + 3);
                        }
                        else if (Image.Contains("png"))
                        {
                            int png = Image.IndexOf("jpeg");
                            Image = Image.Remove(png + 4);
                        }

                        int http = Image.IndexOf("http");
                        Image = Image.Remove(0, http);
                        // string Image
                        /// Done Crawl Image
                        /// 
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {

                }
                // TIME
                try
                {
                    //reporter
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-a__info layout-mb-d']/span");
                    Time = time1.InnerText.Trim();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {


            }
        }
        //ifuun.com
        public void Get_Ifunn()
        {
            // lấy của 4 page
            string[] Link = { "http://www.ifuun.com/category/%E8%B6%A3%E5%91%B3/page/1/",
                "http://www.ifuun.com/category/%E8%B6%A3%E5%91%B3/page/2/",
                "http://www.ifuun.com/category/%E8%B6%A3%E5%91%B3/page/3/",
                "http://www.ifuun.com/category/%E8%B6%A3%E5%91%B3/page/4/" };
            for (int page = 1; page < 5; page++)
            {
                try
                {
                    string _URL = Link[page];
                    HtmlWeb _htmlWeb = new HtmlWeb();
                    _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                    for (int ii = 1; ii < 30; ii++)
                    {
                        try
                        {
                            HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='post_container']/li[{0}]/div[2]/h2", ii));
                            string url_html = URL.InnerHtml;
                            int data = url_html.IndexOf("rel");
                            url_html = url_html.Remove(data - 2);
                            int http = url_html.IndexOf("http");
                            url_html = url_html.Remove(0, http);

                            url += url_html + ",";

                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                catch (Exception)
                {
                }

            }
        }
        public void Crawl_Ifunn(string Link)
        {
            Title = "";
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='content']/div[1]/h1");
            string _Title = pTitle.InnerText.Trim();

            Title = _Title;
            // ok  title
            Text_Content = "";
            //HtmlNode Content33 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@class='news_content']/div[2]/div/p");
            //string asfsdf = Content33.InnerHtml;
            try
            {
                for (int i = 1; i < 100; i++)
                {
                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='artibody']/p[{0}]", i));
                    Text_Content += Content.InnerText;
                    Text_Content += "\r\n" + "\r\n";

                }

            }
            catch (Exception)
            {


            }
            // 2 trường hợp trong phần style
            try
            {
                /// Image crawl
                HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='img_wrapper']");
                Image = image.InnerHtml;
                if (Image.Contains("jpg"))
                {
                    int jpg = Image.IndexOf("jpg");
                    Image = Image.Remove(jpg + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("png");
                    Image = Image.Remove(png + 3);
                }
                else if (Image.Contains("png"))
                {
                    int png = Image.IndexOf("jpeg");
                    Image = Image.Remove(png + 4);
                }

                int http = Image.IndexOf("http");
                Image = Image.Remove(0, http);
                // string Image
                /// Done Crawl Image
                /// 

            }
            catch (Exception)
            {

            }

            try
            {
                HtmlNode Time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='page-info']/span"));
                Time = Time1.InnerText;
            }
            catch (Exception)
            {


            }
        }
        ////aboluowang.com
        public void Get_abo(string Link, string node)
        {
            try
            {
                string _URL = string.Format("http://www.aboluowang.com/{0}/roll/", node);
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int ii = 1; ii < 100; ii++)
                {
                    try
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='list f16'][2]/li[{0}]", ii));
                        string url_html = URL.InnerHtml;
                        int data = url_html.IndexOf("title");
                        url_html = url_html.Remove(data - 2);
                        int http = url_html.IndexOf("href");
                        url_html = url_html.Remove(0, http + 6);
                        url_html = "http://www.aboluowang.com" + url_html;
                        int DoDai = "http://www.aboluowang.com/2017/0921/996950.html".Length;
                        if (url_html.Length <= DoDai + 3)
                        {
                            url += url_html + ",";
                        }


                    }
                    catch (Exception)
                    {

                    }

                }



            }
            catch (Exception)
            {
            }
        }
        public void Crawl_abo(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='Article']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // download content
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='content']/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }

                Text_Content = Text_Content.Replace("&rdquo;", "\"");
                Text_Content = Text_Content.Replace("&ldquo;", "\"");
                Text_Content = Text_Content.Replace("&nbsp;", "");
                //  download image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='conimg']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("m1");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);
                    Image = "https://" + Image;


                    /// 
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

            }

            try
            {
                //  HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode("//*[@id='articletop']/text()[2]");
                //"//*[@class='f15 articletime']"

                //  Time = time1.InnerText;
                Ngay = DateTime.Now.Day;
            }
            catch (Exception)
            {


            }
        }
        //cna.com.tw
        public void Get_Cna(string Link, string node)
        {
            for (int ii = 1; ii < 4; ii++)
            {
                string _URL = string.Format("http://www.cna.com.tw/list/{1}-{0}.aspx", ii, node);

                try
                {

                    HtmlWeb _htmlWeb = new HtmlWeb();
                    _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


                    for (int i = 1; i < 21; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='article_list']/ul/li[{0}]", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.IndexOf(">");
                        url_html = url_html.Remove(data - 1);
                        int http = url_html.IndexOf("/news");
                        url_html = url_html.Remove(0, http);
                        url_html = "http://www.cna.com.tw" + url_html;
                        url += url_html + ",";
                    }


                }
                catch (Exception)
                {
                }
            }//end for ii
        }
        public void Crawl_Cna(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='news_title']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // download content
                try
                {

                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='article_box']/section/p"));
                    Text_Content += Content.InnerText;
                    Text_Content += "\r\n" + "\r\n";



                }
                catch (Exception)
                {


                }

                //Text_Content = Text_Content.Replace("&rdquo;", "\"");
                //Text_Content = Text_Content.Replace("&ldquo;", "\"");
                //Text_Content = Text_Content.Replace("&nbsp;", "");
                //  download image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='pic_750_inner']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);



                    /// 
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

            }

            try
            {
                HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='update_times']");
                Time = time1.InnerText;

                // truyền vào regex là pattern
                // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                // gọi r bằng to string()
                Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}\W\d{2}\W\d{2}");
                Match chuoi = reg1.Match(Time);
                Time = chuoi.ToString();
                string pattern = @"\d{2}";
                Regex reg = new Regex(pattern);
                MatchCollection m = reg.Matches(Time);
                string ngay = "";
                foreach (var item in m)
                {
                    ngay += item.ToString();
                }
                Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());

            }
            catch (Exception)
            {


            }
        }
        //sports.khan.co.kr
        public void Get_Khan(string Link, string node)
        {

            for (int ii = 1; ii < 3; ii++)
            {
                string _URL = string.Format("http://sports.khan.co.kr/news/page/{1}{0}", ii, node);

                try
                {

                    HtmlWeb _htmlWeb = new HtmlWeb();
                    _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


                    for (int i = 2; i < 23; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='content']/div[{0}]/dl/dt", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.IndexOf("title");
                        url_html = url_html.Remove(data - 2);
                        int http = url_html.IndexOf("http");
                        url_html = url_html.Remove(0, http);

                        url += url_html + ",";
                    }


                }
                catch (Exception)
                {
                }
            }//end for ii
        }
        public void Crawl_Khan(string Link)
        {

            string _URL = "http://star.ohmynews.com/NWS_Web/OhmyStar/at_pg.aspx?CNTN_CD=A0002368341";
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='tit']");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // download content
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='articleBody']/p[{0}]", i));


                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";

                    }

                }
                catch (Exception)
                {


                }

                Text_Content = Text_Content.Replace("&rdquo;", "\"");
                Text_Content = Text_Content.Replace("&ldquo;", "\"");
                Text_Content = Text_Content.Replace("&nbsp;", "");
                //  download image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='art_photo_wrap']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("m1");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);
                    Image = "https://" + Image;


                    /// 
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

            }

            try
            {
                //HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='f15 articletime']");
                //Time = time1.InnerHtml;
            }
            catch (Exception)
            {


            }
        }
        // Cwbst.com
        public void Get_Cwbst(string Link, string node)
        {
            try
            {
                string _URL = string.Format("http://www.cwbst.com/{0}", node);
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                for (int ii = 2; ii < 100; ii++)
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='left_list fl']/div[{0}]/div/h3", ii));
                    string url_html = URL.InnerHtml;
                    int data = url_html.IndexOf("html");
                    url_html = url_html.Remove(data + 4);
                    int http = url_html.IndexOf(node);
                    url_html = url_html.Remove(0, http);
                    url_html = "http://www.cwbst.com/" + url_html;
                    int DoDai = "http://www.aboluowang.com/2017/0921/996950.html".Length;
                    url += url_html + ",";
                }



            }
            catch (Exception)
            {

            }

        }
        public void Crawl_Cwbst(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='left_list fl']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                int KeyBreak = 0;
                // download content
                try
                {
                    for (int i = 2; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='article_content']/p[{0}]", i));
                        if (Content.InnerHtml.Contains("jpeg") && KeyBreak == 0)
                        {
                            Image = Content.InnerHtml;

                            int http = Image.IndexOf("//");
                            Image = Image.Remove(0, http);
                            int src = Image.IndexOf("jpeg");
                            Image = Image.Remove(src + 4);
                            Image = "https:" + Image;
                            KeyBreak = 1;
                        }
                        if (Content.InnerText.Length > 10)
                        {
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }

                    }

                }
                catch (Exception)
                {


                }

                Text_Content = Text_Content.Replace("&rdquo;", "\"");
                Text_Content = Text_Content.Replace("&ldquo;", "\"");
                Text_Content = Text_Content.Replace("&nbsp;", "");
                //  download image

            }
            catch (Exception)
            {

            }

            try
            {
                HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-data']/p/span[2]");
                Time = time1.InnerHtml;
                string pattern = @"\d{2}";
                Regex reg = new Regex(pattern);
                MatchCollection m = reg.Matches(Time);
                string ngay = "";
                foreach (var item in m)
                {
                    ngay += item.ToString();
                }
                Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
            }
            catch (Exception)
            {


            }
        }
        //Setn.com
        public void Get_Setn(string Link)
        {
            for (int ii = 1; ii < 11; ii++)
            {
                string _URL = string.Format("http://www.setn.com/ViewAll.aspx?p={0}", ii);

                try
                {

                    HtmlWeb _htmlWeb = new HtmlWeb();
                    _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


                    for (int i = 1; i < 31; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@id='conLt']/div/div[2]/ul/li[{0}]", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.LastIndexOf("style");
                        url_html = url_html.Remove(data - 2);
                        int http = url_html.IndexOf("href");
                        url_html = url_html.Remove(0, http + 6);
                        url_html = "http://www.setn.com" + url_html;
                        url += url_html + ",";
                    }


                }
                catch (Exception)
                {
                }
            }//end for ii


        }
        public void Crawl_Setn(string Link, string node)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {

                HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='title']/h1");
                string _Title = pTitle.InnerText.Trim();
                Title = _Title;
                // download content
                int Key = 0;
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[{1}]/p[{0}]", i, node));
                        if (Content.InnerHtml.Contains("img") && Key == 0)
                        {
                            Image = Content.InnerHtml;
                            int http = Image.IndexOf("http");
                            Image = Image.Remove(0, http);
                            int src = Image.IndexOf("jpg");
                            Image = Image.Remove(src + 3);
                            Key = 1;
                        }
                        else
                        {
                            if (Content.InnerText.Length > 10 && !Content.InnerHtml.Contains("▲"))
                            {
                                Text_Content += Content.InnerText;
                                Text_Content += "\r\n" + "\r\n";
                            }

                        }

                    }

                }
                catch (Exception)
                {
                }
                Text_Content = Text_Content.Replace("&rdquo;", "\"");
                Text_Content = Text_Content.Replace("&ldquo;", "\"");
                Text_Content = Text_Content.Replace("&nbsp;", "");
            }
            catch (Exception)
            {
            }

            try
            {
                HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='title-left-area']/span[2]");
                Time = time1.InnerText;
                // truyền vào regex là pattern
                // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                // gọi r bằng to string()
                Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}\W\d{2}\W\d{2}");
                Match chuoi = reg1.Match(Time);
                Time = chuoi.ToString();
                string pattern = @"\d{2}";
                Regex reg = new Regex(pattern);
                MatchCollection m = reg.Matches(Time);
                string ngay = "";
                foreach (var item in m)
                {
                    ngay += item.ToString();
                }
                Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());

            }
            catch (Exception)
            {
            }
            // tag
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='keyword page-keyword-area']/ul/li[{0}]", i));
                    Tag += Tag1.InnerText;
                    Tag += ",";
                }
            }
            catch (Exception)
            {

            }
        }
        // spanish
        public void Get_AbcES(string Link)
        {
            for (int ii = 0; ii < 2; ii++)
            {
                string _URL = Link;
                string[] mang = { "A", "B" };



                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


                for (int i = 1; i < 31; i++)
                {
                    try
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@id='col-{1}']/article[{0}]/h1", i, mang[ii]));
                        string url_html = URL.InnerHtml;
                        int data = url_html.LastIndexOf("html");
                        url_html = url_html.Remove(data + 4);
                        int http = url_html.IndexOf("href");
                        url_html = url_html.Remove(0, http + 6);
                        url_html = "http://www.abc.es" + url_html;
                        url += url_html + ",";
                    }
                    catch (Exception)
                    {


                    }

                }

            }//end for ii


        }
        public void Crawl_AbcES(string Link)
        {
            try
            {


                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='encabezado-articulo']/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title;
                    // download content

                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='cuerpo-texto ']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {
                    }
                    Text_Content = Text_Content.Replace("&rdquo;", "\"");
                    Text_Content = Text_Content.Replace("&ldquo;", "\"");
                    Text_Content = Text_Content.Replace("&nbsp;", "");
                }
                catch (Exception)
                {
                }
                // dowwnload image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='imagen-principal']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);



                    /// 
                }
                catch (Exception)
                {


                }
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='lugar']");
                    Time = time1.InnerHtml;
                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()
                    Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}\W\d{2}\W\d{2}");
                    Match chuoi = reg1.Match(Time);
                    Time = chuoi.ToString();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());

                }
                catch (Exception)
                {
                }
                // tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='modulo temas']/ul/li[{0}]/a", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//time[contains(text(),'2017')][1]");
                    Time = time1.InnerText;

                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()
                    Regex reg1 = new Regex(@"\d{2}\W\d{2}\W\d{4}");
                    Match chuoi = reg1.Match(Time);
                    Time = chuoi.ToString();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[0].ToString() + ngay[1].ToString());
                }
                catch (Exception)
                {

                    //$x(".//time[contains(text(),'2017')][1]")
                }


            }




            catch (Exception)
            {


            }
        }

        //https://www.elespanol.com/espana/
        public void Get_elespanol(string Link)
        {

            string _URL = Link;

            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


            for (int i = 1; i < 56; i++)
            {
                try
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='home-content']/section/article[{0}]/div[1]", i));
                    string url_html = URL.InnerHtml;
                    int data = url_html.LastIndexOf("html");
                    url_html = url_html.Remove(data + 4);
                    int http = url_html.IndexOf("href");
                    url_html = url_html.Remove(0, http + 6);
                    if (url_html.Length < 100 && url_html.Contains("www.elespanol.com"))
                    {
                        url += url_html + ",";
                    }

                }
                catch (Exception)
                {


                }
            }
        }
        public void Crawl_elespanol(string Link)
        {
            try
            {

                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-header']/h1");
                    string _Title = pTitle.InnerText.Trim();
                    HtmlNode Des = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-header']/h2");// dòng chữ nhỏ
                    Text_Content = Des.InnerText.Trim();
                    Text_Content += "\r\n" + "\r\n";

                    Title = _Title;
                    Title = Title.Replace("&rdquo;", "\"");
                    Title = Title.Replace("&ldquo;", "\"");
                    Title = Title.Replace("&nbsp;", "");
                    // download content

                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='article-body__content']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {
                    }
                    Text_Content = Text_Content.Replace("&rdquo;", "\"");
                    Text_Content = Text_Content.Replace("&ldquo;", "\"");
                    Text_Content = Text_Content.Replace("&nbsp;", "");
                }
                catch (Exception)
                {
                }
                // dowwnload image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-media ']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);



                    /// 
                }
                catch (Exception)
                {


                }
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='article-header__time']");
                    Time = time1.InnerText;
                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()

                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[0].ToString() + ngay[1].ToString());

                }
                catch (Exception)
                {
                }
                // tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='tags__list']/li[{0}]/a", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//time[contains(text(),'2017')][1]");
                    Time = time1.InnerText;

                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()
                    Regex reg1 = new Regex(@"\d{2}\W\d{2}\W\d{4}");
                    Match chuoi = reg1.Match(Time);
                    Time = chuoi.ToString();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[0].ToString() + ngay[1].ToString());
                }
                catch (Exception)
                {

                    //$x(".//time[contains(text(),'2017')][1]")
                }


            }




            catch (Exception)
            {


            }
        }

        //military.china.com
        public void Get_china(string Link)
        {

            string _URL = Link;

            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


            for (int i = 1; i < 56; i++)
            {
                try
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='index_wntjcon']/div[{0}]/div[1]", i));
                    string url_html = URL.InnerHtml;
                    int data = url_html.LastIndexOf("html");
                    url_html = url_html.Remove(data + 4);
                    int http = url_html.IndexOf("href");
                    url_html = url_html.Remove(0, http + 6);
                    url_html = url_html.Insert(url_html.LastIndexOf("."), "_all");
                    url += url_html + ",";


                }
                catch (Exception)
                {


                }
            }
        }
        public void Crawl_china(string Link)
        {
            try
            {

                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='chan_newsBlk']/h1");
                    string _Title = pTitle.InnerText.Trim();

                    Title = _Title;
                    Title = Title.Replace("&rdquo;", "\"");
                    Title = Title.Replace("&ldquo;", "\"");
                    Title = Title.Replace("&nbsp;", "");
                    Title = Title.Replace("&quot;", "\"");
                    // download content
                    bool check = true;
                    for (int i = 1; i < 100; i++)
                    {
                        try
                        {

                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='chan_newsDetail']/p[{0}]", i));
                            if (!Content.InnerHtml.Contains("jpg"))
                            {
                                Text_Content += Content.InnerText;
                                Text_Content += "\r\n" + "\r\n";

                            }
                            else
                            {
                                if (check && Content.InnerHtml.Contains("jpg"))
                                {
                                    Image = Content.InnerHtml;
                                    int http = Image.IndexOf("http");
                                    Image = Image.Remove(0, http);
                                    int src = Image.IndexOf("jpg");
                                    Image = Image.Remove(src + 3);
                                    check = false;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }


                    Text_Content = Text_Content.Replace("&rdquo;", "\"");
                    Text_Content = Text_Content.Replace("&ldquo;", "\"");
                    Text_Content = Text_Content.Replace("&nbsp;", "");
                }
                catch (Exception)
                {
                }

                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='chan_newsInfo']");
                    Time = time1.InnerText;
                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()
                    Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}\W\d{2}\W\d{2}");
                    Match chuoi = reg1.Match(Time);
                    Time = chuoi.ToString();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
                }
                catch (Exception)
                {

                    //$x(".//time[contains(text(),'2017')][1]")
                }


            }




            catch (Exception)
            {


            }
        }
        //news.china.com
        public void Get_chinanews_com(string Link)
        {

            string _URL = Link;

            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);


            for (int i = 1; i < 101; i++)
            {
                try
                {
                    HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='news_content']/li[{0}]", i));
                    string url_html = URL.InnerHtml;
                    int data = url_html.LastIndexOf("html");
                    url_html = url_html.Remove(data + 4);
                    int http = url_html.IndexOf("href");
                    url_html = url_html.Remove(0, http + 6);
                    url_html = url_html.Insert(url_html.LastIndexOf("."), "_all");
                    url += url_html + ",";


                }
                catch (Exception)
                {


                }
            }
        }
        public void Crawl_chinanews_com(string Link)
        {
            try
            {

                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='cont_1_1_2']/h1");
                    string _Title = pTitle.InnerText.Trim();


                    Title = _Title;
                    Title = Title.Replace("&rdquo;", "\"");
                    Title = Title.Replace("&ldquo;", "\"");
                    Title = Title.Replace("&nbsp;", "");
                    // download content

                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='left_zw']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {
                    }
                    Text_Content = Text_Content.Replace("&rdquo;", "\"");
                    Text_Content = Text_Content.Replace("&ldquo;", "\"");
                    Text_Content = Text_Content.Replace("&nbsp;", "");
                }
                catch (Exception)
                {
                }
                // dowwnload image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='left_ph']");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);



                    /// 
                }
                catch (Exception)
                {


                }
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='left-t']");
                    Time = time1.InnerText;
                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()

                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[0].ToString() + ngay[1].ToString());

                }
                catch (Exception)
                {
                }

                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//time[contains(text(),'2017')][1]");
                    Time = time1.InnerText;

                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()
                    Regex reg1 = new Regex(@"\d{2}\W\d{2}\W\d{4}");
                    Match chuoi = reg1.Match(Time);
                    Time = chuoi.ToString();
                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[0].ToString() + ngay[1].ToString());
                }
                catch (Exception)
                {

                    //$x(".//time[contains(text(),'2017')][1]")
                }


            }




            catch (Exception)
            {


            }
        }
        //ent.china.com
        public void Get_chinanews_com_news()
        {

            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            // lọc theo ngày
            string thoigian = DateTime.Now.ToString("yyyy/MM/dd");
            Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}");
            Match chuoi = reg1.Match(thoigian);
            thoigian = chuoi.ToString();
            thoigian = thoigian.Substring(5);

            for (int n = 1; n < 8; n++)
            {
                string _URL = string.Format("http://www.chinanews.com/scroll-news/news{0}.html", n);
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);

                for (int i = 1; i < 126; i++)
                {
                    try
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='content_list']/ul/li[{0}]/div[2]", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.LastIndexOf("html");
                        url_html = url_html.Remove(data + 4);
                        int http = url_html.IndexOf("href");
                        url_html = url_html.Remove(0, http + 6);
                        // lọc theo ngày
                        if (url_html.Contains(thoigian))
                        {
                            url += url_html + ",";
                        }



                    }
                    catch (Exception)
                    {


                    }
                }
            }
        }

        // kknews
        public void Get_kknews()
        {
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            for (int n = 1; n < 18; n++)
            {
                string _URL = "https://kknews.cc/entertainment/?page=" + n;
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {
                    for (int i = 1; i < 16; i++)
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='main-content']/article[{0}]/div/div[1]", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.LastIndexOf("html");
                        url_html = url_html.Remove(data + 4);
                        int http = url_html.IndexOf("href");
                        url_html = url_html.Remove(0, http + 6);
                        url_html = "https://kknews.cc" + url_html;
                        url += url_html + ",";
                    }
                }
                catch (Exception)
                {
                }
            }

        }
        public void Crawl_kknews(string Link)
        {
            try
            {

                string _URL = Link;
                HtmlWeb _htmlWeb = new HtmlWeb();
                _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='post-header']/h1");
                    string _Title = pTitle.InnerText.Trim();


                    Title = _Title;
                    Title = Title.Replace("&rdquo;", "\"");
                    Title = Title.Replace("&ldquo;", "\"");
                    Title = Title.Replace("&nbsp;", "");
                    // download content

                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='basic']/div/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }

                    }
                    catch (Exception)
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='basic']/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";

                        }
                    }
                    Text_Content = Text_Content.Replace("&rdquo;", "\"");
                    Text_Content = Text_Content.Replace("&ldquo;", "\"");
                    Text_Content = Text_Content.Replace("&nbsp;", "");
                }
                catch (Exception)
                {
                }
                // dowwnload image
                try
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='img-container'][1]");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);

                }
                catch (Exception)
                {
                    HtmlNode image = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='sizer'][1]");
                    Image = image.InnerHtml;

                    int http = Image.IndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.IndexOf("jpg");
                    Image = Image.Remove(src + 3);

                }
                //time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='meta post-meta']");
                    Time = time1.InnerText;
                    // truyền vào regex là pattern
                    // truyền vào match là chuỗi cần thực hiện theo cú phápm Match m= reg.Math(chuoi)
                    // gọi r bằng to string()

                    string pattern = @"\d{2}";
                    Regex reg = new Regex(pattern);
                    MatchCollection m = reg.Matches(Time);
                    string ngay = "";
                    foreach (var item in m)
                    {
                        ngay += item.ToString();
                    }
                    Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());

                }
                catch (Exception)
                {
                }



            }

            catch (Exception)
            {


            }
        }

        //huanqiu_com
        public void Get_huanqiu_com(string Link)
        {
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            string _URL = Link;
            string thoigian = DateTime.Now.ToString("yyyy/MM/dd");
            Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}");
            Match chuoi = reg1.Match(thoigian);
            thoigian = chuoi.ToString();
            thoigian = thoigian.Substring(5);

            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
            try
            {
                for (int i = 1; i < 100; i++)
                {
                    HtmlNode Timer = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='fallsFlow']/ul/li[{0}]/h6", i));
                    if (Timer.InnerText.Contains(thoigian))
                    {
                        HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='fallsFlow']/ul/li[{0}]/h3", i));
                        string url_html = URL.InnerHtml;
                        int data = url_html.LastIndexOf("html");
                        url_html = url_html.Remove(data + 4);
                        int http = url_html.IndexOf("http");
                        url_html = url_html.Remove(0, http);

                        url += url_html + ",";
                    }

                }
            }
            catch (Exception)
            {
            }


        }
        public void Crawl_huanqiu_com(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);

            try
            {
                //title
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='conText']/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title.ConvertHTML();


                }
                catch (Exception)
                {

                }

                //  download image
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='text']/p[{0}]", i));

                        if (!Content.InnerHtml.Contains("jpg"))
                        {
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }
                        else
                        {
                            Image = Content.InnerHtml;
                            int http = Image.IndexOf("http");
                            Image = Image.Remove(0, http);
                            int src = Image.LastIndexOf("g");
                            Image = Image.Remove(src + 1);
                        }

                    }


                }
                catch (Exception)
                {
                    Text_Content = Text_Content.ConvertHTML();
                }

                //  content
                try
                {

                }
                catch (Exception)
                {

                }

                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='summaryNew']");
                    Time = time1.InnerText;
                    Ngay = Time.ConvertTimechina();

                }
                catch (Exception)
                {
                }
            }
            catch { }

        }

        //nownews
        public void Get_Nownews(string Link)
        {
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            string _URL = Link;
            string thoigian = DateTime.Now.ToString("yyyy/MM/dd");
            Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}");
            Match chuoi = reg1.Match(thoigian);
            thoigian = chuoi.ToString();
            thoigian = thoigian.Substring(5);
            thoigian = thoigian.Replace("-", "/");

            try
            {
                for (int n = 1; n < 4; n++)
                {

                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL + n);
                    for (int i = 1; i < 100; i++)
                    {
                        try
                        {
                            HtmlNode Timer = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='clearfix']/div[{0}]/a/div[2]/span", i));
                            if (Timer.InnerText.Contains(thoigian))
                            {
                                HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class='clearfix']/div[{0}]", i));
                                string url_html = URL.InnerHtml;
                                int data = url_html.IndexOf("href");
                                url_html = url_html.Remove(0, data + 6);
                                int http = url_html.IndexOf("?from");
                                url_html = url_html.Remove(http);
                                url_html = "https://www.nownews.com" + url_html;
                                url += url_html + ",";
                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                }
            }
            catch (Exception)
            {
            }


        }
        public void Crawl_Nownews(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

            try
            {
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                //title
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='content_omx0ol']/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title.ConvertHTML();

                }
                catch (Exception)
                {
                }

                //  download image
                try
                {

                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='contentDiv_1azf7pu']"));

                    Image = Content.InnerHtml;
                    int http = Image.LastIndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.LastIndexOf("g");
                    Image = Image.Remove(src + 1);


                }
                catch (Exception)
                {

                }

                //  content

                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='box_6sqckh fontSize16']/div[1]/p[{0}]/span", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";
                    }

                }
                catch (Exception)
                {

                }
                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='box_6sqckh fontSize16']/div[3]/p[{0}]/span", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";
                    }

                }
                catch (Exception)
                {

                }
                if (Text_Content == null)
                {

                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='box_6sqckh fontSize16']/div[1]/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }

                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='box_6sqckh fontSize16']/div[3]/p[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
                Text_Content = Text_Content.ConvertHTML();
                // time
                try
                {
                    string date = DateTime.Now.ToString("yyyy");
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//span[contains(text(),'{0}')]", date));
                    Time = time1.InnerText;
                    Ngay = Time.ConvertTimechina();

                }
                catch (Exception)
                {
                }
                // tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class=' margin10_q3b2ke']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
            }
            catch { Text_Content = Text_Content.ConvertHTML(); }

        }

        //sportvnews.co.kr
        public void Get_Sportvnews(string Link)
        {
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            string[] _URL = new string[] { "http://www.spotvnews.co.kr/?page=1&mod=news&act=articleList&total=39800&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1384128643&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",
                                            "http://www.spotvnews.co.kr/?page=2&mod=news&act=articleList&total=39800&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1384128643&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",
                                            "http://www.spotvnews.co.kr/?page=1&mod=news&act=articleList&total=1136&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1476937527&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",
                                            "http://www.spotvnews.co.kr/?page=1&mod=news&act=articleList&total=10160&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1394547726&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",
                                            "http://www.spotvnews.co.kr/?page=1&mod=news&act=articleList&total=12387&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1395273923&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",
                                            "http://www.spotvnews.co.kr/?page=1&mod=news&act=articleList&total=6430&sc_area=&sc_level=&sc_article_type=&sc_view_level=&sc_code=1419317435&sc_sdate=&sc_edate=&sc_order=&sc_recognition=&sc_hosu=&view_type=S&sc_word=&sc_word2=&sc_andor=&subm=",

                                            };

            string thoigian = DateTime.Now.ToString("yyyy/MM/dd");
            Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}");
            Match chuoi = reg1.Match(thoigian);
            thoigian = chuoi.ToString();



            try
            {
                for (int n = 0; n < _URL.Length; n++)
                {

                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL[n]);
                    for (int i = 1; i < 100; i++)
                    {
                        try
                        {
                            HtmlNode Timer = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='summary_list']/div[{0}]/ul/li[1]/p", i));
                            if (Timer.InnerText.Contains(thoigian))
                            {
                                HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='summary_list']/div[{0}]/ul/li[1]", i));
                                string url_html = URL.InnerHtml;
                                int data = url_html.IndexOf("href");
                                url_html = url_html.Remove(0, data + 6);
                                int http = url_html.IndexOf("class");
                                url_html = url_html.Remove(http - 2);
                                url_html = "http://www.spotvnews.co.kr" + url_html;
                                url_html = url_html.Replace("amp;", "");
                                url += url_html + ",";
                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                }
            }
            catch (Exception)
            {
            }


        }
        public void Crawl_Sportvnews(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

            try
            {
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                //title
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@id='arl_view_box']/div[2]/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title.ConvertHTML();
                    Title = Title.Replace("&#039;", "\"");

                }
                catch (Exception)
                {
                }

                //  download image
                try
                {

                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='news_photo_table']"));

                    Image = Content.InnerHtml;
                    int http = Image.LastIndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.LastIndexOf("g");
                    Image = Image.Remove(src + 1);


                }
                catch (Exception)
                {

                }

                //  content

                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='arl_view_content']/div[1]/p[{0}]", i));
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";
                    }

                }
                catch (Exception)
                {

                }
                if (Text_Content == null)
                {
                    try
                    {
                        for (int i = 2; i < 50; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@id='arl_view_content']/div[1]/div[{0}]", i));
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }

                    }
                    catch (Exception)
                    {

                    }
                }

                Text_Content = Text_Content.ConvertHTML();
                // time
                try
                {
                    string date = DateTime.Now.ToString("yyyy");
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//span[contains(text(),'{0}')]", date));
                    Time = time1.InnerText;
                    Ngay = Time.ConvertTimechina_noMinute();

                }
                catch (Exception)
                {
                }
                // tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class=' margin10_q3b2ke']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
            }
            catch { }

        }

        //arabic
        // text get 3 tý sửa lại thành 11
        public void Get_Arabic_Eldawlagia(string Link, string node)
        {
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";

            string _URL = $"http://www.eldawlagia.com/world?page=";
            try
            {
                for (int n = 1; n < 11; n++)
                {
                    _URL = string.Format("http://www.eldawlagia.com/{1}?page={0}", n, node);
                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                    for (int i = 1; i < 100; i++)
                    {
                        try
                        {

                            HtmlNode URL = _htmlDocument.DocumentNode.SelectSingleNode($".//*[@class='clearfix flex flex-wrap posts-list']/article[{i}]/h2");
                            string url_html = URL.InnerHtml;
                            int data = url_html.IndexOf("href");
                            url_html = url_html.Remove(0, data + 6);
                            int http = url_html.IndexOf("rel");
                            url_html = url_html.Remove(http - 2);
                            url_html = "http://www.eldawlagia.com" + url_html;
                            url += url_html + ",";

                        }
                        catch (Exception)
                        {


                        }

                    }
                }
            }
            catch (Exception)
            {
            }


        }
        public void Crawl_Arabic_Eldawlagia(string Link)
        {
            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();
            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

            try
            {
                HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                //title
                try
                {

                    HtmlNode pTitle = _htmlDocument.DocumentNode.SelectSingleNode(".//*[@class='page-header']/h1");
                    string _Title = pTitle.InnerText.Trim();
                    Title = _Title.ConvertHTML();


                }
                catch (Exception)
                {
                }

                //  download image
                try
                {

                    HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//*[@class='image-wrap']"));

                    Image = Content.InnerHtml;
                    int http = Image.LastIndexOf("http");
                    Image = Image.Remove(0, http);
                    int src = Image.LastIndexOf("jpg");
                    Image = Image.Remove(src + 3);


                }
                catch (Exception)
                {

                }

                //  content

                try
                {
                    for (int i = 1; i < 100; i++)
                    {
                        HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode($".//*[@class='contents entry-content']/div[1]/p[{i}]");
                        Text_Content += Content.InnerText;
                        Text_Content += "\r\n" + "\r\n";
                    }

                }
                catch (Exception)
                {

                }

                if (Text_Content == null)
                {
                    try
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            HtmlNode Content = _htmlDocument.DocumentNode.SelectSingleNode($".//*[@class='contents entry-content']/p[{i}]");
                            Text_Content += Content.InnerText;
                            Text_Content += "\r\n" + "\r\n";
                        }


                    }
                    catch { }
                }

                Text_Content = Text_Content.ConvertHTML();
                // time
                try
                {
                    HtmlNode time1 = _htmlDocument.DocumentNode.SelectSingleNode($".//*[@class='meta-item meta-date']/a");
                    Ngay = time1.InnerHtml.ConvertTimeSpan();

                }
                catch (Exception)
                {
                }
                // tag
                try
                {
                    for (int i = 1; i < 10; i++)
                    {
                        HtmlNode Tag1 = _htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[@class=' margin10_q3b2ke']/a[{0}]", i));
                        Tag += Tag1.InnerText;
                        Tag += ",";
                    }
                }
                catch (Exception)
                {

                }
            }
            catch { }

        }

    }// end class post
}
