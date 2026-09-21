using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NP.NumericalModel.Geometry;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.ConsoleSample
{
    public class GeometryMotionDemo : Form
    {
        private Timer timer;

        private TrackBar angleBar;
        private TextBox angleBox;
        private Button angleApplyButton;
        private CheckBox animationCheck;

        private ComboBox functionCombo;
        private TextBox xminBox;
        private TextBox xmaxBox;
        private TextBox xBox;
        private TextBox hBox;
        private TextBox aBox;
        private TextBox bBox;
        private Button applyFunctionButton;

        private Label infoLabel;
        private Label calculusLabel;
        private Panel canvas;

        private double angleDeg;
        private Circle unitCircle;
        private Point2D orbitPoint;
        private Line2D radiusLine;
        private Point2D projectionPoint;

        private CalculusFunctionModel functionModel;
        private double pointX;
        private double xMin;
        private double xMax;
        private double secantH;
        private double integralA;
        private double integralB;

        private bool draggingFunctionPoint;

        public GeometryMotionDemo()
        {
            this.Text = "NP.NumericalModel - Geometry & Calculus";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1200, 800);
            this.MinimumSize = new Size(1050, 700);

            unitCircle = new Circle(new Point2D(0.0, 0.0), 1.0);
            functionModel = new CalculusFunctionModel("x^2");

            angleDeg = 60.0;
            pointX = 1.0;
            xMin = -5.0;
            xMax = 5.0;
            secantH = 1.0;
            integralA = 0.0;
            integralB = 2.0;

            InitializeControls();
            UpdateModel();
        }

        private void InitializeControls()
        {
            Label angleLabel = CreateLabel("زاویه:", 15, 12);
            angleBox = new TextBox();
            angleBox.Location = new Point(55, 8);
            angleBox.Size = new Size(65, 25);
            angleBox.Text = "60";

            angleApplyButton = new Button();
            angleApplyButton.Text = "اعمال";
            angleApplyButton.Location = new Point(125, 7);
            angleApplyButton.Size = new Size(55, 27);
            angleApplyButton.Click += new EventHandler(angleApplyButton_Click);

            angleBar = new TrackBar();
            angleBar.Minimum = 0;
            angleBar.Maximum = 360;
            angleBar.TickFrequency = 30;
            angleBar.Value = 60;
            angleBar.Location = new Point(185, 3);
            angleBar.Size = new Size(300, 45);
            angleBar.ValueChanged += new EventHandler(angleBar_ValueChanged);

            animationCheck = new CheckBox();
            animationCheck.Text = "حرکت";
            animationCheck.AutoSize = true;
            animationCheck.Location = new Point(495, 12);
            animationCheck.CheckedChanged += new EventHandler(animationCheck_CheckedChanged);

            Label functionLabel = CreateLabel("تابع:", 575, 12);
            functionCombo = new ComboBox();
            functionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            functionCombo.Location = new Point(615, 8);
            functionCombo.Size = new Size(135, 25);
            functionCombo.Items.Add("x^2");
            functionCombo.Items.Add("x^3");
            functionCombo.Items.Add("x^3 - 3x");
            functionCombo.Items.Add("sin(x)");
            functionCombo.Items.Add("cos(x)");
            functionCombo.Items.Add("e^x");
            functionCombo.SelectedIndex = 0;

            applyFunctionButton = new Button();
            applyFunctionButton.Text = "اعمال تابع";
            applyFunctionButton.Location = new Point(755, 7);
            applyFunctionButton.Size = new Size(85, 27);
            applyFunctionButton.Click += new EventHandler(applyFunctionButton_Click);

            Label rangeLabel = CreateLabel("بازه x:", 850, 12);
            xminBox = CreateTextBox("-5", 895, 8, 55);
            xmaxBox = CreateTextBox("5", 955, 8, 55);

            Label pointLabel = CreateLabel("نقطه:", 1015, 12);
            xBox = CreateTextBox("1", 1055, 8, 55);

            Label hLabel = CreateLabel("h:", 15, 50);
            hBox = CreateTextBox("1", 38, 46, 55);

            Label aLabel = CreateLabel("a:", 110, 50);
            aBox = CreateTextBox("0", 132, 46, 55);

            Label bLabel = CreateLabel("b:", 205, 50);
            bBox = CreateTextBox("2", 227, 46, 55);

            Button refreshButton = new Button();
            refreshButton.Text = "اعمال مقادیر";
            refreshButton.Location = new Point(292, 45);
            refreshButton.Size = new Size(90, 27);
            refreshButton.Click += new EventHandler(refreshButton_Click);

            infoLabel = new Label();
            infoLabel.Location = new Point(400, 48);
            infoLabel.AutoSize = true;

            calculusLabel = new Label();
            calculusLabel.Location = new Point(15, 75);
            calculusLabel.Size = new Size(1160, 60);
            calculusLabel.Font = new Font("Tahoma", 9, FontStyle.Regular);

            canvas = new Panel();
            canvas.Location = new Point(10, 140);
            canvas.Size = new Size(1180, 645);
            canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                            AnchorStyles.Left | AnchorStyles.Right;
            canvas.BackColor = Color.White;
            canvas.Paint += new PaintEventHandler(canvas_Paint);
            canvas.MouseDown += new MouseEventHandler(canvas_MouseDown);
            canvas.MouseMove += new MouseEventHandler(canvas_MouseMove);
            canvas.MouseUp += new MouseEventHandler(canvas_MouseUp);

            timer = new Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(timer_Tick);

            this.Controls.Add(angleLabel);
            this.Controls.Add(angleBox);
            this.Controls.Add(angleApplyButton);
            this.Controls.Add(angleBar);
            this.Controls.Add(animationCheck);
            this.Controls.Add(functionLabel);
            this.Controls.Add(functionCombo);
            this.Controls.Add(applyFunctionButton);
            this.Controls.Add(rangeLabel);
            this.Controls.Add(xminBox);
            this.Controls.Add(xmaxBox);
            this.Controls.Add(pointLabel);
            this.Controls.Add(xBox);
            this.Controls.Add(hLabel);
            this.Controls.Add(hBox);
            this.Controls.Add(aLabel);
            this.Controls.Add(aBox);
            this.Controls.Add(bLabel);
            this.Controls.Add(bBox);
            this.Controls.Add(refreshButton);
            this.Controls.Add(infoLabel);
            this.Controls.Add(calculusLabel);
            this.Controls.Add(canvas);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            return label;
        }

        private TextBox CreateTextBox(string text, int x, int y, int width)
        {
            TextBox box = new TextBox();
            box.Text = text;
            box.Location = new Point(x, y);
            box.Size = new Size(width, 25);
            return box;
        }

        private void angleApplyButton_Click(object sender, EventArgs e)
        {
            double value;

            if (!double.TryParse(angleBox.Text, out value))
            {
                MessageBox.Show("مقدار زاویه معتبر نیست.");
                return;
            }

            if (value < 0.0 || value > 360.0)
            {
                MessageBox.Show("زاویه باید بین 0 و 360 درجه باشد.");
                return;
            }

            angleDeg = value;
            angleBar.Value = (int)Math.Round(value);
            UpdateModel();
            canvas.Invalidate();
        }

        private void angleBar_ValueChanged(object sender, EventArgs e)
        {
            angleDeg = angleBar.Value;
            angleBox.Text = angleDeg.ToString("0");
            UpdateModel();
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

        private void applyFunctionButton_Click(object sender, EventArgs e)
        {
            string name = functionCombo.SelectedItem as string;

            if (name == null)
                return;

            functionModel = new CalculusFunctionModel(name);
            ApplyNumericInputs();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ApplyNumericInputs();
        }

        private void ApplyNumericInputs()
        {
            double value;

            if (!double.TryParse(xminBox.Text, out value))
            {
                MessageBox.Show("Xmin معتبر نیست.");
                return;
            }
            xMin = value;

            if (!double.TryParse(xmaxBox.Text, out value))
            {
                MessageBox.Show("Xmax معتبر نیست.");
                return;
            }
            xMax = value;

            if (xMax <= xMin)
            {
                MessageBox.Show("Xmax باید از Xmin بزرگ‌تر باشد.");
                return;
            }

            if (!double.TryParse(xBox.Text, out value))
            {
                MessageBox.Show("مقدار نقطه معتبر نیست.");
                return;
            }
            pointX = Clamp(value, xMin, xMax);

            if (!double.TryParse(hBox.Text, out value))
            {
                MessageBox.Show("h معتبر نیست.");
                return;
            }
            secantH = Math.Abs(value);

            if (!double.TryParse(aBox.Text, out value))
            {
                MessageBox.Show("a معتبر نیست.");
                return;
            }
            integralA = value;

            if (!double.TryParse(bBox.Text, out value))
            {
                MessageBox.Show("b معتبر نیست.");
                return;
            }
            integralB = value;

            if (integralB < integralA)
            {
                double temp = integralA;
                integralA = integralB;
                integralB = temp;
            }

            xBox.Text = pointX.ToString("0.###");
            UpdateModel();
            canvas.Invalidate();
        }

        private void UpdateModel()
        {
            double angle = angleDeg * Math.PI / 180.0;

            orbitPoint = new Point2D(
                unitCircle.Center.X + unitCircle.Radius * Math.Cos(angle),
                unitCircle.Center.Y + unitCircle.Radius * Math.Sin(angle));

            projectionPoint = new Point2D(orbitPoint.X, 0.0);
            radiusLine = new Line2D(unitCircle.Center, orbitPoint);

            double y = functionModel.Evaluate(pointX);
            double derivative = functionModel.Derivative(pointX);
            double area = functionModel.Integral(integralA, integralB);

            infoLabel.Text =
                "θ = " + angleDeg.ToString("0.##") +
                "°    x = " + orbitPoint.X.ToString("0.000") +
                "    y = " + orbitPoint.Y.ToString("0.000");

            calculusLabel.Text =
                "تابع: " + functionModel.Formula +
                "    نقطه: (" + pointX.ToString("0.###") + ", " + y.ToString("0.###") + ")" +
                "    مشتق: f'(x) ≈ " + derivative.ToString("0.###") +
                "    شیب مماس = " + derivative.ToString("0.###") +
                "    انتگرال [" + integralA.ToString("0.###") + ", " +
                integralB.ToString("0.###") + "] = " + area.ToString("0.####");
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = canvas.ClientSize.Width;
            int h = canvas.ClientSize.Height;

            float circleCx = Math.Min(250.0f, w * 0.22f);
            float circleCy = h * 0.46f;
            float circleRadius = Math.Min(150.0f, h * 0.30f);

            DrawUnitCircle(g, circleCx, circleCy, circleRadius);

            float graphLeft = w * 0.43f;
            float graphRight = w - 25.0f;
            float graphTop = 35.0f;
            float graphBottom = h - 45.0f;

            DrawFunctionGraph(
                g,
                graphLeft,
                graphRight,
                graphTop,
                graphBottom);
        }

        private void DrawUnitCircle(
            Graphics g,
            float cx,
            float cy,
            float radius)
        {
            g.DrawLine(Pens.Gray, cx - radius - 25, cy, cx + radius + 25, cy);
            g.DrawLine(Pens.Gray, cx, cy - radius - 25, cx, cy + radius + 25);

            g.DrawEllipse(
                Pens.Black,
                cx - radius,
                cy - radius,
                radius * 2.0f,
                radius * 2.0f);

            float px = cx + (float)(orbitPoint.X * radius);
            float py = cy - (float)(orbitPoint.Y * radius);

            g.DrawLine(Pens.DarkBlue, cx, cy, px, py);
            g.DrawLine(Pens.DarkGreen, px, py, px, cy);

            g.FillEllipse(Brushes.Black, px - 5, py - 5, 10, 10);

            g.DrawString(
                "P",
                this.Font,
                Brushes.Black,
                px + 8,
                py - 18);

            g.DrawString(
                "O",
                this.Font,
                Brushes.Black,
                cx - 15,
                cy + 8);

            g.DrawString(
                angleDeg.ToString("0.##") + "°",
                this.Font,
                Brushes.Black,
                cx + 20,
                cy - radius - 30);

            g.DrawString(
                "دایره مثلثاتی",
                this.Font,
                Brushes.Black,
                cx - 45,
                cy + radius + 18);

            g.DrawString(
                "تصویر روی محور",
                this.Font,
                Brushes.DarkGreen,
                px + 8,
                cy + 8);

            g.DrawString(
                "sin(θ) = " + orbitPoint.Y.ToString("0.000"),
                this.Font,
                Brushes.DarkRed,
                cx - 60,
                cy - radius - 55);

            g.DrawString(
                "cos(θ) = " + orbitPoint.X.ToString("0.000"),
                this.Font,
                Brushes.DarkBlue,
                cx - 60,
                cy + radius + 38);
        }

        private void DrawFunctionGraph(
            Graphics g,
            float left,
            float right,
            float top,
            float bottom)
        {
            double[] yRange = CalculateYRange();
            double yMin = yRange[0];
            double yMax = yRange[1];

            if (Math.Abs(yMax - yMin) < 0.000001)
            {
                yMin -= 1.0;
                yMax += 1.0;
            }

            float xAxisY = MapY(0.0, yMin, yMax, top, bottom);
            float yAxisX = MapX(0.0, left, right);

            if (xAxisY >= top && xAxisY <= bottom)
                g.DrawLine(Pens.Gray, left, xAxisY, right, xAxisY);

            if (yAxisX >= left && yAxisX <= right)
                g.DrawLine(Pens.Gray, yAxisX, top, yAxisX, bottom);

            DrawIntegralArea(
                g,
                left,
                right,
                top,
                bottom,
                yMin,
                yMax);

            PointF previous = PointF.Empty;
            bool hasPrevious = false;

            int samples = 500;

            for (int i = 0; i <= samples; i++)
            {
                double x = xMin + (xMax - xMin) * i / (double)samples;
                double y = functionModel.Evaluate(x);

                if (double.IsNaN(y) || double.IsInfinity(y))
                {
                    hasPrevious = false;
                    continue;
                }

                float sx = MapX(x, left, right);
                float sy = MapY(y, yMin, yMax, top, bottom);

                PointF current = new PointF(sx, sy);

                if (hasPrevious)
                    g.DrawLine(Pens.DarkRed, previous, current);

                previous = current;
                hasPrevious = true;
            }

            double fx = functionModel.Evaluate(pointX);
            double derivative = functionModel.Derivative(pointX);

            float px = MapX(pointX, left, right);
            float py = MapY(fx, yMin, yMax, top, bottom);

            double tangentRange = (xMax - xMin) * 0.18;
            double tangentX1 = pointX - tangentRange;
            double tangentX2 = pointX + tangentRange;

            double tangentY1 = fx + derivative * (tangentX1 - pointX);
            double tangentY2 = fx + derivative * (tangentX2 - pointX);

            g.DrawLine(
                Pens.DarkBlue,
                MapX(tangentX1, left, right),
                MapY(tangentY1, yMin, yMax, top, bottom),
                MapX(tangentX2, left, right),
                MapY(tangentY2, yMin, yMax, top, bottom));

            double secantX2 = Clamp(pointX + secantH, xMin, xMax);
            double secantY2 = functionModel.Evaluate(secantX2);

            if (Math.Abs(secantX2 - pointX) > 0.000001)
            {
                g.DrawLine(
                    Pens.DarkGreen,
                    px,
                    py,
                    MapX(secantX2, left, right),
                    MapY(secantY2, yMin, yMax, top, bottom));
            }

            g.FillEllipse(Brushes.Black, px - 6, py - 6, 12, 12);
            g.DrawString(
                "P",
                this.Font,
                Brushes.Black,
                px + 8,
                py - 20);

            g.DrawString(
                "تابع: " + functionModel.Formula,
                this.Font,
                Brushes.DarkRed,
                left + 10,
                top + 5);

            g.DrawString(
                "مماس: y - f(a) = f'(a)(x-a)",
                this.Font,
                Brushes.DarkBlue,
                left + 10,
                top + 25);

            g.DrawString(
                "قاطع: h = " + secantH.ToString("0.###"),
                this.Font,
                Brushes.DarkGreen,
                left + 10,
                top + 45);

            g.DrawString(
                "انتگرال و مساحت زیر منحنی",
                this.Font,
                Brushes.Purple,
                right - 180,
                bottom + 8);

            g.DrawString(
                "x = " + pointX.ToString("0.###") +
                "   f(x) = " + fx.ToString("0.###") +
                "   f'(x) = " + derivative.ToString("0.###"),
                this.Font,
                Brushes.Black,
                left + 10,
                bottom + 8);
        }

        private void DrawIntegralArea(
            Graphics g,
            float left,
            float right,
            float top,
            float bottom,
            double yMin,
            double yMax)
        {
            if (integralB <= integralA)
                return;

            ListPointBuilder builder = new ListPointBuilder();

            int samples = 120;
            float axisY = MapY(0.0, yMin, yMax, top, bottom);

            builder.Add(
                new PointF(
                    MapX(integralA, left, right),
                    axisY));

            for (int i = 0; i <= samples; i++)
            {
                double x =
                    integralA +
                    (integralB - integralA) * i / (double)samples;

                double y = functionModel.Evaluate(x);

                if (double.IsNaN(y) || double.IsInfinity(y))
                    continue;

                builder.Add(
                    new PointF(
                        MapX(x, left, right),
                        MapY(y, yMin, yMax, top, bottom)));
            }

            builder.Add(
                new PointF(
                    MapX(integralB, left, right),
                    axisY));

            if (builder.Count >= 3)
                g.FillPolygon(
                    new SolidBrush(Color.FromArgb(55, Color.Purple)),
                    builder.ToArray());
        }

        private double[] CalculateYRange()
        {
            double min = double.MaxValue;
            double max = double.MinValue;

            int samples = 300;

            for (int i = 0; i <= samples; i++)
            {
                double x =
                    xMin +
                    (xMax - xMin) * i / (double)samples;

                double y = functionModel.Evaluate(x);

                if (double.IsNaN(y) || double.IsInfinity(y))
                    continue;

                if (y < min)
                    min = y;

                if (y > max)
                    max = y;
            }

            if (min == double.MaxValue)
            {
                min = -1.0;
                max = 1.0;
            }

            double padding = (max - min) * 0.15;

            if (padding < 1.0)
                padding = 1.0;

            return new double[]
            {
                min - padding,
                max + padding
            };
        }

        private float MapX(
            double x,
            float left,
            float right)
        {
            return left +
                (float)((x - xMin) / (xMax - xMin)) *
                (right - left);
        }

        private float MapY(
            double y,
            double yMin,
            double yMax,
            float top,
            float bottom)
        {
            return bottom -
                (float)((y - yMin) / (yMax - yMin)) *
                (bottom - top);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            double[] range = CalculateYRange();
            float left = canvas.ClientSize.Width * 0.43f;
            float right = canvas.ClientSize.Width - 25.0f;
            float top = 35.0f;
            float bottom = canvas.ClientSize.Height - 45.0f;

            float px = MapX(pointX, left, right);
            float py = MapY(
                functionModel.Evaluate(pointX),
                range[0],
                range[1],
                top,
                bottom);

            double distance =
                Math.Sqrt(
                    (e.X - px) * (e.X - px) +
                    (e.Y - py) * (e.Y - py));

            if (distance <= 14.0)
                draggingFunctionPoint = true;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingFunctionPoint)
                return;

            float left = canvas.ClientSize.Width * 0.43f;
            float right = canvas.ClientSize.Width - 25.0f;

            double x =
                xMin +
                (e.X - left) / (right - left) *
                (xMax - xMin);

            pointX = Clamp(x, xMin, xMax);
            xBox.Text = pointX.ToString("0.###");

            UpdateModel();
            canvas.Invalidate();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            draggingFunctionPoint = false;
        }

        private double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        private class CalculusFunctionModel
        {
            private string name;

            public CalculusFunctionModel(string name)
            {
                this.name = name;
            }

            public string Formula
            {
                get
                {
                    return name;
                }
            }

            public double Evaluate(double x)
            {
                switch (name)
                {
                    case "x^2":
                        return x * x;

                    case "x^3":
                        return x * x * x;

                    case "x^3 - 3x":
                        return x * x * x - 3.0 * x;

                    case "sin(x)":
                        return Math.Sin(x);

                    case "cos(x)":
                        return Math.Cos(x);

                    case "e^x":
                        return Math.Exp(x);

                    default:
                        return x * x;
                }
            }

            public double Derivative(double x)
            {
                switch (name)
                {
                    case "x^2":
                        return 2.0 * x;

                    case "x^3":
                        return 3.0 * x * x;

                    case "x^3 - 3x":
                        return 3.0 * x * x - 3.0;

                    case "sin(x)":
                        return Math.Cos(x);

                    case "cos(x)":
                        return -Math.Sin(x);

                    case "e^x":
                        return Math.Exp(x);

                    default:
                        return 2.0 * x;
                }
            }

            public double Integral(double a, double b)
            {
                if (Math.Abs(b - a) < 0.0000001)
                    return 0.0;

                int n = 400;

                if (n % 2 != 0)
                    n++;

                double h = (b - a) / n;
                double sum = Evaluate(a) + Evaluate(b);

                int i;

                for (i = 1; i < n; i++)
                {
                    double x = a + i * h;

                    if (i % 2 == 0)
                        sum += 2.0 * Evaluate(x);
                    else
                        sum += 4.0 * Evaluate(x);
                }

                return sum * h / 3.0;
            }
        }

        private class ListPointBuilder
        {
            private System.Collections.Generic.List<PointF> points;

            public ListPointBuilder()
            {
                points =
                    new System.Collections.Generic.List<PointF>();
            }

            public int Count
            {
                get { return points.Count; }
            }

            public void Add(PointF point)
            {
                points.Add(point);
            }

            public PointF[] ToArray()
            {
                return points.ToArray();
            }
        }
    }
}
