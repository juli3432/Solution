using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    public static class Extension
    {
        public static string ConvertHTML(this string Title)
        {
            Title = Title.Replace("&rdquo;", "\"");
            Title = Title.Replace("&ldquo;", "\"");
            Title = Title.Replace("&nbsp;", "");
            Title = Title.Replace("&rsquo;", "\"");
            Title = Title.Replace("&lsquo;", "\"");
            Title = Title.Replace("&quot;", "\"");
            return Title;
        }
        public static int ConvertTimechina(this string Time)
        {
            int Ngay = 0;
            Regex reg1 = new Regex(@"\d{4}\W\d{2}\W\d{2}\W\d{2}\W\d{2}");
            Match chuoi = reg1.Match(Time);
            Time = chuoi.ToString();
            string pattern = @"\d{2}";
            Regex reg = new Regex(pattern);
            MatchCollection m = reg.Matches(Time);
            string ngay = "";
            foreach (var item in m)
            {
                ngay += item.ToString();
            }
            return Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
        }

        public static int ConvertTimechina_noMinute(this string Time)
        {
            int Ngay = 0;
            Regex reg1 = new Regex(@"\d{4}..\d{2}..\d{2}");
            Match chuoi = reg1.Match(Time);
            Time = chuoi.ToString();
            string pattern = @"\d{2}";
            Regex reg = new Regex(pattern);
            MatchCollection m = reg.Matches(Time);
            string ngay = "";
            foreach (var item in m)
            {
                ngay += item.ToString();
            }
            return Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
        }

        public static int ConvertTimeSpan(this string Time)
        {
            int Ngay = 0;
            Regex reg1 = new Regex(@"\d{4}.\d{2}.\d{2}");
            Match chuoi = reg1.Match(Time);
            Time = chuoi.ToString();
            string pattern = @"\d{2}";
            Regex reg = new Regex(pattern);
            MatchCollection m = reg.Matches(Time);
            string ngay = "";
            foreach (var item in m)
            {
                ngay += item.ToString();
            }
            return Ngay = int.Parse(ngay[6].ToString() + ngay[7].ToString());
        }

    }
}
