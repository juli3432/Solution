using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoClip.Library
{
    class Parametter
    {
        // kiểu render
        public string Type { get; set; } = "china";

        public int To { get; set; } = 0;
        public int From { get; set; } = 100;

        //theme
        public string theme { get; set; } = "0,1,2,3";

        public void Serialize()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\datapara.json";

            using (StreamWriter sw = new StreamWriter(path))
            {

                Parametter para = new Parametter();


                string jsonData = JsonConvert.SerializeObject(para);

                sw.Write(jsonData);

            }
        }
        public Parametter DEserialize()
        {
            Parametter para = new Parametter();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"datapara.json";

            string jsonData = File.ReadAllText(path);

            para = JsonConvert.DeserializeObject<Parametter>(jsonData);

            return para;

        }


    }
}
