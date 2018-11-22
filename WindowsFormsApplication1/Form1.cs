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
        //string selectedFolderPath;
       // string selectedFilePath;
        List<FileDetail> Files;
        public static string zee = "";

        public Form1()
        {
            InitializeComponent();
            Files = new List<FileDetail>();
            listBox1.Visible = false;
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
                        listBox1.Visible = true;
                    }
            Files = getFiles(selectedFolderPath);



        }

        

        private List<FileDetail> getFiles(string selectedFolderPath)
        {
            List<FileDetail> f = null;
           
            string[] files = Directory.GetFiles(selectedFolderPath, "*.java", SearchOption.AllDirectories);
           f = new List<FileDetail>();
            
            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                f.Add(new FileDetail(info.Name,info.DirectoryName));
                    
               listBox1.Items.Add(info.Name);
              
            }

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
            zee = listBox1.SelectedItem.ToString();
            string ponka = listBox1.SelectedIndex.ToString();
            MessageBox.Show(ponka +" : " + zee + Files[0].ToString() + "   " + Files[1].ToString());
            
        }
    }
}
