using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Drawing;


namespace AutoClip
{
    class CodeFFMPEG
    {
        public void Join_Voice(int k)
        {
            // code image =>video
            string[] Count_Mp3 = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt", Encoding.UTF8);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            string Ghep_File = "";



            // function check sound download exits
            for (int i = 0; i < Count_Mp3.Length; i++)
            {
                if (File.Exists($"C:\\RACC\\Data\\Video{k}\\Voice\\{i}.mp3"))
                {

                    Ghep_File += "\"" + i + ".mp3" + "\"" + "+ ";

                }
            }
            string name_sound = Ghep_File.Substring(0, Ghep_File.Length - 2);




            ////    Ghép âm thanh đã tải về
            {

                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Voice";// tạo đường dẫn để thực hiện lệnh arguments
                startInfo.FileName = "cmd.exe";// khởi tạo cmd
                process.StartInfo = startInfo; //
                startInfo.Arguments = "/C copy /b " + name_sound + @" C:\RACC\Data\Video" + k + @"\Image\TotalMusic.mp3";
                // System.Windows.Forms.MessageBox.Show(startInfo.Arguments);
                process.Start();
                process.Close();

                //  mg("Ghép xong TotalMusic");

            }

            // done ghép âm thanh đã tải về
            Thread.Sleep(2000);

        }
        public void Change_Name(int k)
        {

            // tạo file info.vbs
            FileStream fs = new FileStream(@"C:\RACC\info.vbs", FileMode.Create);
            StreamWriter w = new StreamWriter(fs, Encoding.Unicode);
            string s = "";
            s += "\n\n set fso = CreateObject(\"Scripting.FileSystemObject\")";
            s += "\n\n  set root = fso.getFolder(\"C:\\RACC\\Data\\Video" + k + "\\Image\")";
            s += "\n\n  Dim number";
            s += "\n\n  for each file in root.Files";
            s += "\n\n number=number+1";
            s += "\n\n  file.Name = \"\" & number & \".\" & fso.GetExtensionName(file.Name)";
            s += "\n\n  next  \n";
            w.WriteLineAsync(s);
            w.Close();

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.WorkingDirectory = string.Format("C:\\RACC", k);
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C info.vbs";
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(1000);



            //if (!File.Exists(string.Format("C:\\RACC\\Data\\Video{0}\\Image\\1.jpg",k)))
            //{
            //    startInfo.WorkingDirectory = string.Format(@"C:\RACC\Data\Video{0}\Image", k);
            //    startInfo.Arguments = "/C copy C:\\RACC\\VideoProduct\\Image.jpg 1.jpg";
            //    process.Start();
            //}
            #region convenr size
            //startInfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image\\", k);
            //// ChangeSize = "/C magick convert 1.jpg -resize 1080x720! t.jpg";
            //startInfo.Arguments = "/C magick convert 1.jpg -resize 1080x720! 1.jpg";
            //process.Start();
            //process.Close();
            #endregion

            //  Thread.Sleep(1000);
            if (!File.Exists(string.Format("C:\\RACC\\Data\\Video{0}\\Image\\2.jpg", k)))
            {
                startInfo.WorkingDirectory = string.Format(@"C:\RACC\Data\Video{0}\Image", k);
                startInfo.Arguments = "/C copy 1.jpg 2.jpg";
                process.Start();
            }
            //Thread.Sleep(1000);
            //if (!File.Exists(string.Format("C:\\RACC\\Data\\Video{0}\\Image\\3.jpg", k)))
            //{
            //    startInfo.WorkingDirectory = string.Format(@"C:\RACC\Data\Video{0}\Image", k);
            //    startInfo.Arguments = "/C copy 2.jpg 3.jpg";
            //    process.Start();
            //}
            process.Close();

        }
        public void Convert_Image_All_Jpg(int k)
        {


            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ////    Ghép âm thanh đã tải về
            {

                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo.FileName = "cmd.exe";// khởi tạo cmd
                process.StartInfo = startInfo; //
                startInfo.Arguments = "/C ren *.jpeg *.jpg";
                process.Start();

                startInfo.Arguments = "/C ren *.png *.jpg";
                process.Start();
                startInfo.Arguments = "/C ren *.bmp *.jpg";
                process.Start();
                startInfo.Arguments = "/C ren *.tiff *.jpg";
                process.Start();
                process.Close();

            }
        }
        public void Create_Video(int k)
        {
            // chạy code làm ảnh

            // tạo video


            System.Diagnostics.Process process1 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo1 = new System.Diagnostics.ProcessStartInfo();
            startInfo1.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


            // kết thúc đếm ảnh

            #region Chỉnh thời gian theo ảnh
            //string Timer = "";
            //if (!ck) // nếu nút auto image/s check on
            //{
            //    string[] Count_Mp3 = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt", Encoding.UTF8);
            //    //Count_Mp3 : số tập tin âm thanh tải về

            //    for (int i = 0; i < Count_Mp3.Length; i++)
            //    {
            //        if (File.Exists(@"C:\RACC\Data\Video" + k + @"\Voice\" + i + ".mp3"))
            //        {
            //            try
            //            {
            //                var file = TagLib.File.Create(@"C:\RACC\Data\Video" + k + @"\Voice\" + i + ".mp3");
            //                var mp3Length = (int)file.Properties.Duration.TotalSeconds; // mp3 length
            //                                                                            // xử lý đổi s sang g
            //                Time += mp3Length;
            //            }
            //            catch (Exception)
            //            {

            //                var file = TagLib.File.Create(@"C:\RACC\Data\Video" + k + @"\Voice\" + (i + 1) + ".mp3");
            //                var mp3Length = (int)file.Properties.Duration.TotalSeconds; // mp3 length
            //                                                                            // xử lý đổi s sang g
            //                Time += mp3Length;
            //            }
            //        }
            //    }
            //    int _tb = Time / (So_Anh - 1);
            //    int _Du = Time % (So_Anh - 1);
            //    if (_Du > 0)
            //    {
            //        _tb++;
            //    }
            //    Timer = _tb.ToString();
            //}// end iff
            //else
            //{
            //    Timer = Time.ToString();
            //}
            #endregion

            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            double duration1 = duration * 1.7;
            double dration2 = Math.Truncate(duration1);

            string Create_Video = "/C ffmpeg -r 1/" + dration2 + " -i %d.jpg -c:v mpeg4 -b:v 2400k -pix_fmt yuv420p -r 30 -g 60 -threads 0 -preset superfast C:\\RACC\\Data\\Video" + k + "\\Image\\VideoImage.mp4 -y";
            startInfo1.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo1.FileName = "cmd.exe";
            startInfo1.Arguments = "/C ffmpeg.exe ";
            process1.StartInfo = startInfo1;
            process1.Start();

            //chạy code tạo video
            startInfo1.Arguments = Create_Video;
            process1.Start();
            process1.Close();




            // hàm chờ dựa theo số ảnh

            //   mg("Tạo xong VideoImage");

            //Add sound vào video
            //{
            //    System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            //    System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            //    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


            //    string Add_Sound = "/C ffmpeg -i VideoImage.mp4 -i TotalMusic.mp3 -c:v mpeg4 -b:v 2400k -c:a copy -shortest C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
            //    startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            //    startInfo2.FileName = "cmd.exe";
            //    startInfo2.Arguments = "/C ffmpeg.exe ";
            //    process2.StartInfo = startInfo2;
            //    process2.Start();

            //    //chạy code add sound vào video
            //    startInfo2.Arguments = Add_Sound;
            //    process2.Start();
            //    Thread.Sleep(So_Anh * 4000);// chờ đợi để chạy xong video

            //}

            //mg("Tạo xong VideoSound");

        }
        public void Create_Folder(int iCount)// icount là số foler cần tạo// cần chú đề để không bị bug
        {



            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


            //// tao thu muc trong cmd:
            startInfo.WorkingDirectory = @"C:\RACC\Data";
            startInfo.FileName = "cmd.exe";
            String _Folder = "Video";
            for (int i = 0; i < iCount; i++)
            {

                startInfo.Arguments = "/C md " + _Folder + i;
                process.StartInfo = startInfo;
                process.Start();

            }
            Thread.Sleep(2000);
            for (int i = 0; i < iCount; i++)// tạo file Voice cho mỗi folder
            {
                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C md Voice";
                process.StartInfo = startInfo;
                process.Start();

            }
            Thread.Sleep(500);
            for (int i = 0; i < iCount; i++)// tạo file Voice cho mỗi folder
            {
                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C md Image";
                process.StartInfo = startInfo;
                process.Start();

            }

            // mỗi folder tạo thêm 1 file txt
            {


                Thread.Sleep(500);
                for (int i = 0; i < iCount; i++)// tạo file Content
                {
                    /////copy C:\RACC\Content.txt C:\RACC\Data\Video0
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C copy  C:\RACC\txtFile\Title.txt C:\RACC\Data\Video" + i;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.Close();

                }


                for (int i = 0; i < iCount; i++)// tạo file Text
                {
                    /////copy C:\RACC\Text.txt C:\RACC\Data\Video0
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C copy  C:\RACC\txtFile\Input.txt C:\RACC\Data\Video" + i;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.Close();

                }
                Thread.Sleep(500);
                for (int i = 0; i < iCount; i++)// tạo file TagVideo
                {
                    /////copy C:\RACC\Text.txt C:\RACC\Data\Video0
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C copy  C:\RACC\txtFile\TagVideo.txt C:\RACC\Data\Video" + i;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.Close();

                }
                Thread.Sleep(500);
                for (int i = 0; i < iCount; i++)// tạo file LinkVoice
                {
                    /////copy C:\RACC\Text.txt C:\RACC\Data\Video0
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + i;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C copy  C:\RACC\txtFile\LinkVoice.txt C:\RACC\Data\Video" + i;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.Close();

                }


                Thread.Sleep(500);
                //   System.Windows.Forms.MessageBox.Show("Done");

            }

            ///  end tao thu muc
        }
        public void Create_Sub(int k)
        {
            string SubLength = "00:00:00,500";
            int Dem_Giay = 0;
            // int Dem_Phut = 0;
            string[] Count_Mp3 = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt", Encoding.UTF8);
            //Count_Mp3 : số tập tin âm thanh tải về
            FileStream fst = new FileStream(@"C:\RACC\Data\Video" + k + @"\Sub.srt", FileMode.Create); // in ra inputupdate
            StreamWriter sw = new StreamWriter(fst, Encoding.UTF8);
            for (int i = 0; i < Count_Mp3.Length; i++)
            {
                if (File.Exists(@"C:\RACC\Data\Video" + k + @"\Voice\" + i + ".mp3"))
                {
                    int mp3Length = 0;
                    try
                    {
                        var file = TagLib.File.Create(@"C:\RACC\Data\Video" + k + @"\Voice\" + i + ".mp3");
                        mp3Length = (int)file.Properties.Duration.TotalSeconds;
                        file.Save();
                    }
                    catch (Exception)
                    {

                        var file = TagLib.File.Create(@"C:\RACC\Data\Video" + k + @"\Voice\" + (i + 1) + ".mp3");
                        mp3Length = (int)file.Properties.Duration.TotalSeconds;
                        file.Save();
                    }


                    int giay = 0;
                    int phut = 0;
                    int gio = 0;
                    giay = mp3Length + Dem_Giay; // cộng dồn giây

                    if (giay < 60)
                    {
                        giay = mp3Length + Dem_Giay;

                    }
                    else if (giay >= 60 && giay < 3600)
                    {
                        phut = (giay - giay % 60) / 60;
                        giay %= 60;
                    }
                    else
                    {
                        gio = (giay - giay % 3600) / 3600;
                        phut = ((giay % 3600) - (giay % 3600) % 60) / 60;
                        giay = giay - phut * 60 - gio * 3600;
                    }
                    // xử lý để có 2 chữ số vf; 03 06
                    string _giay = giay.ToString();
                    string _phut = phut.ToString();
                    string _gio = gio.ToString();
                    if (giay < 10)
                    {
                        _giay = "0" + giay;
                    }

                    if (phut < 10)
                    {
                        _phut = "0" + phut;
                    }
                    if (gio < 10)
                    {
                        _gio = "0" + gio;
                    }
                    Dem_Giay = phut * 60 + giay;
                    string _Sub = "";
                    _Sub += (i + 1) + "";
                    _Sub += "\n" + SubLength + " --> " + _gio + ":" + _phut + ":" + _giay + ",000";
                    SubLength = "" + _gio + ":" + _phut + ":" + _giay + ",050";
                    _Sub += "\n" + Count_Mp3[i];
                    _Sub += "\n";
                    sw.WriteLine(_Sub);

                }

            }
            sw.Close();
            // xong phần get ra thời lượng của từng âm thanh


        }
        public void Add_Sub(int k)
        {
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


            string Add_Sub = "/C ffmpeg  -i C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -i C:\\RACC\\Data\\Video" + k + "\\Sub.srt -c:s mov_text -c:v copy -c:a copy C:\\RACC\\VideoProduct\\" + k + "videosub.mp4 -y";
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = "/C ffmpeg.exe ";
            process2.StartInfo = startInfo2;
            process2.Start();

            //chạy code add sound vào video
            startInfo2.Arguments = Add_Sub;
            process2.Start();

            Thread.Sleep(4000);

        }
        public void Add_Sound(int k)
        {
            {
                System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


                string Add_Sound = "/C ffmpeg -i VideoSound1.mp4 -i TotalMusic.mp3 -c:v mpeg4 -b:v 2400k -c:a copy -shortest C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
                startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = "/C ffmpeg.exe ";
                process2.StartInfo = startInfo2;
                process2.Start();

                //chạy code add sound vào video
                startInfo2.Arguments = Add_Sound;
                process2.Start();


            }

        }
        public void Add_Sound_ImageVoice(int k)
        {
            {
                System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


                string Add_Sound = "/C ffmpeg -i VideoImage.mp4 -i TotalMusic.mp3 -c:v mpeg4 -b:v 2400k -c:a copy -shortest C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
                startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = "/C ffmpeg.exe ";
                process2.StartInfo = startInfo2;
                process2.Start();

                //chạy code add sound vào video
                startInfo2.Arguments = Add_Sound;
                process2.Start();


            }

        }
        public void Change_Title_Sub(int kk)
        {
            for (int k = 0; k < kk; k++)
            {
                string[] Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Title.txt");
                Title[0] = Title[0].Replace(",", "-");
                Title[0] = Title[0].Replace(" ", "-");
                Title[0] = Title[0].Trim();
                string s = Title[0];
                // MessageBox.Show(Title[0]);
                // C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
                if (File.Exists("C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4"))
                {
                    System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                    startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                    startInfo2.FileName = "cmd.exe";
                    startInfo2.Arguments = "/C ren VideoSound.mp4 " + s + ".mp4";
                    process2.StartInfo = startInfo2;
                    process2.Start();
                    //Thread.Sleep(500);
                    //startInfo2.Arguments = @"/C copy C:\RACC\Data\Video" + k + @"\Sub.srt C:\RACC\Data\Video" + k + @"\Image\" + s + ".srt";
                    // process2.Start();
                    //Thread.Sleep(500);
                    //startInfo2.Arguments = "/C move " + s + ".mp4 " + @"C:\RACC\VideoProduct\";
                    // process2.Start();
                    //Thread.Sleep(500);
                    //startInfo2.Arguments = "/C move " + s + ".srt " + @"C:\RACC\VideoProduct\";
                    //process2.Start();
                    //Thread.Sleep(500);
                    //startInfo2.Arguments = "/C ren Thumb.jpg " + s + ".jpg";
                    //process2.Start();
                    Thread.Sleep(500);
                    //startInfo2.Arguments = "/C move " + s + ".jpg " + @"C:\RACC\VideoProduct\";
                    //process2.Start();

                    process2.Close();

                }

            }
        }
        public void Change_Folder_Name(int kk)
        {
            for (int k = 0; k < kk; k++)
            {
                string[] Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Title.txt");
                Title[0] = Title[0].Replace(" ", "-");
                Title[0] = Title[0].Trim();
                string s = Title[0];
                // MessageBox.Show(Title[0]);
                // C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
                if (Directory.Exists("C:\\RACC\\Data\\Video" + k))
                {
                    System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                    startInfo2.WorkingDirectory = @"C:\RACC\Data";
                    startInfo2.FileName = "cmd.exe";
                    startInfo2.Arguments = "/C ren Video" + k + " " + s;
                    process2.StartInfo = startInfo2;
                    process2.Start();
                    process2.Close();



                }

            }
        }
        public void Create_Thumb(int font, int k, string language, string Line1, string Line2, string Line3, bool TextInThumb, bool ImageOrigin)
        {
            string[] Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Title.txt");
            string Text = Title[0];
            if (Text.Length < 20)
            {
                Text += "                       ";
            }
            Text = Text.Replace("\"", "");
            if (language == "Vn" || language == "En")
            {
                string[] s = Text.Split(' ');

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                string CreatThumb_Part1 = string.Format("/C ffmpeg -i t.jpg -vf \"[in]drawtext = fontsize =130:fontcolor ={3}:fontfile ='/RACC/Font/{2}.ttf':text = '{0} {1}':x = (w - text_w) / 2:y = (h - text_h-480),", s[0], s[1], language, Line1);
                string CreatThumb_Part2 = string.Format(" drawtext=fontsize=200:fontcolor={3} :fontfile='/RACC/Font/{2}.ttf':text='{0} {1}':x=(w - text_w)/2:y = (h - text_h-250),", s[2], s[3], language, Line2);
                string CreatThumb_Part3 = string.Format(" drawtext=fontsize=130:fontcolor={3} :fontfile='/RACC/Font/{2}.ttf':text='{0} {1}':x=(w - text_w)/2:y = (h - text_h-50)[out]\" -y Thumb.png", s[4], s[5], language, Line3);
                string ChangeSize = "/C magick convert 1.jpg -resize 1080x720! Thumb.png";
                if (TextInThumb == true)
                {
                    ChangeSize = "/C magick convert 1.jpg -resize 1080x720! t.jpg";
                }
                string CreatThumb = CreatThumb_Part1 + CreatThumb_Part2 + CreatThumb_Part3;
                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = ChangeSize;
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(2000);
                if (TextInThumb == true)
                {
                    startInfo.Arguments = CreatThumb;
                    process.Start();
                    Thread.Sleep(1000);
                }
                process.Close();
            }
            else
            {
                string s = Text;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                string CreatThumb_Part1 = string.Format("/C ffmpeg -i t.jpg -vf \"[in]drawtext = fontsize =150:fontcolor ={6}:fontfile ='/RACC/Font/{5}.ttf':text = '{0} {1} {2} {3} {4}':x = (w - text_w) / 2:y = (h - text_h-500),", s[0], s[1], s[2], s[3], s[4], language, Line1);
                string CreatThumb_Part2 = string.Format(" drawtext=fontsize=215:fontcolor={5} :fontfile='/RACC/Font/{4}.ttf':text='{0} {1} {2} {3}':x=(w - text_w)/2:y = (h - text_h-250),", s[5], s[6], s[7], s[8], language, Line2);
                string CreatThumb_Part3 = string.Format(" drawtext=fontsize=150:fontcolor={6} :fontfile='/RACC/Font/{5}.ttf':text='{0} {1} {2} {3} {4}':x=(w - text_w)/2:y = (h - text_h-50)[out]\" -y Thumb.png", s[9], s[10], s[11], s[12], s[13], language, Line3);
                string ChangeSize = "/C magick convert 1.jpg -resize 1080x720! Thumb.png";
                if (TextInThumb == true)
                {
                    ChangeSize = "/C magick convert 1.jpg -resize 1080x720! t.jpg";

                }
                if (ImageOrigin == true)
                {
                    ChangeSize = string.Format(@"/C magick convert C:\RACC\Data\Video{0}\image.jpg -resize 1080x720! C:\RACC\Data\Video{0}\Image\Thumb.png", k);
                }




                string CreatThumb = CreatThumb_Part1 + CreatThumb_Part2 + CreatThumb_Part3;
                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = ChangeSize;
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(2000);
                if (TextInThumb == true)
                {
                    startInfo.Arguments = CreatThumb;
                    process.Start();
                    Thread.Sleep(1000);
                }
                process.Close();


            }
        }
        public void Create_Image_And_Check(int k)
        {
            if (!File.Exists(@"C:\RACC\Data\Video" + k + @"\Image\1.jpg"))
            {
                System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;



                startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = string.Format(@"/C copy C:\RACC\VideoProduct\Image.jpg 1.jpg");
                process2.StartInfo = startInfo2;
                process2.Start();
                process2.Close();

            }
        }
        public void Create_Thumb_List(int font, int To, int From, string language, string Line1, string Line2, string Line3, bool TextInThumb, bool Del, bool ImageOrigin)
        {
            for (int k = To; k < From; k++)
            {
                if (Del)
                {
                    Copy_Iamge(k);

                }

                string[] Title = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Title.txt");
                string Text = Title[0];
                if (Text.Length < 20)
                {
                    Text += "                       ";
                }
                Text = Text.Replace("\"", "");
                if (language == "Vn" || language == "En")
                {
                    string[] s = Text.Split(' ');

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    string CreatThumb_Part1 = string.Format("/C ffmpeg -i t.jpg -vf \"[in]drawtext = fontsize =130:fontcolor ={3}:fontfile ='/RACC/Font/{2}.ttf':text = '{0} {1}':x = (w - text_w) / 2:y = (h - text_h-480),", s[0], s[1], language, Line1);
                    string CreatThumb_Part2 = string.Format(" drawtext=fontsize=200:fontcolor={3} :fontfile='/RACC/Font/{2}.ttf':text='{0} {1}':x=(w - text_w)/2:y = (h - text_h-250),", s[2], s[3], language, Line2);
                    string CreatThumb_Part3 = string.Format(" drawtext=fontsize=130:fontcolor={3} :fontfile='/RACC/Font/{2}.ttf':text='{0} {1}':x=(w - text_w)/2:y = (h - text_h-50)[out]\" -y Thumb.png", s[4], s[5], language, Line3);
                    string ChangeSize = "/C magick convert 1.jpg -resize 1080x720! Thumb.png";
                    if (TextInThumb == true)
                    {
                        ChangeSize = "/C magick convert 1.jpg -resize 1080x720! t.jpg";
                    }
                    string CreatThumb = CreatThumb_Part1 + CreatThumb_Part2 + CreatThumb_Part3;
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = ChangeSize;
                    process.StartInfo = startInfo;
                    process.Start();
                    Thread.Sleep(2000);
                    if (TextInThumb == true)
                    {
                        startInfo.Arguments = CreatThumb;
                        process.Start();
                        Thread.Sleep(1000);
                    }
                    process.Close();
                }
                else
                {
                    string s = Text;
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    string CreatThumb_Part1 = string.Format("/C ffmpeg -i t.jpg -vf \"[in]drawtext = fontsize =150:fontcolor ={6}:fontfile ='/RACC/Font/{5}.ttf':text = '{0} {1} {2} {3} {4}':x = (w - text_w) / 2:y = (h - text_h-500),", s[0], s[1], s[2], s[3], s[4], language, Line1);
                    string CreatThumb_Part2 = string.Format(" drawtext=fontsize=215:fontcolor={5} :fontfile='/RACC/Font/{4}.ttf':text='{0} {1} {2} {3}':x=(w - text_w)/2:y = (h - text_h-250),", s[5], s[6], s[7], s[8], language, Line2);
                    string CreatThumb_Part3 = string.Format(" drawtext=fontsize=150:fontcolor={6} :fontfile='/RACC/Font/{5}.ttf':text='{0} {1} {2} {3} {4}':x=(w - text_w)/2:y = (h - text_h-50)[out]\" -y Thumb.png", s[9], s[10], s[11], s[12], s[13], language, Line3);
                    string ChangeSize = "/C magick convert 1.jpg -resize 1080x720! Thumb.png";
                    if (TextInThumb == true)
                    {
                        ChangeSize = "/C magick convert 1.jpg -resize 1080x720! t.jpg";

                    }
                    if (ImageOrigin == true)
                    {
                        ChangeSize = string.Format(@"/C magick convert C:\RACC\Data\Video{0}\image.jpg -resize 1080x720! C:\RACC\Data\Video{0}\Image\Thumb.png", k);
                    }



                    string CreatThumb = CreatThumb_Part1 + CreatThumb_Part2 + CreatThumb_Part3;
                    startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = ChangeSize;
                    process.StartInfo = startInfo;
                    process.Start();
                    Thread.Sleep(2000);
                    if (TextInThumb == true)
                    {
                        startInfo.Arguments = CreatThumb;
                        process.Start();
                        Thread.Sleep(1000);
                    }
                    process.Close();
                }

            }


        }
        public void Copy_Iamge(int k)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C del 1.jpg";
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(1000);

            if (!File.Exists(string.Format("C:\\RACC\\Data\\Video{0}\\Image\\1.jpg", k)))
            {
                startInfo.Arguments = "/C copy C:\\RACC\\VideoProduct\\Image.jpg 1.jpg";
                process.Start();
            }
            process.Close();
        }
        public void Japan_Video(int k, string FontSize, string Speed, string color, string Xline, string language)
        {
            string[] Text = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt");
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            double x = double.Parse(Xline);
            int Line = Text.Length; // số dòng
            // truyền thời gian vào
            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            string Add_Text = string.Format("/C ffmpeg -i VideoImage.mp4 -vf \"drawtext = fontsize = {0}:fontcolor ={4}:fontfile='/RACC/Font/{3}.ttf':textfile='/RACC/Data/Video{2}/InputUpdate.txt':x=(w-text_w)/2:y=h-{1}*t\" -c:v mpeg4 -b:v 1200k -c:a copy -preset superfast VideoSound1.mp4 -y", FontSize, Speed, k, language, color);
            //ffmpeg -ss 10 -i C:\RACC\VideoProduct\Background.mp4 -c copy -t 30 C:\RACC\Data\Video0\Image\luong.mp4 -y
            string Cut_Video = string.Format("/C ffmpeg -ss 10 -i C:\\RACC\\VideoProduct\\Background.mp4 -c copy -t {0} C:\\RACC\\Data\\Video{1}\\Image\\VideoImage.mp4 -y", duration * 1.7, k);
            startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = "/C ffmpeg.exe ";
            process2.StartInfo = startInfo2;
            process2.Start();
            Thread.Sleep(1000);
            //chạy code add sound vào video
            startInfo2.Arguments = Cut_Video;
            process2.Start();
            Thread.Sleep(Line * 50);
            startInfo2.Arguments = Add_Text;
            process2.Start();
            Thread.Sleep(1000);
            process2.Close();
            Thread.Sleep(Line * 100);
        }
        public void Japan_Video_New(int k, string FontSize, string Speed, string color, string Xline, string language)
        {
            string[] Text = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt");
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            double x = double.Parse(Xline);
            int Line = Text.Length; // số dòng
            // truyền thời gian vào
            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            string Add_Text = string.Format("/C ffmpeg -i VideoImage.mp4 -vf \"drawtext = fontsize = {0}:fontcolor ={4}:fontfile='/RACC/Font/{3}.ttf':textfile='/RACC/Data/Video{2}/InputUpdate.txt':x=(w-text_w)/2:y=h-{1}*t\" -c:v mpeg4 -b:v 2400k -c:a copy -threads 0 -preset superfast VideoSound1.mp4 -y", FontSize, Speed, k, language, color);
            // string Add_Text = string.Format("/C ffmpeg -i VideoImage.mp4 -vf \"drawtext = fontsize = {0}:fontcolor ={4}:fontfile='/RACC/Font/hline.ttf':textfile='/RACC/Data/Video{2}/InputUpdate.txt':x=(w-text_w)/2:y=h-{1}*t\" -c:v mpeg4 -b:v 2400k -c:a copy -threads 0 -preset superfast VideoSound1.mp4 -y", FontSize, Speed, k, language, color);
            //ffmpeg -ss 10 -i C:\RACC\VideoProduct\Background.mp4 -c copy -t 30 C:\RACC\Data\Video0\Image\luong.mp4 -y
            // string Cut_Video = string.Format("/C ffmpeg -ss 10 -i C:\\RACC\\VideoProduct\\Background.mp4 -c copy -t {0} C:\\RACC\\Data\\Video{1}\\Image\\VideoImage.mp4 -y", duration * 1.7, k);
            startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = Add_Text;
            process2.StartInfo = startInfo2;
            process2.Start();
            Thread.Sleep(1000);
            //chạy code add sound vào video


            process2.Close();
            Thread.Sleep(Line * 100);
        }
        public void Download_China_Speech(int k, string language)
        {
            string[] Text = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\Input.txt", k));
            WebClient Wc = new WebClient();
            string Chuoi = "";

            foreach (var item in Text)
            {
                if (item != "")
                {
                    Chuoi += item + ".";
                }
            }
            // tách câu bằng , trong chuỗi

            string[] s1 = Chuoi.Split('.');
            string s = "";
            string SplitS = "";
            foreach (var item in s1)
            {
                s = item;
                if (s.Length > 250)
                {
                    for (int i = 249; i > 0; i--)
                    {
                        if (s[i] == '，')
                        {
                            s = s.Insert(i, ".");
                            break;
                        }
                    }
                }
                SplitS += s + ".";
            }
            for (int i = 0; i < SplitS.Length; i++)
            {
                if (i == 490 || i == 740 || i == 990 || i == 1240 || i == 1490 || i == 1740)
                {
                    SplitS = SplitS.Insert(i, ".");
                }
            }

            string[] TextSplit = SplitS.Split('.');
            string VoidIndex = "";
            if (language == "korean")
            {
                VoidIndex = "18";
            }
            else if (language == "japan")
            {
                VoidIndex = "36";

            }

            for (int i = 0; i < TextSplit.Length; i++)
            {
                try
                {
                    // download json
                    String txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\LinkVoice.txt", k);
                    string url = "http://www.neospeech.com/service/demo?voiceId=" + VoidIndex + "&content=" + TextSplit[i];

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
        public static void Edit_Image(int k)
        {
            ////Lấy thống số ảnh
            string path = string.Format("C:\\RACC\\Data\\Video{0}\\Image\\image.jpg", k);
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            //  MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
            int x = (int)img.Width;
            int y = (int)img.Height;
            img.Dispose();
            int wh = 1280 * y / x;

            Process process = new Process();
            ProcessStartInfo starinfo = new ProcessStartInfo();
            starinfo.WindowStyle = ProcessWindowStyle.Hidden;
            starinfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
            starinfo.FileName = "cmd.exe";
            starinfo.Arguments = string.Format("/C ffmpeg -i image.jpg -vf \"scale = 1280:{0}\" image1.jpg -y", wh);
            process.StartInfo = starinfo;
            process.Start();
            Thread.Sleep(500);
            starinfo.Arguments = "/C ffmpeg -i image1.jpg -vf \"crop = 1280:720\" image1.jpg -y";
            process.Start();
            Thread.Sleep(500);
            starinfo.Arguments = "/C ffmpeg -i image1.jpg -i C:\\RACC\\VideoProduct\\black.png -filter_complex \"[0:v][1:v] overlay = 0:0\" image1.jpg -y";
            process.Start();

            process.Close();
        }
        public void Download_Korean_Speech(int k)
        {
            string[] Text = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\Input.txt", k));
            WebClient Wc = new WebClient();
            string Chuoi = "";
            foreach (var item in Text)
            {
                if (item != "")
                {
                    Chuoi += item + ".";
                }
            }
            // tách câu bằng , trong chuỗi
            string[] s1 = Chuoi.Split('.');
            string s = "";
            string SplitS = "";
            foreach (var item in s1)
            {
                s = item;
                if (s.Length > 250)
                {
                    for (int i = 249; i > 0; i--)
                    {
                        if (s[i] == '，')
                        {
                            s = s.Insert(i, ".");
                            break;
                        }
                    }
                }
                SplitS += s + ".";
            }
            string[] TextSplit = SplitS.Split('.');

            for (int i = 0; i < TextSplit.Length; i++)
            {
                try
                {
                    // download json
                    String txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\LinkVoice.txt", k);
                    string url = "http://www.neospeech.com/service/demo?voiceId=18&content=" + TextSplit[i];

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
        public void EN_Video(int k)
        {

            string Text = File.ReadAllText(@"C:\RACC\Data\Video" + k + @"\Input.txt");
            Text = Text.Replace("\r\n", "");
            using (StreamWriter sw = new StreamWriter(@"C:\RACC\Data\Video" + k + @"\Input.txt"))
            {
                sw.Write(Text);
            }

            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            // truyền thời gian vào
            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            string Add_Text = string.Format("/C ffmpeg -i VideoImage.mp4 -vf \"drawtext=fontsize=50:fontcolor =white:fontfile='/RACC/Font/Action_Man.ttf':textfile='/RACC/Data/Video{0}/Input.txt':y=h-line_h:x=-{1}*t\" -c:v mpeg4 -b:v 1200k -c:a copy -preset superfast VideoSound1.mp4 -y", k, duration * 2.08);
            //ffmpeg -ss 10 -i C:\RACC\VideoProduct\Background.mp4 -c copy -t 30 C:\RACC\Data\Video0\Image\luong.mp4 -y
            //  string Cut_Video = string.Format("/C ffmpeg -ss 10 -i C:\\RACC\\VideoProduct\\Background.mp4 -c copy -t {0} C:\\RACC\\Data\\Video{1}\\Image\\VideoImage.mp4 -y", duration * 1.7, k);
            startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = "/C ffmpeg.exe ";
            process2.StartInfo = startInfo2;
            process2.Start();
            Thread.Sleep(1000);
            //chạy code add sound vào video

            startInfo2.Arguments = Add_Text;
            process2.Start();
            Thread.Sleep(1000);
            process2.Close();

        }
        public void Download_Span_Speech(int k)
        {
            string[] Text = File.ReadAllLines(string.Format(@"C:\RACC\Data\Video{0}\InputUpdate.txt", k));
            WebClient Wc = new WebClient();

            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i].Length > 5)
                {


                    try
                    {
                        // download json
                        String txtSaveFile = string.Format(@"C:\RACC\Data\Video{0}\LinkVoice.txt", k);
                        string url = "http://www.neospeech.com/service/demo?voiceId=46&content=" + Text[i];

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

        }
        public static void Edit_Image_Span(int k)
        {
            ////Lấy thống số ảnh
            string path = string.Format("C:\\RACC\\Data\\Video{0}\\Image\\image.jpg", k);
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            //  MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
            int x = (int)img.Width;
            int y = (int)img.Height;
            img.Dispose();
            int wh = 1280 * y / x;

            Process process = new Process();
            ProcessStartInfo starinfo = new ProcessStartInfo();
            starinfo.WindowStyle = ProcessWindowStyle.Hidden;
            starinfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
            starinfo.FileName = "cmd.exe";
            starinfo.Arguments = string.Format("/C ffmpeg -i image.jpg -vf \"scale = 1280:{0}\" image1.jpg -y", wh);
            process.StartInfo = starinfo;
            process.Start();
            Thread.Sleep(500);
            starinfo.Arguments = "/C ffmpeg -i image1.jpg -vf \"crop = 1280:720\" image1.jpg -y";
            process.Start();
            Thread.Sleep(500);
            starinfo.Arguments = "/C ffmpeg -i image1.jpg -i C:\\RACC\\VideoProduct\\foot.png -filter_complex \"[0:v][1:v] overlay = 0:0\" image1.jpg -y";
            process.Start();

            process.Close();
        }
        public static void Edit_Image_noText(int k)
        {
            ////Lấy thống số ảnh
            string path = string.Format("C:\\RACC\\Data\\Video{0}\\Image\\image.jpg", k);
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            //  MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
            int x = (int)img.Width;
            int y = (int)img.Height;
            img.Dispose();
            int wh = 1280 * y / x;

            Process process = new Process();
            ProcessStartInfo starinfo = new ProcessStartInfo();
            starinfo.WindowStyle = ProcessWindowStyle.Hidden;
            starinfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
            starinfo.FileName = "cmd.exe";
            starinfo.Arguments = string.Format("/C ffmpeg -i image.jpg -vf \"scale = 1280:{0}\" image1.jpg -y", wh);
            process.StartInfo = starinfo;
            process.Start();
            Thread.Sleep(500);
            starinfo.Arguments = "/C ffmpeg -i image1.jpg -vf \"crop = 1280:720\" image1.jpg -y";
            process.Start();
            Thread.Sleep(500);
            // starinfo.Arguments = "/C ffmpeg -i image1.jpg -i C:\\RACC\\VideoProduct\\black.png -filter_complex \"[0:v][1:v] overlay = 0:0\" image1.jpg -y";
            // process.Start();

            process.Close();
        }
        public void Span_Video(int k)
        {

            string Text = File.ReadAllText(@"C:\RACC\Data\Video" + k + @"\Input.txt");
            Text = Text.Replace("\r\n", "");
            using (StreamWriter sw = new StreamWriter(@"C:\RACC\Data\Video" + k + @"\Input.txt"))
            {
                sw.Write(Text);
            }

            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            // truyền thời gian vào
            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;

            // tốc độ

            int n = 0;
            string str = File.ReadAllText(string.Format(@"C:\RACC\Data\Video{0}\Input.txt", k));

            int speed = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    n++;
                }
            }
            Console.WriteLine(n);
            if (n < 300)
            {
                speed = 230;
            }
            if (n > 300)
            {
                speed = 200;
            }
            if (n > 500 && n < 700)
            {
                speed = 210;
            }
            if (n > 800 && n < 1000)
            {
                speed = 200;
            }
            //
            string Add_Text = string.Format("/C ffmpeg -i VideoImage.mp4 -vf \"drawtext=fontsize=40:fontcolor =white:fontfile='/RACC/Font/Action_Man.ttf':textfile='/RACC/Data/Video{0}/Input.txt':y=h-line_h:x=-{1}*t\" -c:v mpeg4 -b:v 1200k -c:a copy -preset superfast VideoSound1.mp4 -y", k, speed);
            //ffmpeg -ss 10 -i C:\RACC\VideoProduct\Background.mp4 -c copy -t 30 C:\RACC\Data\Video0\Image\luong.mp4 -y
            //  string Cut_Video = string.Format("/C ffmpeg -ss 10 -i C:\\RACC\\VideoProduct\\Background.mp4 -c copy -t {0} C:\\RACC\\Data\\Video{1}\\Image\\VideoImage.mp4 -y", duration * 1.7, k);
            startInfo2.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = "/C ffmpeg.exe ";
            process2.StartInfo = startInfo2;
            process2.Start();
            Thread.Sleep(1000);
            //chạy code add sound vào video

            startInfo2.Arguments = Add_Text;
            process2.Start();
            Thread.Sleep(1000);
            process2.Close();

        }
        public void Render_Video_Manhua(int k,List<int> listImg, int time, int speed)
        {
            for (int i = 0; i < listImg.Count; i++)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                startInfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
                startInfo.FileName = "cmd.exe";
                // chưa resize về hd
                startInfo.Arguments = $"/C ffmpeg -loop 1 -t {time} -i \"{listImg[i]}.jpg\" -filter_complex \"color=000000:s=1280x720[bg];[bg][0]overlay=shortest=1:y='min(0,-(t)*{speed})'\" -y {listImg[i]}.mp4";
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(10000);

                process.Close();
            }
           
        }

        public void Join_Video_Manhua(int k,  List<int> listImg)
        {
            // creat text
           
          
            using (FileStream fs = new FileStream($"C:\\RACC\\Data\\Video{k}\\Image\\list.txt", FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < listImg.Count; i++)
                    {
                        sw.WriteLine($"file '{listImg[i]}.mp4'");
                    }
                   
                }
            }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                startInfo.WorkingDirectory = string.Format("C:\\RACC\\Data\\Video{0}\\Image", k);
                startInfo.FileName = "cmd.exe";
                // chưa resize về hd
                startInfo.Arguments = $"/C ffmpeg -f concat -i list.txt -c copy VideoImage.mp4";
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(5000);

                process.Close();
            

        }

    }
}
