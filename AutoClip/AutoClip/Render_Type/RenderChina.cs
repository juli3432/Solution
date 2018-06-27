using AutoClip.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Render_Type
{
    class RenderChina
    {

        public static void Start(int ToFolder, int FormFolder)
        {
            Render_Video(ToFolder, FormFolder);

            thrdSleep(10);
            Render_VideoSound1(ToFolder, FormFolder);

            thrdSleep(10);

            AddSound(ToFolder, FormFolder);

            thrdSleep(10);

        }

        static void Render_Video(int ToFolder, int FormFolder)
        {
            try
            {


                CodeFFMPEG code = new CodeFFMPEG();
            

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = string.Format(@"C:\RACC");
                startInfo.FileName = "copy.bat";
                process.StartInfo = startInfo;
                process.Start();
                process.Close();

                #region Edit Image
                #region Xử lý video0
                try
                {
                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video0\Image\image1.jpg")))
                    {
                        Edit_Image.Convert_All_Jpg(0);
                        Thread.Sleep(5000);
                        Edit_Image.Blur_Black(0);
                    }
                    File.Delete(@"C:\RACC\Data\Video0\Image\image1.jpg");
                }
                catch
                {
                }

                #endregion

                for (int i = ToFolder; i < FormFolder; i++)
                {
                    if (File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\image1.jpg", FormFolder - 1)))
                    {
                        break;
                    }
                jump:
                    if (i >= FormFolder)
                    {
                        break;
                    }
                    try
                    {
                        if (File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\image1.jpg", i)))
                        {
                            i++;
                            goto jump;
                        }
                        Edit_Image.Convert_All_Jpg(i);
                        Thread.Sleep(1000);

                        Edit_Image.Blur_Black(i);
                        Thread.Sleep(2000);
                        string path = string.Format(@"C:\RACC\Data\Video{0}\Image\image.jpg", i);
                        File.Delete(path);

                        Thread.Sleep(1000);

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Không xử lý đk ảnh thứ : " + i);
                    }
                    Thread.Sleep(1000);

                }

                #endregion

                #region Code

                for (int k = ToFolder; k < FormFolder; k++)
                {
                Jump:

                    if (k >= FormFolder)
                    {
                        break;
                    }
                    #region Kiểm tra File tồn tại và xử lý chuỗi input

                    // nếu file đã được tạo thì pass qua
                    if (File.Exists($"C:\\RACC\\Data\\Video{k}\\Image\\VideoImage.mp4"))
                    {

                        Console.WriteLine("\n Đã hoàn thành Video:" + k + " OK");
                        k++;
                        goto Jump;

                    }

                    Standardize_The_String.China(k);


                    #endregion
                    #region Hàm code
                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\1.jpg", k)))
                    {
                        if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\2.jpg", k)))
                        {
                            Edit_Image.Change_Image_Name(k);

                            Thread.Sleep(5000);
                        }
                    }

                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k)))
                    {
                        TextToSpeech.Start(k, "china");

                        Thread.Sleep(3000);
                        code.Join_Voice(k);

                        Thread.Sleep(2000);

                    }

                    try
                    {
                        code.Create_Video(k);
                    }
                    catch
                    {

                        goto ketthuc;
                    }


                    #endregion


                    bool check = false;
                    int SolanLap = 0;
                    do
                    {
                        if (File.Exists("C:\\RACC\\Data\\Video" + k + "\\Image\\VideoImage.mp4"))
                        {
                            Console.WriteLine("\n Đã hoàn thành Video:" + k + " OK");
                            check = true;
                        }
                        SolanLap++;
                        if (SolanLap == 2)
                        {
                            goto ketthuc;
                        }
                        Thread.Sleep(5000);
                    } while (!check);

                ketthuc:
                    #region Create Thumb
                    Create_Thumbnail.Origin(k);
                    #endregion


                }

                #endregion
            }
            catch (Exception)
            {

            }

        }

        static void Render_VideoSound1(int ToFolder, int FormFolder)
        {
            try
            {


                thrdSleep(5);
                CodeFFMPEG code = new CodeFFMPEG();
               

                #region Code china Video

                for (int k = ToFolder; k < FormFolder; k++)
                {
                #region Check
                Jump1:
                    if (k >= FormFolder)
                    {
                        break;
                    }

                    if (File.Exists("C:\\RACC\\Data\\Video" + (k) + "\\Image\\VideoSound1.mp4"))
                    {
                        k++;
                        Console.WriteLine("\n Success VideoSound1:" + k + " OK");
                        goto Jump1;

                    }
                    #endregion

                    #region Code

                    try
                    {
                        Create_VideoSound1(k);
                    }
                    catch (Exception)
                    {

                        Console.WriteLine(string.Format("Error VideoSound1: {0}", k));
                        k++;
                        goto Jump1;
                    }

                    // Thread.Sleep(18000);
                    thrdSleep(50);

                    bool check = false;
                    int SoLanLap = 0;
                    do
                    {
                        if (File.Exists("C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound1.mp4"))
                        {
                            Console.WriteLine("\n Success VideoSound1:" + k + " OK");
                            check = true;

                        }
                        SoLanLap++;
                        if (SoLanLap == 3)
                        {
                            goto Video_Error;
                        }

                        Thread.Sleep(2000);
                    } while (!check);
                Video_Error:;
                    #endregion

                }

                #endregion
            }
            catch (Exception)
            {


            }
        }

        static void AddSound(int To, int From)
        {
            try
            {


                //thrdSleep(30);
                CodeFFMPEG code = new CodeFFMPEG();
                for (int i = To; i < From; i++)
                {
                Jump:
                    if (i == From)
                    {
                        break;
                    }
                    if (File.Exists("C:\\RACC\\Data\\Video" + (i) + "\\Image\\VideoSound.mp4"))
                    {
                        i++;
                        Console.WriteLine("\n Success VideoSound:" + i + " OK");
                        goto Jump;

                    }
                    code.Add_Sound(i);
                    bool check = false;
                    int SolanLap = 0;
                    do
                    {
                        if (File.Exists("C:\\RACC\\Data\\Video" + i + "\\Image\\VideoSound.mp4"))
                        {
                            Console.WriteLine("\n Success VideoSound1:" + i + " OK");
                            check = true;
                        }
                        SolanLap++;
                        if (SolanLap == 3)
                        {
                            goto videoError;
                        }
                        //  Thread.Sleep(13500);
                        thrdSleep(20);


                    } while (!check);
                videoError:;
                }
            }
            catch (Exception)
            {


            }
        }

        static void Create_VideoSound1(int k)
        {
            //default
            int speed = 34;
            int fontSize = 52;
            string fontColor = "#ffffff";
            string language = "china";


            string[] Text = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt");
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            int Line = Text.Length; // số dòng
            // truyền thời gian vào
            TagLib.File f = TagLib.File.Create(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            string Add_Text = $"/C ffmpeg -i VideoImage.mp4 -vf \"drawtext = fontsize = {fontSize}:fontcolor ={fontColor}:fontfile='/RACC/Font/{language}.ttf':textfile='/RACC/Data/Video{k}/InputUpdate.txt':x=(w-text_w)/2:y=h-{speed}*t\" -c:v mpeg4 -b:v 2400k -c:a copy -threads 0 -preset superfast VideoSound1.mp4 -y";

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

        public static void thrdSleep(int giay)
        {
            Thread.Sleep(giay * 1000);
        }
    }
}
