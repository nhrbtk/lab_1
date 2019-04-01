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
        private float Width;
        private float Height;
        private float Size;
        private List<MyPoint> GridPoints;

        public Grid(float _step, float _width, float _height)
        {
            Step = _step;
            Size = Math.Max(_width, _height);

            Width = _width;
            Height = _height;
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

        //private void CalculateGridPoints()
        //{
        //    List<MyPoint> ListToReturn = new List<MyPoint>();




        //    for (float i = Width/2; i < Width; i += Step)
        //    {
        //        PointF start = new PointF(i, 0);
        //        PointF end = new PointF(i, Height);

        //        ListToReturn.Add(new MyPoint(start, true));
        //        ListToReturn.Add(new MyPoint(end, false));
                
        //    }
        //    for (float i = Width / 2 - Step; i > 0; i -= Step)
        //    {
        //        PointF start = new PointF(i, 0);
        //        PointF end = new PointF(i, Height);

        //        ListToReturn.Add(new MyPoint(start, true));
        //        ListToReturn.Add(new MyPoint(end, false));
        //    }

        //    for (float i = Height / 2; i < Height; i += Step)
        //    {
        //        PointF start = new PointF(0, i);
        //        PointF end = new PointF(Width, i);

        //        ListToReturn.Add(new MyPoint(start, true));
        //        ListToReturn.Add(new MyPoint(end, false));

        //    }
        //    for (float i = Height / 2 - Step; i > 0; i -= Step)
        //    {
        //        PointF start = new PointF(0, i);
        //        PointF end = new PointF(Width, i);

        //        ListToReturn.Add(new MyPoint(start, true));
        //        ListToReturn.Add(new MyPoint(end, false));

        //    }

        //    GridPoints = ListToReturn;
        //}
    }
}
