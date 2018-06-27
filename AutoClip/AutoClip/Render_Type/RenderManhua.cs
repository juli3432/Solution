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
    class RenderManhua
    {


        public static void Start(int ToFolder, int FormFolder)
        {


            Render_Video_Single(ToFolder, FormFolder);

            thrdSleep(10);    

            AddSound(ToFolder, FormFolder);

            thrdSleep(10);
            

        }
        // chia 3 giai đoạn
        //1. each image => video single
        //2. join video single => video total => delete video single
        //3. Add  sound free


        static void Render_Video_Single(int ToFolder, int FormFolder)
        {
            CodeFFMPEG code = new CodeFFMPEG();
            try
            {

                for (int k = ToFolder; k < FormFolder; k++)
                {
                Jump:
                    if (k == FormFolder)
                    {
                        break;
                    }
                    if (File.Exists($"C:\\RACC\\Data\\Video{k}\\Image\\VideoImage.mp4"))
                    {
                        k++;
                        Console.WriteLine("\n Da hoan thanh video :" + k + " OK");
                        goto Jump;

                    }

                    int time = 20;
                    int speed = 70;
                    List<int> listImg = new List<int>();
                    listImg = countImg(k);
                    ///  // truyền vào thư mục videox và số ảnh
                    Edit_Image.Convert_1280x(k, listImg);
                   
                    thrdSleep(10);
                    code.Render_Video_Manhua(k, listImg, time, speed);
                    Console.WriteLine($"Render success video{k}");
                    thrdSleep(10);

                    code.Join_Video_Manhua(k, listImg);


                }
            }
            catch
            {


            }


        }


        static void AddSound(int ToFolder, int FormFolder)
        {
            try
            {
                //thrdSleep(30);
                CodeFFMPEG code = new CodeFFMPEG();
                for (int i = ToFolder; i < FormFolder; i++)
                {
                Jump:
                    if (i == FormFolder)
                    {
                        break;
                    }
                    if (File.Exists($"C:\\RACC\\Data\\Video{i}\\Image\\VideoSound.mp4"))
                    {
                        i++;
                        Console.WriteLine("\n Đã hoàn thành Video:" + i + " OK");
                        goto Jump;

                    }
                    AddSoundManhua(i);
                    bool check = false;
                    int SolanLap = 0;
                    do
                    {
                        if (File.Exists("C:\\RACC\\Data\\Video" + i + "\\Image\\VideoSound.mp4"))
                        {
                            Console.WriteLine("\n Đã hoàn thành Video:" + i + " OK");
                            check = true;
                            Delete(ToFolder, FormFolder);
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

        public static List<int> countImg(int k)
        {
            List<int> listImg = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                if (File.Exists($"C:\\RACC\\Data\\Video{k}\\Image\\{i}.jpg"))
                {
                    listImg.Add(i);
                }
            }


            return listImg;

        }

        public static void Delete(int ToFolder, int FormFolder)
        {
            try
            {
                for (int k = ToFolder; k < FormFolder; k++)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string path = $@"C:\RACC\Data\Video{k}\Image\{i}.mp4";
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        string path2 = $@"C:\RACC\Data\Video{k}\Image\{i+1}.jpg";
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                    }
                }
            }
            catch 
            {

              
            }
        
           
            
        }

        public static void AddSoundManhua(int k)
        {

            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;


            string Add_Sound = "/C ffmpeg -i VideoImage.mp4 -i C:\\RACC\\VideoProduct\\bg.mp3 -c:v mpeg4 -b:v 2400k -c:a copy -shortest C:\\RACC\\Data\\Video" + k + "\\Image\\VideoSound.mp4 -y";
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

}
