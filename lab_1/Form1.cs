using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_1
{
    public partial class Form1 : Form
    {
        bool MousePressed = false;
        int mouseX, mouseY, pX, pY, angleShow = 0, xShift = 0, yShift = 0;
        Draw draw;
        IPoints figure;
        Grid grid;
        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap(showField.Width, showField.Height);
            showField.Image = bmp;
            draw = new Draw((Bitmap)showField.Image);
            grid = new Grid((float)gridSize_nud.Value, showField.Width, showField.Height);
            draw.SetGrid(grid);
        }

        private void draw_btn_Click(object sender, EventArgs e)
        {
            figure = null;
            switch (figure_tabControl.SelectedIndex)
            {
                case 0:
                    figure = new Detail((float)X_nud.Value, (float)Y_nud.Value, (float)A_nud.Value, (float)B_nud.Value, (float)C_nud.Value, (float)D_nud.Value, (float)E_nud.Value, (float)R1_nud.Value, (float)R2_nud.Value, (float)R3_nud.Value);
                    break;
                case 1:
                    //place for krivaya
                    break;
            }
            
            if (figure != null) draw.SetFigure(figure);
            showField.Image = draw.ShowDrawing();

            angleShow = 0;
            xShift = 0;
            yShift = 0;
            angle_label.Text = $"Angle: {angleShow}";
            xShift_label.Text = $"X Shift: {xShift}";
            yShift_label.Text = $"Y Shift: {yShift}";
        }

        private void showField_MouseDown(object sender, MouseEventArgs e)
        {
            MousePressed = true;
            pX = e.X;
            pY = e.Y;
        }

        private void grid_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (draw.IsGridEmpty()) draw.SetGrid(grid = new Grid((float)gridSize_nud.Value, showField.Width, showField.Height));
            draw.SetShowGrid(grid_checkbox.Checked);
            showField.Image = draw.ShowDrawing();

        }

        private void gridSize_nud_ValueChanged(object sender, EventArgs e)
        {
            draw.SetGrid(grid = new Grid((float)gridSize_nud.Value, showField.Width, showField.Height));
            showField.Image = draw.ShowDrawing();
        }

        private void Xx_nud_ValueChanged(object sender, EventArgs e)
        {
            Yx_nud.Value = 1 - Xx_nud.Value;
        }

        private void Yx_nud_ValueChanged(object sender, EventArgs e)
        {
            Xx_nud.Value = 1 - Yx_nud.Value;
        }

        private void Xy_nud_ValueChanged(object sender, EventArgs e)
        {
            Yy_nud.Value = 1 - Xy_nud.Value;
        }

        private void Yy_nud_ValueChanged(object sender, EventArgs e)
        {
            Xy_nud.Value = 1 - Yy_nud.Value;
        }

        private void figureColor_btn_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            Pen pen = new Pen(colorDialog.Color, (int)figureSize_nud.Value);
            draw.SetFigurePen(pen);
            figureColor_btn.BackColor = colorDialog.Color;
        }

        private void figureSize_nud_ValueChanged(object sender, EventArgs e)
        {
            Pen pen = new Pen(figureColor_btn.BackColor, (int)figureSize_nud.Value);
            draw.SetFigurePen(pen);
        }

        private void projective_btn_Click(object sender, EventArgs e)
        {
            MyPoint xEnd = new MyPoint((float)XE_x_nud.Value, (float)XE_y_nud.Value, true, (float)XE_w_nud.Value);
            MyPoint yEnd = new MyPoint((float)YE_x_nud.Value, (float)YE_y_nud.Value, true, (float)YE_w_nud.Value);
            MyPoint startPoint = new MyPoint((float)X0_nud.Value, (float)Y0_nud.Value, true, (float)W0_nud.Value);

            if (draw.FigureExists())
            {
                figure.SetPoints(Transform.Projective(figure, startPoint, xEnd, yEnd));

                grid.SetPoints(Transform.Projective(grid, startPoint, xEnd, yEnd));

                draw.SetFigure(figure);
                draw.SetGrid(grid);

                showField.Image = draw.ShowDrawing();
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            draw.Clear();
            grid = new Grid((float)gridSize_nud.Value, showField.Width, showField.Height);
            angleShow = 0;
            xShift = 0;
            yShift = 0;
            angle_label.Text = $"Angle: {angleShow}";
            xShift_label.Text = $"X Shift: {xShift}";
            yShift_label.Text = $"Y Shift: {yShift}";
            grid_checkbox.Checked = false;
            Bitmap bmp = new Bitmap(showField.Width, showField.Height);
            showField.Image = bmp;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            controls_panel.Height = this.Size.Height - 60;
            showField.Width = this.Size.Width - 287;
            showField.Height = this.Size.Height - 60;
            Bitmap bmp = new Bitmap(showField.Width, showField.Height);
            draw.SetSizeBmp(bmp);
        }

        private void affine_btn_Click(object sender, EventArgs e)
        {
            PointF x_Vector = new PointF((float)Xx_nud.Value, (float)Yx_nud.Value);
            PointF y_Vector = new PointF((float)Xy_nud.Value, (float)Yy_nud.Value);
            PointF T = new PointF((float)Tx_nud.Value, (float)Ty_nud.Value);

            if (draw.FigureExists())
            {
                figure.SetPoints(Transform.Affine(figure, x_Vector, y_Vector, T));
                figure.SetPoints(Transform.Show(figure));

                grid.SetPoints(Transform.Affine(grid, x_Vector, y_Vector, T));
                grid.SetPoints(Transform.Show(grid));
                
                draw.SetFigure(figure);
                draw.SetGrid(grid);

                showField.Image = draw.ShowDrawing();
            }
        }

        private void showField_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
        }

        private void showField_MouseLeave(object sender, EventArgs e)
        {
            MousePressed = false;
        }

        private void showField_MouseMove(object sender, MouseEventArgs e)
        {
            widthLabel.Text = "Width: " + e.X.ToString();
            heightLabel.Text = "Height: " + e.Y.ToString();
            if (rotate_rbtn.Checked)
            {
                if (MousePressed && draw.FigureExists()) 
                {
                    figure.SetPoints(Transform.Turn(figure, e.Y - mouseY, new PointF(pX, pY)));
                    draw.SetFigure(figure);
                    showField.Image = draw.ShowDrawing();
                    angleShow += e.Y - mouseY;
                    angle_label.Text = $"Angle: {angleShow}";
                }
                mouseX = e.X;
                mouseY = e.Y;
            }

            if (move_rbtn.Checked && draw.FigureExists())
            {
                if (MousePressed)
                {
                    figure.SetPoints(Transform.Move(figure, e.X - mouseX, e.Y - mouseY));
                    draw.SetFigure(figure);
                    showField.Image = draw.ShowDrawing();
                    xShift += e.X - mouseX;
                    yShift += e.Y - mouseY;
                    xShift_label.Text= $"X Shift: {xShift}";
                    yShift_label.Text= $"Y Shift: {yShift}";
                }
                mouseX = e.X;
                mouseY = e.Y;
            }

            
        }
    }
}
