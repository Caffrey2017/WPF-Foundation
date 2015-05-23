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
            displayImage.Source = bitmap;
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            open.ShowDialog();
            string location = open.FileName;
            bitmap = new BitmapImage(new Uri(location));
            displayImage.Source = bitmap;
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {

        }

        private void GrayImage(object sender, RoutedEventArgs e)
        {
            ByteToBitmapSourceConverter converter = new ByteToBitmapSourceConverter();
            byte[] imageData = (byte[])converter.ConvertBack(bitmap,typeof(BitmapImage),0,System.Globalization.CultureInfo.CurrentCulture);


            for(int i = 0;i < imageData.Length;i++)
            {
                if(i % 5 > 2)
                imageData[i] = 255;
            }

            bitmap = converter.ConvertBack(imageData,typeof(byte[]),0,System.Globalization.CultureInfo.CurrentCulture) as BitmapImage;
            displayImage.Source = bitmap;
        }

        private void InvertedImage(object sender, RoutedEventArgs e)
        {


        }
    }
}
