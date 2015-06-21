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
using WPF_ImageProcessing.HandGeasture;

namespace WPF_ImageProcessing
{
    /// <summary>
    /// OpenCVWindows.xaml 的互動邏輯
    /// </summary>
    public partial class OpenCVWindows : Window
    {
        public OpenCVWindows()
        {
            InitializeComponent();
        }

        private void ChangeToFaceDetectionMode(object sender, RoutedEventArgs e)
        {
            FaceDetectionWindows faceDetectionWindows = new FaceDetectionWindows();
            this.Hide();
            faceDetectionWindows.ShowDialog();
            this.Show();
        }

        private void ChangeToFaceCamShiftMode(object sender, RoutedEventArgs e)
        {
            CamShiftWindows camShiftWindows = new CamShiftWindows();
            this.Hide();
            camShiftWindows.ShowDialog();
            this.Show();
        }

        private void ChangeToFaceHandGeastureMode(object sender, RoutedEventArgs e)
        {
            HandGeastureWindows camShiftWindows = new HandGeastureWindows();
            this.Hide();
            camShiftWindows.ShowDialog();
            this.Show();
        }
    }
}
