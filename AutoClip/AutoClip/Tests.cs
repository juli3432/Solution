using AutoClip.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip
{
    class Tests
    {
      static  public void Crawl_Test()
        {
            Posts p = new Posts();
            p.Crawl("http://news.ltn.com.tw/news/society/breakingnews/2401545");


        }
        
    }
}
