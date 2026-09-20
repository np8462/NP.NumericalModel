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
                "width=\"" + width + "\" " +
                "height=\"" + height + "\" " +
                "viewBox=\"0 0 " + width + " " + height + "\">");

            svg.AppendLine(
                "<rect x=\"0\" y=\"0\" width=\"" +
                width + "\" height=\"" + height +
                "\" fill=\"white\" />");

            svg.AppendLine(
                "<text x=\"" + Format(centerX) +
                "\" y=\"34\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"24\" font-weight=\"bold\">" +
                "NP.NumericalModel - Geometry</text>");

            svg.AppendLine(
                "<text x=\"" + Format(centerX) +
                "\" y=\"58\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"14\">" +
                "Circle + Cartesian Plane + Spiral + 3D-like Ratio Line</text>");

            // Cartesian axes.
            svg.AppendLine(
                "<line x1=\"" + Format(0) +
                "\" y1=\"" + Format(centerY) +
                "\" x2=\"" + Format(width) +
                "\" y2=\"" + Format(centerY) +
                "\" stroke=\"#888\" stroke-width=\"1.5\" />");

            svg.AppendLine(
                "<line x1=\"" + Format(centerX) +
                "\" y1=\"" + Format(70) +
                "\" x2=\"" + Format(centerX) +
                "\" y2=\"" + Format(height - 45) +
                "\" stroke=\"#888\" stroke-width=\"1.5\" />");

            // Tick marks and labels.
            int tick;
            for (tick = -7; tick <= 7; tick++)
            {
                if (tick == 0)
                    continue;

                double x = centerX + tick * scale;
                double y = centerY - tick * scale;

                svg.AppendLine(
                    "<line x1=\"" + Format(x) +
                    "\" y1=\"" + Format(centerY - 5) +
                    "\" x2=\"" + Format(x) +
                    "\" y2=\"" + Format(centerY + 5) +
                    "\" stroke=\"#888\" />");

                svg.AppendLine(
                    "<line x1=\"" + Format(centerX - 5) +
                    "\" y1=\"" + Format(y) +
                    "\" x2=\"" + Format(centerX + 5) +
                    "\" y2=\"" + Format(y) +
                    "\" stroke=\"#888\" />");
            }

            svg.AppendLine(
                "<text x=\"" + Format(width - 18) +
                "\" y=\"" + Format(centerY - 10) +
                "\" text-anchor=\"end\" font-family=\"Arial\" font-size=\"13\">X</text>");

            svg.AppendLine(
                "<text x=\"" + Format(centerX + 10) +
                "\" y=\"82\" font-family=\"Arial\" font-size=\"13\">Y</text>");

            // Circle.
            svg.AppendLine(
                "<circle cx=\"" + Format(centerX) +
                "\" cy=\"" + Format(centerY) +
                "\" r=\"" + Format(circleRadius) +
                "\" fill=\"none\" stroke=\"#222\" stroke-width=\"3\" />");

            // Radius reference.
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

            // Trigonometric reference at pi/4.
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

            // Spiral surrounding the circle.
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

            // 3D-like line projected into the same 2D canvas.
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
                "\" font-family=\"Arial\" font-size=\"13\">" +
                "3:4 → 3D</text>");

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
                "\" font-family=\"Arial\" font-size=\"14\">Spiral: r = " +
                Format(spiral.StartRadius) + " + " +
                Format(spiral.GrowthPerTurn) + "·θ/(2π)</text>");

            svg.AppendLine(
                "<text x=\"" + Format(width - 20) +
                "\" y=\"" + Format(height - 20) +
                "\" text-anchor=\"end\" font-family=\"Arial\" font-size=\"12\">" +
                "Geometry model only</text>");

            svg.AppendLine("</svg>");

            return svg.ToString();
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
