using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetFidu
{
    class FID_3
    {
        const string _FID_NUMBER = "FID_3";

        public static string FID_REF
        {
            get
            {
                return ConfigFiles.reader(_FID_NUMBER, "FID_REF", Globals.CONFIG_FILE);
            }
        }

        public static int ROI_X
        {
            get
            {
                return Convert.ToInt32((ConfigFiles.reader(_FID_NUMBER, "ROI_X", Globals.CONFIG_FILE)));
            }
        }

        public static int ROI_Y
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(_FID_NUMBER, "ROI_Y", Globals.CONFIG_FILE));
            }
        }

        public static int ROI_Width
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(_FID_NUMBER, "ROI_Width", Globals.CONFIG_FILE));
            }
        }

        public static int ROI_Height
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(_FID_NUMBER, "ROI_Height", Globals.CONFIG_FILE));
            }
        }


        public static double SCORE
        {
            get
            {
                return Convert.ToDouble(ConfigFiles.reader(_FID_NUMBER, "SCORE", Globals.CONFIG_FILE));
            }
        }
    }
}
