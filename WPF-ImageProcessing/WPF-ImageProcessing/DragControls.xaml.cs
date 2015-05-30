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

namespace WPF_ImageProcessing
{
    /// <summary>
    /// DragControls.xaml 的互動邏輯
    /// </summary>
    public partial class DragControls : Window
    {
        bool isDrag;
        Point startPosition;
        public DragControls()
        {
            InitializeComponent();
        }

        private void ControlDown(object sender, MouseButtonEventArgs e)
        {
            isDrag = true;
            startPosition = e.GetPosition(this);
        }

        private void ControlUp(object sender, MouseButtonEventArgs e)
        {
            isDrag = false;
        }

        private void ControlMove(object sender, MouseEventArgs e)
        {
            if(isDrag)
            {
                Point nowPosition = e.GetPosition(this);
                double x = nowPosition.X - startPosition.X;
                double y = nowPosition.Y - startPosition.Y;

                var transform = ControlLabel.RenderTransform as TranslateTransform;
                if(transform == null)
                {
                    transform = new TranslateTransform();
                    ControlLabel.RenderTransform = transform;
                }
                transform.X = x;
                transform.Y = y;
            }
        }

    }
}
