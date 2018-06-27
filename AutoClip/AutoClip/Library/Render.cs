using AutoClip.ListManhua;
using AutoClip.Render_Type;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClip.Library
{
    public class Render
    {
        private Render() { }

        static private volatile Render instance;

        static object key = new object();

        public static Render Instance
        {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new Render();
                    }
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        internal void Start(Parametter para)
        {

            //Parametter para = new Parametter();
            //para.DEserialize();
            if (para.Type.Equals("china"))
            {
                RenderChina.Start(para.To, para.From);
            }
            if (para.Type.Equals("span"))
            {
                RenderSpan.Start(para.To, para.From);
            }
            if (para.Type.Equals("arabic"))
            {
                RenderImage.Start(para.To, para.From, para.Type);
            }


        }

    }



}


