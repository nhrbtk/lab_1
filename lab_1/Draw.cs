using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Draw
    {
        private Pen figurePen = new Pen(Color.Blue, 3);
        private Pen gridPen = new Pen(Color.FromArgb(64, Color.Gray), 1);
        private Brush gridSizeBrush = Brushes.Gray;
        private Font gridSizeFont = new Font("Calibri", 9);

        private IPoints figure;
        private Grid grid;
        private bool showGrid = false;
        private bool showFigure = false;

        private Bitmap sizeBmp;
        private Bitmap figureBmp;
        private Bitmap gridBmp;



        public Draw(Bitmap _sizeBmp, IPoints _F, Grid _G, bool _showGrid)
        {
            figure = _F;
            grid = _G;
            showGrid = _showGrid;
            showFigure = true;
            gridBmp = DrawGrid(grid);
            figureBmp = DrawFigure(figure);
        }

        public Draw(Bitmap _sizeBmp)
        {
            sizeBmp = _sizeBmp;
            showGrid = false;
            showFigure = false;
        }

        public void SetShowGrid(bool _show)
        {
            showGrid = _show;

            gridBmp = DrawGrid(grid);
        }

        public void SetFigurePen(Pen _pen)
        {
            figurePen = _pen;
        }
        public void SetGrid(Grid _grid)
        {
            grid = _grid;
            gridBmp = DrawGrid(grid);
        }

        public bool IsGridEmpty()
        {
            return grid == null;
        }

        public void SetFigure(IPoints _figure)
        {
            figure = _figure;
            showFigure = true;
            figureBmp = DrawFigure(figure);
        }

        public void SetSizeBmp(Bitmap _sizeBmp)
        {
            sizeBmp = _sizeBmp;
        }

        private Bitmap DrawGrid(Grid G)
        {
            Bitmap bmp = new Bitmap(sizeBmp);
            Graphics graphics = Graphics.FromImage(bmp);
            List<MyPoint> gridPoints = G.GetPoints();
            for (int i = 0; i < gridPoints.Count; i++)
                if (!gridPoints[i].IsNewStart()) graphics.DrawLine(gridPen, gridPoints[i - 1].GetPoint(), gridPoints[i].GetPoint());

            float sizeCounter = 0;
            for (int i = 2; i < gridPoints.Count; i += 4)
            {
                graphics.DrawString(sizeCounter.ToString(), gridSizeFont, gridSizeBrush, gridPoints[i - 2].GetPoint());
                graphics.DrawString(sizeCounter.ToString(), gridSizeFont, gridSizeBrush, gridPoints[i].GetPoint());
                sizeCounter += grid.GetStep();
            }

            List<MyPoint> Axle = new List<MyPoint>();
            Axle.Add(new MyPoint(0, 0, true));
            Axle.Add(new MyPoint(sizeBmp.Width - 100, 0, false));
            Axle.Add(new MyPoint(0, 0, true));
            Axle.Add(new MyPoint(0, sizeBmp.Height - 100, false));
            Build b = new Build();
            Axle.AddRange(b.Arrow(new PointF(sizeBmp.Width - 100, 0), 0));
            Axle.AddRange(b.Arrow(new PointF(0, sizeBmp.Height - 100), 90));
            for (int i = 0; i < Axle.Count; i++)
                if (!Axle[i].IsNewStart()) graphics.DrawLine(new Pen(Color.Black, 1), Axle[i - 1].GetPoint(), Axle[i].GetPoint());
            return bmp;
        }

        private Bitmap DrawFigure(IPoints F)
        {
            Bitmap bmp = new Bitmap(sizeBmp);
            Graphics graphics = Graphics.FromImage(bmp);

            List<MyPoint> figurePoints = F.GetPoints();
            for (int i = 0; i < figurePoints.Count; i++)
                if (!figurePoints[i].IsNewStart()) graphics.DrawLine(figurePen, figurePoints[i - 1].GetPoint(), figurePoints[i].GetPoint());

            return bmp;
        } 

        public Bitmap ShowDrawing()
        {
            Bitmap bmp = new Bitmap(sizeBmp);
            Graphics graphics = Graphics.FromImage(bmp);

            if (showGrid) graphics.DrawImage(gridBmp, 0, 0);
            if (showFigure) graphics.DrawImage(figureBmp, 0, 0);

            return bmp;

        }

        public bool FigureExists()
        {
            return showFigure;
        }

        public bool GridExists()
        {
            return showGrid;
        }

        public void Clear()
        {
            figure = null;
            grid = null;
            showFigure = false;
            showGrid = false;

        }
    }
}
