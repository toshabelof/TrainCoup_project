using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;

namespace ModulWrapper
{
    class TreatmentPhotos
    {
        public string patch;
        public int countPhotos;
        public DirectoryInfo dir;
        public List<string> listPhotos = new List<string>();
        private Form1 f1;

        public TreatmentPhotos(string patch)
        {
            this.patch = patch;
            this.countPhotos = 0;
            this.dir = new DirectoryInfo(patch);
        }

        public TreatmentPhotos(string patch, Form1 f1)
        {
            this.patch = patch;
            this.countPhotos = 0;
            this.dir = new DirectoryInfo(patch);
            this.f1 = f1;
        }

        ~TreatmentPhotos()
        {
            this.patch = null;
            this.countPhotos = 0;
            this.dir = null;
            GC.Collect();
        }

        private void setPatchPotosInFile()
        {
            int count = listPhotos.Count;

            using (StreamWriter sw = new StreamWriter(@"test.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < listPhotos.Count; i++)
                {
                    if (i != listPhotos.Count - 1)
                    {
                        sw.WriteLine(listPhotos[i].ToString());
                    }
                    else
                    {
                        sw.Write(listPhotos[i].ToString());
                    }
                }
               
            }
        }

        /// <summary> Возаращает количество найденных фотографийв выбранной папке. </summary>
        public int getCountPhotos() 
        {
            foreach (var item in dir.GetFiles())
            {
                if (Path.GetExtension(item.ToString()) == ".jpg" || Path.GetExtension(item.ToString()) == ".jpeg")
                {
                    listPhotos.Add(dir.FullName + @"\" + item.ToString());
                }
            }

            setPatchPotosInFile();
            countPhotos = listPhotos.Count;
            return countPhotos;
        }

        /// <summary> Возаращает список найденных фотографий. </summary>
        public List<string> getListPhotos()
        {
            return listPhotos;
        }
    }
}
