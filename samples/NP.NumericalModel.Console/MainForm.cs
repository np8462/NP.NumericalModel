using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NP.NumericalModel.ConsoleSample
{
    public partial class MainForm : Form
    {
        private ComboBox cmbCategory;
        private ComboBox cmbDemo;
        private Button btnRun;

        private Dictionary<string, List<DemoItem>> demoGroups;

        public MainForm()
        {
            InitializeDemoGroups();
            InitializeUI();
            LoadCategories();
        }

        private void InitializeUI()
        {
            this.Text = "NP.NumericalModel";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 300);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblTitle = new Label();
            lblTitle.Text = "NP.NumericalModel";
            lblTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(165, 25);

            Label lblCategory = new Label();
            //lblCategory.Text = "دسته:";
            lblCategory.Text = "Category :";
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(70, 85);

            cmbCategory = new ComboBox();
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(140, 80);
            cmbCategory.Size = new Size(280, 25);
            cmbCategory.SelectedIndexChanged +=
                new EventHandler(cmbCategory_SelectedIndexChanged);

            Label lblDemo = new Label();
            //lblDemo.Text = "آزمون:";
            lblDemo.Text = "Demo :";
            lblDemo.AutoSize = true;
            lblDemo.Location = new Point(70, 125);

            cmbDemo = new ComboBox();
            cmbDemo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDemo.Location = new Point(140, 120);
            cmbDemo.Size = new Size(280, 25);

            btnRun = new Button();
            //btnRun.Text = "اجرای Demo";
            btnRun.Text = "Run Demo";
            btnRun.Size = new Size(140, 40);
            btnRun.Location = new Point(180, 175);
            btnRun.Click += new EventHandler(btnRun_Click);

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblDemo);
            this.Controls.Add(cmbDemo);
            this.Controls.Add(btnRun);
        }

        private void InitializeDemoGroups()
        {
            demoGroups = new Dictionary<string, List<DemoItem>>();

            demoGroups.Add(
                "Base & Conversion",
                new List<DemoItem>
                {
                    new DemoItem("BaseSystemDemo", RunBaseSystemDemo),
                    new DemoItem("BaseConversionDemo", RunBaseConversionDemo)
                });

            demoGroups.Add(
                "Structural Model",
                new List<DemoItem>
                {
                    new DemoItem("StructuralModelDemo", RunStructuralModelDemo),
                    new DemoItem("BoundaryTransitionDemo", RunBoundaryTransitionDemo)
                });

            demoGroups.Add(
                "Relations & Analysis",
                new List<DemoItem>
                {
                    new DemoItem("RelationAnalyzerDemo", RunRelationAnalyzerDemo),
                    new DemoItem("FactorialStateDemo", RunFactorialStateDemo),
                    new DemoItem("FactorRelationDemo", RunFactorRelationDemo),
                    new DemoItem("ConceptualRelationDemo", RunConceptualRelationDemo)
                });

            demoGroups.Add(
                "Relation Engine",
                new List<DemoItem>
                {
                    new DemoItem("RelationEngineDemo", RunRelationEngineDemo)
                });

            demoGroups.Add(
                "Interpretation",
                new List<DemoItem>
                {
                    new DemoItem("InterpretationDemo", RunInterpretationDemo)
                });
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();

            foreach (string category in demoGroups.Keys)
            {
                cmbCategory.Items.Add(category);
            }

            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void cmbCategory_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            cmbDemo.Items.Clear();

            string category = cmbCategory.SelectedItem as string;

            if (category == null)
            {
                return;
            }

            List<DemoItem> demos = demoGroups[category];

            foreach (DemoItem demo in demos)
            {
                cmbDemo.Items.Add(demo);
            }

            if (cmbDemo.Items.Count > 0)
            {
                cmbDemo.SelectedIndex = 0;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DemoItem selectedDemo = cmbDemo.SelectedItem as DemoItem;

            if (selectedDemo == null)
            {
                MessageBox.Show(
                    "لطفاً یک Demo را انتخاب کنید.",
                    "NP.NumericalModel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            TestConsole.Open();

            try
            {
                selectedDemo.Run();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine(ex.ToString());
                System.Console.WriteLine();
                System.Console.WriteLine("Press any key...");
                System.Console.ReadKey();
            }
            finally
            {
                TestConsole.Hide();
            }
        }

        private void RunBaseSystemDemo()
        {
            BaseSystemDemo.Run();
        }

        private void RunBaseConversionDemo()
        {
            BaseConversionDemo.Run();
        }

        private void RunStructuralModelDemo()
        {
            StructuralModelDemo.Run();
        }

        private void RunBoundaryTransitionDemo()
        {
            BoundaryTransitionDemo.Run();
        }

        private void RunRelationAnalyzerDemo()
        {
            RelationAnalyzerDemo.Run();
        }

        private void RunFactorialStateDemo()
        {
            FactorialStateDemo.Run();
        }

        private void RunFactorRelationDemo()
        {
            FactorRelationDemo.Run();
        }

        private void RunConceptualRelationDemo()
        {
            ConceptualRelationDemo.Run();
        }

        private void RunRelationEngineDemo()
        {
            RelationEngineDemo.Run();
        }

        private void RunInterpretationDemo()
        {
            InterpretationDemo.Run();
        }
    }

    public class DemoItem
    {
        private readonly string name;
        private readonly Action action;

        public DemoItem(string name, Action action)
        {
            this.name = name;
            this.action = action;
        }

        public void Run()
        {
            action();
        }

        public override string ToString()
        {
            return name;
        }
    }
}