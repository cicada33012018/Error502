using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.WinForms;
using System.IO;
using System.Threading;

public delegate void ParameterizedThreadStart(string obj);
public delegate void ThreadStart();
namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        List<FileDetail> Files;
        public static string zee = "";
        public static int counter = 0;

        public Form1()
        {
            InitializeComponent();
            Files = new List<FileDetail>();
            button2.Visible = false;

        }


        private void button1_Click(object sender, EventArgs e)
        {
             folderBrowserDialog1.ShowDialog();
           string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                if(selectedFolderPath != string.Empty)
                    {
                        button1.Visible = false;
                        button2.Visible = false;
                    }
            Files = getFiles(selectedFolderPath);
           


        }

        

        private List<FileDetail> getFiles(string selectedFolderPath)
        {
            List<FileDetail> f = null;
           
            string[] files = Directory.GetFiles(selectedFolderPath, "*.java", SearchOption.AllDirectories);
           f = new List<FileDetail>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Directory");


            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                DataRow dr = dt.NewRow();
                dr[0] = ""+info.Name;
                dr[1] = ""+info.DirectoryName;
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            return f;
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private static int CalculateComplexity(string Path)
        {
            string line;
            int complexity = 0;
            System.IO.StreamReader str = new System.IO.StreamReader(@"" + Path);
            while ((line = str.ReadLine()) != null)
            {
                if (line.Contains("void") || line.Contains("int") || line.Contains("float") || line.Contains("string") || line.Contains("char") && line.Contains("("))
                { complexity++; }
                if (line.Contains("if") || line.Contains("else") || line.Contains("case") || line.Contains("default"))
                    complexity++;
                if (line.Contains("for") || line.Contains("while") || line.Contains(" do ") || line.Contains("break") || line.Contains("continue"))
                    complexity++;
                if (line.Contains("&&") || line.Contains("||") || line.Contains("?"))
                    complexity++;
                if (line.Contains("catch") || line.Contains("finally") || line.Contains("throw") || line.Contains("throws"))
                    complexity++;
            }
            return complexity;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var FileName = dataGridView1.SelectedCells[0].Value;
            var FileDirectory = dataGridView1.SelectedCells[1].Value;
            string FilePath = FileDirectory + "/" + FileName;
            MessageBox.Show(""+CalculateComplexity(FilePath));
        }
    }
}
