using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    static class Transform
    {
        static public List<MyPoint> Move(IPoints figure, int x_move, int y_move)
        {
            List<MyPoint> myPoints = figure.GetPoints();
            foreach (MyPoint p in myPoints)
            {
                PointF newPoint = p.GetPoint();
                newPoint.X += x_move;
                newPoint.Y += y_move;
                p.SetPoint(newPoint);
            }
            return myPoints;
        }

        static public List<MyPoint> Turn(IPoints figure, float angle, PointF axle)
        {
            List<MyPoint> myPoints = figure.GetPoints();
            angle *= (float)Math.PI / 180;
            foreach (MyPoint p in myPoints)
            {
                PointF oldPoint = p.GetPoint();
                PointF newPoint = new PointF();
                newPoint.X = (float)(axle.X + (oldPoint.X - axle.X) * Math.Cos(angle) - (oldPoint.Y - axle.Y) * Math.Sin(angle));
                newPoint.Y = (float)(axle.Y + (oldPoint.X - axle.X) * Math.Sin(angle) + (oldPoint.Y - axle.Y) * Math.Cos(angle));
                p.SetPoint(newPoint);
            }
            return myPoints;
        }

        static public List<MyPoint> Affine(IPoints figure, PointF x_Vector, PointF y_Vector, PointF startPoint)
        {
            List<MyPoint> myPoints = figure.GetPoints();
            foreach (MyPoint p in myPoints)
            {
                PointF oldPoint = p.GetPoint();
                PointF newPoint = new PointF();

                newPoint.X = startPoint.X + x_Vector.X * oldPoint.X + y_Vector.X * oldPoint.Y;
                newPoint.Y = startPoint.Y + x_Vector.Y * oldPoint.X + y_Vector.Y * oldPoint.Y;

                p.SetPoint(newPoint);
            }
            return myPoints;
        }

        static public List<MyPoint> Projective(IPoints figure, MyPoint startPoint, MyPoint X_AxleEnd, MyPoint Y_AxleEnd)
        {
            List<MyPoint> myPoints = figure.GetPoints();
            float X0 = startPoint.Point.X;
            float Y0 = startPoint.Point.Y;
            float W0 = startPoint.Weight;

            float Xx = X_AxleEnd.Point.X;
            float Yx = X_AxleEnd.Point.Y;
            float Wx = X_AxleEnd.Weight;

            float Xy = Y_AxleEnd.Point.X;
            float Yy = Y_AxleEnd.Point.Y;
            float Wy = Y_AxleEnd.Weight;


            foreach (MyPoint p in myPoints)
            {
                float X = p.Point.X;
                float Y = p.Point.Y;
                float W = p.Weight;

                //float newX = (startPoint.Point.X * startPoint.Weight + X_AxleEnd.Point.X * X_AxleEnd.Weight * p.Point.X + Y_AxleEnd.Point.X * Y_AxleEnd.Weight * p.Point.Y)/(startPoint.Weight+X_AxleEnd.Weight*)
                float newX = (X0 * W0 + Xx * Wx * X + Xy * Wy * Y) / (W0 + Wx * X + Wy * Y);
                float newY = (Y0 * W0 + Yx * Wx * X + Yy * Wy * Y) / (W0 + Wx * X + Wy * Y);
                p.SetPoint(new PointF(newX, newY));
            }        

            return myPoints;
        }

        

        static public List<MyPoint> Show(IPoints figure)
        {
            List<MyPoint> myPoints = figure.GetPoints();
            float minX = float.MaxValue, minY = float.MaxValue;
            foreach (MyPoint p in myPoints)
            {
                if(p.GetPoint().X<minX || p.GetPoint().Y < minY)
                {
                    minX = p.GetPoint().X;
                    minY = p.GetPoint().Y;
                }
            }

            foreach (MyPoint p in myPoints)
            {
                p.SetPoint(new PointF(p.GetPoint().X + minX, p.GetPoint().Y + minY));
            }
            return myPoints;
        }
    }
}
