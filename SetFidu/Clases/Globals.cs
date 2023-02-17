﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SetFidu
{
    class Globals
    {
        #region VARIABLES CONFIGURATION
        public static string CONFIG_FILE
        {
            get
            {
                return @"C:\Images\Config.INI";
            }
        }

        public static int CAMERA
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("CAMERA_SETTINGS", "CAMERA", CONFIG_FILE));
            }
        }

        public static int AUTO_FOCUS
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("CAMERA_SETTINGS", "AUTO_FOCUS", CONFIG_FILE));
            }
        }

        public static int AUTO_EXPLOSURE
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("CAMERA_SETTINGS", "AUTO_EXPLOSURE", CONFIG_FILE));
            }
        }

        public static int QF_INTERVAL
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("CAMERA_SETTINGS", "QF_INTERVAL", CONFIG_FILE));
            }
        }


        public static string DEBUG_QUERY_FRAME
        {
            get
            {
                return ConfigFiles.reader("DEUBG", "DEBUG_QUERY_FRAME", CONFIG_FILE);
            }
        }

        public static bool DEBUG_QF_ENABLE
        {
            get
            {
                return Convert.ToBoolean(ConfigFiles.reader("DEUBG", "DEBUG_QF_ENABLE", CONFIG_FILE));
            }
        }

        private static string _CRDName = string.Empty;
        public static string CRD_NAME
        {
            get
            {
                return _CRDName;
            }
            set
            {
               
                _CRDName = value;
            }
        }





        public static string SINGLE_SCANNER
        {
            get
            {
                return ConfigFiles.reader("KEYENCE", "SINGLE_SCANNER", CONFIG_FILE);
            }
        }

        public static int BAUD_RATE
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("KEYENCE", "BAUDRATE", CONFIG_FILE));
            }
        }

        public static int DATA_BITS
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("KEYENCE", "DATABITS", CONFIG_FILE));
            }
        }


        private static bool _isActiveLane1 = false;
        public static bool IS_ACTIVE_LANE_1
        {
            get
            {
                return Convert.ToBoolean(ConfigFiles.reader("GOOD BAD CONVEYOR", "ALANE1", CONFIG_FILE));
            }
            set
            {
                ConfigFiles.write("GOOD BAD CONVEYOR", "ALANE1", value.ToString(), CONFIG_FILE);
                _isActiveLane1 = value;
            }
        }

        private static bool _isActiveLane2 = false;
        public static bool IS_ACTIVE_LANE_2
        {
            get
            {
                return Convert.ToBoolean(ConfigFiles.reader("GOOD BAD CONVEYOR", "ALANE2", CONFIG_FILE));
            }
            set
            {
                ConfigFiles.write("GOOD BAD CONVEYOR", "ALANE2", value.ToString(), CONFIG_FILE);
                _isActiveLane2 = value;
            }
        }

        public static int SN_LENGH
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("GOOD BAD CONVEYOR", "SN_LENGH", CONFIG_FILE));
            }
        }
        public static int SMOTHER_LENGH
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("GOOD BAD CONVEYOR", "SMOTHER_LENGH", CONFIG_FILE));
            }
        }

        public static string DAQ_NAME
        {
            get
            {
                return ConfigFiles.reader("GOOD BAD CONVEYOR", "DAQ_NAME", CONFIG_FILE);
            }
        }

        public static bool IS_SHUTTLE
        {
            get
            {
                return Convert.ToBoolean(ConfigFiles.reader("GOOD BAD CONVEYOR", "IS_SHUTTLE", CONFIG_FILE));
            }
        }


        #endregion

        #region VARIABLES LAYOUT AND DESIGN APP

        private static bool _DOCK_MENU = true;
        public static bool DOCK_MENU
        {
            get
            {
                return _DOCK_MENU;
            }
            set
            {
                _DOCK_MENU = value;
            }
        }

        #endregion

        #region VARIABLES DAQ CONTRO

        private static bool[] outputs = new bool[8];
        public static bool[] DAQ_OUT_PUTS
        {
            get
            {
                return outputs;
            }
            set
            {
                outputs = value;
            }
        }

        private static bool _DaqOK = true;
        public static bool DAQ_OK
        {
            get
            {
                return _DaqOK;
            }
            set
            {
                _DaqOK = value;
            }
        }

        private static bool _PassThrue = false;
        public static bool PASS_THRUE
        {
            get
            {
                return _PassThrue;
            }
            set
            {
                _PassThrue = value;
            }
        }



        #endregion

        #region Variables MES INFORMATION

        private static string _SERIAL_NUMBER1;
        private static string _SERIAL_NUMBER2;
        public static string SERIAL_NUMBER1
        {
            get
            {
                return _SERIAL_NUMBER1;
            }
            set
            {
                _SERIAL_NUMBER1 = value;
            }
        }

        public static string SERIAL_NUMBER2
        {
            get
            {
                return _SERIAL_NUMBER2;
            }
            set
            {
                _SERIAL_NUMBER2 = value;
            }
        }

        private static int _CUSTOMER_ID;
        public static int CUSTOMER_ID
        {
            get
            {
                return _CUSTOMER_ID;
            }
            set
            {
                _CUSTOMER_ID = value;
            }
        }

        private static int _WIP_ID;
        public static int WIP_ID
        {
            get
            {
                return _WIP_ID;
            }
            set
            {
                _WIP_ID = value;
            }
        }

        public static List<String> DATA_MATRIX
        {
            get
            {
                return ConfigFiles.GetKeys("DATA_MATRIX");
            }
        }

        private static string _STEPS_MISSING1;
        private static string _STEPS_MISSING2;
        public static string STEPS_MISSING1
        {
            get
            {
                return _STEPS_MISSING1;
            }
            set
            {
                _STEPS_MISSING1 = value;
            }
        }

        public static string STEPS_MISSING2
        {
            get
            {
                return _STEPS_MISSING2;
            }
            set
            {
                _STEPS_MISSING2 = value;
            }
        }

        private static int _COUNT_MATRIX1;
        private static int _COUNT_MATRIX2;
        public static int COUNT_MATRIX1
        {
            get
            {
                return _COUNT_MATRIX1;
            }
            set
            {
                _COUNT_MATRIX1 = value;
            }
        }

        
        public static int COUNT_MATRIX2
        {
            get
            {
                return _COUNT_MATRIX2;
            }
            set
            {
                _COUNT_MATRIX2 = value;
            }
        }


        public static string STEP_TO_CHECK
        {
            get
            {
                return ConfigFiles.reader("CHECK PROCESS", "STEP_TO_CHECK", CONFIG_FILE);
            }
        }

        public static string CHECK_PREVIOUS
        {
            get             
            {               
                return ConfigFiles.reader("CHECK PROCESS", "CHECK_PREVIOUS", CONFIG_FILE).ToUpper();
            }
        }

        private static DataTable _DT_LANE1;
        private static DataTable _DT_LANE2;
        public static DataTable DT_LANE1
        {
            get
            {
                return _DT_LANE1;
            }
            set
            {
                _DT_LANE1 = value;
            }
        }
        public static DataTable DT_LANE2
        {
            get
            {
                return _DT_LANE2;
            }
            set
            {
                _DT_LANE2 = value;
            }
        }


        private static bool _DebugMES = false;
        public static bool DEBUG_MES
        {
            get
            {
                return _DebugMES;
            }
            set
            {
                _DebugMES = value;
            }
        }


        private static string _Shuttle_Lane_Active;
        public static string _SHUTTLE_LANE_ACTIVE
        {
            get
            {
                return _Shuttle_Lane_Active;
            }
            set
            {
                _Shuttle_Lane_Active = value;
            }
        }


        #endregion

        #region VARIABLES FOR REJECTION ALARM

        private static int _COUNT_RETRY1;
        private static int _COUNT_RETRY2;
        private static int _COUNT_RETRY_SCANNING1;
        private static int _COUNT_RETRY_SCANNING2;
        public static int COUNT_RETRY1
        {
            get
            {
                return _COUNT_RETRY1;
            }
            set
            {
                _COUNT_RETRY1 = value;
            }
        }
        public static int COUNT_RETRY2
        {
            get
            {
                return _COUNT_RETRY2;
            }
            set
            {
                _COUNT_RETRY2 = value;
            }
        }

        public static int COUNT_RETRY_SCANNING1
        {
            get
            {
                return _COUNT_RETRY_SCANNING1;
            }
            set
            {
                _COUNT_RETRY_SCANNING1 = value;
            }
        }
        public static int COUNT_RETRY_SCANNING2
        {
            get
            {
                return _COUNT_RETRY_SCANNING2;
            }
            set
            {
                _COUNT_RETRY_SCANNING2 = value;
            }
        }


        public static int RETRIES_CHECKPROCESS
        {
            get 
            {
                return Convert.ToInt32(ConfigFiles.reader("CHECK PROCESS", "RETRIES_CHECKPROCESS", Globals.CONFIG_FILE)); 
            }
        }
        public static int RETRIES_SCANNING
        {
            get
            {
                return Convert.ToInt32(ConfigFiles.reader("CHECK PROCESS", "RETRIES_SCANNING", Globals.CONFIG_FILE));
            }
        }

        #endregion
    }
}
