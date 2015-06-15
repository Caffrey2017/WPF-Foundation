using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_ImageProcessing.DrawingWindows
{
    //Writable Bitmap 
    //Byte[]
    public class ModifiablePicture
    {
        public WriteableBitmap SourceBitmap
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        } //Picture Pixel Width [500] * 500

        public int Height
        {
            get;
            private set;
        } // Picture Pixel Height 500 * [500]

        public int BytesPerPixel
        {
            get;
            private set;
        } // (BitsPerPixel + 7) / 8;

        public int Stride
        {
            get;
            private set;
        }  // Picture Pixel 4 bytes = (32 + 7)/8  -> width * bytes = stride = one horizontal line bytes

        public byte[] BitmapBytes
        {
            get;
            private set;
        }  // new byte[height * stride];

        public ModifiablePicture(WriteableBitmap sourceBitmap)
        {
            Initialize(sourceBitmap);
        }

        public ModifiablePicture(ModifiablePicture copyInstance)
        {
            SourceBitmap = copyInstance.SourceBitmap.Clone();
            Initialize(SourceBitmap);
        }

        private void Initialize(WriteableBitmap sourceBitmap)
        {
            SourceBitmap = sourceBitmap;
            Width = sourceBitmap.PixelWidth; //Picture Pixel Width [500] * 500
            Height = sourceBitmap.PixelHeight; //Picture Pixel Height 500 * [500]
            BytesPerPixel = ((sourceBitmap.Format.BitsPerPixel + 7) / 8);
            Stride = Width * ((sourceBitmap.Format.BitsPerPixel + 7) / 8); //Picture Pixel 4 bytes = (32 + 7)/8  -> width * bytes = stride = one horizontal line bytes
            BitmapBytes = new byte[Height * Stride];
            SourceBitmap.CopyPixels(BitmapBytes, Stride, 0);
            SourceBitmap.Lock();
            SourceBitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
            SourceBitmap.Unlock();
        }

        public void SetBytes(byte[] pixels, int x, int y, int width, int height)
        {
            Int32Rect cutArea = new Int32Rect(x, y, width, height);
            //SourceBitmap.Lock();
            SourceBitmap.CopyPixels(cutArea, BitmapBytes, Stride, 0);
            //SourceBitmap.Unlock();
            BitmapSource newBitmap = BitmapSource.Create(width, height, 96.0, 96.0, PixelFormats.Bgr32, null, BitmapBytes, Stride);
            WriteableBitmap tempBitmap = new WriteableBitmap(newBitmap);
            Initialize(tempBitmap);
            //SourceBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, Stride, 0);
        }

        /*public WriteableBitmap GetSplitPicture()
        {

        }*/
    }
}
