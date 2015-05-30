using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_ImageProcessing
{
    /// <summary>
    /// MergeForm.xaml 的互動邏輯
    /// </summary>
    public partial class MergeForm : Window
    {
        OpenFileDialog open;
        BitmapImage foreground;
        BitmapImage background;
        WriteableBitmap foreBitmap;
        WriteableBitmap backBitmap;
        Image foreTempImage;
        Image backTempImage;
        public MergeForm()
        {
            InitializeComponent();
        }

        private void ForegroundLoadImage(object sender, RoutedEventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            open.ShowDialog();
            string location = open.FileName;
            if (!File.Exists(location))
                return;
            foreground = new BitmapImage(new Uri(location));
            foreBitmap = new WriteableBitmap(foreground);
            foreImage.Children.Remove(foreTempImage);
            foreTempImage = new Image();
            foreTempImage.Opacity = 0.9;
            foreTempImage.Source = foreground;
            foreImage.Children.Add(foreTempImage);
        }

        private void BackgroundLoadImage(object sender, RoutedEventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            open.ShowDialog();
            string location = open.FileName;
            if (!File.Exists(location))
                return;
            background = new BitmapImage(new Uri(location));
            backBitmap = new WriteableBitmap(background);
            backImage.Children.Remove(backTempImage);
            backTempImage = new Image();
            backTempImage.Opacity = 0.5;
            backTempImage.Source = background;
            backImage.Children.Add(backTempImage);
        }
    }
}
