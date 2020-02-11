using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library;
using log4net;

namespace CarbonIT_challenge
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        private Game map;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chargerUnFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = openFileDialog.FileName;
                    log.Info($"Load File {Path.GetFullPath(sourceFile)}");
                    this.map = new Game();
                    map.Fill(Path.GetFullPath(sourceFile));
                    loadMap(map);

                }
            }
            catch (Exception exc)
            {
                log.Debug($"{exc.Message}");
                MessageBox.Show($"{exc.Message}", "Error");
            }


        }

        private void loadMap(Game map)
        {
            log.Info($"Generate map");
            var rowCount = map.Map.Height;
            var columnCount = map.Map.Width;
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = columnCount;
            this.tableLayoutPanel1.RowCount = rowCount;

            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }
            for (int i = 0; i < rowCount; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {

                    var button = new Button();
                    if (map.Map.TravelerMap[i, j] is Treasure)
                    {
                        button.Text = $"{map.Map.TravelerMap[i, j].getLetter()}( {((Treasure)map.Map.TravelerMap[i, j]).NumberOfTreasure})";
                    }
                    else if (map.Map.TravelerMap[i, j] is Adventurer)
                    {
                        button.Text = $"{map.Map.TravelerMap[i, j].getLetter()} ({((Adventurer)map.Map.TravelerMap[i, j]).Name})";
                    }
                    else
                    {
                        button.Text = map.Map.TravelerMap[i, j].getLetter();
                    }

                    button.Name = string.Format("button_{0}{1}", i, j);
                    button.Enabled = false;
                    button.BackColor = Color.Transparent;
                    button.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(button, j, i);
                }
            }
            if (map.Adventurer.Moves.Count() > 0)
            {
                this.label1.Text = $"Next Move : {map.Adventurer.Moves[0].ToString()}";
            }
            else
            {

                this.label1.Text = $"No next move";
            }

            this.label2.Text = $"Orientation : {map.Adventurer.Orientation.ToString()}";
            this.label3.Text = $"Number of treasure collected : {map.Adventurer.NumberOfTreasureCollected}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (map != null)
                {
                    if (map.Adventurer.Moves.Count() > 0)
                    {
                        log.Debug("The User click on next move button");
                        map.RunOneMove();
                        this.tableLayoutPanel1.Refresh();
                        loadMap(map);
                    }
                    else
                    {
                        string result = map.PrintResult();
                        MessageBox.Show($"{result}", "Result");
                    }
                }
                else
                {
                    string errorMessage = "No Map downloaded, Please insert a map";
                    log.Debug($"{errorMessage}");
                    MessageBox.Show($"{errorMessage}", "Important Message");
                }
            }
            catch (Exception exc)
            {
                log.Debug($"{exc.Message}");
                MessageBox.Show($"{exc.Message}", "Error");
            }

        }
    }
}
