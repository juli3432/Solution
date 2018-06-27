using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class Join_Voice
    {
        static public void Follow_INPUTtxt(int k)
        {
            // code image =>video
            string[] Count_Mp3 = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Input.txt", Encoding.UTF8);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            string Ghep_File = "";
            for (int i = 0; i < Count_Mp3.Length; i++)
            {
                if (i == Count_Mp3.Length - 1)
                {
                    Ghep_File += "\"" + i + ".mp3" + "\"";
                }
                else
                {
                    Ghep_File += "\"" + i + ".mp3" + "\"" + "+ ";
                }


            }


            ////    Ghép âm thanh đã tải về
            {

                startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Voice";// tạo đường dẫn để thực hiện lệnh arguments
                startInfo.FileName = "cmd.exe";// khởi tạo cmd
                process.StartInfo = startInfo; //
                startInfo.Arguments = "/C copy /b " + Ghep_File + @" C:\RACC\Data\Video" + k + @"\Image\TotalMusic.mp3";
                // System.Windows.Forms.MessageBox.Show(startInfo.Arguments);
                process.Start();
                process.Close();

                //  mg("Ghép xong TotalMusic");

            }

            // done ghép âm thanh đã tải về
            Thread.Sleep(2000);

        }
    }
}
