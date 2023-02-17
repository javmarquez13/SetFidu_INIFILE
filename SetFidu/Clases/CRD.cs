using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetFidu
{
    class CRD
    {
        public static string IMG_REF
        {
            get
            {
                return ConfigFiles.reader(Globals.CRD_NAME, "IMG_REF", Globals.CONFIG_FILE);
            }
        }

        public static int ROI_X
        {
            get
            {
                return Convert.ToInt32((ConfigFiles.reader(Globals.CRD_NAME, "ROI_X", Globals.CONFIG_FILE)));
            }
        }

        public static int ROI_Y
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(Globals.CRD_NAME, "ROI_Y", Globals.CONFIG_FILE));
            }
        }

        public static int ROI_Width
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(Globals.CRD_NAME, "ROI_Width", Globals.CONFIG_FILE));
            }
        }

        public static int ROI_Height
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader(Globals.CRD_NAME, "ROI_Height", Globals.CONFIG_FILE));
            }
        }

        public static string ALGORITHM_TYPE
        {
            get
            {
                return ConfigFiles.reader(Globals.CRD_NAME, "ALGORITHM_TYPE", Globals.CONFIG_FILE);
            }
        }

        public static double UPPER_LIMIT
        {
            get
            {
                return Convert.ToDouble(ConfigFiles.reader(Globals.CRD_NAME, "UPPER_LIMIT", Globals.CONFIG_FILE));
            }
        }

        public static double LOWER_LIMIT
        {
            get
            {
                return Convert.ToDouble(ConfigFiles.reader(Globals.CRD_NAME, "LOWER_LIMIT", Globals.CONFIG_FILE));
            }
        }

        public static double SCORE
        {
            get
            {
                return Convert.ToDouble(ConfigFiles.reader(Globals.CRD_NAME, "SCORE", Globals.CONFIG_FILE));
            }
        }
    }
}
