using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Reflection;

namespace WPF_ImageProcessing
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog open;
        BitmapImage bitmap;
        WriteableBitmap wBitmap;

        MergeForm mergeForm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*open = new OpenFileDialog();
            open.ShowDialog();*/
            string location = AppDomain.CurrentDomain.BaseDirectory + "/images/test.jpg"; //open.FileName;
            bitmap = new BitmapImage(new Uri(location));
            wBitmap = new WriteableBitmap(bitmap);
            displayImage.Source = bitmap;
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            open.ShowDialog();
            string location = open.FileName;
            if (!File.Exists(location))
                return;
            bitmap = new BitmapImage(new Uri(location));
            wBitmap = new WriteableBitmap(bitmap);
            displayImage.Source = bitmap;
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {

        }

        private void GrayImage(object sender, RoutedEventArgs e)
        {
            var width = wBitmap.PixelWidth; //Picture Pixel Width [500] * 500
            var height = wBitmap.PixelHeight; //Picture Pixel Height 500 * [500]
            var bytesPerPixel = ((wBitmap.Format.BitsPerPixel + 7) / 8);
            var stride = width * ((wBitmap.Format.BitsPerPixel + 7) / 8); //Picture Pixel 4 bytes = (32 + 7)/8  -> width * bytes = stride = one horizontal line bytes
            var bitmapData = new byte[height * stride];
            wBitmap.CopyPixels(bitmapData, stride, 0);

            byte[] pixel = bitmapData;

            //wBitmap.CopyPixels(pixel, stride, 0);

            Parallel.For(0, height, y =>
            {
                int x;
                /*for (x = 0; x < stride; x++)
                {
                    pixel[x + y * stride] = (byte)(255 - pixel[x + y * stride]);
                }*/
                for (x = 0; x < stride; x += bytesPerPixel)
                {
                    //contrast
                    /*pixel[x + y * stride] = (byte)(255 - pixel[x + y * stride]);
                    pixel[x + y * stride + 1] = (byte)(255 - pixel[x + y * stride + 1]);
                    pixel[x + y * stride + 2] = (byte)(255 - pixel[x + y * stride + 2]);
                    pixel[x + y * stride + 3] = (byte)(255 - pixel[x + y * stride + 3]);*/
                    //gray
                    int R = pixel[x + y * stride];
                    int G = pixel[x + y * stride + 1];
                    int B = pixel[x + y * stride + 2];
                    int a = pixel[x + y * stride + 3];
                    int value = (R * 299 + G * 587 + B * 114 + 500) / 1000;
                    pixel[x + y * stride] = (byte)(value);
                    pixel[x + y * stride + 1] = (byte)(value);
                    pixel[x + y * stride + 2] = (byte)(value);
                    pixel[x + y * stride + 3] = 0xff;
                }
            });

            wBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixel, stride, 0);
            displayImage.Source = wBitmap;
        }

        private void InvertedImage(object sender, RoutedEventArgs e)
        {
            var width = wBitmap.PixelWidth; //Picture Pixel Width [500] * 500
            var height = wBitmap.PixelHeight; //Picture Pixel Height 500 * [500]
            var bytesPerPixel = ((wBitmap.Format.BitsPerPixel + 7) / 8);
            var stride = width * ((wBitmap.Format.BitsPerPixel + 7) / 8); //Picture Pixel 4 bytes = (32 + 7)/8  -> width * bytes = stride = one horizontal line bytes
            int startCount = Environment.TickCount;
            var bitmapData = new byte[height * stride];
            wBitmap.CopyPixels(bitmapData, stride, 0);
            byte[] pixel = (byte[])bitmapData.Clone();
            int pixelCount = Environment.TickCount;
            Console.WriteLine("pixel :{0}", pixelCount - startCount);
            //wBitmap.CopyPixels(pixel, stride, 0);

            Parallel.For(0, height, y =>
            {
                int x;
                for (x = 0; x < stride; x += bytesPerPixel)
                {
                    //gray
                    pixel[x + y * stride] = bitmapData[y * stride + (stride - x - 1 - 3)];
                    pixel[x + y * stride + 1] = bitmapData[y * stride + (stride - x - 1 - 2)];
                    pixel[x + y * stride + 2] = bitmapData[y * stride + (stride - x - 1 - 1)];
                    pixel[x + y * stride + 3] = 0xff;
                }
            });
            int computeCount = Environment.TickCount;
            Console.WriteLine("compute :{0}", computeCount - pixelCount);

            wBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixel, stride, 0);

            int writeCount = Environment.TickCount;
            Console.WriteLine("tick :{0}", writeCount - computeCount);

            displayImage.Source = wBitmap;
        }

        private void MediumImage(object sender, RoutedEventArgs e)
        {
            var width = wBitmap.PixelWidth; //Picture Pixel Width [500] * 500
            var height = wBitmap.PixelHeight; //Picture Pixel Height 500 * [500]
            var bytesPerPixel = ((wBitmap.Format.BitsPerPixel + 7) / 8);
            var stride = width * ((wBitmap.Format.BitsPerPixel + 7) / 8); //Picture Pixel 4 bytes = (32 + 7)/8  -> width * bytes = stride = one horizontal line bytes
            int startCount = Environment.TickCount;
            var bitmapData = new byte[height * stride];
            wBitmap.CopyPixels(bitmapData, stride, 0);
            byte[] pixel = (byte[])bitmapData.Clone();
            int pixelCount = Environment.TickCount;
            Console.WriteLine("pixel :{0}", pixelCount - startCount);
            //wBitmap.CopyPixels(pixel, stride, 0);

            Parallel.For(0, height, y =>
            {
                int x;
                for (x = 0; x < stride; x += bytesPerPixel)
                {
                    if ((y != 0) && (y != height - 1 ) && (x != 0) && (x != stride - 1 * bytesPerPixel))     //不考慮最外層像素      
                    {
                        for (int moveBit = 0; moveBit < 3; moveBit++) //R G B
                        {
                            int[] Mask = new int[9];
                            Mask[0] = bitmapData[(y - 1) * stride + x - 1 * bytesPerPixel + moveBit];
                            Mask[1] = bitmapData[(y - 1) * stride + x + moveBit];
                            Mask[2] = bitmapData[(y - 1) * stride + x + 1 * bytesPerPixel + moveBit];
                            Mask[3] = bitmapData[y * stride + x - 1 * bytesPerPixel + moveBit];
                            Mask[4] = bitmapData[y * stride + x + moveBit];
                            Mask[5] = bitmapData[y * stride + x + 1 * bytesPerPixel + moveBit];
                            Mask[6] = bitmapData[(y + 1) * stride + x - 1 * bytesPerPixel + moveBit];
                            Mask[7] = bitmapData[(y + 1) * stride + x + moveBit];
                            Mask[8] = bitmapData[(y + 1) * stride + x + 1 * bytesPerPixel + moveBit];
                            for (int i = 0; i < 9; i++)
                            {
                                for (int j = i; j < 9; j++)
                                    if (Mask[i] > Mask[j])
                                    {
                                        int temp = Mask[j];
                                        Mask[j] = Mask[i];
                                        Mask[i] = temp;
                                    }
                            }
                            pixel[x + y * stride + moveBit] = (byte)Mask[4];
                        }
                    }
                    else
                        pixel[x + y * stride] = bitmapData[x + y * stride];
                }
            });
            int computeCount = Environment.TickCount;
            Console.WriteLine("compute :{0}", computeCount - pixelCount);

            wBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixel, stride, 0);

            int writeCount = Environment.TickCount;
            Console.WriteLine("tick :{0}", writeCount - computeCount);

            displayImage.Source = wBitmap;
        }

        private void MergeImage(object sender, RoutedEventArgs e)
        {
            mergeForm = new MergeForm();
            mergeForm.Show();
            this.Close();
        }

        private void DragImage(object sender, RoutedEventArgs e)
        {
            DragControls drags = new DragControls();
            drags.Show();
            this.Close();
        }


        //http://lbt95.pixnet.net/blog/post/33941436-%5Bc%23%5D-wpf%E7%9A%84%E9%AB%98%E9%80%9F%E5%BD%B1%E5%83%8F%E8%99%95%E7%90%86
    }
}
