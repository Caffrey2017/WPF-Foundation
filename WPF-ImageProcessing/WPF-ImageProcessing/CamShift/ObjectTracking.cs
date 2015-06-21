using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;


namespace WPF_ImageProcessing.CamShift
{
    public class ObjectTracking
    {
        public Image<Hsv, Byte> hsv;
        public Image<Gray, Byte> hue;
        public Image<Gray, Byte> mask;
        public Image<Gray, Byte> backproject;
        public DenseHistogram hist;
        private Rectangle trackingWindow;
        private MCvConnectedComp trackcomp;
        private MCvBox2D trackbox;

        //1. 畫面上選取要追蹤的區域這裡我稱為 物件 (Object)，也就是要被追蹤的東東
        //2. 計算出物件的 直方圖 (Histogram)
        //3. 將物件的直方圖與輸入畫面進行 反向投影(Backproject) 計算
        //4. 透過 物件追蹤演算法(Camshift) 快速地框出反向投影計算出的結果
        //5. 回傳追蹤區域
        //於是一直重複 3~5 的過程，我們的物件追蹤程式就大功告成了！

        public ObjectTracking(Image<Bgr, Byte> image, Rectangle ROI)
        {
            // Initialize parameters

            //A boundary box
            trackbox = new MCvBox2D();
            //連通物件
            trackcomp = new MCvConnectedComp();
            //Hue圖?
            hue = new Image<Gray, byte>(image.Width, image.Height);
            //*待理解
            hue._EqualizeHist();
            //遮罩
            mask = new Image<Gray, byte>(image.Width, image.Height);
            //直方圖
            hist = new DenseHistogram(30, new RangeF(0, 180));
            //一張普通的灰階圖
            backproject = new Image<Gray, byte>(image.Width, image.Height);

            // Assign Object's ROI from source image.
            // Region of Interest 只針對特定區域進行處理
            trackingWindow = ROI;

            // Producing Object's hist
            //產生物件直方圖
            CalObjectHist(image);
        }

        private void CalObjectHist(Image<Bgr, Byte> image)
        {
            //更新Hue影像
            UpdateHue(image);

            // Set tracking object's ROI
            // 設定欲追蹤物件的區域
            hue.ROI = trackingWindow;
            mask.ROI = trackingWindow;
            // 直方圖計算追蹤區域
            hist.Calculate(new Image<Gray, Byte>[] { hue }, false, mask);

            // Scale Historgram
            float max = 0, min = 0, scale = 0;
            int[] minLocations, maxLocations;
            hist.MinMax(out min, out max, out minLocations, out maxLocations);
            if (max != 0)
            {
                scale = 255 / max;
            }
            //http://www.opencv.org.cn/forum.php?mod=viewthread&tid=2702
            CvInvoke.cvConvertScale(hist.MCvHistogram.bins, hist.MCvHistogram.bins, scale, 0);

            // Clear ROI
            hue.ROI = System.Drawing.Rectangle.Empty;
            mask.ROI = System.Drawing.Rectangle.Empty;

            // Now we have Object's Histogram, called hist.
        }


        //http://www.cnblogs.com/steven-blog/p/3351834.html
        private void UpdateHue(Image<Bgr, Byte> image)
        {
            // release previous image memory
            if (hsv != null) hsv.Dispose();
            hsv = image.Convert<Hsv, Byte>();

            // Drop low saturation pixels
            mask = hsv.Split()[1].ThresholdBinary(new Gray(60), new Gray(255));
            //CvInRangeS 函數的功能是檢查輸入陣列每個元素大小是否在2個給定數值之間，
            //可以有多通道,mask保存0通道的最小值，也就是h分量；這裡利用了hsv的3個通道，
            //比較h,0~180,s,smin~256,v,min(_vmin,_vmax),max(_vmin,_vmax)。如果3個通道都在對應的範圍內，
            //則mask對應的那個點的值全為1(0xff)，否則為0(0x00).
            //CvScalar 存放四個像素點
            /*CvInvoke.cvInRangeS(hsv, new MCvScalar(0, 30, Math.Min(10, 255), 0),
                new MCvScalar(180, 256, Math.Max(10, 255), 0), mask);*/

            CvInvoke.cvInRangeS(hsv, new MCvScalar(0, 30, Math.Min(10, 255), 0),
            new MCvScalar(180, 256, Math.Max(10, 255), 0), mask);

            // Get Hue (gray image)
            hue = hsv.Split()[0];
        }

        public Rectangle Tracking(Image<Bgr, Byte> image)
        {
            //更新Hue畫面
            UpdateHue(image);

            // Calucate BackProject 計算反投影
            backproject = hist.BackProject(new Image<Gray, Byte>[] { hue });

            // Apply mask  遮罩
            backproject._And(mask);

            // Tracking windows empty means camshift lost bounding-box last time
            // here we give camshift a new start window from 0,0 (you could change it)
            if (trackingWindow.IsEmpty || trackingWindow.Width == 0 || trackingWindow.Height == 0)
            {
                trackingWindow = new Rectangle(0, 0, 100, 100);
            }
            CvInvoke.cvCamShift(backproject, trackingWindow,
                new MCvTermCriteria(10, 1.1), out trackcomp, out trackbox);
            /*CvInvoke.cvMeanShift(backproject, trackingWindow,
                new MCvTermCriteria(10, 1.1), out trackcomp);*/

            // update tracking window
            trackingWindow = trackcomp.rect;

            return trackingWindow;
        }
    }
}
