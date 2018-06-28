using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip.Library
{

    class DS_Get_Link
    {

        public static Posts Get(int STT)
        {
            Posts p = new Posts();


            string Link1 = "";
            #region Select Combobox

            #region Ltn.com.tw


            if (STT == 0)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/politics";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 1)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/society";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 2)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/world";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 3)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/business";
                int Limit = 5;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 4)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/life";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 5)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/sports";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 6)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/entertainment";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }
            else if (STT == 7)
            {
                Link1 = "http://news.ltn.com.tw/list/breakingnews/style";
                int Limit = 7;
                p.GetLink(Link1, Limit);
            }

            #endregion

            #region chinatime
            else if (STT == 8)
            {
                Link1 = "http://opinion.chinatimes.com/total";
                p.GetLink_Chinatimes_Opinion(Link1, "opinion");
            }
            else if (STT == 9)
            {
                Link1 = "http://www.chinatimes.com/world/total";
                p.GetLink_Chinatimes_Opinion(Link1, "world");
            }
            else if (STT == 10)
            {
                Link1 = "http://www.chinatimes.com/chinese/total";
                p.GetLink_Chinatimes_Opinion(Link1, "china");
            }
            else if (STT == 11)
            {
                Link1 = "http://www.chinatimes.com/armament/total";
                p.GetLink_Chinatimes_Opinion(Link1, "armament");
            }
            else if (STT == 12)
            {
                Link1 = "http://www.chinatimes.com/sports/total/";
                p.GetLink_Chinatimes_Opinion(Link1, "sports");
            }

            #endregion

            #region UDN

            else if (STT == 13)
            {
                Link1 = "https://udn.com/news/breaknews/1/7#breaknews";
                p.Get_udn_Breaknews(Link1);
            }
            else if (STT == 14)
            {
                Link1 = "https://udn.com/news/breaknews/1/2#breaknews";
                p.Get_udn_Breaknews(Link1);
            }
            else if (STT == 15)
            {
                Link1 = "https://udn.com/news/breaknews/1/1#breaknews";
                p.Get_udn_Breaknews(Link1);
            }
            else if (STT == 16)
            {
                Link1 = "https://udn.com/news/breaknews/1/0#breaknews";
                p.Get_udn_Breaknews(Link1);
            }
            else if (STT == 17)
            {
                Link1 = "https://udn.com/news/breaknews/1/3#breaknews";
                p.Get_udn_Breaknews(Link1);
            }
            else if (STT == 18)
            {
                Link1 = "https://udn.com/news/breaknews/1/5#breaknews";
                p.Get_udn_Breaknews(Link1);
            }

            #endregion

            #region Sina

            else if (STT == 19)
            {
                Link1 = "http://news.sina.com.cn/world/";
                p.Get_sina(Link1);
            }
            else if (STT == 20)
            {
                Link1 = "http://news.sina.com.cn/china/";
                p.Get_sina(Link1);
            }
            else if (STT == 21)
            {
                Link1 = "http://news.sina.com.cn/society/";
                p.Get_sina(Link1);
            }
            else if (STT == 22)
            {
                Link1 = "http://roll.news.sina.com.cn/news/gnxw/gatxw/index.shtml";
                p.Get_News_Sina(Link1);
            }
            else if (STT == 23)
            {
                Link1 = "http://roll.mil.news.sina.com.cn/col/zgjq/index.shtml";
                p.Get_Military(Link1);
            }
            else if (STT == 24)
            {
                Link1 = "http://roll.mil.news.sina.com.cn/col/gjjq/index.shtml";
                p.Get_Military(Link1);
            }
            else if (STT == 25)
            {
                Link1 = "http://sports.sina.com.cn/";
                string node = "footballchina";
                p.Get_Sport_Sina(Link1, node);
            }
            else if (STT == 26)
            {
                //footballglobal
                Link1 = "http://sports.sina.com.cn/";
                string node = "footballglobal";
                p.Get_Sport_Sina(Link1, node);
            }
            else if (STT == 27)
            {
                //nba
                Link1 = "http://sports.sina.com.cn/";
                string node = "nba";
                p.Get_Sport_Sina(Link1, node);
            }
            else if (STT == 28)
            {
                //cba
                Link1 = "http://sports.sina.com.cn/";
                string node = "cba";
                p.Get_Sport_Sina(Link1, node);
            }
            else if (STT == 29)
            {
                //other
                Link1 = "http://sports.sina.com.cn/";
                string node = "sportsother";
                p.Get_Sport_Sina(Link1, node);
            }
            #endregion

            #region Abo
            else if (STT == 30)
            {
                Link1 = "http://www.aboluowang.com/news/roll/";
                string node = "news";
                p.Get_abo(Link1, node);
            }
            else if (STT == 31)
            {
                Link1 = "http://www.aboluowang.com/news/roll/";
                string node = "comment";
                p.Get_abo(Link1, node);
            }
            else if (STT == 32)
            {
                Link1 = "http://www.aboluowang.com/news/roll/";
                string node = "life";
                p.Get_abo(Link1, node);
            }
            else if (STT == 33)
            {
                Link1 = "http://www.aboluowang.com/news/roll/";
                string node = "fresh";
                p.Get_abo(Link1, node);
            }
            else if (STT == 34)
            {
                Link1 = "http://www.aboluowang.com/news/roll/";
                string node = "ent";
                p.Get_abo(Link1, node);
            }

            #endregion

            #region Cna


            else if (STT == 35)
            {
                Link1 = "";
                string node = "aipl";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 36)
            {
                Link1 = "";
                string node = "aopl";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 37)
            {
                Link1 = "";
                string node = "acul";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 38)
            {
                Link1 = "";
                string node = "acn";
                p.Get_Cna(Link1, node);
            }

            else if (STT == 39)
            {
                Link1 = "";
                string node = "afe";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 40)
            {
                Link1 = "";
                string node = "ait";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 41)
            {
                Link1 = "";
                string node = "asoc";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 42)
            {
                Link1 = "";
                string node = "aloc";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 43)
            {
                Link1 = "";
                string node = "ahel";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 44)
            {
                Link1 = "";
                string node = "aspt";
                p.Get_Cna(Link1, node);
            }
            else if (STT == 45)
            {
                Link1 = "";
                string node = "amov";
                p.Get_Cna(Link1, node);
            }

            #endregion

            #region Cwbst

            else if (STT == 46)
            {
                Link1 = "";
                string node = "news";
                p.Get_Cwbst(Link1, node);
            }
            else if (STT == 47)
            {
                Link1 = "";
                string node = "yule";
                p.Get_Cwbst(Link1, node);
            }
            else if (STT == 48)
            {
                Link1 = "";
                string node = "sports";
                p.Get_Cwbst(Link1, node);
            }
            else if (STT == 49)
            {
                Link1 = "";
                string node = "auto";
                p.Get_Cwbst(Link1, node);
            }
            else if (STT == 50)
            {
                Link1 = "";
                string node = "shehui";
                p.Get_Cwbst(Link1, node);
            }

            else if (STT == 51)
            {
                Link1 = "";
                string node = "history";
                p.Get_Cwbst(Link1, node);
            }

            else if (STT == 52)
            {
                Link1 = "";
                string node = "astro";
                p.Get_Cwbst(Link1, node);
            }
            #endregion

            #region Setn

            else if (STT == 53)
            {
                Link1 = "";
                p.Get_Setn(Link1);
            }

            #endregion

            #region 3 page
            else if (STT == 54)
            {
                Link1 = "http://military.china.com/important/";
                p.Get_china(Link1);
            }

            else if (STT == 55)
            {
                Link1 = "http://news.china.com/news100/";
                p.Get_chinanews_com(Link1);
            }
            else if (STT == 56)
            {
                p.Get_kknews();
            }
            #endregion

            #region Huangiu
            else if (STT == 57)
            {
                p.Get_huanqiu_com("http://taiwan.huanqiu.com/article/index.html");
            }
            else if (STT == 58)
            {
                p.Get_huanqiu_com("http://china.huanqiu.com/article/index.html");
            }
            else if (STT == 59)
            {
                p.Get_huanqiu_com("http://mil.huanqiu.com/china/");
                p.Get_huanqiu_com("http://mil.huanqiu.com/world/");
                p.Get_huanqiu_com("http://mil.huanqiu.com/observation/");
                p.Get_huanqiu_com("http://mil.huanqiu.com/strategysituation/");
            }

            else if (STT == 60)
            {
                p.Get_huanqiu_com("http://world.huanqiu.com/article/index.html");
            }
            #endregion

            #region Nownews

            else if (STT == 61)
            {
                p.Get_Nownews("https://www.nownews.com/cat/sport?page=");
            }
            else if (STT == 62)
            {
                p.Get_Nownews("https://www.nownews.com/cat/politic?page=");
            }
            else if (STT == 63)
            {
                p.Get_Nownews("https://www.nownews.com/cat/finance?page=");
            }
            else if (STT == 64)
            {
                p.Get_Nownews("https://www.nownews.com/cat/society?page=");
            }
            else if (STT == 65)
            {
                p.Get_Nownews("https://www.nownews.com/cat/entertainment?page=");
            }
            else if (STT == 66)
            {
                p.Get_Nownews("https://www.nownews.com/cat/novelty?page=");
            }
            else if (STT == 67)
            {
                p.Get_Nownews("https://www.nownews.com/cat/local?page=");
            }
            else if (STT == 68)
            {
                p.Get_Nownews("https://www.nownews.com/cat/global?page=");
            }
            #endregion

            #region abc.es

            else if (STT == 69)
            {
                //sport
                Link1 = "http://www.abc.es/deportes/";
                p.Get_AbcES(Link1);
            }

            else if (STT == 70)
            {
                Link1 = "http://www.abc.es/familia/";
                p.Get_AbcES(Link1);
            }

            else if (STT == 71)
            {
                Link1 = "http://www.abc.es/espana/";

                p.Get_AbcES(Link1);

            }
            else if (STT == 72)
            {
                Link1 = "http://www.abc.es/espana/madrid/";
                p.Get_AbcES(Link1);
            }
            else if (STT == 73)
            {
                Link1 = "http://www.abc.es/internacional/";

                p.Get_AbcES(Link1);
            }
            else if (STT == 74)
            {
                Link1 = "http://www.abc.es/estilo/";
                p.Get_AbcES(Link1);
            }
            else if (STT == 75)
            {
                Link1 = "http://www.abc.es/economia/";

                p.Get_AbcES(Link1);

            }

            #endregion

            #region elespanol

            else if (STT == 76)
            {
                Link1 = "https://www.elespanol.com/mundo/";
                p.Get_elespanol(Link1);
            }
            else if (STT == 77) // doanh nghiệp
            {
                Link1 = "https://www.elespanol.com/economia/";
                p.Get_elespanol(Link1);
            }
            else if (STT == 78) // khoa học
            {
                Link1 = "https://www.elespanol.com/ciencia/";
                p.Get_elespanol(Link1);
            }
            else if (STT == 79)
            {
                Link1 = "https://www.elespanol.com/cultura/"; // văn hóa
                p.Get_elespanol(Link1);
            }
            else if (STT == 80)
            {
                Link1 = "https://www.elespanol.com/deportes/"; // sport
                p.Get_elespanol(Link1);
            }
            else if (STT == 81)
            {
                Link1 = "https://www.elespanol.com/social/"; // Social
                p.Get_elespanol(Link1);
            }
            else if (STT == 82)
            {
                Link1 = "https://www.elespanol.com/espana/";
                p.Get_elespanol(Link1);
            }
            else if (STT == 83)
            {
                Link1 = "https://www.elespanol.com/reportajes/";
                p.Get_elespanol(Link1);
            }
            #endregion

            #region Dwnews
            else if (STT == 84)
            {
                Link1 = "http://culture.dwnews.com/big5";
                p.GetLink_DWnews(Link1);
            }
            #endregion

            #region Arabic

            #region eldawlagia

            else if (STT == 85)
            {
                string node = "world";
                p.Get_Arabic_Eldawlagia(Link1, node);
            }
            else if (STT == 86)
            {
                string node = "MiddleEast";
                p.Get_Arabic_Eldawlagia(Link1, node);
            }
            else if (STT == 87)
            {
                string node = "Sports";
                p.Get_Arabic_Eldawlagia(Link1, node);
            }
            #endregion














            #endregion
            #endregion


            return p;
        }

        public static Hashtable addList()
        {
            Hashtable ds = new Hashtable();

            #region add list
            //ltn
            ds.Add(0, "Ltn_Chính trị");
            ds.Add(1, "Ltn_Xã hội");
            ds.Add(2, "Ltn_Quốc tế");
            ds.Add(3, "Ltn_Tài chính");
            ds.Add(4, "Ltn_Cuộc sống");
            ds.Add(5, "Ltn_Thể thao");
            ds.Add(6, "Ltn_Giải trí");
            ds.Add(7, "Ltn_Thời trang");

            //chinatime
            ds.Add(8, "Chinatimes- Quan Điểm");
            ds.Add(9, "Chinatimes- World");
            ds.Add(10, "Chinatimes- China");
            ds.Add(11, "Chinatime- Vũ Khí");
            ds.Add(12, "Chinatime- Thể Thao");

            //UDN
            ds.Add(13, "UDN.Com- Thể Thao");
            ds.Add(14, "UDN.Com- Xã Hội");
            ds.Add(15, "UDN.Com- Tin Tức");
            ds.Add(16, "UDN.Com- Đặc Trưng");
            ds.Add(17, "UDN.Com- Local");
            ds.Add(18, "UDN.Com- International");

            //Sina
            ds.Add(19, "Sina.com.cn-World");
            ds.Add(20, "Sina.com.cn-China");
            ds.Add(21, "Sina.com.cn-Society");
            ds.Add(22, "Sina.com.cn-News");
            ds.Add(23, "Sina.com.cn-military-Chinese");
            ds.Add(24, "Sina.com.cn-military-International");
            ds.Add(25, "Sports.sina.com.cn-FootBall China");
            ds.Add(26, "Sports.sina.com.cn-FootBall Global");
            ds.Add(27, "Sports.sina.com.cn-NBA");
            ds.Add(28, "Sports.sina.com.cn-CBA");
            ds.Add(29, "Sports.sina.com.cn-Other");

            //Abo
            ds.Add(30, "aboluowang.com-News");
            ds.Add(31, "aboluowang.com-Comment");
            ds.Add(32, "aboluowang.com-Life");
            ds.Add(33, "aboluowang.com-Fresh");
            ds.Add(34, "aboluowang.com-Ent");

            //Cna
            ds.Add(35, "Cna.com.tw-political");
            ds.Add(36, "Cna.com.tw-International");
            ds.Add(37, "Cna.com.tw_Culture");
            ds.Add(38, "Cna.com.tw-Both Sides");
            ds.Add(39, "Cna.com.tw_finance");
            ds.Add(40, "Cna.com.tw_Technology");
            ds.Add(41, "Cna.com.tw_Society");
            ds.Add(42, "Cna.com.tw-Local");
            ds.Add(43, "Cna.com.tw_Life");
            ds.Add(44, "Cna.com.tw_Edu");
            ds.Add(45, "Cna.com.tw_Entertaiment");

            //Cwbst
            ds.Add(46, "Cwbst_News");
            ds.Add(47, "Cwbst_Yule");
            ds.Add(48, "Cwbst_Sports");
            ds.Add(49, "Cwbst_Car");
            ds.Add(50, "Cwbst__Social");
            ds.Add(51, "Cwbst__History");
            ds.Add(52, "Cwbst__Chats");

            ds.Add(53, "Setn_All");

            //3 page
            ds.Add(54, "military.china.com/important/");
            ds.Add(55, "news.china.com/news100/");
            ds.Add(56, "kknews_ent");

            //Huangiu
            ds.Add(57, "http://taiwan.huanqiu.com");
            ds.Add(58, "http://china.huanqiu.com/article/index.html");
            ds.Add(59, "http://mil.huanqiu.com/china/");
            ds.Add(60, "http://world.huanqiu.com/article/2.html");

            //nownews
            ds.Add(61, "nownews_sport");
            ds.Add(62, "nownews_politic");
            ds.Add(63, "nownews_finance");
            ds.Add(64, "nownews_society");
            ds.Add(65, "nownews_entertainment");
            ds.Add(66, "nownews_novelty");
            ds.Add(67, "nownews_local");
            ds.Add(68, "nownews_global");

            //abc.es

            ds.Add(69, "[Spanish]abc.es_Sport");
            ds.Add(70, "[Spanish]abc.es_Family");
            ds.Add(71, "[Spanish]abc.es_People");
            ds.Add(72, "[Spanish]abc.es_Madrid");
            ds.Add(73, "[Spanish]abc.es_internacional");
            ds.Add(74, "[Spanish]abc.es_Spanish");
            ds.Add(75, "[Spanish]abc.es_Economy");

            //elespanol

            ds.Add(76, "[Spanish]elespanol-WORD");
            ds.Add(77, "[Spanish]elespanol-ECONOMY");
            ds.Add(78, "[Spanish]elespanol-Science");
            ds.Add(79, "[Spanish]elespanol-Culture");
            ds.Add(80, "[Spanish]elespanol-Sport");
            ds.Add(81, "[Spanish]elespanol-Social");
            ds.Add(82, "[Spanish]elespanol-SPAN");
            ds.Add(83, "[Spanish]elespanol-REPORT");

            ds.Add(84, "DWnews-Văn hóa");
            //   ds.Add(97, "spotvnews.co.kr");

            //eldawlagia
            ds.Add(85, "[Arabic]eldawlagia_World]");
            ds.Add(86, "[Arabic]eldawlagia_MiddleEast]");
            ds.Add(87, "[Arabic]eldawlagia_Sport]");
            #endregion

            return ds;
        }
    }
}
