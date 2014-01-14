using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSF_3DedExporter
{
    public partial class FenetrePrincipale : Form
    {
        public FenetrePrincipale()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled = false;
            if (ExportFile(textBox1.Text))
            {
                var result = MessageBox.Show("Export done !\nOpen destination Directory ?", "Export finished",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(System.IO.Directory.GetParent(textBox1.Text).ToString());
                }
            }
            else
            {
            }

            button1.Enabled = true;
        }
        private bool ExportFile(String file)
        {
            int indexLine = 0;
            string FichierClasseVehicule = "";
            string FichierClasseEntete = "";
            try
            {
                foreach (string line in System.IO.File.ReadLines(file))
                {
                    if (indexLine > 0)
                    {
                        string[] parts = line.Split(';');
                        FichierClasseVehicule += "     class Item" + (indexLine - 1).ToString() + Environment.NewLine;
                        FichierClasseVehicule += "     {" + Environment.NewLine;
                        FichierClasseVehicule += "       position[]=" + parts[6] + ";" + Environment.NewLine;
                        FichierClasseVehicule += "       azimut =" + parts[5] + ";" + Environment.NewLine;
                        FichierClasseVehicule += "       id=" + (indexLine - 1).ToString() + ";" + Environment.NewLine;
                        FichierClasseVehicule += @"       side=""EMPTY"";" + Environment.NewLine;
                        FichierClasseVehicule += "        vehicle=" + parts[1] + ";" + Environment.NewLine;
                        FichierClasseVehicule += @"       init=""this setvectorup [0,0,1];this setposASL [" + parts[2] + "," + parts[3] + "," + parts[4] + @"];"";" + Environment.NewLine;
                        FichierClasseVehicule += "     };" + Environment.NewLine;
                    }
                    indexLine++;
                }

                FichierClasseEntete += "version=12;" + Environment.NewLine;
                FichierClasseEntete += "class Mission" + Environment.NewLine;
                FichierClasseEntete += "{" + Environment.NewLine;
                FichierClasseEntete += "class Intel" + Environment.NewLine;
                FichierClasseEntete += "{};" + Environment.NewLine;
                FichierClasseEntete += "class Vehicles" + Environment.NewLine;
                FichierClasseEntete += "{" + Environment.NewLine;
                FichierClasseEntete += "items=" + (indexLine - 1).ToString() + ";" + Environment.NewLine;
                FichierClasseEntete += FichierClasseVehicule;
                FichierClasseEntete += "class Intro" + Environment.NewLine;
                FichierClasseEntete += "{};" + Environment.NewLine;
                FichierClasseEntete += "class OutroWin" + Environment.NewLine;
                FichierClasseEntete += "{};" + Environment.NewLine;
                FichierClasseEntete += "class OutroLoose" + Environment.NewLine;
                FichierClasseEntete += "{};" + Environment.NewLine;
                FichierClasseEntete += "};" + Environment.NewLine;
                FichierClasseEntete += "};" + Environment.NewLine;
                System.IO.File.WriteAllText(System.IO.Directory.GetParent(file) + @"\mission.sqm", FichierClasseEntete);
                return true;
            }
            catch
            {
                return false;
            };


        }

        private void button2_Click(object sender, EventArgs e)
        {
           OpenFileDialog FichierSource = new OpenFileDialog();
          FichierSource.Filter = "CSV files (*.csv)|*.csv";
           FichierSource.ShowDialog();
           textBox1.Text = FichierSource.FileName;
        }
    }
}
