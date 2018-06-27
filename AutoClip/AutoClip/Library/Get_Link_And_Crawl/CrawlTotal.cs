using AutoClip.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip
{
    class CrawlTotal
    {

        public Posts Crawl(string Link)
        {

            Posts p = new Posts();
            if (Link != "")
            {
                #region Danh sách link


                if (Link.Contains("http://news.ltn.com.tw/news/entertainment"))
                {
                    p.Crawl_Entertainment(Link);
                }
                else if (Link.Contains("http://news.ltn.com.tw/news/sports"))
                {
                    p.Crawl_Sports(Link);
                }
                else if (Link.Contains("http://istyle.ltn.com.tw/article"))
                {
                    p.Crawl_Style(Link);
                }
                else if (Link.Contains("http://opinion.chinatimes.com/"))
                {
                    p.Crawl_Chinatimes_Opinion(Link);
                }
                else if (Link.Contains("http://www.chinatimes.com/realtimenews/"))
                {
                    p.Crawl_Chinatimes_Opinion(Link);
                }
                else if (Link.Contains("http://www.chinatimes.com/realtimenews/"))
                {
                    p.Crawl_Chinatimes_Opinion(Link);
                }
                else if (Link.Contains("http://culture.dwnews.com"))
                {
                    p.Crawl_DWnews(Link);
                }
                else if (Link.Contains("https://udn.com/news/story/"))
                {
                    p.Crawl_UDN_Sports(Link);
                }
                else if (Link.Contains("http://news.sina.com.cn"))
                {
                    p.Crawl_sina(Link);
                }
                else if (Link.Contains("http://mil.news.sina.com.cn/china"))
                {
                    p.Crawl_Military(Link);

                }
                else if (Link.Contains("http://mil.news.sina.com.cn"))
                {
                    p.Crawl_Military_International(Link);
                }
                else if (Link.Contains("http://sports.sina.com.cn"))
                {
                    p.Crawl_Sport_Sina(Link);
                }
                else if (Link.Contains("http://www.aboluowang.com"))
                {
                    p.Crawl_abo(Link);
                }

                else if (Link.Contains("http://www.cna.com.tw"))
                {
                    //http://www.cna.com.tw
                    p.Crawl_Cna(Link);
                }
                else if (Link.Contains("http://sports.khan.co.kr/news"))
                {
                    // this.dgvPost.DefaultCellStyle.Font = new Font("Baekmuk Headline Regular", 10);
                    p.Crawl_Khan(Link);
                }

                else if (Link.Contains("http://www.cwbst.com"))
                {

                    p.Crawl_Cwbst(Link);
                }
                else if (Link.Contains("http://www.setn.com/News.aspx?NewsID"))
                {
                    string node = "@id='Content1'";
                    p.Crawl_Setn(Link, node);
                }

                else if (Link.Contains("http://www.setn.com/E/News.aspx?NewsID"))
                {
                    string node = "@class='Content2'";
                    p.Crawl_Setn(Link, node);
                }
                else if (Link.Contains("http://www.abc.es"))
                {

                    p.Crawl_AbcES(Link);
                }
                else if (Link.Contains("https://www.elespanol.com"))
                {

                    p.Crawl_elespanol(Link);
                }
                else if (Link.Contains("http://military.china.com/important"))
                {

                    p.Crawl_china(Link);
                }
                else if (Link.Contains("http://news.china.com"))
                {

                    p.Crawl_china(Link);
                }
                #endregion
                else if (Link.Contains("http://www.chinanews.com"))
                {

                    p.Crawl_chinanews_com(Link);
                }
                else if (Link.Contains("https://kknews.cc/entertainment"))
                {

                    p.Crawl_kknews(Link);
                }
                else if (Link.Contains("http://taiwan.huanqiu.com") || Link.Contains("http://china.huanqiu.com") || Link.Contains("http://mil.huanqiu.com/china/") || Link.Contains("http://world.huanqiu.com/article/2.html") || Link.Contains("huanqiu.com"))
                {

                    p.Crawl_huanqiu_com(Link);
                }
                else if (Link.Contains("https://www.nownews.com"))
                {

                    p.Crawl_Nownews(Link);
                }
                else if (Link.Contains("http://www.spotvnews.co.kr"))
                {

                    p.Crawl_Sportvnews(Link);
                }
                else if (Link.Contains("http://www.eldawlagia.com"))
                {
                    p.Crawl_Arabic_Eldawlagia(Link);
                }






                else
                {
                    // bao gồm tổng quan chính trị cuộc sống xã hội
                    p.Crawl(Link);
                }


            }



            return p;
        }
    }
}
