using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
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

namespace AutoClip.Library
{
    class ThumbnailsSample
    {

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

        public async void UPLOAD()
        {
            String client_secret_path = @"C:\RACC\json\user.json"; // here you put the path to your .json client secret file, generated in your Google Developer Account
            String username = "chalky"; // youtube account login username


            UserCredential credential;
            using (var stream = new FileStream(client_secret_path, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeUpload },
                    username,
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            // first, we get the video we want to update
            var my_video_request = youtubeService.Videos.List("snippet, status");
            my_video_request.Id = "9P9jLuwXSP0"; // the Youtube video id of the video you want to update
            my_video_request.MaxResults = 1;
            var my_video_response = await my_video_request.ExecuteAsync();
            var video = my_video_response.Items[0];

            // then we change it's attributes
            string title = "New title";
            string description = "New description";
            List<String> keywords = new List<String>();

            video.Snippet.Title = title;
            video.Snippet.Description = description;
            // đang truyền tham số kiếm tiền bị sai
            AccessPolicy access = new AccessPolicy();
            access.Allowed = true;
            access.ETag = "If-None-Match";


            // video.MonetizationDetails = new VideoMonetizationDetails { Access = new AccessPolicy { Allowed = true } };
            video.MonetizationDetails = new VideoMonetizationDetails();
            video.Snippet.Tags = new System.Collections.Generic.List<String>();

            // and tell the changes we want to youtube
            var my_update_request = youtubeService.Videos.Update(video, "snippet, status");
            my_update_request.Execute();
        }

    }
}
