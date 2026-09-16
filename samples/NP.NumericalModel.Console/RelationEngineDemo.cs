using System;
using System.Collections.Generic;
using NP.NumericalModel.Analysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NP.NumericalModel.ConsoleSample
{
    class RelationEngineDemo
    {
        //[DllImport("kernel32.dll"
        //private static extern bool AllocConsole();

        //[DllImport("kernel32.dll")]
        //private static extern bool FreeConsole();

       public static void Run()
       {
           RelationEngine engine =
                new RelationEngine(6);

            ConceptualRelationGraph graph = engine.Analyze(252);
            GraphLayout layout = new GraphLayout();

            layout.NodeWidth = 110;
            layout.NodeHeight = 42;
            layout.HorizontalSpacing = 50;
            layout.VerticalSpacing = 80;
            layout.Margin = 50;

            layout.Build(graph);

            SvgGraphRenderer renderer =
                new SvgGraphRenderer();

            renderer.ShowRules = true;
            renderer.ShowKinds = false;

            string filePath =
                "252-relations.svg";

            renderer.Render(
                graph,
                layout,
                filePath);

            //TestConsole.Open();

            Console.WriteLine(
                "SVG created: "
                + filePath);
            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                "NP.NumericalModel Relation Engine");

            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                "Input: 252");

            Console.WriteLine();

            foreach (RelationEdge edge in graph.Edges)
            {
                Console.WriteLine(
                    edge.ToString());
            }

            Console.WriteLine();

            Console.WriteLine(
                graph.ToString());

            Console.WriteLine();

            Console.WriteLine(
                "Press ENTER to exit.");

            Console.ReadLine();

            //TestConsole.Hide();
        }
    }
}