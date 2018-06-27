using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    class Standardize_The_String
    {
        public static void English(int k)
        {
            string Input = File.ReadAllText(@"C:\RACC\Data\Video" + k + @"\Input.txt");
            FileStream fst = new FileStream(@"C:\RACC\Data\Video" + k + @"\Input.txt", FileMode.Create); // in ra inputupdate
            StreamWriter sw = new StreamWriter(fst, Encoding.UTF8);

            for (int i = 0; i < 100; i++)
            {
                Input = Input.Replace("\r\n\r\n", "\r\n");
                Input = Input.Replace("  ", " ");
            }
            // chia ra các câu phân cách bằng đấu chấm
            string[] Cau = Input.Split('.');
            //Có một mảng các câu đơn
            // Chia các câu đơn ra thành những đoạn nho phù hợp
            for (int i = 0; i < Cau.Length; i++)
            {
                if (Cau[i].Length > 172)
                {
                    string Cau1 = Cau[i].Substring(0, Cau[i].IndexOf(" ", Cau[i].Length / 2));
                    string Cau2 = Cau[i].Substring(Cau[i].IndexOf(" ", Cau[i].Length / 2));
                    sw.WriteLine(Cau1);
                    sw.WriteLine(Cau2);
                }

                sw.WriteLine(Cau[i]);


            }
            sw.Close();
        }

        /// <summary>
        /// Save file InputUpdate
        /// </summary>
        public static void China(int k)
        {
            FileStream fst = new FileStream(@"C:\RACC\Data\Video" + k + @"\InputUpdate.txt", FileMode.Create); // in ra inputupdate
            StreamWriter sw = new StreamWriter(fst, Encoding.UTF8);
            string[] input = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Input.txt");


            string Input = "";
            foreach (var item in input)
            {
                if (item != "")
                {
                    Input += item + ",";
                }
            }

            string[] _Input = Input.Split(',');
            bool BreakFor = false;
            // tách từng đoạn 50 word
            for (int kk = 0; kk < _Input.Length; kk++)
            {
                Input = _Input[kk];
                for (int i = 0; i < Input.Length; i++)
                {

                    do
                    {
                        BreakFor = false;
                        string _Gan = Input;
                        if (_Gan.Length > 18)
                        {
                            int j = 18;

                            _Gan = _Gan.Remove(j);
                            Input = Input.Remove(0, j);
                            sw.WriteLine(_Gan);
                            sw.WriteLine("");
                            // System.Windows.Forms.MessageBox.Show(arrText[i]);
                            if (Input.Length <= 18)
                            {
                                sw.WriteLine(Input);
                                sw.WriteLine("");

                                BreakFor = true;
                            }
                            break;

                        }
                        else
                        {
                            sw.WriteLine(_Gan);
                            sw.WriteLine("");

                            BreakFor = true;
                        }

                    } while (Input.Length > 18);
                    if (BreakFor)
                    {
                        break;
                    }
                }
            }

            sw.Close();
        }

        /// <summary>
        /// Standard Input.txt
        /// </summary>
        public static void China_Input_Edit(int k)
        {
            int max_char = 250;
            string[] input = File.ReadAllLines(@"C:\RACC\Data\Video" + k + @"\Input.txt");
            FileStream fst = new FileStream(@"C:\RACC\Data\Video" + k + @"\Input.txt", FileMode.Create); // in ra inputupdate
            StreamWriter sw = new StreamWriter(fst, Encoding.UTF8);


            string Input = "";
            foreach (var item in input)
            {
                if (item != "")
                {
                    Input += item + ",";
                }
            }


            string[] _Input = Input.Split(',');
            bool BreakFor = false;
            // tách từng đoạn 50 word
            for (int kk = 0; kk < _Input.Length; kk++)
            {
                Input = _Input[kk];
                for (int i = 0; i < Input.Length; i++)
                {

                    do
                    {
                        BreakFor = false;
                        string _Gan = Input;
                        if (_Gan.Length > max_char)
                        {
                            int j = max_char;

                            _Gan = _Gan.Remove(j);
                            Input = Input.Remove(0, j);
                            sw.WriteLine(_Gan);
                            if (Input.Length <= max_char)
                            {
                                sw.WriteLine(Input);
                                sw.WriteLine("");

                                BreakFor = true;
                            }
                            break;

                        }
                        else
                        {
                            sw.WriteLine(_Gan);
                            sw.WriteLine("");

                            BreakFor = true;
                        }

                    } while (Input.Length > max_char);
                    if (BreakFor)
                    {
                        break;
                    }
                }
            }

            sw.Close();
        }


    }
}
