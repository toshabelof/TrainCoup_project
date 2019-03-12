using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ModulWrapper
{
    //Доделаю - осталось заполнять лишь данными из врапера
    class InJson
    {
        public class MainS
        {
            public int[] TopLeft { get; set; }

            public int[] BotRight { get; set; }

        }

        public class Alternative
        {
            public int[] Center { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }
        }

        public class EachPoint
        {
            public string Comment { get; set; }

            public int[] Point { get; set; }
        }

        public class Region
        {
            public MainS main { get; set; }

            public Alternative alter { get; set; }

            public EachPoint[] epoint { get; set; }
        }

        class oldCoup
        {

            public string ImageName { get; set; }

            public Region region { get; set; }
        }

        public void CreateJsonFile()
        {
            MainS pmain = new MainS
            {
                TopLeft = new int[] { 20, 55 },
                BotRight = new int[] { 80, 65 }
            };

            Alternative palter = new Alternative
            {
                Center = new int[] { 50, 60 },
                Width = 30,
                Height = 10
            };

            EachPoint epoint1 = new EachPoint
            {
                Comment = "top-left (x;y)",
                Point = new int[] { 20, 55 }
            };

            EachPoint epoint2 = new EachPoint
            {
                Comment = "top-right (x;y)",
                Point = new int[] { 50, 55 }
            };

            Region pregion = new Region
            {
                main = pmain,
                alter = palter,
                epoint = new EachPoint[] { epoint1, epoint2 }
            };

            oldCoup o = new oldCoup
            {
                ImageName = "image1.jpg",
                region = pregion
            };

            string jsonData = JsonConvert.SerializeObject(o);
            File.WriteAllText("image1.json", JsonConvert.SerializeObject(o));

            Console.WriteLine(jsonData);

        }
    }
}
