using System;
using System.Drawing;
using System.Windows.Forms;
using NP.NumericalModel.Geometry;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.ConsoleSample
{
    public class GeometryMotionDemo : Form
    {
        private Timer timer;
        private TrackBar angleBar;
        private CheckBox animationCheck;
        private Label infoLabel;
        private Panel canvas;
        private Bitmap oscillationBitmap;
        private float oscillationLeft;
        private float oscillationCy;
        private float oscillationRadius;
        private float oscillationSpan;

        private double angleDeg;
        private Circle unitCircle;
        private Point2D orbitPoint;
        private Line2D radiusLine;
        private Point2D projectionPoint;

        public GeometryMotionDemo()
        {
            this.Text = "NP.NumericalModel - Geometry Motion";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1050, 700);
            this.MinimumSize = new Size(900, 620);

            unitCircle = new Circle(new Point2D(0.0, 0.0), 1.0);

            Label angleLabel = new Label();
            angleLabel.Text = "Angle:";
            angleLabel.Location = new Point(20, 18);
            angleLabel.AutoSize = true;

            angleBar = new TrackBar();
            angleBar.Minimum = 0;
            angleBar.Maximum = 360;
            angleBar.TickFrequency = 30;
            angleBar.Value = 60;
            angleBar.Location = new Point(70, 10);
            angleBar.Size = new Size(420, 45);
            angleBar.ValueChanged += new EventHandler(angleBar_ValueChanged);

            animationCheck = new CheckBox();
            animationCheck.Text = "Animate";
            animationCheck.AutoSize = true;
            animationCheck.Location = new Point(510, 18);
            animationCheck.CheckedChanged += new EventHandler(animationCheck_CheckedChanged);

            infoLabel = new Label();
            infoLabel.Location = new Point(650, 15);
            infoLabel.AutoSize = true;

            canvas = new Panel();
            canvas.Location = new Point(10, 60);
            canvas.Size = new Size(1030, 625);
            canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                            AnchorStyles.Left | AnchorStyles.Right;
            canvas.BackColor = Color.White;
            canvas.Paint += new PaintEventHandler(canvas_Paint);

            timer = new Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(timer_Tick);

            this.Controls.Add(angleLabel);
            this.Controls.Add(angleBar);
            this.Controls.Add(animationCheck);
            this.Controls.Add(infoLabel);
            this.Controls.Add(canvas);

            UpdateModel();
            canvas.Resize += new EventHandler(canvas_Resize);
        }

        private void angleBar_ValueChanged(object sender, EventArgs e)
        {
            angleDeg = angleBar.Value;
            UpdateModel();
            canvas.Invalidate();
        }

        private void canvas_Resize(object sender, EventArgs e)
        {
            CreateOscillationBitmap();
            canvas.Invalidate();
        }

        private void animationCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCheck.Checked)
                timer.Start();
            else
                timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int next = angleBar.Value + 2;

            if (next > angleBar.Maximum)
                next = angleBar.Minimum;

            angleBar.Value = next;
        }

        private void UpdateModel()
        {
            double angle = angleDeg * Math.PI / 180.0;

            orbitPoint = new Point2D(
                unitCircle.Center.X + unitCircle.Radius * Math.Cos(angle),
                unitCircle.Center.Y + unitCircle.Radius * Math.Sin(angle));

            projectionPoint = new Point2D(
                orbitPoint.X,
                0.0);

            radiusLine = new Line2D(unitCircle.Center, orbitPoint);

            infoLabel.Text =
                "theta = " + angleDeg.ToString("0") +
                " deg    x = " + orbitPoint.X.ToString("0.000") +
                "    y = " + orbitPoint.Y.ToString("0.000");
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int w = canvas.ClientSize.Width;
            int h = canvas.ClientSize.Height;

            float cx = w / 3.0f;
            float cy = h / 2.0f;
            float radius = Math.Min(w / 6.0f, h / 3.0f);

            DrawUnitCircle(g, cx, cy, radius);
            PrepareOscillationLayout(w, h, cx + radius + 90.0f, cy, radius);
            if (oscillationBitmap == null)
                CreateOscillationBitmap();
            if (oscillationBitmap != null)
                g.DrawImageUnscaled(oscillationBitmap, (int)oscillationLeft, 0); 
            DrawOscillationCursor(g);
        }

        private void DrawUnitCircle(
            Graphics g,
            float cx,
            float cy,
            float radius)
        {
            Pen axisPen = Pens.Gray;
            Pen circlePen = Pens.Black;
            Pen radiusPen = Pens.DarkBlue;
            Pen projectionPen = Pens.DarkGreen;

            g.DrawLine(axisPen, cx - radius - 25, cy, cx + radius + 25, cy);
            g.DrawLine(axisPen, cx, cy - radius - 25, cx, cy + radius + 25);

            g.DrawEllipse(
                circlePen,
                cx - radius,
                cy - radius,
                radius * 2.0f,
                radius * 2.0f);

            float px = cx + (float)(orbitPoint.X * radius);
            float py = cy - (float)(orbitPoint.Y * radius);
            float projY = cy;

            g.DrawLine(radiusPen, cx, cy, px, py);
            g.DrawLine(projectionPen, px, py, px, projY);

            g.FillEllipse(
                Brushes.Black,
                px - 5.0f,
                py - 5.0f,
                10.0f,
                10.0f);

            g.DrawString(
                "P",
                this.Font,
                Brushes.Black,
                px + 8.0f,
                py - 18.0f);

            g.DrawString(
                "O",
                this.Font,
                Brushes.Black,
                cx - 15.0f,
                cy + 8.0f);

            g.DrawString(
                angleDeg.ToString("0") + " deg",
                this.Font,
                Brushes.Black,
                cx + 20.0f,
                cy - radius - 35.0f);

            g.DrawString(
                "unit circle",
                this.Font,
                Brushes.Black,
                cx - 35.0f,
                cy + radius + 20.0f);

            g.DrawString(
                "projection",
                this.Font,
                Brushes.DarkGreen,
                px + 8.0f,
                cy + 8.0f);
        }

        private void PrepareOscillationLayout(
            int width,
            int height,
            float left,
            float cy,
            float radius)
        {
            oscillationLeft = left;
            oscillationCy = cy;
            oscillationRadius = radius;
            oscillationSpan = width - left - 45.0f;
        }

        private void CreateOscillationBitmap()
        {
            if (canvas == null || canvas.ClientSize.Width <= 0 || canvas.ClientSize.Height <= 0)
                return;

            int width = canvas.ClientSize.Width;
            int height = canvas.ClientSize.Height;

            float w = width;
            float h = height;
            float cx = w / 3.0f;
            float cy = h / 2.0f;
            float radius = Math.Min(w / 6.0f, h / 3.0f);
            float left = cx + radius + 90.0f;
            float span = width - left - 45.0f;

            if (oscillationBitmap != null)
                oscillationBitmap.Dispose();

            oscillationBitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(oscillationBitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                float top = cy - radius;
                float bottom = cy + radius;

                g.DrawLine(Pens.Gray, left, top, left, bottom);
                g.DrawLine(Pens.Gray, left, cy, width - 30.0f, cy);

                PointF previous = PointF.Empty;
                bool hasPrevious = false;
                int samples = 360;

                for (int i = 0; i <= samples; i++)
                {
                    double a = (i * Math.PI * 2.0) / samples;
                    float x = left + span * i / (float)samples;
                    float y = cy - (float)Math.Sin(a) * radius;
                    PointF current = new PointF(x, y);

                    if (hasPrevious)
                        g.DrawLine(Pens.DarkRed, previous, current);

                    previous = current;
                    hasPrevious = true;
                }

                g.DrawString("oscillation: sin(theta)", this.Font, Brushes.DarkRed,
                    left + 10.0f, top - 25.0f);
                g.DrawString("0", this.Font, Brushes.Black, left - 15.0f, cy - 5.0f);
                g.DrawString("+1", this.Font, Brushes.Black, left - 20.0f, top - 5.0f);
                g.DrawString("-1", this.Font, Brushes.Black, left - 20.0f, bottom - 5.0f);

                Ratio ratio = new Ratio(1, 3);
                g.DrawString("Model relation: " + ratio.ToString() +
                    "  -> 60 deg reference", this.Font, Brushes.Black,
                    left + 10.0f, bottom + 20.0f);
                g.DrawString("same theta -> orbit position -> projection -> oscillation",
                    this.Font, Brushes.Black, left + 10.0f, bottom + 42.0f);
            }
        }

        private void DrawOscillationCursor(Graphics g)
        {
            if (oscillationBitmap == null)
                return;

            float currentX = oscillationLeft +
                oscillationSpan * (float)(angleDeg / 360.0);
            float currentY = oscillationCy -
                (float)orbitPoint.Y * oscillationRadius;

            g.DrawLine(Pens.DarkGreen, currentX, oscillationCy,
                currentX, currentY);
            g.FillEllipse(Brushes.Black, currentX - 5.0f,
                currentY - 5.0f, 10.0f, 10.0f);
        }
        }
    }
}
