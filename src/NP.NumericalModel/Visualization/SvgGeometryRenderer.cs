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
                "Circle + 3D Ratio + Spiral + Trigonometric Waves</text>");

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

            // Circle.
            svg.AppendLine(
                "<circle cx=\"" + Format(centerX) +
                "\" cy=\"" + Format(centerY) +
                "\" r=\"" + Format(circleRadius) +
                "\" fill=\"none\" stroke=\"#222\" stroke-width=\"3\" />");

            // Radius and pi/4.
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

            // 3:4 construction in the first quadrant.
            // The red 3-segment lies exactly on the horizontal X axis.
            double triA = centerX;
            double triB = centerY;
            double triRadius = circleRadius * 0.80;

            double triX = centerX + triRadius * 3.0 / 5.0;
            double triY = centerY - triRadius * 4.0 / 5.0;

            svg.AppendLine(
                "<line x1=\"" + Format(triA) + "\" y1=\"" + Format(triB) +
                "\" x2=\"" + Format(triX) + "\" y2=\"" + Format(triB) +
                "\" stroke=\"#c33\" stroke-width=\"4\" />");

            svg.AppendLine(
                "<line x1=\"" + Format(triX) + "\" y1=\"" + Format(triB) +
                "\" x2=\"" + Format(triX) + "\" y2=\"" + Format(triY) +
                "\" stroke=\"#333\" stroke-width=\"3\" />");

            svg.AppendLine(
                "<line x1=\"" + Format(triA) + "\" y1=\"" + Format(triB) +
                "\" x2=\"" + Format(triX) + "\" y2=\"" + Format(triY) +
                "\" stroke=\"#111\" stroke-width=\"4\" />");

            svg.AppendLine(
                "<circle cx=\"" + Format(triX) +
                "\" cy=\"" + Format(triY) +
                "\" r=\"6\" fill=\"#111\" />");

            svg.AppendLine(
                "<text x=\"" + Format(centerX + triRadius * 0.30) +
                "\" y=\"" + Format(centerY + 18) +
                "\" text-anchor=\"middle\" font-family=\"Arial\" font-size=\"11\">3</text>");

            svg.AppendLine(
                "<text x=\"" + Format(triX + 10) +
                "\" y=\"" + Format(centerY - triRadius * 0.40) +
                "\" font-family=\"Arial\" font-size=\"11\">4</text>");

            svg.AppendLine(
                "<text x=\"" + Format(triX + 12) +
                "\" y=\"" + Format(triY - 12) +
                "\" font-family=\"Arial\" font-size=\"13\">3:4 / 3D</text>");

            // 3:4 direction in the third quadrant.
            // Only the thick spatial line is kept here.
            double thirdX = centerX - triRadius * 3.0 / 5.0;
            double thirdY = centerY + triRadius * 4.0 / 5.0;

            svg.AppendLine(
                "<line x1=\"" + Format(triA) + "\" y1=\"" + Format(triB) +
                "\" x2=\"" + Format(thirdX) + "\" y2=\"" + Format(thirdY) +
                "\" stroke=\"#111\" stroke-width=\"4\" />");

            // Extended dashed continuation toward the next spiral/ring paradigm.
            double continuationLength = triRadius * 1.15;
            double dx = thirdX - triA;
            double dy = thirdY - triB;
            double length = Math.Sqrt(dx * dx + dy * dy);
            double ux = dx / length;
            double uy = dy / length;

            double contX = thirdX + ux * continuationLength;
            double contY = thirdY + uy * continuationLength;

            svg.AppendLine(
                "<line x1=\"" + Format(thirdX) + "\" y1=\"" + Format(thirdY) +
                "\" x2=\"" + Format(contX) + "\" y2=\"" + Format(contY) +
                "\" stroke=\"#333\" stroke-width=\"2\" " +
                "stroke-dasharray=\"7,5\" />");

            svg.AppendLine(
                "<circle cx=\"" + Format(thirdX) +
                "\" cy=\"" + Format(thirdY) +
                "\" r=\"6\" fill=\"#111\" />");

            svg.AppendLine(
                "<text x=\"" + Format(thirdX - 12) +
                "\" y=\"" + Format(thirdY + 18) +
                "\" text-anchor=\"end\" font-family=\"Arial\" font-size=\"13\">3:4</text>");

            svg.AppendLine(
                "<text x=\"" + Format(centerX - triRadius * 0.30) +
                "\" y=\"" + Format(centerY + triRadius * 0.12) +
                "\" font-family=\"Arial\" font-size=\"11\">3</text>");

            svg.AppendLine(
                "<text x=\"" + Format(thirdX - 10) +
                "\" y=\"" + Format(centerY + triRadius * 0.55) +
                "\" font-family=\"Arial\" font-size=\"11\">4</text>");

                        // Spiral.
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

            double[] ringAngles = new double[] { 0.0, 2.0 * Math.PI, 4.0 * Math.PI, 6.0 * Math.PI };

            for (i = 0; i < ringAngles.Length; i++)
            {
                Point2D ringPoint = spiral.PointAt(ringAngles[i]);
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

            // Horizontal waves on the positive X direction.
            double waveStartX = centerX;
            double waveEndX = width - 25.0;
            double waveBaseY = centerY;
            double waveScaleX = (waveEndX - waveStartX) / (4.0 * Math.PI);
            double waveScaleY = scale * 0.9;

            StringBuilder sinePath = new StringBuilder();
            StringBuilder cosinePath = new StringBuilder();
            StringBuilder tangentPath = new StringBuilder();
            StringBuilder cotangentPath = new StringBuilder();
            StringBuilder secantPath = new StringBuilder();
            StringBuilder cosecantPath = new StringBuilder();

            int waveSamples = 720;

            for (i = 0; i <= waveSamples; i++)
            {
                double t = 4.0 * Math.PI * i / waveSamples;
                double wx = waveStartX + t * waveScaleX;

                AppendWavePoint(sinePath, wx, waveBaseY - Math.Sin(t) * waveScaleY, i == 0);
                AppendWavePoint(cosinePath, wx, waveBaseY - Math.Cos(t) * waveScaleY, i == 0);

                double tan = Math.Tan(t);
                if (Math.Abs(Math.Cos(t)) > 0.08)
                    AppendWavePoint(tangentPath, wx,
                        waveBaseY - Clamp(tan, -4.0, 4.0) * waveScaleY * 0.25, i == 0);
                else
                    tangentPath.Append(" M ");

                double cot = 1.0 / Math.Tan(t);
                if (Math.Abs(Math.Sin(t)) > 0.08)
                    AppendWavePoint(cotangentPath, wx,
                        waveBaseY - Clamp(cot, -4.0, 4.0) * waveScaleY * 0.25, i == 0);
                else
                    cotangentPath.Append(" M ");

                double sec = 1.0 / Math.Cos(t);
                if (Math.Abs(Math.Cos(t)) > 0.08)
                    AppendWavePoint(secantPath, wx,
                        waveBaseY - Clamp(sec, -4.0, 4.0) * waveScaleY * 0.18, i == 0);
                else
                    secantPath.Append(" M ");

                double csc = 1.0 / Math.Sin(t);
                if (Math.Abs(Math.Sin(t)) > 0.08)
                    AppendWavePoint(cosecantPath, wx,
                        waveBaseY - Clamp(csc, -4.0, 4.0) * waveScaleY * 0.18, i == 0);
                else
                    cosecantPath.Append(" M ");
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
                "<path d=\"" + secantPath.ToString() +
                "\" fill=\"none\" stroke=\"#a4a\" stroke-width=\"1.2\" />");

            svg.AppendLine(
                "<path d=\"" + cosecantPath.ToString() +
                "\" fill=\"none\" stroke=\"#4aa\" stroke-width=\"1.2\" />");

            svg.AppendLine(
                "<text x=\"" + Format(waveStartX + 15) +
                "\" y=\"" + Format(waveBaseY - waveScaleY - 10) +
                "\" font-family=\"Arial\" font-size=\"12\">" +
                "sin / cos / tan / cot / sec / csc</text>");

            // Vertical waves along the positive Y direction.
            double verticalStartY = centerY;
            double verticalEndY = 90.0;
            double verticalScaleY = (verticalStartY - verticalEndY) / (4.0 * Math.PI);
            double verticalBaseX = centerX;
            double verticalScaleX = scale * 0.55;

            StringBuilder verticalSine = new StringBuilder();
            StringBuilder verticalCosine = new StringBuilder();

            for (i = 0; i <= waveSamples; i++)
            {
                double t = 4.0 * Math.PI * i / waveSamples;
                double vy = verticalStartY - t * verticalScaleY;

                AppendWavePoint(verticalSine, verticalBaseX + Math.Sin(t) * verticalScaleX,
                    vy, i == 0);
                AppendWavePoint(verticalCosine, verticalBaseX + Math.Cos(t) * verticalScaleX,
                    vy, i == 0);
            }

            svg.AppendLine(
                "<path d=\"" + verticalSine.ToString() +
                "\" fill=\"none\" stroke=\"#d55\" stroke-width=\"1.6\" />");

            svg.AppendLine(
                "<path d=\"" + verticalCosine.ToString() +
                "\" fill=\"none\" stroke=\"#55d\" stroke-width=\"1.6\" />");

            svg.AppendLine(
                "<text x=\"" + Format(verticalBaseX + 20) +
                "\" y=\"96\" font-family=\"Arial\" font-size=\"11\">" +
                "vertical sin / cos</text>");

            // Projected 3D line from the model, shown beside the construction.
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

            // Information box.
            svg.AppendLine(
                "<rect x=\"20\" y=\"" + Format(height - 105) +
                "\" width=\"500\" height=\"75\" rx=\"8\" " +
                "fill=\"white\" stroke=\"#444\" />");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 78) +
                "\" font-family=\"Arial\" font-size=\"14\">Ratio: " +
                EscapeXml(ratio.ToString()) + " = " +
                Format(ratio.DecimalValue) + " | 3:4 / 3D construction</text>");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 55) +
                "\" font-family=\"Arial\" font-size=\"14\">Circle radius: " +
                Format(circle.Radius) + " | Area: " +
                Format(circle.Area) + "</text>");

            svg.AppendLine(
                "<text x=\"35\" y=\"" + Format(height - 32) +
                "\" font-family=\"Arial\" font-size=\"14\">" +
                "360° → next spiral ring | conceptual 5:6 transition</text>");

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
            bool first)
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
