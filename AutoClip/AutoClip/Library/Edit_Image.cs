using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class Edit_Image
    {

        public static void Blur_Black(int k)
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

        public static void Keep_Origin_Image(int k)
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

            process.Close();
        }

        public static void Convert_All_Jpg(int k)
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

        public static void Change_Image_Name(int k)
        {
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


            if (!File.Exists(string.Format("C:\\RACC\\Data\\Video{0}\\Image\\2.jpg", k)))
            {
                startInfo.WorkingDirectory = string.Format(@"C:\RACC\Data\Video{0}\Image", k);
                startInfo.Arguments = "/C copy 1.jpg 2.jpg";
                process.Start();
            }
        }

        public static void Convert_1280x(int k,List<int> listID)
        {
            for (int i = 0; i < listID.Count; i++)
            {
                string path = string.Format($"C:\\RACC\\Data\\Video{k}\\Image\\{listID[i]}.jpg");
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
                starinfo.Arguments = string.Format($"/C ffmpeg -i {listID[i]}.jpg -vf \"scale = 1280:{wh}\" {listID[i]}.jpg -y");
                process.StartInfo = starinfo;
                process.Start();
                Thread.Sleep(200);
                process.Close();
            }
            
        }

    }
}
