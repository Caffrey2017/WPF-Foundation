using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WPF_ImageProcessing.HandGeasture
{
    public class CustomGeasture
    {
        int _fingerNumber;
        PointF _handMiddle;

        public CustomGeasture() { }

        public int CheckGeastureNumber(int fingerNum,PointF handMiddle,PointF[] fingers)
        {
            if (CheckDistanceNearly(handMiddle, fingers) && fingerNum <= 3)
                return 0;
            else
                return fingerNum;
        }

        private bool CheckDistanceNearly(PointF handMiddle,PointF[] fingers)
        {
            float distanceX = 0.0f;
            float distanceY = 0.0f;
            float distance = 0.0f;
            int count = 0;
            distanceX = handMiddle.X - fingers[0].X;
            distanceY = handMiddle.Y - fingers[0].Y;
            distance = distanceX * distanceX + distanceY * distanceY;
            distance = (float)Math.Sqrt((double)distance);
            foreach(PointF finger in fingers)
            {
                if (CheckNearlyRange(finger, handMiddle, distance, 70))
                    count++;
                if (count == 5)
                    return true;
            }
            return false;
        }

        private bool CheckNearlyRange(PointF p1,PointF p2,float distance,float offset)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            float dt = dx * dx + dy * dy;
            dt = (float)Math.Sqrt((double)dt);
            if (CheckNearlyRange(dt, distance, offset))
                return true;
            return false;
        }

        private bool CheckNearlyRange(float comparedDistance, float refDistance, float offset)
        {
            if (comparedDistance <= refDistance + offset && comparedDistance >= refDistance - offset)
                return true;
            return false;
        }

    }
}
