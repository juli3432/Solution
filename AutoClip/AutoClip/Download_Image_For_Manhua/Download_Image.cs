using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoClip.Download_Image_For_Manhua
{
    class Download_Image
    {
        public static void Start(int k, string Link)
        {
           // createUrlFile(k);

            List<string> Urls = new List<string>();
            Urls = Crawl_Image(k, Link);

            for (int i = 0; i < Urls.Count; i++)
            {
                Download(k, Urls[i], i);
            }


        }

        public static void createUrlFile(int k)
        {
            String path = $@"C:\RACC\Data\Video{k}\Image\";
            using (FileStream fs = new FileStream(path + "urlManhua.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("this is my url");
                }
            }
        }

        public static List<string> Crawl_Image(int k, string Link)
        {
            List<string> Urls = new List<string>();
            //String path = $@"C:\RACC\Data\Video{k}\Image\urlManhua.txt";
            //string[] url = File.ReadAllLines(path);

            // using regex filter number

            Regex reg1 = new Regex(@"\d+");
            Match chuoi = reg1.Match(Link);

            string num1 = chuoi.ToString().Substring(0, chuoi.Length - 2);

            string num2 = chuoi.ToString().Substring(chuoi.Length - 2); // int 2 digit

            string titleNum= chuoi.ToString().Substring(0, 4);




            string _URL = Link;
            HtmlWeb _htmlWeb = new HtmlWeb();

            _htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            try
            {
                int indexImage = int.Parse(num2);// convert numbers last
                for (int i = 0; i < 100; i++)
                {
                    //index url
                    string indexURL = indexImage.ToString();
                    if (indexURL.Length == 1)
                    {
                        indexURL = indexURL.Insert(0, "0");
                    }
                    _URL = "http://www.cartoonmad.com/comic/" + num1 + indexURL + ".html";

                    // index href
                    string indexHref = (indexImage + 1).ToString(); //  src larger 1 point than url
                    if (indexHref.Length == 1)
                    {
                        indexHref = indexHref.Insert(0, "0");
                    }
                    string src = num1 + indexHref;


                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                    HtmlNode node = _htmlDocument.DocumentNode.SelectSingleNode($".//*[contains(@href,'{src}.html')]");
                    string img = node.FirstChild.Attributes["src"].Value;

                    //
                    //if (i==0)
                    //{
                    //    HtmlNode nodeTitle = _htmlDocument.DocumentNode.SelectSingleNode($".//*[contains(@href,'http://www.cartoonmad.com/comic/{titleNum}.html')]");
                    //    string title = nodeTitle.InnerText;
                       
                    //}

                    Urls.Add(img + "");
                    indexImage++;

                }
            }
            catch
            {
                try
                {

                    HtmlAgilityPack.HtmlDocument _htmlDocument = _htmlWeb.Load(_URL);
                    HtmlNode node = _htmlDocument.DocumentNode.SelectSingleNode($".//*[contains(@href,'thend.asp')]");
                    string img = node.FirstChild.Attributes["src"].Value;

                    Urls.Add(img + "");
                }
                catch
                {


                }

            }

            return Urls;

        }

        public static void Download(int k, string url, int index)
        {
            WebClient Wc = new WebClient();
            String txtSaveFile = $@"C:\RACC\Data\Video{k}\Image\";

            Uri FileUrl = new Uri(url);//Uri để tạo đầu vào cho Wc tải về, Trim để xóa kí tự rỗng ở 2 đầu


            Wc.DownloadFileAsync(FileUrl, txtSaveFile + $"{index}.jpg");
            while (Wc.IsBusy)
            {

            }
        }
    }
}
