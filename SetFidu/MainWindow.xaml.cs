using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using static System.Net.Mime.MediaTypeNames;
using static Emgu.Util.Platform;
using Rectangle = System.Drawing.Rectangle;

namespace SetFidu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            _Timer1.Interval = new TimeSpan(0, 0, 0, 0, Globals.QF_INTERVAL);
            _Timer1.Tick += _Timer1_Tick;
            _Timer1.Start();


            _VideoCapture = new VideoCapture(1);
            _VideoCapture.SetCaptureProperty(CapProp.Autofocus, Globals.AUTO_FOCUS);
            _VideoCapture.SetCaptureProperty(CapProp.AutoExposure, Globals.AUTO_EXPLOSURE);
            //_VideoCapture.SetCaptureProperty(CapProp.Zoom, 27);
            _Capture = _VideoCapture as ICapture;
        }

      

        DispatcherTimer _Timer1 = new DispatcherTimer();
        Image<Bgr, byte> imgQueryFrame = null;
        Image<Bgr, byte> imgCorpROI;
        TemplateMatchingType method;

        ICapture _Capture;
        VideoCapture _VideoCapture;
        Mat _ImgMat;

        Rectangle _MatchFid_1;
        Rectangle _MatchFid_2;
        Rectangle _MatchFid_3;
        Rectangle _MatchFid_4;

        public double _ConfidenceScore { get; set; }
        System.Drawing.Bitmap Debugbitmap;

        private void _Timer1_Tick(object sender, EventArgs e)
        {
            MainFunction();
        }

        void MainFunction() 
        {
            bool _fidPass = false;

            QueryFrame();

            _fidPass = SearchFid_1();

            _fidPass = SearchFid_2();

            //_fidPass = SearchFid_3();

            //_fidPass = SearchFid_4();

            List<string> _CRDList = ConfigFiles.GetKeys("CRD_NAME");

            foreach(string _CRD in _CRDList) 
            {
                string _CRDToTest = ConfigFiles.reader("CRD_NAME", _CRD, Globals.CONFIG_FILE);

                TestingCRDs(_CRDToTest);

               
            }
        }


        void QueryFrame() 
        {      
            try
            { 
                _ImgMat = _Capture.QueryFrame();
                imgQueryFrame = _Capture.QueryFrame().ToImage<Bgr, Byte>();
                Debugbitmap = imgQueryFrame.ToBitmap();

                if(Globals.DEBUG_QF_ENABLE) Debugbitmap.Save(Globals.DEBUG_QUERY_FRAME);

            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            };
        }

        bool SearchFid_1() 
        {
            Rectangle ROI = new Rectangle(FID_1.ROI_X, FID_1.ROI_Y, FID_1.ROI_Width, FID_1.ROI_Height);

            imgQueryFrame.Draw(ROI, new Bgr(0, 255, byte.MaxValue), 1, LineType.AntiAlias, 0);
         
            imgCorpROI = null;
            imgCorpROI = imgQueryFrame.Copy();
            imgCorpROI.ROI = ROI;

            Image<Bgr, byte> _TemplateImg = new Image<Bgr, byte>(FID_1.FID_REF);

            using (Image<Gray, float> result = imgCorpROI.MatchTemplate(_TemplateImg, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                System.Drawing.Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                _ConfidenceScore = maxValues[0];
                lblFindMatchScore.Content = "Find Match Score: " + _ConfidenceScore;

                if (maxValues[0] > FID_1.SCORE)
                {
                    Rectangle match = new Rectangle(maxLocations[0], _TemplateImg.Size);

                    match.X = match.X + FID_1.ROI_X; 
                    match.Y = match.Y + FID_1.ROI_Y; 

                    _MatchFid_1 = match;

                    imgQueryFrame.Draw(match, new Bgr(0, 50, (double)byte.MaxValue), 1);
                    CvInvoke.PutText(imgQueryFrame, "FID_1", new System.Drawing.Point(match.X, match.Y), FontFace.HersheySimplex, .4, new Rgb(0, 0, 0).MCvScalar, 1);
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    lblFindMatchScore.Content = "Find Match Score: " + maxValues[0].ToString();
                    return true;
                }
                else
                {
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    return false;
                }
            }
        }

        bool SearchFid_2()
        {
            Rectangle ROI = new Rectangle(FID_2.ROI_X, FID_2.ROI_Y, FID_2.ROI_Width, FID_2.ROI_Height);

            imgQueryFrame.Draw(ROI, new Bgr(0, 255, byte.MaxValue), 1, LineType.AntiAlias, 0);
            

            imgCorpROI = null;
            imgCorpROI = imgQueryFrame.Copy();
            imgCorpROI.ROI = ROI;

            Image<Bgr, byte> _TemplateImg = new Image<Bgr, byte>(FID_2.FID_REF);

            using (Image<Gray, float> result = imgCorpROI.MatchTemplate(_TemplateImg, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                System.Drawing.Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                _ConfidenceScore = maxValues[0];
                lblFindMatchScore.Content = "Find Match Score: " + _ConfidenceScore;

                if (maxValues[0] > FID_2.SCORE)
                {
                    Rectangle match = new Rectangle(maxLocations[0], _TemplateImg.Size);

                    match.X = match.X + FID_2.ROI_X;
                    match.Y = match.Y + FID_2.ROI_Y;
                    
                    _MatchFid_2 = match;

                    imgQueryFrame.Draw(match, new Bgr(0, 50, (double)byte.MaxValue), 1);
                    CvInvoke.PutText(imgQueryFrame, "FID_2", new System.Drawing.Point(match.X, match.Y), FontFace.HersheySimplex, .4, new Rgb(0, 0, 0).MCvScalar, 1);
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    lblFindMatchScore.Content = "Find Match Score: " + maxValues[0].ToString();
                    return true;
                }
                else
                {
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    return false;
                }
            }
        }

        bool SearchFid_3()
        {
            Rectangle ROI = new Rectangle(FID_3.ROI_X, FID_3.ROI_Y, FID_3.ROI_Width, FID_3.ROI_Height);

            imgQueryFrame.Draw(ROI, new Bgr(0, 255, byte.MaxValue), 1, LineType.AntiAlias, 0);
            

            imgCorpROI = null;
            imgCorpROI = imgQueryFrame.Copy();
            imgCorpROI.ROI = ROI;

            Image<Bgr, byte> _TemplateImg = new Image<Bgr, byte>(FID_3.FID_REF);

            using (Image<Gray, float> result = imgCorpROI.MatchTemplate(_TemplateImg, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                System.Drawing.Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                _ConfidenceScore = maxValues[0];
                lblFindMatchScore.Content = "Find Match Score: " + _ConfidenceScore;

                if (maxValues[0] > FID_3.SCORE)
                {
                    Rectangle match = new Rectangle(maxLocations[0], _TemplateImg.Size);

                    match.X = match.X + FID_3.ROI_X;
                    match.Y = match.Y + FID_3.ROI_Y;
                    
                    _MatchFid_3 = match;

                    imgQueryFrame.Draw(match, new Bgr(0, 50, (double)byte.MaxValue), 1);
                    CvInvoke.PutText(imgQueryFrame, "FID_3", new System.Drawing.Point(match.X, match.Y), FontFace.HersheySimplex, .4, new Rgb(0, 0, 0).MCvScalar, 1);
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    lblFindMatchScore.Content = "Find Match Score: " + maxValues[0].ToString();
                    return true;
                }
                else
                {
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    return false;
                }
            }
        }

        bool SearchFid_4()
        {
            Rectangle ROI = new Rectangle(FID_4.ROI_X, FID_4.ROI_Y, FID_4.ROI_Width, FID_4.ROI_Height);
           
            imgQueryFrame.Draw(ROI, new Bgr(0, 255, byte.MaxValue), 1, LineType.AntiAlias, 0);

            imgCorpROI = null;
            imgCorpROI = imgQueryFrame.Copy();
            imgCorpROI.ROI = ROI;

            Image<Bgr, byte> _TemplateImg = new Image<Bgr, byte>(FID_4.FID_REF);

            using (Image<Gray, float> result = imgCorpROI.MatchTemplate(_TemplateImg, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                System.Drawing.Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                _ConfidenceScore = maxValues[0];
                lblFindMatchScore.Content = "Find Match Score: " + _ConfidenceScore;

                if (maxValues[0] > FID_4.SCORE)
                {
                    Rectangle match = new Rectangle(maxLocations[0], _TemplateImg.Size);

                    match.X = match.X + FID_4.ROI_X;
                    match.Y = match.Y + FID_4.ROI_Y;

                    _MatchFid_4 = match;

                    imgQueryFrame.Draw(match, new Bgr(0, 50, (double)byte.MaxValue), 1);
                    CvInvoke.PutText(imgQueryFrame, "FID_4", new System.Drawing.Point(match.X, match.Y), FontFace.HersheySimplex, .4, new Rgb(0, 0, 0).MCvScalar, 1);
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    lblFindMatchScore.Content = "Find Match Score: " + maxValues[0].ToString();
                    return true;
                }
                else
                {
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    return false;
                }
            }
        }


        void TestingCRDs(string _CRDToTest) 
        {
            Globals.CRD_NAME = _CRDToTest;

            int _offSetX = CRD.ROI_X + _MatchFid_1.X;
            int _offSetY = CRD.ROI_Y - _MatchFid_2.Y;

            Rectangle ROI = new Rectangle(CRD.ROI_X - _MatchFid_1.X, CRD.ROI_Y + _MatchFid_2.Y, CRD.ROI_Width, CRD.ROI_Height);

            imgQueryFrame.Draw(ROI, new Bgr(0, 255, 0), 1, LineType.AntiAlias, 0);
            CvInvoke.PutText(imgQueryFrame, Globals.CRD_NAME, new System.Drawing.Point(ROI.X, ROI.Y), FontFace.HersheySimplex, .3, new Rgb(255, 0, 255).MCvScalar, 1);

            imgCorpROI = null;
            imgCorpROI = imgQueryFrame.Copy();
            imgCorpROI.ROI = ROI;

            PicBox.Image = imgQueryFrame.ToBitmap();
        }






        private PointF[] GetPoints(RectangleF rectangle)
        {
            return new PointF[3]
            {
            new PointF(rectangle.Left, rectangle.Top),
            new PointF(rectangle.Right, rectangle.Top),
            new PointF(rectangle.Left, rectangle.Bottom)
            };
        }

        void DrawPCBEdges() 
        {
            int ROI_X = _MatchFid_1.X;
            int ROI_Y = _MatchFid_2.Y;

            //CvInvoke.Polylines(imgQueryFrame, , true, new MCvScalar(0, 0, 255), 5);

            System.Drawing.Point[] _pointToROI = new System.Drawing.Point[3];

            _pointToROI[0].X = _MatchFid_1.X;
            _pointToROI[0].Y = _MatchFid_1.Y;

            _pointToROI[1].X = _MatchFid_2.X;
            _pointToROI[1].Y = _MatchFid_2.Y;

            _pointToROI[2].X = _MatchFid_3.X;
            _pointToROI[2].Y = _MatchFid_3.Y;

            _pointToROI[3].X = _MatchFid_4.X;
            _pointToROI[3].Y = _MatchFid_4.Y;
            

            //List<Point2f> output = new List<Point2f>();

            //Rectangle rect = CvInvoke.BoundingRectangle();

            CvInvoke.Line(imgQueryFrame, new System.Drawing.Point(_MatchFid_1.X, _MatchFid_1.Y), new System.Drawing.Point(_MatchFid_2.X, _MatchFid_2.Y), new MCvScalar(0, 255, 0), 5, LineType.AntiAlias, 0);

            CvInvoke.Line(imgQueryFrame, new System.Drawing.Point(_MatchFid_2.X, _MatchFid_2.Y), new System.Drawing.Point(_MatchFid_3.X, _MatchFid_3.Y), new MCvScalar(0, 255, 0), 5, LineType.AntiAlias, 0);

            CvInvoke.Line(imgQueryFrame, new System.Drawing.Point(_MatchFid_3.X, _MatchFid_3.Y), new System.Drawing.Point(_MatchFid_4.X, _MatchFid_4.Y), new MCvScalar(0, 255, 0), 5, LineType.AntiAlias, 0);

            CvInvoke.Line(imgQueryFrame, new System.Drawing.Point(_MatchFid_4.X, _MatchFid_4.Y), new System.Drawing.Point(_MatchFid_1.X, _MatchFid_1.Y), new MCvScalar(0, 255, 0), 5, LineType.AntiAlias, 0);

            PicBox.Image = imgQueryFrame.ToBitmap();
            //Rectangle PCBEdges = new Rectangle(ROI_X, ROI_Y, ROI_Width, ROI_Height);

            //imgQueryFrame.Draw(ROI, new Bgr(0, 255, byte.MaxValue), 2, LineType.AntiAlias, 0);

            //imgCorpROI = null;
            //imgCorpROI = imgQueryFrame.Copy();
            //imgCorpROI.ROI = ROI;
        }

        private void btnTry_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            int xCoordinate = e.X;
            int yCoordinate = e.Y;

            lblXY.Content = "X:" + xCoordinate + " Y:" + yCoordinate;
        }

        void FindMatchTemplate(Image<Bgr, byte> QueryFrame, Image<Bgr, byte> _RefImg)
        {
            Image<Bgr, byte> _TemplateImg = new Image<Bgr, byte>(@"C:\Image\fid.bmp");

            using (Image<Gray, float> result = imgCorpROI.MatchTemplate(_TemplateImg, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                System.Drawing.Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                _ConfidenceScore = maxValues[0];
                lblFindMatchScore.Content = "Find Match Score: " + _ConfidenceScore;

                if (maxValues[0] > .9)
                {
                    Rectangle match = new Rectangle(maxLocations[0], _TemplateImg.Size);

                    match.X = match.X + 20;
                    match.Y = match.Y + 50;


                    imgQueryFrame.Draw(match, new Bgr(0, 255, (double)byte.MaxValue), 3);
                    PicBox.Image = imgQueryFrame.ToBitmap();
                    lblFindMatchScore.Content = "Find Match Score: " + maxValues[0].ToString();
                    //_CountConfidence++;
                    //lblConfidence.Text = "Confidence: " + _CountConfidence.ToString();
                }
                else
                {
                    PicBox.Image = imgCorpROI.ToBitmap();
                    //_CountConfidence = 0;
                    //lblConfidence.Text = "Confidence: " + _CountConfidence.ToString();
                }
            }



            _VideoCapture.Dispose();
        }
    }
}
