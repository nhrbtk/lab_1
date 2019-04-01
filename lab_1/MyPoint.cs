using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    public class MyPoint
    {
        public PointF Point;
        public bool NewStart;
        public float Weight { get; set; }


        public MyPoint(PointF _point, bool _ns, float _weight = 1)
        {
            Point = _point;
            NewStart = _ns;
            Weight = _weight;
        }

        public MyPoint(float _x, float _y, bool _ns, float _weight = 1)
        {
            Point = new PointF(_x, _y);
            NewStart = _ns;
            Weight = _weight;
        }

        public PointF GetPoint()
        {
            return Point;
        }

        public void SetPoint(PointF _point)
        {
            Point = _point;
        }

        public bool IsNewStart()
        {
            return NewStart;
        }

        public void SetNewStart()
        {
            NewStart = true;
        }


    }
}
