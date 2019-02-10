using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModulWrapper
{
    class Utilities
    {
        
        public static string YOLO_CONFIG  = "yolov3-obj.cfg";
        public static string YOLO_WEIGHTS = "yolov3-obj_8000.weights";
        public static string YOLO_NAMES   = "obj.names";
        public static int YOLO_DETECTOR_WIDTH   = 416;
        public static int YOLO_DETECTOR_HEIGHT   = 416;

        public static void debugmessage(string str)
        {
            Console.WriteLine("[YOLOWRAPPER]: " + str);
        }

        public static void showMsg(string mess, string type)
        {
            MessageBox.Show(mess,type,MessageBoxButtons.OK);
        }

    }
   
}
