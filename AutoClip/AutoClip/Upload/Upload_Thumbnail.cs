using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
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
    class Upload_Thumbnail
    {

        private async Task Run1()
        {
            UserCredential credential;
            FileStream fs = new FileStream(@"C:\RACC\Data\idTitle.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            using (var stream = new FileStream(string.Format(@"C:\RACC\json\user.json"), FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for read-only access to the authenticated 
                    // user's account, but not other types of account access.
                    new[] {   YouTubeService.Scope.Youtube,
                           YouTubeService.Scope.Youtubepartner,
                           YouTubeService.Scope.YoutubeUpload, },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            var channelsListResponse = await channelsListRequest.ExecuteAsync();

            foreach (var channel in channelsListResponse.Items)
            {
                // From the API response, extract the playlist ID that identifies the list
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

                Console.WriteLine(string.Format("Videos in list {0}", uploadsListId));

                var nextPageToken = "";
                //  while (nextPageToken != null)
                int count = 0;
                while (nextPageToken != null)
                {
                    var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;


                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                    foreach (var playlistItem in playlistItemsListResponse.Items)
                    {
                        // Print information about each video.
                        //mg(string.Format("{0} ({1})", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId));
                        Console.WriteLine(string.Format("{0}", playlistItem.Snippet.ResourceId.VideoId));
                        sw.WriteLine(string.Format("{1}@{0}", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId));
                    }

                    nextPageToken = playlistItemsListResponse.NextPageToken;
                    count++;
                    if (count >= 2)
                    {
                        break;
                    }
                }
            }
            sw.Close();
        }


        public async Task Run(string ImagePath, string videoID)
        {

            UserCredential credential;
            using (var stream = new FileStream(@"C:\RACC\json\user.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.Youtube,
                           YouTubeService.Scope.Youtubepartner,
                           YouTubeService.Scope.YoutubeUpload, },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });


            using (var tStream = new FileStream(ImagePath, FileMode.Open))
            {

                var videosInsertRequest = youtubeService.Thumbnails.Set(videoID, tStream, "image/png");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;

                await videosInsertRequest.UploadAsync();

            }

        } // hàm upload

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)// báo dung lượng upload
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    long Mbupload = progress.BytesSent / (1024 * 1024);
                    long Mbupload1 = progress.BytesSent % (1024 * 1024);
                    Mbupload = Mbupload + Mbupload1;

                    break;

                case UploadStatus.Failed:

                    break;
            }
        }

        public async Task Upload()
        {
            //  Thumb thumb = new Thumb();
            int FormFolder = 100;
            int ToFolder = 0;
            string[] txtID = File.ReadAllLines(@"C:\RACC\Data\idTitle.txt");
            for (int i = 0; i < txtID.Length; i++)
            {

                string[] ID = txtID[i].Split('@');// 
                //ID[0] : ID
                // ID[1]: Title
                for (int k = ToFolder; k < FormFolder; k++)
                {
                    try
                    {
                        string path = string.Format(@"C:\RACC\Data\Video{0}\Title.txt", k);
                        string ImagePath = string.Format(@"C:\RACC\Data\Video{0}\Image\Thumb.png", k);
                        string[] Title = File.ReadAllLines(path);
                        if (Title[0].Contains(ID[1]))
                        {
                            //    await thumb.Run(ImagePath, ID[0]);
                            Console.WriteLine(" Thumb upload was successfully :Video" + k);
                            break;
                        }

                    }
                    catch (Exception)
                    {


                    }


                }

            }
        }
    }
}
