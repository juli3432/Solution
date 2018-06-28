using AutoClip.Download_Image_For_Manhua;
using AutoClip.Library;
using AutoClip.ListManhua;
using AutoClip.Render_Type;
using AutoClip.Upload;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip
{
    class Program
    {
        static void Main(string[] args)
        {
            // lỗi videosound1 0kb là do chưa đổi tên file font từ japan sang china
            //  Test upload image
            Tests.Crawl_Test();
            
            // Upload_Video.Start();
            //Parametter para = new Parametter();
            //para = para.DEserialize();
            //RenderManhua.Start(para.To, para.From);

            int delay = 0;
            Console.WriteLine("Waitting time....(hour)");

            try
            {
                delay = int.Parse(Console.ReadLine());
                // notification("Start....");
            }
            catch (Exception)
            {


            }

            Thread.Sleep(delay * 1000 * 60);

            DateTime dt = DateTime.Now; // ngay gốc
                                        // Console.WriteLine("date time goc      " + dt);
                                        // hiên ra đây là ngày hôm nay của tao
                                        //  Process process = new Process();
            do
            {
                DateTime dt2 = DateTime.Now;// ngày đối chiếu
                Console.WriteLine("" + (dt - dt2));
                if (dt.Year == dt2.Year && dt.Month == dt2.Month && dt.Day == dt2.Day && dt.Hour == dt2.Hour && dt.Minute == dt2.Minute)
                {
                    // Console.WriteLine("Ngon");
                    Auto();
                    Thread.Sleep(10000);
                    dt = dt.AddDays(1);
                }
                Thread.Sleep(5000);
            } while (true);
            // cầm thêm ham_main
        }

        static void Auto()
        {
            Parametter para = new Parametter();
            para = para.DEserialize();

            // hide code  when user need
         
           Create_Folder.Delete();
           Create_Folder.Create(para.From);

            Console.WriteLine("waiting for create folder....");

          
            if (para.Type.Equals("manhua"))
            {
                Manhua.Start(para.To, para.From);
            }
            else
            {
                //crawl
                // check thông số từ para.json
                Console.WriteLine("waiting for crawling....");
               Crawl crawl = new Crawl();
               crawl.Auto_Crawl(para);


                ////  render
                ////    check thông số từ para.json
                Console.WriteLine("Waiting for Render....");
                Render.Instance.Start(para);
            }

            ////    upload
            Console.WriteLine("Waiting for Upload....");
            Upload_Console.Start(para);

        }
        static void test()// tesst ngay 21/2/2018
        {
            //Test 21/2/2018
            //  Tests.Crawl_Test();
            // Test_posts.Test_Posts();
        }

    }


}
