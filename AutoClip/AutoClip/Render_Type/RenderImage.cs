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
    class RenderImage
    {
        public static void Start(int ToFolder, int FormFolder, string language)
        {
           Console.WriteLine("Render Video Span....");
            Render_Span(ToFolder, FormFolder, language);
            thrdSleep(10);

            thrdSleep(10);
            AddSound(ToFolder, FormFolder);
            thrdSleep(10);
        }

        static void Render_Span(int ToFolder, int FormFolder, string language)
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
               Console.WriteLine("Edit Image....");
                #region Edit Image
                #region Xử lý video0
                try
                {
                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video0\Image\image1.jpg")))
                    {
                        Edit_Image.Convert_All_Jpg(0);
                        Thread.Sleep(5000);
                        Edit_Image.Keep_Origin_Image(0);
                        Thread.Sleep(3000);
                        File.Delete(@"C:\RACC\Data\Video0\Image\image.jpg");
                    }
                }
                catch (Exception)
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

                        Edit_Image.Keep_Origin_Image(i);
                        string path = string.Format(@"C:\RACC\Data\Video{0}\Image\image.jpg", i);
                        Thread.Sleep(5000);
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

                #region Code trong theard

                for (int k = ToFolder; k < FormFolder; k++)
                {
                Jump:

                    if (k >= FormFolder)
                    {
                        break;
                    }
                    #region Kiểm tra File tồn tại và xử lý chuỗi input

                    // nếu file đã được tạo thì pass qua
                    if (File.Exists("C:\\RACC\\Data\\Video" + (k) + "\\Image\\VideoImage.mp4"))
                    {

                       Console.WriteLine("\n Đã hoàn thành Video:" + k + " OK");
                        k++;
                        goto Jump;

                    }
                    if (language == "english")
                    {
                        Standardize_The_String.English(k);
                    }


                    #endregion
                    #region Hàm code
                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\1.jpg", k)))
                    {
                        if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\2.jpg", k)))
                        {
                            Edit_Image.Change_Image_Name(k);
                            // Thread.Sleep(1000);
                            Thread.Sleep(5000);
                        }
                    }

                    if (!File.Exists(string.Format(@"C:\RACC\Data\Video{0}\Image\TotalMusic.mp3", k)))
                    {
                       Console.WriteLine(" TextToSpeech .....");
                        Console.WriteLine();
                        TextToSpeech.Start(k, language);
                        // Thread.Sleep(1000);
                        Thread.Sleep(3000);
                        Join_Voice.Follow_INPUTtxt(k);
                        // Thread.Sleep(1000);
                        Thread.Sleep(2000);

                    }

                    try
                    {
                       Console.WriteLine(" Create Video .....");
                        code.Create_Video(k);
                    }
                    catch (Exception)
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


        static void Render_VideoSound1_Span(int ToFolder, int FormFolder)
        {
            try
            {

                thrdSleep(5);
                CodeFFMPEG code = new CodeFFMPEG();
             


                #region Code Japan Video

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
                        Console.WriteLine("\n Đã hoàn thành Video:" + k + " OK");
                        goto Jump1;

                    }

                    #endregion

                    #region Code
                  
                    Standardize_The_String.English(k);
                    try
                    {
                        code.Span_Video(k);
                    }
                    catch (Exception)
                    {

                        Console.WriteLine(string.Format("Có lỗi khi render video: {0} : Bỏ qua và tiếp tục làm video khác", k));
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
                            Console.WriteLine("\n Đã hoàn thành Video:" + k + " OK");
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
                        Console.WriteLine("\n Đã hoàn thành Video:" + i + " OK");
                        goto Jump;

                    }
                    code.Add_Sound_ImageVoice(i);
                    bool check = false;
                    int SolanLap = 0;
                    do
                    {
                        if (File.Exists("C:\\RACC\\Data\\Video" + i + "\\Image\\VideoSound.mp4"))
                        {
                           Console.WriteLine("\n Đã hoàn thành Video:" + i + " OK");

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

        public static void thrdSleep(int giay)
        {
            Thread.Sleep(giay * 1000);
        }

    }
}
