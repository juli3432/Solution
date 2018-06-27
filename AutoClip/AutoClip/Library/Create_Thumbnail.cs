using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class Create_Thumbnail
    {

        public static void Origin(int k)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            string ChangeSize = string.Format(@"/C magick convert C:\RACC\Data\Video{0}\image.jpg -resize 1080x720! C:\RACC\Data\Video{0}\Image\Thumb.png", k);

            startInfo.WorkingDirectory = @"C:\RACC\Data\Video" + k + @"\Image";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = ChangeSize;
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(2000);

            process.Close();



        }
    }
}
