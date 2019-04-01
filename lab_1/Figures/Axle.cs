using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class Axle:Build, IPoints
    {
        List<MyPoint> AxlePoints;
        private float margin = 100;
        private float arrowAngle = 15;
        private float Width;
        private float Height;

        public Axle(float _width, float _height)
        {
            Width = _width;
            Height = _height;
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            List<MyPoint> Axle = new List<MyPoint>();
            Axle.Add(new MyPoint(0, 0, true));
            Axle.Add(new MyPoint(Width - 100, 0, false));
            Axle.Add(new MyPoint(0, 0, true));
            Axle.Add(new MyPoint(0, Height - 100, false));
            Axle.AddRange(Arrow(new PointF(Width - 100, 0), 0));
            Axle.AddRange(Arrow(new PointF(0, Height - 100), 90));
            AxlePoints = Axle;
        }

        public void SetPoints(List<MyPoint> myPoints)
        {
            AxlePoints = myPoints;
        }

        public List<MyPoint> GetPoints()
        {
            return AxlePoints;
        }
    }
}
