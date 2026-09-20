using System;
using System.Globalization;
using System.Text;
using NP.NumericalModel.Geometry;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Visualization
{
    public static class SvgGeometryRenderer
    {
        public static string RenderDemo(
            Circle circle,
            CartesianPlane plane,
            Spiral spiral,
            Line3D line,
            Ratio ratio,
            int width,
            int height)
        {
            if (circle == null)
                throw new ArgumentNullException("circle");
            if (plane == null)
                throw new ArgumentNullException("plane");
            if (spiral == null)
                throw new ArgumentNullException("spiral");
            if (line == null)
                throw new ArgumentNullException("line");
            if (ratio == null)
                throw new ArgumentNullException("ratio");
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height");

            StringBuilder svg = new StringBuilder();

            double centerX = width / 2.0;
            double centerY = height / 2.0;
            double scale = Math.Min(width, height) / 18.0;
            double circleRadius = circle.Radius * scale;

            svg.AppendLine(
                "<svg xmlns=\"http://www.w3.org/2000/svg\" " +
                "width=\"" + width + "\" height=\"" + height + "\" " +
                "viewBox=\"0 0 " + width + " " + height + "\">");

            svg.AppendLine(
                "<rect x=\"0\" y=\"0\" width=\"" + width +
                "\" height=\"" + height + "\" fill=\"white\" />");

            svg.AppendLine(
                "<text x=\"" + Format(centerX) +
                "\" y=\"34\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"24\" font-weight=\"bold\">" +
                "NP.NumericalModel - Geometry</text>");

            svg.AppendLine(
                "<text x=\"" + Format(centerX) +
                "\" y=\"58\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"14\">" +
                "Circle + Cartesian Plane + Spiral + Trigonometric Waves</text>");

            // Cartesian axes.
            svg.AppendLine(
                "<line x1=\"0\" y1=\"" + Format(centerY) +
                "\" x2=\"" + Format(width) + "\" y2=\"" + Format(centerY) +
                "\" stroke=\"#888\" stroke-width=\"1.5\" />");

            svg.AppendLine(
                "<line x1=\"" + Format(centerX) + "\" y1=\"70\" " +
                "x2=\"" + Format(centerX) + "\" y2=\"" + Format(height - 45) +
                "\" stroke=\"#888\" stroke-width=\"1.5\" />");

            int tick;
            for (tick = -7; tick <= 7; tick++)
            {
                if (tick == 0)
                    continue;

                double x = centerX + tick * scale;
                double y = centerY - tick * scale;

                svg.AppendLine(
                    "<line x1=\"" + Format(x) + "\" y1=\"" +
                    Format(centerY - 5) + "\" x2=\"" + Format(x) +
                    "\" y2=\"" + Format(centerY + 5) +
                    "\" stroke=\"#888\" />");

                svg.AppendLine(
                    "<line x1=\"" + Format(centerX - 5) + "\" y1=\"" +
                    Format(y) + "\" x2=\"" + Format(centerX + 5) +
                    "\" y2=\"" + Format(y) + "\" stroke=\"#888\" />");
            }

            svg.AppendLine(
                "<text x=\"" + Format(width - 18) + "\" y=\"" +
                Format(centerY - 10) +
                "\" text-anchor=\"end\" font-family=\"Arial\" font-size=\"13\">X</text>");

            svg.AppendLine(
                "<text x=\"" + Format(centerX + 10) +
                "\" y=\"82\" font-family=\"Arial\" font-size=\"13\">Y</text>");

            // Circle and radius.
            svg.AppendLine(
                "<circle cx=\"" + Format(centerX) +
                "\" cy=\"" + Format(centerY) +
                "\" r=\"" + Format(circleRadius) +
                "\" fill=\"none\" stroke=\"#222\" stroke-width=\"3\" />");

            svg.AppendLine(
                "<line x1=\"" + Format(centerX) +
                "\" y1=\"" + Format(centerY) +
                "\" x2=\"" + Format(centerX + circleRadius) +
                "\" y2=\"" + Format(centerY) +
                "\" stroke=\"#555\" stroke-width=\"2\" />");

            svg.AppendLine(
                "<text x=\"" + Format(centerX + circleRadius / 2.0) +
                "\" y=\"" + Format(centerY - 10) +
                "\" text-anchor=\"middle\" font-family=\"Arial\" font-size=\"13\">r</text>");

            // pi/4 reference.
            double angle = Math.PI / 4.0;
            double px = centerX + circleRadius * Math.Cos(angle);
            double py = centerY - circleRadius * Math.Sin(angle);

            svg.AppendLine(
                "<line x1=\"" + Format(centerX) +
                "\" y1=\"" + Format(centerY) +
                "\" x2=\"" + Format(px) +
                "\" y2=\"" + Format(py) +
                "\" stroke=\"#555\" stroke-width=\"2\" />");

            svg.AppendLine(
                "<circle cx=\"" + Format(px) +
                "\" cy=\"" + Format(py) +
                "\" r=\"5\" fill=\"#222\" />");

            svg.AppendLine(
                "<text x=\"" + Format(px + 9) +
                "\" y=\"" + Format(py - 8) +
                "\" font-family=\"Arial\" font-size=\"13\">π/4</text>");

            // Spiral: circle continues outward into 5:6-style ring spacing.
            StringBuilder spiralPath = new StringBuilder();
            double maxAngle = 6.0 * Math.PI;
            int samples = 360;
            int i;

            for (i = 0; i <= samples; i++)
            {
                double a = maxAngle * i / samples;
                Point2D point = spiral.PointAt(a);

                double sx = centerX + point.X * scale;
                double sy = centerY - point.Y * scale;

                if (i == 0)
                    spiralPath.Append("M ");
                else
                    spiralPath.Append(" L ");

                spiralPath.Append(Format(sx));
                spiralPath.Append(" ");
                spiralPath.Append(Format(sy));
            }

            svg.AppendLine(
                "<path d=\"" + spiralPath.ToString() +
                "\" fill=\"none\" stroke=\"#777\" stroke-width=\"1.6\" />");

            // Mark the 360-degree transitions and their conceptual 5:6 relation.
            double[] ringAngles = new double[] { 0.0, 2.0 * Math.PI, 4.0 * Math.PI, 6.0 * Math.PI };

            for (i = 0; i < ringAngles.Length; i++)
            {
                double ringAngle = ringAngles[i];
                Point2D ringPoint = spiral.PointAt(ringAngle);
                double rx = centerX + ringPoint.X * scale;
                double ry = centerY - ringPoint.Y * scale;

                svg.AppendLine(
                    "<circle cx=\"" + Format(rx) +
                    "\" cy=\"" + Format(ry) +
                    "\" r=\"4\" fill=\"#555\" />");

                svg.AppendLine(
                    "<text x=\"" + Format(rx + 8) +
                    "\" y=\"" + Format(ry - 8) +
                    "\" font-family=\"Arial\" font-size=\"11\">" +
                    Format(i) + "×360°</text>");
            }

            // Trigonometric waves continue from the positive X axis.
            double waveStartX = centerX;
            double waveEndX = width - 25.0;
            double waveBaseY = centerY;
            double waveScaleX = (waveEndX - waveStartX) / (4.0 * Math.PI);
            double waveScaleY = scale * 0.9;

            StringBuilder sinePath = new StringBuilder();
            StringBuilder cosinePath = new StringBuilder();
            StringBuilder tangentPath = new StringBuilder();
            StringBuilder cotangentPath = new StringBuilder();

            int waveSamples = 720;

            for (i = 0; i <= waveSamples; i++)
            {
                double t = 4.0 * Math.PI * i / waveSamples;
                double wx = waveStartX + t * waveScaleX;

                AppendWavePoint(sinePath, wx, waveBaseY - Math.Sin(t) * waveScaleY, i == 0, false);
                AppendWavePoint(cosinePath, wx, waveBaseY - Math.Cos(t) * waveScaleY, i == 0, false);

                double tan = Math.Tan(t);
                if (Math.Abs(Math.Cos(t)) > 0.08)
                    AppendWavePoint(tangentPath, wx, waveBaseY - Clamp(tan, -4.0, 4.0) * waveScaleY * 0.25, i == 0, false);
                else
                    tangentPath.Append(" M ");

                double cot = 1.0 / Math.Tan(t);
                if (Math.Abs(Math.Sin(t)) > 0.08)
                    AppendWavePoint(cotangentPath, wx, waveBaseY - Clamp(cot, -4.0, 4.0) * waveScaleY * 0.25, i == 0, false);
                else
                    cotangentPath.Append(" M ");
            }

            svg.AppendLine(
                "<path d=\"" + sinePath.ToString() +
                "\" fill=\"none\" stroke=\"#c44\" stroke-width=\"2\" />");

            svg.AppendLine(
                "<path d=\"" + cosinePath.ToString() +
                "\" fill=\"none\" stroke=\"#44c\" stroke-width=\"2\" />");

            svg.AppendLine(
                "<path d=\"" + tangentPath.ToString() +
                "\" fill=\"none\" stroke=\"#4a4\" stroke-width=\"1.4\" />");

            svg.AppendLine(
                "<path d=\"" + cotangentPath.ToString() +
                "\" fill=\"none\" stroke=\"#a64\" stroke-width=\"1.4\" />");

            svg.AppendLine(
                "<text x=\"" + Format(waveStartX + 15) +
                "\" y=\"" + Format(waveBaseY - waveScaleY - 10) +
                "\" font-family=\"Arial\" font-size=\"12\">sin / cos / tan / cot</text>");

            // 3D-like line.
            Point2D projectedStart = Project(line.Start, centerX, centerY, scale);
            Point2D projectedEnd = Project(line.End, centerX, centerY, scale);

            svg.AppendLine(
                "<line x1=\"" + Format(projectedStart.X) +
                "\" y1=\"" + Format(projectedStart.Y) +
                "\" x2=\"" + Format(projectedEnd.X) +
                "\" y2=\"" + Format(projectedEnd.Y) +
                "\" stroke=\"#111\" stroke-width=\"4\" />");

            svg.AppendLine(
                "<circle cx=\"" + Format(projectedStart.X) +
                "\" cy=\"" + Format(projectedStart.Y) +
                "\" r=\"6\" fill=\"#111\" />");

            svg.AppendLine(
                "<circle cx=\"" + Format(projectedEnd.X) +
                "\" cy=\"" + Format(projectedEnd.Y) +
                "\" r=\"6\" fill=\"#111\" />");

            svg.AppendLine(
                "<text x=\"" + Format(projectedStart.X + 10) +
                "\" y=\"" + Format(projectedStart.Y - 10) +
                "\" font-family=\"Arial\" font-size=\"13\">3D origin</text>");

            svg.AppendLine(
                "<text x=\"" + Format(projectedEnd.X + 10) +
                "\" y=\"" + Format(projectedEnd.Y - 10) +
                "\" font-family=\"Arial\" font-size=\"13\">3:4 → 3D</text>");

            // Numerical information.
            svg.AppendLine(
                "<rect x=\"20\" y=\"" + Format(height - 105) +
                "\" width=\"350\" height=\"75\" rx=\"8\" " +
                "fill=\"white\" stroke=\"#444\" />");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 78) +
                "\" font-family=\"Arial\" font-size=\"14\">Ratio: " +
                EscapeXml(ratio.ToString()) + " = " +
                Format(ratio.DecimalValue) + "</text>");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 55) +
                "\" font-family=\"Arial\" font-size=\"14\">Circle radius: " +
                Format(circle.Radius) + " | Area: " +
                Format(circle.Area) + "</text>");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 32) +
                "\" font-family=\"Arial\" font-size=\"14\">" +
                "Spiral/ring transition: 360° → next ring, conceptual 5:6</text>");

            svg.AppendLine(
                "<text x=\"" + Format(width - 20) +
                "\" y=\"" + Format(height - 20) +
                "\" text-anchor=\"end\" font-family=\"Arial\" font-size=\"12\">" +
                "Geometry model only</text>");

            svg.AppendLine("</svg>");

            return svg.ToString();
        }

        private static void AppendWavePoint(
            StringBuilder path,
            double x,
            double y,
            bool first,
            bool unused)
        {
            if (first)
                path.Append("M ");
            else
                path.Append(" L ");

            path.Append(Format(x));
            path.Append(" ");
            path.Append(Format(y));
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            if (value < minimum)
                return minimum;
            if (value > maximum)
                return maximum;
            return value;
        }

        private static Point2D Project(
            Point3D point,
            double centerX,
            double centerY,
            double scale)
        {
            double x = point.X - point.Z * 0.35;
            double y = point.Y - point.Z * 0.25;

            return new Point2D(
                centerX + x * scale,
                centerY - y * scale);
        }

        private static string Format(double value)
        {
            return value.ToString("0.###", CultureInfo.InvariantCulture);
        }

        private static string EscapeXml(string value)
        {
            if (value == null)
                return "";

            return value
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }
    }
}
