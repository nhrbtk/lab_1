using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1.Figures
{
    class Curve : Build, IPoints
    {
        List<MyPoint> CurvePoints;
        float A = 100;

        public Curve(float _A)
        {
            A = _A;
            CalculatePoints();
        }

        public void SetPoints(List<MyPoint> myPoints)
        {
            CurvePoints = myPoints;
        }

        public List<MyPoint> GetPoints()
        {
            return CurvePoints;
        }

        private void CalculatePoints()
        {
            List<MyPoint> ListToReturn = new List<MyPoint>();
            for (int i = 1; i < 180; i++)
            {
                float x, y, fi = i;
                fi = (float)(fi/180 * Math.PI);

                x = (float)(A * Math.Cos(fi) + A * Math.Log(Math.Tan(fi / 2)));
                y = (float)(A * Math.Sin(fi));
                ListToReturn.Add(new MyPoint(x, y, false));
            }
            ListToReturn[0].SetNewStart();
            CurvePoints = ListToReturn;
        }
    }
}