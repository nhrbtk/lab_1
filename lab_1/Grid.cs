using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Grid:IPoints
    {
        private float Step;
        private float Size;
        private List<MyPoint> GridPoints;

        public Grid(float _step, float _width, float _height)
        {
            Step = _step;
            Size = Math.Max(_width, _height);
            CalculateGridPoints();
        }

        public void SetPoints(List<MyPoint> myPoints)
        {
            GridPoints = myPoints;
        }

        public List<MyPoint> GetPoints()
        {
            return GridPoints;
        }

        public float GetStep()
        {
            return Step;
        }

        private void CalculateGridPoints()
        {
            List<MyPoint> ListToReturn = new List<MyPoint>();
            for(float i = 0; i < Size; i += Step)
            {
                //for (float j = i; j < Size; j++)
                //{

                //    PointF Xstart = new PointF(i, j);
                //    PointF Xend = new PointF(i, j+1);
                //    PointF Ystart = new PointF(j, i);
                //    PointF Yend = new PointF(j+1, i);

                //    ListToReturn.Add(new MyPoint(Xstart, true));
                //    ListToReturn.Add(new MyPoint(Xend, false));
                //    ListToReturn.Add(new MyPoint(Ystart, true));
                //    ListToReturn.Add(new MyPoint(Yend, false));
                //}



                PointF Xstart = new PointF(i, 0);
                PointF Xend = new PointF(i, Size);
                PointF Ystart = new PointF(0, i);
                PointF Yend = new PointF(Size, i);

                ListToReturn.Add(new MyPoint(Xstart, true));
                ListToReturn.Add(new MyPoint(Xend, false));
                ListToReturn.Add(new MyPoint(Ystart, true));
                ListToReturn.Add(new MyPoint(Yend, false));
            }

            GridPoints = ListToReturn;
        }
    }
}
