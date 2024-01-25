using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Computer_Graphics
{
    public partial class Form1 : Form
    {

        //private int rectangleX = 50;  // Initial X-coordinate of the rectangle
        //private const int rectangleY = 100;  // Y-coordinate of the rectangle
        //private const int rectangleWidth = 50;
        //private const int rectangleHeight = 50;
        //private const int animationSpeed = 5;

        private const int triangleSize = 100;
        private const int animationSpeed = 5;
        private float rotationAngle = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Rectangle(object sender, PaintEventArgs e)
        {
            // Paint the rectangle with red color
            //e.Graphics.FillRectangle(Brushes.Red, rectangleX, rectangleY, rectangleWidth, rectangleHeight);

            float centerX = this.Width / 2;
            float centerY = this.Height / 2;

            // Calculate the vertices of the triangle
            PointF[] triangleVertices = new PointF[]
            {
            new PointF(centerX, centerY - triangleSize / 2),
            new PointF(centerX - triangleSize / 2, centerY + triangleSize / 2),
            new PointF(centerX + triangleSize / 2, centerY + triangleSize / 2)
            };

            // Rotate the triangle around its center
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(rotationAngle, new PointF(centerX, centerY));
            e.Graphics.Transform = rotationMatrix;

            // Paint the first half of the triangle as red
            e.Graphics.FillPolygon(Brushes.Red, new PointF[] { triangleVertices[0], triangleVertices[1], triangleVertices[2] });

            // Paint the second half of the triangle as blue
            e.Graphics.FillPolygon(Brushes.Blue, new PointF[] { triangleVertices[0], triangleVertices[2], triangleVertices[1] });

        }

        //private void MoveRectangle()
        //{
        //    while (true)
        //    {
        //        // Move the rectangle horizontally
        //        rectangleX += animationSpeed;

        //        // Check if the rectangle goes beyond the form's width, reset to the beginning
        //        if (rectangleX > this.Width)
        //        {
        //            rectangleX = -rectangleWidth;
        //        }

        //        // Force the form to redraw
        //        this.Invalidate();

        //        // Pause for a short duration to create animation effect
        //        Thread.Sleep(50);
        //    }
        //}

        private void RotateTriangle()
        {
            while (true)
            {
                // Rotate the triangle
                rotationAngle += animationSpeed;

                // Ensure the rotation angle stays within 360 degrees
                if (rotationAngle >= 360)
                    rotationAngle -= 360;

                // Force the form to redraw
                this.Invalidate();

                // Pause for a short duration to create animation effect
                Thread.Sleep(50);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(RotateTriangle);
            thread.Start();
        }
    }
}
