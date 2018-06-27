using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class TextToSpeech
    {
        /// <summary>
        /// chuyển text thành voice mp3
        /// </summary>
        /// <param name="k"></param>
        /// <param name="language"></param>
        static public void Start(int k, string language)
        {
            if (language.Equals("china"))
            {
                ispeech(k, language);
            }
            //else if (language == "china" || language == "korean" || language == "span")
            //{
            //    neospeech(k, language);
            //}

        }

        static void neospeech(int k, string language)
        {
            // phải xử lý chuỗi trước khi gọi TextToSpeech
            string[] Text = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\Input.txt", k));

            WebClient Wc = new WebClient();

            string VoidIndex = "";


            if (language == "korean")
            {
                VoidIndex = "18";
            }
            else if (language == "china")
            {
                VoidIndex = "36";
            }
            else if (language == "span")
            {
                VoidIndex = "46";
            }
            for (int i = 0; i < Text.Length; i++)
            {
                try
                {
                    // download json
                    String txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\LinkVoice.txt", k);
                    string url = "http://www.neospeech.com/service/demo?voiceId=" + VoidIndex + "&content=" + Text[i];

                    Uri FileUrl = new Uri(url);


                    Wc.DownloadFileAsync(FileUrl, txtSaveFile);
                    while (Wc.IsBusy)
                    {

                    }
                    //// dowwnload mp3
                    string[] json = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\LinkVoice.txt", k));
                    int http = json[1].IndexOf("http");
                    json[1] = json[1].Remove(0, http);
                    int mp3 = json[1].IndexOf("mp3");
                    json[1] = json[1].Remove(mp3 + 3);
                    url = json[1].Replace("\\", "");

                    Uri FileUr2 = new Uri(url);

                    txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\Voice\{1}.mp3", k, i);
                    Wc.DownloadFileAsync(FileUr2, txtSaveFile);
                    while (Wc.IsBusy)
                    {

                    }

                }
                catch (Exception)
                {


                }


            }
        }


        static void ispeech(int k, string language)
        {
            string[] Text = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\Input.txt", k));

            WebClient Wc = new WebClient();

            for (int i = 0; i < Text.Length; i++)
            {
                try
                {
                    string url = "https://api.ispeech.org/api/rest?apikey=34b06ef0ba220c09a817fe7924575123&action=convert&pitch=100&voice=chchinesefemale&speed=0&text=" + Text[i];
                    Uri FileUr2 = new Uri(url);

                    string txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\Voice\{1}.mp3", k, i);
                    Wc.DownloadFileAsync(FileUr2, txtSaveFile);
                    while (Wc.IsBusy)
                    {

                    }

                }
                catch (Exception)
                {


                }

            }
        }
    }
}
