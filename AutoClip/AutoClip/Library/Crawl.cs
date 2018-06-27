using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AutoClip.Library
{
    class Crawl
    {
        List<string> DsPosts_Link = new List<string>();
        List<string> DsPosts_Link_Test = new List<string>();
        List<Posts> DSPosts = new List<Posts>();
        List<int> DsError = new List<int>();
        public int SoVideo = 100;

        public void Auto_Crawl(Parametter para)
        {
            
            string str = para.theme;

            string[] Link_Crawl = str.Split(',');

            #region Get_Link lưu vào DSPOSTS_Link

            for (int i = 0; i < Link_Crawl.Length; i++)
            {
                Posts p = new Posts();

                p = DS_Get_Link.Get(int.Parse(Link_Crawl[i]));

                string[] Arr_Link = p.url.Split(',');
                foreach (var item in Arr_Link)
                {
                    if (item != "")
                    {
                        DsPosts_Link_Test.Add(item);
                    }

                }
            }
            DsPosts_Link = DsPosts_Link_Test.Distinct().ToList();

            #endregion


            // danh sách post đã có rất nhiều post
            #region Crawl

            // đã lưu trữ link vào trong list
            int dem = 0;
            DsPosts_Link.ForEach(Link =>
            {
                #region Crawl

                if (Link != "")
                {
                    Posts post = new Posts();
                    CrawlTotal crawlTotal = new CrawlTotal();

                    post = crawlTotal.Crawl(Link);

                    Thread.Sleep(500);
                    DSPosts.Add(post);
                    //  mg("Add new:  " + dem);
                    dem++;

                }
                #endregion
            });
            #endregion
            // test

            #region Filter
            // test goto cho nhanh

              try
                {
                    DSPosts.RemoveAll(x =>
                          x.Title.Length < 3 || x.Text_Content.Length < 300
                              || x.Image.Length < 20 || x.Ngay != DateTime.Now.Day
                              || x.Image == null || x.Image == "");
                    //  mg("Filter Success   ");
                }
                catch
                {
                    //   mg(ex + "");

                }

           

            #endregion


            #region Lưu
            //Posts po = new Posts();
            int k = 0;
            DSPosts.ForEach(x => { x.STT = k; k++; });
            var DsUnder100 = DSPosts.Where(x => x.STT < SoVideo);
            int K_Save = 0;
            DsUnder100.ToList().ForEach(x =>
            {
                x.Handle(K_Save + "", x.Text_Content, x.Title, x.Tag, x.Image);
                Thread.Sleep(500);
                // mg(string.Format("Saving input {0}", K_Save));
                K_Save++;
            });
            #endregion

            // biến đếm tăng dần số lượng video
            int soluong = 0;
            do
            {

                #region Check
                DsError.Clear();
                // thử để check-- cần sửa cái đấu ==
                for (int i = 0; i < SoVideo; i++)
                {
                    try
                    {
                        string path = string.Format("C:\\RACC\\Data\\Video{0}\\Image\\image.jpg", i);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(path);

                        //  MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
                        int x = (int)img.Width;
                        int y = (int)img.Height;
                        if (x < 700 && y < 150)
                        {
                            DsError.Add(i);
                            //  mg(i + "error  ");
                        }
                        else
                        {
                            // mg(i + "---Ok ");
                        }
                        img.Dispose();


                    }
                    catch (Exception)
                    {

                        DsError.Add(i);
                        //  mg(i + "---error  ");
                    }


                }
                #endregion

                // lưu lại những file lỗi
                #region Lưu lại file lỗi
                if (DsError.Count == 0 || DSPosts.Count < 100)
                {
                    break;// danh sách trống là thoát
                }
                for (int i = 0; i < DsError.Count; i++)
                {
                    var DsThua = DSPosts.Where(x => x.STT > SoVideo + i + soluong && x.STT <= SoVideo + 1 + i + soluong).ToList();
                    DsThua.ForEach(x =>
                    {
                        x.Handle(DsError[i] + "", x.Text_Content, x.Time, x.Tag, x.Image);
                        Thread.Sleep(500);
                        //  mg(string.Format("Re-Saving {0} ", i));
                        soluong++;
                    });
                }

                #endregion

            } while (DsError.Count > 0);

            // đóng tab
        }
    }
}
