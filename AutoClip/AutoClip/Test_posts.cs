using AutoClip.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip
{
    class Test_posts
    {
        static public void Test_Posts()
        {
            Posts p = new Posts();
            string Link = "http://opinion.chinatimes.com/20180220002304-262105";
            p.Crawl_Chinatimes_Opinion(Link);

        }
        
    }
}
