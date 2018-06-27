using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    static class Create_Folder
    {
        static public void Create(int iCount)// icount là số foler cần tạo// cần chú đề để không bị bug
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
            Thread.Sleep(1000);
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


                Thread.Sleep(1000);
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
                Thread.Sleep(1000);
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
                Thread.Sleep(1000);
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

                Thread.Sleep(1000);

            }

            ///  end tao thu muc
        }

        static public void Delete()
        {
            string path = @"C:\RACC\Data\";
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(path);
            try
            {
                directory.Delete(true);
                Thread.Sleep(60000);
                directory.Create();
            }
            catch (Exception)
            {
                directory.Create();
            }
        }
    }
}
