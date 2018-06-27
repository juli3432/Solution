using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AutoClip.Library
{
    class Upload_Console
    {
        static public void Start(Parametter para)
        {
           
            Process process = new Process();

            process.StartInfo.FileName = string.Format(@"C:\RACC\Console\Run\run0.bat");
            process.Start();

            Thread.Sleep(6060000);//61 phút

            process.Close();
            if (!para.Type.Equals("manhua"))
            {

                Thread.Sleep(600000);//5 phút
                process.StartInfo.FileName = string.Format(@"C:\RACC\Console\Thumb\UploadThumb.exe");
                process.Start();
                process.Close();
            }

        }



    }
}
