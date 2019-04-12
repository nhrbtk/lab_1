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
        private Viewport viewport;
        private Grid grid;
        private Axle axle;
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
            viewport = new Viewport(sizeBmp.Width, sizeBmp.Height);
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
            viewport = new Viewport(sizeBmp.Width, sizeBmp.Height);
        }

        private Bitmap DrawGrid(Grid G)
        {
            Bitmap bmp = new Bitmap(sizeBmp);
            Graphics graphics = Graphics.FromImage(bmp);
            List<MyPoint> gridPoints = viewport.FactToLogic(G.GetPoints());
            for (int i = 0; i < gridPoints.Count; i++)
                if (!gridPoints[i].IsNewStart()) graphics.DrawLine(gridPen, gridPoints[i - 1].GetPoint(), gridPoints[i].GetPoint());

            float sizeCounter = 0;
            for (int i = 2; i < gridPoints.Count; i += 4)
            {
                graphics.DrawString(sizeCounter.ToString(), gridSizeFont, gridSizeBrush, gridPoints[i - 2].GetPoint());
                graphics.DrawString(sizeCounter.ToString(), gridSizeFont, gridSizeBrush, gridPoints[i].GetPoint());
                sizeCounter += grid.GetStep();
            }

            if (axle != null)
            {
                List<MyPoint> A = viewport.FactToLogic(axle.GetPoints());

                for (int i = 0; i < A.Count; i++)
                    if (!A[i].IsNewStart()) graphics.DrawLine(new Pen(Color.Black, 1), A[i - 1].GetPoint(), A[i].GetPoint());
                graphics.DrawString("X, pixels", gridSizeFont, Brushes.Black, A[1].GetPoint());
                graphics.DrawString("Y, pixels", gridSizeFont, Brushes.Black, A[3].GetPoint());
            }
            return bmp;
        }

        private Bitmap DrawFigure(IPoints F)
        {
            Bitmap bmp = new Bitmap(sizeBmp);
            Graphics graphics = Graphics.FromImage(bmp);

            List<MyPoint> figurePoints = viewport.FactToLogic(F.GetPoints());
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

        public void SetAxle(Axle _axle)
        {
            axle = _axle;
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
