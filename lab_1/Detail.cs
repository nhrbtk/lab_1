using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Detail:Build, IPoints
    {
        private List<MyPoint> DetailPoints;

        private float X;
        private float Y;
        private float A;
        private float B;
        private float C;
        private float D;
        private float E;
        private float R1;
        private float R2;
        private float R3;
        public Detail(float _x, float _y, float _a, float _b, float _c, float _d, float _e, float _r1, float _r2, float _r3)
        {
            X = _x;
            Y = _y;
            A = _a;
            B = _b;
            C = _c;
            D = _d;
            E = _e;
            R1 = _r1;
            R2 = _r2;
            R3 = _r3;
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            List<MyPoint> ListToReturn = new List<MyPoint>();
            MyPoint lastPoint, pointToAdd;

            PointF R1Center = new PointF(X, Y);
            PointF R2Center = new PointF(X, Y);
            float A_visible = A - E;
            float B_Visible = (B - R3 * 2) / 2;
            float D_visible = R1Center.X - R1 + D - GetPointByAngle(R1Center, R1, GetCollisionAngle(R1, C)).X;


            //outer part
            //R2 circle points
            float CollAngle = GetCollisionAngle(R2, B);
            ListToReturn.AddRange(CirclePoints(R2Center, R2, CollAngle, 360 - CollAngle));

            //E upper
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X + E, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;


            //B upper half
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X, lastPoint.GetPoint().Y + B_Visible, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //A upper
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X + A_visible, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //R3 circle points
            lastPoint = ListToReturn.Last();
            PointF R3Center = new PointF(lastPoint.GetPoint().X, lastPoint.GetPoint().Y + R3);
            ListToReturn.AddRange(CirclePoints(R3Center, R3, 270, 90));

            //A bottom
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X - A_visible, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //B bottom half
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X, lastPoint.GetPoint().Y + B_Visible, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //E bottom
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X - E, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;


            //inner part
            //R1 circle
            CollAngle = GetCollisionAngle(R1, C);
            ListToReturn.AddRange(CirclePoints(R1Center, R1, CollAngle, 360 - CollAngle));

            //D upper
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X + D_visible, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //C line
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X, lastPoint.GetPoint().Y + C, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            //D bottom
            lastPoint = ListToReturn.Last();
            pointToAdd = new MyPoint(lastPoint.GetPoint().X - D_visible, lastPoint.GetPoint().Y, false);
            ListToReturn.Add(pointToAdd);
            pointToAdd = null;

            DetailPoints = ListToReturn;
        }

        public void SetPoints(List<MyPoint> myPoints)
        {
            DetailPoints = myPoints;
        }

        public List<MyPoint> GetPoints()
        {
            return DetailPoints;
        }
    }
}
