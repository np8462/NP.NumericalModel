using System;
using System.Globalization;
using System.Text;
using NP.NumericalModel.Interpretation;

namespace NP.NumericalModel.ConsoleSample.Svg
{
    public static class SvgRenderer
    {
        public static string RenderSegmentDivision(
            SegmentDivision division,
            int width,
            int height)
        {
            if (division == null)
                throw new ArgumentNullException("division");

            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height");

            StringBuilder svg = new StringBuilder();

            double left = 80.0;
            double right = width - 80.0;
            double y = height / 2.0;

            double segmentWidth =
                (right - left) / division.DivisionCount;

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
                "<text x=\"" + (width / 2) +
                "\" y=\"40\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"22\">" +
                "Segment Division" +
                "</text>");

            svg.AppendLine(
                "<line x1=\"" + left +
                "\" y1=\"" + y +
                "\" x2=\"" + right +
                "\" y2=\"" + y +
                "\" stroke=\"black\" stroke-width=\"2\" />");

            int i;

            for (i = 0; i <= division.DivisionCount; i++)
            {
                double x = left + segmentWidth * i;

                svg.AppendLine(
                    "<circle cx=\"" +
                    x.ToString("0.###", CultureInfo.InvariantCulture) +
                    "\" cy=\"" +
                    y.ToString("0.###", CultureInfo.InvariantCulture) +
                    "\" r=\"6\" fill=\"black\" />");

                string label;

                if (i == 0)
                    label = "0";
                else if (i == division.DivisionCount)
                    label = "1";
                else
                    label = i + "/" + division.DivisionCount;

                svg.AppendLine(
                    "<text x=\"" +
                    x.ToString("0.###", CultureInfo.InvariantCulture) +
                    "\" y=\"" +
                    (y + 35).ToString(
                        "0.###",
                        CultureInfo.InvariantCulture) +
                    "\" text-anchor=\"middle\" " +
                    "font-family=\"Arial\" font-size=\"16\">" +
                    label +
                    "</text>");
            }

            svg.AppendLine(
                "<text x=\"" +
                (width / 2) +
                "\" y=\"" +
                (height - 20) +
                "\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"16\">" +
                division.ToString() +
                "</text>");

            svg.AppendLine("</svg>");

            return svg.ToString();
        }

        public static string RenderRelationDiagram(
    InterpretationDefinition definition,
    string title)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            if (title == null)
                throw new ArgumentNullException("title");

            StringBuilder svg = new StringBuilder();

            int width = 1100;
            int rowHeight = 90;
            int height = 100 + definition.Relations.Count * rowHeight;

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
                "<text x=\"" + (width / 2) +
                "\" y=\"40\" text-anchor=\"middle\" " +
                "font-family=\"Arial\" font-size=\"24\" " +
                "font-weight=\"bold\">" +
                EscapeXml(title) +
                "</text>");

            int i;

            for (i = 0; i < definition.Relations.Count; i++)
            {
                ConceptualRelation relation =
                    definition.Relations[i];

                double y = 80 + i * rowHeight;

                double sourceX = 180;
                double targetX = 850;

                svg.AppendLine(
                    "<rect x=\"" +
                    (sourceX - 80) +
                    "\" y=\"" +
                    (y - 25) +
                    "\" width=\"160\" height=\"50\" " +
                    "rx=\"8\" fill=\"white\" stroke=\"black\" />");

                svg.AppendLine(
                    "<rect x=\"" +
                    (targetX - 80) +
                    "\" y=\"" +
                    (y - 25) +
                    "\" width=\"160\" height=\"50\" " +
                    "rx=\"8\" fill=\"white\" stroke=\"black\" />");

                svg.AppendLine(
                    "<text x=\"" +
                    sourceX +
                    "\" y=\"" +
                    (y + 6) +
                    "\" text-anchor=\"middle\" " +
                    "font-family=\"Arial\" font-size=\"16\">" +
                    EscapeXml(relation.Source.Symbol) +
                    "</text>");

                svg.AppendLine(
                    "<text x=\"" +
                    targetX +
                    "\" y=\"" +
                    (y + 6) +
                    "\" text-anchor=\"middle\" " +
                    "font-family=\"Arial\" font-size=\"16\">" +
                    EscapeXml(relation.Target.Symbol) +
                    "</text>");

                svg.AppendLine(
                    "<line x1=\"" +
                    (sourceX + 80) +
                    "\" y1=\"" +
                    y +
                    "\" x2=\"" +
                    (targetX - 80) +
                    "\" y2=\"" +
                    y +
                    "\" stroke=\"black\" stroke-width=\"2\" />");

                svg.AppendLine(
                    "<text x=\"" +
                    (width / 2) +
                    "\" y=\"" +
                    (y - 8) +
                    "\" text-anchor=\"middle\" " +
                    "font-family=\"Arial\" font-size=\"13\">" +
                    EscapeXml(relation.RelationType) +
                    "</text>");

                svg.AppendLine(
                    "<text x=\"" +
                    (width / 2) +
                    "\" y=\"" +
                    (y + 18) +
                    "\" text-anchor=\"middle\" " +
                    "font-family=\"Arial\" font-size=\"12\">" +
                    EscapeXml(relation.Symbol) +
                    "</text>");
            }

            svg.AppendLine("</svg>");

            return svg.ToString();
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