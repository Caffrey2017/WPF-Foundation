using Emgu.CV;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WPF_ImageProcessing
{
    /// <summary>
    /// OpenCVWindows.xaml 的互動邏輯
    /// </summary>
    public partial class OpenCVWindows : Window
    {
        DispatcherTimer timer;
        Capture capture;
        HaarCascade haarCascade;
        public OpenCVWindows()
        {
            InitializeComponent();
            capture = new Capture();
            haarCascade = new HaarCascade(Environment.CurrentDirectory + "\\faceXML\\haarcascade_frontalface_default.xml");
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();

                var detectedFaces = grayFrame.DetectHaarCascade(haarCascade)[0];

                foreach (var face in detectedFaces)
                    currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
                var image = new System.Windows.Controls.Image();
                image.Source = ToBitmapSource(currentFrame);
                image.Stretch = Stretch.Fill;
                image.StretchDirection = StretchDirection.DownOnly;
                canvas.Children.Add(image);
                this.Title = string.Format("{0:F} x {1:F}", canvas.ActualWidth, canvas.ActualHeight);
            }
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop
                  .Imaging.CreateBitmapSourceFromHBitmap(
                  ptr,
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}
