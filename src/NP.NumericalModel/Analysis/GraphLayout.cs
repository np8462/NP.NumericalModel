using System;
using System.Collections.Generic;
using System.Drawing;

namespace NP.NumericalModel.Analysis
{
    public class GraphLayout
    {
        private readonly Dictionary<string, Point> _positions;
        private readonly Dictionary<string, int> _layers;

        public int NodeWidth { get; set; }
        public int NodeHeight { get; set; }

        public int HorizontalSpacing { get; set; }
        public int VerticalSpacing { get; set; }

        public int Margin { get; set; }

        public GraphLayout()
        {
            NodeWidth = 110;
            NodeHeight = 42;

            HorizontalSpacing = 40;
            VerticalSpacing = 80;

            Margin = 40;

            _positions =
                new Dictionary<string, Point>(
                    StringComparer.Ordinal);

            _layers =
                new Dictionary<string, int>(
                    StringComparer.Ordinal);
        }

        public void Build(
            ConceptualRelationGraph graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            _positions.Clear();
            _layers.Clear();

            List<RelationNode> nodes =
                new List<RelationNode>();

            foreach (RelationNode node in graph.Nodes)
            {
                nodes.Add(node);
            }

            if (nodes.Count == 0)
            {
                return;
            }

            /*
             * ابتدا همه Nodeها را در لایه صفر قرار می‌دهیم.
             */
            Dictionary<string, int> distances =
                new Dictionary<string, int>(
                    StringComparer.Ordinal);

            foreach (RelationNode node in nodes)
            {
                distances[node.Expression] = -1;
            }

            /*
             * Rootها Nodeهایی هستند که Edge ورودی ندارند.
             */
            Queue<string> queue =
                new Queue<string>();

            foreach (RelationNode node in nodes)
            {
                List<RelationEdge> incoming =
                    graph.FindIncoming(node.Expression);

                if (incoming.Count == 0)
                {
                    distances[node.Expression] = 0;
                    queue.Enqueue(node.Expression);
                }
            }

            /*
             * اگر Graph چرخه داشته باشد و Root نداشته باشد،
             * اولین Node را به عنوان نقطه شروع می‌گیریم.
             */
            if (queue.Count == 0)
            {
                distances[nodes[0].Expression] = 0;
                queue.Enqueue(nodes[0].Expression);
            }

            /*
             * BFS برای تعیین عمق تقریبی هر Node.
             */
            while (queue.Count > 0)
            {
                string current =
                    queue.Dequeue();

                int currentLayer =
                    distances[current];

                List<RelationEdge> outgoing =
                    graph.FindOutgoing(current);

                foreach (RelationEdge edge in outgoing)
                {
                    string target =
                        edge.Target.Expression;

                    int nextLayer =
                        currentLayer + 1;

                    int oldLayer;

                    if (!distances.TryGetValue(
                        target,
                        out oldLayer))
                    {
                        continue;
                    }

                    if (oldLayer == -1 ||
                        nextLayer < oldLayer)
                    {
                        distances[target] = nextLayer;
                        queue.Enqueue(target);
                    }
                }
            }

            /*
             * Nodeهایی که به Root متصل نبوده‌اند،
             * در لایه‌های بعدی قرار می‌گیرند.
             */
            int maxLayer = 0;

            foreach (KeyValuePair<string, int> item
                in distances)
            {
                if (item.Value > maxLayer)
                {
                    maxLayer = item.Value;
                }
            }

            foreach (RelationNode node in nodes)
            {
                if (distances[node.Expression] < 0)
                {
                    maxLayer++;
                    distances[node.Expression] = maxLayer;
                }
            }

            foreach (KeyValuePair<string, int> item
                in distances)
            {
                _layers[item.Key] = item.Value;
            }

            /*
             * ابتدا تعداد Nodeهای هر لایه را محاسبه می‌کنیم.
             */
            Dictionary<int, int> layerCounts =
                new Dictionary<int, int>();

            foreach (KeyValuePair<string, int> item
                in distances)
            {
                int count;

                if (!layerCounts.TryGetValue(
                    item.Value,
                    out count))
                {
                    count = 0;
                }

                layerCounts[item.Value] = count + 1;
            }

            /*
             * ترتیب ساده و پایدار بر اساس Expression.
             */
            nodes.Sort(
                delegate(RelationNode left, RelationNode right)
                {
                    return StringComparer.Ordinal.Compare(
                        left.Expression,
                        right.Expression);
                });

            Dictionary<int, int> layerIndexes =
                new Dictionary<int, int>();

            foreach (RelationNode node in nodes)
            {
                int layer =
                    distances[node.Expression];

                int index;

                if (!layerIndexes.TryGetValue(
                    layer,
                    out index))
                {
                    index = 0;
                }

                layerIndexes[layer] = index + 1;

                int count =
                    layerCounts[layer];

                int totalWidth =
                    count * NodeWidth
                    + (count - 1) * HorizontalSpacing;

                int startX =
                    Margin
                    - (totalWidth / 2);

                if (startX < Margin)
                {
                    startX = Margin;
                }

                int x =
                    startX
                    + index
                    * (NodeWidth + HorizontalSpacing);

                int y =
                    Margin
                    + layer
                    * (NodeHeight + VerticalSpacing);

                _positions[node.Expression] =
                    new Point(x, y);
            }
        }

        public Point GetPosition(
            RelationNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return GetPosition(node.Expression);
        }

        public Point GetPosition(
            string expression)
        {
            Point position;

            if (!_positions.TryGetValue(
                expression,
                out position))
            {
                throw new KeyNotFoundException(
                    "No layout position exists for expression: "
                    + expression);
            }

            return position;
        }

        public bool Contains(
            string expression)
        {
            return _positions.ContainsKey(expression);
        }

        public int GetLayer(
            string expression)
        {
            int layer;

            if (!_layers.TryGetValue(
                expression,
                out layer))
            {
                return -1;
            }

            return layer;
        }

        public int GetWidth()
        {
            int maxX = Margin;

            foreach (Point position in _positions.Values)
            {
                int right =
                    position.X + NodeWidth;

                if (right > maxX)
                {
                    maxX = right;
                }
            }

            return maxX + Margin;
        }

        public int GetHeight()
        {
            int maxY = Margin;

            foreach (Point position in _positions.Values)
            {
                int bottom =
                    position.Y + NodeHeight;

                if (bottom > maxY)
                {
                    maxY = bottom;
                }
            }

            return maxY + Margin;
        }
    }
}