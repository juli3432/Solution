using AutoClip.Download_Image_For_Manhua;
using AutoClip.Library;
using AutoClip.Render_Type;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip.ListManhua
{
    class Manhua
    {
        // /chứa những web truyện
        public static void Start(int To, int From)
        {
            String path = $@"C:\RACC\urlManhua.txt";
            List<string> url = new List<string>();
            List<string> title = new List<string>();

            url = getUrl();

            title = getTitle();
           
            try
            {
                //test manhua


                for (int k = To; k < From; k++)
                {

                    Download_Image.Start(k, url[k]);


                    // save file Title
                    String filepath1 = @"C:\RACC\Data\Video" + k + @"\Title.txt";// đường dẫn của file muốn tạo
                    FileStream fs1 = new FileStream(filepath1, FileMode.Create);
                    StreamWriter sWriter1 = new StreamWriter(fs1, Encoding.UTF8);//fs là 1 FileStream 
                    sWriter1.WriteLine(title[k]);
                    sWriter1.Flush();
                    fs1.Close();

                    String dulieunhap = " ";
                    String filepath = @"C:\RACC\Data\Video" + k + @"\Input.txt";// đường dẫn của file muốn tạo
                    FileStream fs = new FileStream(filepath, FileMode.Create);
                    StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
                    sWriter.WriteLine(dulieunhap);
                    sWriter.Flush();
                    fs.Close();

                    string Tag = " ";
                    String filepath2 = @"C:\RACC\Data\Video" + k + @"\TagVideo.txt";// đường dẫn của file muốn tạo
                    FileStream fs2 = new FileStream(filepath2, FileMode.Create);
                    StreamWriter sWriter2 = new StreamWriter(fs2, Encoding.UTF8);//fs là 1 FileStream 
                    sWriter2.WriteLine(Tag);
                    sWriter2.Flush();
                    fs2.Close();
                }
            }
            catch { }
            RenderManhua.Start(To, From);

        }

        public static List<string> getTitle()
        {
            List<string> _title = new List<string>();
            String path_title = $@"C:\RACC\urlManhua.txt";
            var arrTitle = File.ReadAllLines(path_title);
            foreach (var item in arrTitle)
            {
                _title.Add(item.Split(';')[1]);
            }

            return _title;
        }

        public static List<string> getUrl()
        {
            List<string> _url = new List<string>();
            String path_url = $@"C:\RACC\urlManhua.txt";

            var arrUrl = File.ReadAllLines(path_url);
            foreach (var item in arrUrl)
            {
                
                _url.Add(item.Split(';')[0]);
            }


            return _url;
        }

    }
}
