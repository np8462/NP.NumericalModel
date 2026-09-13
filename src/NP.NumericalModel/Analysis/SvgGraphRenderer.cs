using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Security;

namespace NP.NumericalModel.Analysis
{
    public class SvgGraphRenderer
    {
        public int FontSize { get; set; }
        public int RuleFontSize { get; set; }

        public bool ShowRules { get; set; }
        public bool ShowKinds { get; set; }

        public SvgGraphRenderer()
        {
            FontSize = 14;
            RuleFontSize = 11;

            ShowRules = true;
            ShowKinds = false;
        }

        public void Render(
            ConceptualRelationGraph graph,
            GraphLayout layout,
            string filePath)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (layout == null)
            {
                throw new ArgumentNullException("layout");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            string svg =
                RenderToString(
                    graph,
                    layout);

            File.WriteAllText(
                filePath,
                svg,
                Encoding.UTF8);
        }

        public string RenderToString(
            ConceptualRelationGraph graph,
            GraphLayout layout)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (layout == null)
            {
                throw new ArgumentNullException("layout");
            }

            StringBuilder builder =
                new StringBuilder();

            int width =
                layout.GetWidth();

            int height =
                layout.GetHeight();

            builder.AppendLine(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            builder.AppendLine(
                "<svg xmlns=\"http://www.w3.org/2000/svg\" "
                + "width=\"" + width.ToString() + "\" "
                + "height=\"" + height.ToString() + "\" "
                + "viewBox=\"0 0 "
                + width.ToString()
                + " "
                + height.ToString()
                + "\">");

            /*
             * Arrow definition
             */
            builder.AppendLine(
                "<defs>");

            builder.AppendLine(
                "<marker id=\"arrow\" "
                + "markerWidth=\"10\" "
                + "markerHeight=\"10\" "
                + "refX=\"9\" "
                + "refY=\"3\" "
                + "orient=\"auto\" "
                + "markerUnits=\"strokeWidth\">");

            builder.AppendLine(
                "<path d=\"M0,0 L0,6 L9,3 z\" />");

            builder.AppendLine(
                "</marker>");

            builder.AppendLine(
                "</defs>");

            /*
             * ابتدا Edgeها را رسم می‌کنیم
             * تا Nodeها روی آن‌ها قرار بگیرند.
             */
            foreach (RelationEdge edge in graph.Edges)
            {
                if (!layout.Contains(
                    edge.Source.Expression))
                {
                    continue;
                }

                if (!layout.Contains(
                    edge.Target.Expression))
                {
                    continue;
                }

                DrawEdge(
                    builder,
                    edge,
                    layout);
            }

            /*
             * سپس Nodeها.
             */
            foreach (RelationNode node in graph.Nodes)
            {
                if (!layout.Contains(
                    node.Expression))
                {
                    continue;
                }

                DrawNode(
                    builder,
                    node,
                    layout);
            }

            builder.AppendLine(
                "</svg>");

            return builder.ToString();
        }

        private void DrawEdge(
            StringBuilder builder,
            RelationEdge edge,
            GraphLayout layout)
        {
            Point source =
                layout.GetPosition(
                    edge.Source);

            Point target =
                layout.GetPosition(
                    edge.Target);

            int sourceX =
                source.X + layout.NodeWidth / 2;

            int sourceY =
                source.Y + layout.NodeHeight;

            int targetX =
                target.X + layout.NodeWidth / 2;

            int targetY =
                target.Y;

            /*
             * اگر دو Node در یک نقطه افقی باشند،
             * خط مستقیم کافی است.
             *
             * در غیر این صورت نیز فعلاً خط مستقیم
             * استفاده می‌شود تا Renderer ساده بماند.
             */
            builder.AppendLine(
                "<line "
                + "x1=\"" + sourceX.ToString() + "\" "
                + "y1=\"" + sourceY.ToString() + "\" "
                + "x2=\"" + targetX.ToString() + "\" "
                + "y2=\"" + targetY.ToString() + "\" "
                + "stroke=\"black\" "
                + "stroke-width=\"1.2\" "
                + "marker-end=\"url(#arrow)\" />");

            if (ShowRules)
            {
                int labelX =
                    (sourceX + targetX) / 2;

                int labelY =
                    (sourceY + targetY) / 2;

                string rule =
                    EscapeXml(edge.Rule);

                builder.AppendLine(
                    "<text "
                    + "x=\"" + labelX.ToString() + "\" "
                    + "y=\"" + labelY.ToString() + "\" "
                    + "font-size=\""
                    + RuleFontSize.ToString()
                    + "\" "
                    + "text-anchor=\"middle\" "
                    + "fill=\"black\">"
                    + rule
                    + "</text>");
            }
        }

        private void DrawNode(
            StringBuilder builder,
            RelationNode node,
            GraphLayout layout)
        {
            Point position =
                layout.GetPosition(node);

            int x = position.X;
            int y = position.Y;

            builder.AppendLine(
                "<rect "
                + "x=\"" + x.ToString() + "\" "
                + "y=\"" + y.ToString() + "\" "
                + "width=\""
                + layout.NodeWidth.ToString()
                + "\" "
                + "height=\""
                + layout.NodeHeight.ToString()
                + "\" "
                + "rx=\"6\" "
                + "ry=\"6\" "
                + "fill=\"white\" "
                + "stroke=\"black\" "
                + "stroke-width=\"1.2\" />");

            int centerX =
                x + layout.NodeWidth / 2;

            int centerY =
                y + layout.NodeHeight / 2;

            string expression =
                EscapeXml(node.Expression);

            builder.AppendLine(
                "<text "
                + "x=\"" + centerX.ToString() + "\" "
                + "y=\"" + (centerY + 5).ToString() + "\" "
                + "font-size=\""
                + FontSize.ToString()
                + "\" "
                + "text-anchor=\"middle\" "
                + "fill=\"black\">"
                + expression
                + "</text>");

            if (ShowKinds)
            {
                string kind =
                    EscapeXml(node.Kind);

                builder.AppendLine(
                    "<text "
                    + "x=\"" + centerX.ToString() + "\" "
                    + "y=\"" + (y + layout.NodeHeight + 14).ToString() + "\" "
                    + "font-size=\"10\" "
                    + "text-anchor=\"middle\" "
                    + "fill=\"gray\">"
                    + kind
                    + "</text>");
            }
        }

        private string EscapeXml(
            string value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return SecurityElement.Escape(value);
        }
    }
}