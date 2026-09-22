using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NP.NumericalModel.Geometry;

namespace NP.NumericalModel.ConsoleSample
{
    public class ObliqueGeometryMotionDemo : Form
    {
        private Timer timer;
        private TrackBar angleBar;
        private TextBox angleBox;
        private ComboBox sideCombo;
        private CheckBox zVisibleCheck;
        private CheckBox animationCheck;
        private ComboBox functionCombo;
        private TextBox functionBox;
        private Panel canvas;
        private Label infoLabel;

        private double angleDeg;
        private int side;
        private bool zVisible;
        private bool animate;
        private CalculusFunctionModel functionModel;

        public ObliqueGeometryMotionDemo()
        {
            Text = "NP.NumericalModel - 2D / Z Oblique Motion";
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Tahoma", 9, FontStyle.Regular);
            ClientSize = new Size(1120, 700);
            MinimumSize = new Size(1000, 620);

            angleDeg = 90.0;
            side = 1;
            zVisible = true;
            functionModel = new CalculusFunctionModel("sin(x)");

            InitializeControls();
            UpdateModel();
        }

        private void InitializeControls()
        {
            Label angleLabel = CreateLabel("زاویه Z:", 15, 12);
            angleBox = CreateTextBox("90", 70, 8, 55);

            Button applyAngle = new Button();
            applyAngle.Text = "اعمال";
            applyAngle.Location = new Point(130, 7);
            applyAngle.Size = new Size(55, 27);
            applyAngle.Click += new EventHandler(applyAngle_Click);

            angleBar = new TrackBar();
            angleBar.Minimum = 0;
            angleBar.Maximum = 180;
            angleBar.TickFrequency = 15;
            angleBar.Value = 90;
            angleBar.Location = new Point(190, 3);
            angleBar.Size = new Size(280, 45);
            angleBar.ValueChanged += new EventHandler(angleBar_ValueChanged);

            Label sideLabel = CreateLabel("ناحیه:", 485, 12);
            sideCombo = new ComboBox();
            sideCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            sideCombo.Items.Add("+1");
            sideCombo.Items.Add("-1");
            sideCombo.SelectedIndex = 0;
            sideCombo.Location = new Point(530, 8);
            sideCombo.Size = new Size(60, 25);
            sideCombo.SelectedIndexChanged += new EventHandler(sideCombo_SelectedIndexChanged);

            zVisibleCheck = new CheckBox();
            zVisibleCheck.Text = "محور Z";
            zVisibleCheck.Checked = true;
            zVisibleCheck.AutoSize = true;
            zVisibleCheck.Location = new Point(605, 11);
            zVisibleCheck.CheckedChanged += new EventHandler(zVisibleCheck_CheckedChanged);

            animationCheck = new CheckBox();
            animationCheck.Text = "حرکت";
            animationCheck.AutoSize = true;
            animationCheck.Location = new Point(685, 11);
            animationCheck.CheckedChanged += new EventHandler(animationCheck_CheckedChanged);

            Label functionLabel = CreateLabel("تابع:", 755, 12);
            functionCombo = new ComboBox();
            functionCombo.DropDownStyle = ComboBoxStyle.DropDown;
            functionCombo.Items.Add("sin(x)");
            functionCombo.Items.Add("cos(x)");
            functionCombo.Items.Add("x^2");
            functionCombo.Items.Add("x^3 - 3x");
            functionCombo.SelectedIndex = 0;
            functionCombo.Location = new Point(795, 8);
            functionCombo.Size = new Size(120, 25);
            functionCombo.SelectedIndexChanged += new EventHandler(functionCombo_SelectedIndexChanged);

            functionBox = CreateTextBox("sin(x)", 920, 8, 100);
            Button applyFunction = new Button();
            applyFunction.Text = "اعمال";
            applyFunction.Location = new Point(1025, 7);
            applyFunction.Size = new Size(65, 27);
            applyFunction.Click += new EventHandler(applyFunction_Click);

            infoLabel = new Label();
            infoLabel.Location = new Point(15, 52);
            infoLabel.AutoSize = true;

            canvas = new Panel();
            canvas.Location = new Point(10, 80);
            canvas.Size = new Size(1100, 605);
            canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                            AnchorStyles.Left | AnchorStyles.Right;
            canvas.BackColor = Color.White;
            canvas.Paint += new PaintEventHandler(canvas_Paint);

            timer = new Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(timer_Tick);

            Controls.Add(angleLabel);
            Controls.Add(angleBox);
            Controls.Add(applyAngle);
            Controls.Add(angleBar);
            Controls.Add(sideLabel);
            Controls.Add(sideCombo);
            Controls.Add(zVisibleCheck);
            Controls.Add(animationCheck);
            Controls.Add(functionLabel);
            Controls.Add(functionCombo);
            Controls.Add(functionBox);
            Controls.Add(applyFunction);
            Controls.Add(infoLabel);
            Controls.Add(canvas);
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

        private void applyAngle_Click(object sender, EventArgs e)
        {
            double value;

            if (!double.TryParse(angleBox.Text, out value) ||
                value < 0.0 || value > 180.0)
            {
                MessageBox.Show("زاویه Z باید بین 0 و 180 درجه باشد.");
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

        private void sideCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            side = sideCombo.SelectedIndex == 0 ? 1 : -1;
            canvas.Invalidate();
        }

        private void zVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            zVisible = zVisibleCheck.Checked;
            canvas.Invalidate();
        }

        private void animationCheck_CheckedChanged(object sender, EventArgs e)
        {
            animate = animationCheck.Checked;
            if (animate)
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

        private void functionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (functionCombo.SelectedIndex >= 0)
                functionBox.Text = functionCombo.SelectedItem.ToString();
        }

        private void applyFunction_Click(object sender, EventArgs e)
        {
            try
            {
                CalculusFunctionModel candidate =
                    new CalculusFunctionModel(functionBox.Text.Trim());

                candidate.Validate();
                functionModel = candidate;
                canvas.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "تابع قابل ترسیم نیست.\r\n\r\n" + ex.Message,
                    "خطای تابع",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateModel()
        {
            infoLabel.Text =
                "ZAngle = " + angleDeg.ToString("0.##") +
                "°    Side = " + side.ToString() +
                "    Mapping: (x,y) → (0,y,z=x)";
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = canvas.ClientSize.Width;
            int h = canvas.ClientSize.Height;
            float divider = w * 0.50f;

            g.DrawLine(Pens.LightGray, divider, 10, divider, h - 10);

            DrawTwoDimensional(g, 10, 10, divider - 20, h - 20);
            DrawOblique(g, divider + 10, 10, w - divider - 20, h - 20);
        }

        private void DrawTwoDimensional(Graphics g, float left, float top, float width, float height)
        {
            float cx = left + width * 0.50f;
            float sx = width * 0.40f;
            float sy = height * 0.36f;

            int samples = 500;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            double[] values = new double[samples + 1];
            bool[] valid = new bool[samples + 1];

            for (int i = 0; i <= samples; i++)
            {
                double x = -Math.PI * 2.0 + 4.0 * Math.PI * i / samples;
                double y = functionModel.Evaluate(x);

                if (double.IsNaN(y) || double.IsInfinity(y) || Math.Abs(y) > 100000.0)
                    continue;

                values[i] = y;
                valid[i] = true;

                if (y < yMin)
                    yMin = y;

                if (y > yMax)
                    yMax = y;
            }

            if (yMin == double.MaxValue)
            {
                g.DrawString(
                    "تابع در بازه فعلی مقدار قابل ترسیمی ندارد.",
                    Font, Brushes.DarkRed, left + 10, top + 10);
                return;
            }

            double maxAbsY = Math.Max(Math.Abs(yMin), Math.Abs(yMax));
            if (maxAbsY < 0.000000000001)
                maxAbsY = 1.0;

            float cy = top + height * 0.52f;
            sy = (float)(height * 0.36 / maxAbsY);

            DrawAxes(g, cx, cy, sx, height * 0.36f, true, "X", "Y");

            PointF previous = PointF.Empty;
            bool hasPrevious = false;

            for (int i = 0; i <= samples; i++)
            {
                if (!valid[i])
                {
                    hasPrevious = false;
                    continue;
                }

                double x = -Math.PI * 2.0 + 4.0 * Math.PI * i / samples;
                double y = values[i];

                PointF current = new PointF(
                    cx + (float)(x / (2.0 * Math.PI) * sx),
                    cy - (float)(y * sy));

                if (hasPrevious)
                    g.DrawLine(Pens.DarkRed, previous, current);

                previous = current;
                hasPrevious = true;
            }

            g.DrawString("ترسیم اصلی 2D:  y = " + functionModel.Formula,
                Font, Brushes.DarkRed, left + 10, top + 10);
        }

        private void DrawOblique(Graphics g, float left, float top, float width, float height)
        {
            float ox = left + width * 0.46f;
            float oy = top + height * 0.52f;
            float unit = Math.Min(width, height) * 0.16f;

            double radians = angleDeg * Math.PI / 180.0;
            float zLength = unit * 2.8f;
            PointF zEnd = new PointF(
                ox + side * (float)(zLength * Math.Sin(radians)),
                oy - side * (float)(zLength * Math.Cos(radians)));

            float yLength = unit * 2.6f;

            g.DrawLine(Pens.Gray, ox, oy, ox, oy - yLength);
            g.DrawLine(Pens.Gray, ox, oy, ox, oy + yLength);

            if (zVisible)
            {
                using (Pen zPen = new Pen(Color.DarkBlue, 2.0f))
                {
                    g.DrawLine(zPen, ox, oy, zEnd.X, zEnd.Y);
                }

                g.DrawString("Z", Font, Brushes.DarkBlue, zEnd.X + 5, zEnd.Y - 10);
            }

            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(
                    new ObliqueProjectionSettings(angleDeg, side, 1.0));

            PointF previous = PointF.Empty;
            bool hasPrevious = false;
            int samples = 500;

            for (int i = 0; i <= samples; i++)
            {
                double x = -Math.PI * 2.0 + 4.0 * Math.PI * i / samples;
                double y = functionModel.Evaluate(x);

                if (double.IsNaN(y) || double.IsInfinity(y) || Math.Abs(y) > 100000.0)
                {
                    hasPrevious = false;
                    continue;
                }

                Point2D projected = projection.Project(new Point2D(x, y));

                PointF current = new PointF(
                    ox + (float)(projected.X * unit),
                    oy - (float)(projected.Y * unit));

                if (hasPrevious)
                    g.DrawLine(Pens.DarkGreen, previous, current);

                previous = current;
                hasPrevious = true;
            }

            g.DrawString(
                "همان منحنی در Y-Z | θ = " + angleDeg.ToString("0.##") +
                "° | Side = " + side.ToString(),
                Font, Brushes.DarkGreen, left + 10, top + 10);

            g.DrawString(
                zVisible ? "محور Z: Visible" : "محور Z: Hidden",
                Font, Brushes.Black, left + 10, top + 30);
        }

        private void DrawAxes(Graphics g, float cx, float cy, float sx, float sy, bool visible, string xName, string yName)
        {
            g.DrawLine(Pens.Gray, cx - sx, cy, cx + sx, cy);
            g.DrawLine(Pens.Gray, cx, cy - sy, cx, cy + sy);

            g.DrawString(xName, Font, Brushes.Black, cx + sx - 15, cy + 5);
            g.DrawString(yName, Font, Brushes.Black, cx + 5, cy - sy);
        }

        private class CalculusFunctionModel
        {
            private string expression;
            private FunctionExpressionParser parser;

            public CalculusFunctionModel(string expression)
            {
                if (expression == null)
                    throw new ArgumentNullException("expression");

                this.expression = expression.Trim();
                if (this.expression.Length == 0)
                    throw new FormatException("عبارت تابع خالی است.");

                parser = new FunctionExpressionParser(this.expression);
            }

            public string Formula
            {
                get { return expression; }
            }

            public void Validate()
            {
                parser.Parse();
            }

            public double Evaluate(double x)
            {
                return parser.Evaluate(x);
            }
        }

        private class FunctionExpressionParser
        {
            private string text;
            private int position;
            private Node root;
            private bool parsed;

            public FunctionExpressionParser(string text)
            {
                this.text = text;
            }

            public void Parse()
            {
                if (parsed)
                    return;

                position = 0;
                root = ParseExpression();
                SkipSpaces();

                if (position < text.Length)
                    throw Error("عبارت اضافی یا نویسه ناشناخته در موقعیت " + position.ToString() + ".");

                parsed = true;
            }

            public double Evaluate(double x)
            {
                Parse();
                return root.Evaluate(x);
            }

            private Node ParseExpression()
            {
                Node left = ParseTerm();

                while (true)
                {
                    SkipSpaces();

                    if (Match('+'))
                        left = new BinaryNode('+', left, ParseTerm());
                    else if (Match('-'))
                        left = new BinaryNode('-', left, ParseTerm());
                    else
                        return left;
                }
            }

            private Node ParseTerm()
            {
                Node left = ParsePower();

                while (true)
                {
                    SkipSpaces();

                    if (Match('*'))
                        left = new BinaryNode('*', left, ParsePower());
                    else if (Match('/'))
                        left = new BinaryNode('/', left, ParsePower());
                    else if (IsImplicitMultiplication())
                        left = new BinaryNode('*', left, ParsePower());
                    else
                        return left;
                }
            }

            private Node ParsePower()
            {
                Node left = ParseUnary();
                SkipSpaces();

                if (Match('^'))
                    return new BinaryNode('^', left, ParsePower());

                return left;
            }

            private Node ParseUnary()
            {
                SkipSpaces();

                if (Match('+'))
                    return ParseUnary();

                if (Match('-'))
                    return new UnaryNode('-', ParseUnary());

                return ParsePrimary();
            }

            private Node ParsePrimary()
            {
                SkipSpaces();

                if (position >= text.Length)
                    throw Error("انتظار یک عدد، x یا تابع وجود داشت.");

                if (Match('('))
                {
                    Node inside = ParseExpression();
                    Expect(')');
                    return inside;
                }

                if (char.IsDigit(text[position]) || text[position] == '.')
                    return new NumberNode(ParseNumber());

                if (char.IsLetter(text[position]))
                {
                    string name = ParseName();

                    if (string.Equals(name, "x", StringComparison.OrdinalIgnoreCase))
                        return new VariableNode();

                    if (string.Equals(name, "pi", StringComparison.OrdinalIgnoreCase))
                        return new NumberNode(Math.PI);

                    if (string.Equals(name, "e", StringComparison.OrdinalIgnoreCase))
                        return new NumberNode(Math.E);

                    SkipSpaces();

                    if (Match('('))
                    {
                        Node argument = ParseExpression();
                        Expect(')');
                        return new FunctionNode(name, argument);
                    }

                    throw Error("تابع یا ثابت ناشناخته: " + name);
                }

                throw Error("نویسه نامعتبر: " + text[position]);
            }

            private bool IsImplicitMultiplication()
            {
                SkipSpaces();

                if (position >= text.Length)
                    return false;

                char ch = text[position];
                return ch == '(' || ch == '.' || char.IsDigit(ch) || char.IsLetter(ch);
            }

            private double ParseNumber()
            {
                int start = position;
                bool hasDigits = false;

                while (position < text.Length && char.IsDigit(text[position]))
                {
                    hasDigits = true;
                    position++;
                }

                if (position < text.Length && text[position] == '.')
                {
                    position++;

                    while (position < text.Length && char.IsDigit(text[position]))
                    {
                        hasDigits = true;
                        position++;
                    }
                }

                if (!hasDigits)
                    throw Error("عدد معتبر نیست.");

                string value = text.Substring(start, position - start);
                double result;

                if (!double.TryParse(value,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out result))
                    throw Error("عدد نامعتبر: " + value);

                return result;
            }

            private string ParseName()
            {
                int start = position;

                while (position < text.Length &&
                       (char.IsLetter(text[position]) || char.IsDigit(text[position])))
                    position++;

                return text.Substring(start, position - start);
            }

            private void Expect(char ch)
            {
                SkipSpaces();
                if (!Match(ch))
                    throw Error("انتظار '" + ch + "' وجود داشت.");
            }

            private bool Match(char ch)
            {
                if (position < text.Length && text[position] == ch)
                {
                    position++;
                    return true;
                }

                return false;
            }

            private void SkipSpaces()
            {
                while (position < text.Length && char.IsWhiteSpace(text[position]))
                    position++;
            }

            private FormatException Error(string message)
            {
                return new FormatException(message);
            }

            private abstract class Node
            {
                public abstract double Evaluate(double x);
            }

            private class NumberNode : Node
            {
                private double value;
                public NumberNode(double value) { this.value = value; }
                public override double Evaluate(double x) { return value; }
            }

            private class VariableNode : Node
            {
                public override double Evaluate(double x) { return x; }
            }

            private class UnaryNode : Node
            {
                private char operation;
                private Node value;

                public UnaryNode(char operation, Node value)
                {
                    this.operation = operation;
                    this.value = value;
                }

                public override double Evaluate(double x)
                {
                    double v = value.Evaluate(x);
                    return operation == '-' ? -v : v;
                }
            }

            private class BinaryNode : Node
            {
                private char operation;
                private Node left;
                private Node right;

                public BinaryNode(char operation, Node left, Node right)
                {
                    this.operation = operation;
                    this.left = left;
                    this.right = right;
                }

                public override double Evaluate(double x)
                {
                    double a = left.Evaluate(x);
                    double b = right.Evaluate(x);

                    switch (operation)
                    {
                        case '+': return a + b;
                        case '-': return a - b;
                        case '*': return a * b;
                        case '/': return Math.Abs(b) < 0.000000000000001 ? double.NaN : a / b;
                        case '^': return Math.Pow(a, b);
                        default: return double.NaN;
                    }
                }
            }

            private class FunctionNode : Node
            {
                private string name;
                private Node argument;

                public FunctionNode(string name, Node argument)
                {
                    this.name = name;
                    this.argument = argument;

                    string normalized = name.ToLowerInvariant();
                    if (normalized != "sin" &&
                        normalized != "cos" &&
                        normalized != "tan" &&
                        normalized != "sqrt" &&
                        normalized != "abs" &&
                        normalized != "exp" &&
                        normalized != "log" &&
                        normalized != "ln" &&
                        normalized != "log10")
                        throw new FormatException("تابع ناشناخته: " + name);
                }

                public override double Evaluate(double x)
                {
                    double value = argument.Evaluate(x);

                    switch (name.ToLowerInvariant())
                    {
                        case "sin": return Math.Sin(value);
                        case "cos": return Math.Cos(value);
                        case "tan": return Math.Tan(value);
                        case "sqrt": return value < 0.0 ? double.NaN : Math.Sqrt(value);
                        case "abs": return Math.Abs(value);
                        case "exp": return Math.Exp(value);
                        case "log":
                        case "ln": return value <= 0.0 ? double.NaN : Math.Log(value);
                        case "log10": return value <= 0.0 ? double.NaN : Math.Log10(value);
                        default: return double.NaN;
                    }
                }
            }
        }
    }
}