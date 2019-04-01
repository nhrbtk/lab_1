using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Build
    {
        protected PointF GetPointByAngle(PointF start_Point, float distance, float angle)
        {
            PointF pointToReturn = new PointF((float)(distance * Math.Cos(Math.PI * angle / 180)) + start_Point.X, (float)(distance * Math.Sin(Math.PI * angle / 180)) + start_Point.Y);
            return pointToReturn;
        }

        protected List<MyPoint> CirclePoints(PointF CircleCenter, float R, float start_Angle, float end_Angle)
        {
            bool newStart = true;
            List<MyPoint> ListToReturn = new List<MyPoint>();
            if (start_Angle <= end_Angle)
            {
                for (int ang = (int)Math.Ceiling(start_Angle); ang <= end_Angle; ang++)
                {
                    PointF p = GetPointByAngle(CircleCenter, R, ang);
                    ListToReturn.Add(new MyPoint(p, newStart));
                    newStart = false;
                }
            }
            else
            {
                for (int ang = (int)Math.Ceiling(start_Angle); ang <= 360; ang++)
                {
                    PointF p = GetPointByAngle(CircleCenter, R, ang);
                    ListToReturn.Add(new MyPoint(p, newStart));
                    newStart = false;
                }
                for (int ang = 0; ang <= end_Angle; ang++)
                {
                    PointF p = GetPointByAngle(CircleCenter, R, ang);
                    ListToReturn.Add(new MyPoint(p, false));
                }
            }
            if (ListToReturn.Any() == false) throw new Exception("Circle points ListToReturn is empty");
            return ListToReturn;
        }

        protected float GetCollisionAngle(float R, float length)
        {
            float res = (float)(Math.Asin(length / 2 / R) * 180 / Math.PI);

            if (res > 90) res %= 90;
            return res;
        }

        public List<MyPoint> Arrow(PointF point, float angle)
        {
            List<MyPoint> ListToReturn = new List<MyPoint>();
            int lenght = 20;
            int arrowAngle = 15;
            angle = (angle + 180) % 360;
            PointF pointArrowLeft = GetPointByAngle(point, lenght, (angle + (360 - arrowAngle)) % 360);
            PointF pointArrowRight = GetPointByAngle(point, lenght, (angle + arrowAngle) % 360);

            ListToReturn.Add(new MyPoint(pointArrowLeft, true));
            ListToReturn.Add(new MyPoint(point, false));
            ListToReturn.Add(new MyPoint(pointArrowRight, false));

            return ListToReturn;
        }
    }
}
