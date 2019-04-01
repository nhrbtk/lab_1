using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class Viewport
    {
        private PointF CENTER;
        public Viewport(float _width, float _height)
        {
            CENTER = new PointF(_width / 2, _height / 2);
        }
        public List<MyPoint> FactToLogic(List<MyPoint> myPoints)
        {
            foreach(MyPoint mp in myPoints)
            {
                PointF oldPoint = mp.GetPoint();
                //moving to axles
                oldPoint.X += CENTER.Y;
                oldPoint.Y += CENTER.X;

                //upside down
                oldPoint.Y -= (oldPoint.Y - CENTER.Y);

                mp.SetPoint(oldPoint);
            }
            return myPoints;
        }
    }
}
