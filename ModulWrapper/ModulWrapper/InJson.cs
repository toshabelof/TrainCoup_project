using System;
using Newtonsoft.Json;
using System.IO;

namespace ModulWrapper
{
    //Доделаю - осталось заполнять лишь данными из врапера
    class InJson
    {
        string name;
        float[] center;
        float width;
        float height;
        string path;

        public InJson(string n, float[] c, float w, float h, string p)
        {
            this.name = Path.GetFileNameWithoutExtension(n); //берем только имя от всего пути 
            this.center = c;
            this.width = w;
            this.height = h;
            this.path = p;
        }

        public class MainS
        {
            public float[] TopLeft { get; set; }

            public float[] BotRight { get; set; }
        }

        public class Alternative
        {
            public float[] Center { get; set; }

            public float Width { get; set; }

            public float Height { get; set; }
        }

        public class EachPoint
        {
            public string Comment { get; set; }

            public float[] Point { get; set; }
        }

        public class Region
        {
            public MainS Main { get; set; }

            public Alternative Alternative { get; set; }

            public EachPoint[] EachPoint { get; set; }
        }

        class oldCoup
        {
            public string ImageName { get; set; }

            public Region Region { get; set; }
        }

        public void CreateJsonFile()
        {
            MainS pmain = new MainS
            {
                TopLeft  = new float[] { center[0] - width / 2, center[1] - height / 2 },
                BotRight = new float[] { center[0] + width / 2, center[1] + height / 2 }
            };

            Alternative palter = new Alternative
            {
                Center = center,
                Width = width,
                Height = height
            };

            EachPoint epoint1 = new EachPoint
            {
                Comment = "top-left, (x;y)",
                Point = new float[] { center[0] - width / 2, center[1] - height / 2 }
            };

            EachPoint epoint2 = new EachPoint
            {
                Comment = "top-right, (x;y)",
                Point = new float[] { center[0] + width / 2, center[1] - height / 2 }
            };

            EachPoint epoint3 = new EachPoint
            {
                Comment = "bottom-left, (x;y)",
                Point = new float[] { center[0] - width / 2, center[1] + height / 2 }
            };

            EachPoint epoint4 = new EachPoint
            {
                Comment = "bottom-right, (x;y)",
                Point = new float[] { center[0] + width / 2, center[1] + height / 2 }
            };

            Region pregion = new Region
            {
                Main = pmain,
                Alternative = palter,
                EachPoint = new EachPoint[] { epoint1, epoint2, epoint4, epoint3 }
            };

            oldCoup o = new oldCoup
            {
                ImageName = name,
                Region = pregion
            };

            string jsonData = JsonConvert.SerializeObject(o);
            File.WriteAllText(path + '/' + name + ".json", JsonConvert.SerializeObject(o));

        }
    }
}