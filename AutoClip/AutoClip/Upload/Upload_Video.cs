using AutoClip.Library;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Upload
{
    class Upload_Video
    {

        public static void Start()
        {
            Parametter para = new Parametter();
            para.DEserialize();

            RunUpload(para.To, para.From);

            Thread.Sleep(195000);


        }

        static void RunUpload(int To, int From)
        {
            double Text_Hour = 0;
            for (int k = To; k < From; k++)
            {
                if (File.Exists("C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4"))
                {
                    #region xử lý đầu vào
                   
                    string Path = "C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4";

                    string[] Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Title.txt");

                    string[] Des_In_Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Input.txt");

                    string[] Des = File.ReadAllLines(@"C:\RACC\txtFile\Des.txt");
                    string[] File_Tag = File.ReadAllLines(@"C:\RACC\txtFile\Tag.txt");
                    string[] TagVideo = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\TagVideo.txt");
                    string Des_Update = Title[0] + "\n" + Des_In_Title[0];
                    // add des to File 
                    foreach (var item in Des)
                    {
                        Des_Update += "\n" + item;
                    }

                    string TieuDe = Title[0]; // default title

                    // add tag to File 
                    string Tag = "";
                    foreach (var item in File_Tag)
                    {
                        Tag += "," + item;
                    }
                    foreach (var item in TagVideo)
                    {
                        Tag += "," + item;
                    }


                    // split title
                    string s = Title[0];
                    s = s.Replace(" ", ",");
                    string[] Tag_Title = s.Split(',');
                    string Tag_Update = "";
                    Tag_Update += Title[0] + ",";
                    foreach (var item in Tag_Title)
                    {
                        Tag_Update += item + ",";
                    }
                    for (int i = 0; i < Tag_Title.Length - 1; i++)
                    {
                        Tag_Update += Tag_Title[i] + " " + Tag_Title[i + 1] + ",";

                        i++;
                    }
                    // add TAG
                    string Both_Tag = Tag + Tag_Update;
                    string[] Tag_Last_Update = Both_Tag.Split(',');
                    //txtTitle.Text = Title[0];
                    //txtDes.Text = Des_Update;
                    //txtTag.Text = Tag_Update;
                    string ImagePath = "C:\\RACC\\Data\\Video" + k + "\\Image\\Thumb.png";
                    // Text_Hour
                    // Text_Hour += double.Parse(txbHourVideo.Text);
                    #endregion


                    UploadVideo(TieuDe, Des_Update, Tag_Last_Update, Path, ImagePath, Text_Hour);

                    Thread.Sleep(60000);
                }
            }
        }

        static void UploadVideo(string Title, string Des, string[] Tag, string Path, string ImagePath, double Text_Hour)
        {
            Console.WriteLine("Uploading Video");

            try
            {
                Run(Title, Des, Tag, Path, ImagePath, Text_Hour).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var er in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + er.Message);
                }
            }
        }

        private static async Task Run(string Title, string Des, string[] Tag, string Path, string ImagePath, double Text_Hour)
        {
            UserCredential credential;

            using (var stream = new FileStream(string.Format(@"C:\RACC\json\user.json"), FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] {
                           YouTubeService.Scope.Youtube,
                           YouTubeService.Scope.Youtubepartner,
                           YouTubeService.Scope.YoutubeUpload,
                           //YouTubeService.Scope.YoutubepartnerChannelAudit,
                           //YouTubeService.Scope.YoutubeReadonly
                    },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = Title;
            video.Snippet.Description = Des;
            video.Snippet.DefaultAudioLanguage = "en";
            video.Snippet.DefaultLanguage = "en";
            video.Snippet.Tags = Tag;

            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();

            Text_Hour = 8;
            DateTime date = new DateTime();
            date = DateTime.Today;
            double Text_Hour1 = Math.Truncate(Text_Hour);// lấy phần nguyên của double
            int Hour = (int)Text_Hour1; // ép kiểu double
            double Phan_Du = Text_Hour - Text_Hour1;
            double Minute1 = Phan_Du * 60;
            int Minute = (int)Minute1;
            TimeSpan t = new TimeSpan(Hour, Minute, 0);

            string ngay = date.Add(t).ToString("yyyy-MM-ddTHH:mm:ss.45+07:00");
            DateTime dt = Convert.ToDateTime(ngay);
            video.Status.PublishAt = dt;



            string statusVideo = "private"; // get status in form
            video.Status.PrivacyStatus = statusVideo; // or "private" or "public"

            var filePath = Path; // Replace with path to actual movie file.

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();

            }
            // xử lý id trong textBox




        } // hàm upload

        static void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)// báo dung lượng upload
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    long Mbupload = progress.BytesSent / (1024 * 1024);
                    long Mbupload1 = progress.BytesSent % (1024 * 1024);
                    Mbupload = Mbupload + Mbupload1;
                    Console.WriteLine(string.Format("{0} MB uploaded.", Mbupload));
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine(string.Format("Đã có lỗi.\n{0}", progress.Exception));
                    break;
            }
        }

        public static int Dem = 0;

        static void videosInsertRequest_ResponseReceived(Video video)
        {
            ////WriteLog(string.Format("Video id '{0}' was successfully uploaded.", video.Id));
            ////txtUpload.Text += "\r\n===================================" + txtUpload.Text;

            Console.WriteLine(string.Format("Video id '{0}' was successfully uploaded'", video.Id));
            Dem++;
            Console.WriteLine(string.Format("Video {0}", Dem));
            //Console.WriteLine += "\r\n===================================" + txtUpload.Text;
            //txtVideoID.Text += video.Id + "\r\n";


        } //báo đã upload thành công // trả 

    }
}
