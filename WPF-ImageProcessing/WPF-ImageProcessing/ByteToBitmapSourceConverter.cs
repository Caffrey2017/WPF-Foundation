using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF_ImageProcessing
{
    public class ByteToBitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                var stream = new MemoryStream(value as byte[]);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is BitmapImage)
            {
                Stream stream = ((BitmapImage)value).StreamSource;
                Byte[] buffer = null;
                if (stream != null && stream.Length > 0)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        buffer = br.ReadBytes((Int32)stream.Length);
                    }
                }
                return buffer;
            }
            return null;
        }
    }
}
