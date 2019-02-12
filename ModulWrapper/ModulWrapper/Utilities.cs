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
        
        public static string YOLO_CONFIG  = "cfg/yolov2-obj.cfg";
        public static string YOLO_WEIGHTS = "cfg/yolov2-obj_last.weights";
        public static string YOLO_NAMES   = "cfg/obj.names";
        public static int YOLO_DETECTOR_WIDTH   = 416;
        public static int YOLO_DETECTOR_HEIGHT   = 416;
        public static int picBoxW, picBoxH;
        public static int picBoxSmallW, picBoxSmallH;


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
