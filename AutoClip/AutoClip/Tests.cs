using AutoClip.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip
{
    class Tests
    {
      static  public void Crawl_Test()
        {
            List<string> DsPosts_Link_Test = new List<string>();
            List<string> DsPosts_Link = new List<string>();
            Posts p = new Posts();
            // số thứ tự của news
            // test nes ở đây

            p = DS_Get_Link.Get(45);


            //
            string[] Arr_Link = p.url.Split(',');
            foreach (var item in Arr_Link)
            {
                if (item != "")
                {
                    DsPosts_Link_Test.Add(item);
                }

            }
            // lọc những link trùng
            DsPosts_Link = DsPosts_Link_Test.Distinct().ToList();

            //crawl
          //  Console.WriteLine("test getlink o day");
            int dem=0;
            DsPosts_Link.ForEach(Link =>
            {
                #region Crawl

                if (Link != "")
                {
                    Posts post = new Posts();
                    CrawlTotal crawlTotal = new CrawlTotal();

                    // check ở đây
                    post = crawlTotal.Crawl(Link);

                    Thread.Sleep(500);

                    if (post.Image != "" && post.Ngay == 28)
                    {
                        dem++;
                    }

                    //if (post.Image != "")
                    //{
                    //    dem++;
                    //}

                }
                #endregion
            });


            dem++;
            p.Crawl("http://news.ltn.com.tw/news/society/breakingnews/2401545");


        }
        
    }
}
