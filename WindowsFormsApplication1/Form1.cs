using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

public delegate void ParameterizedThreadStart(string obj);
public delegate void ThreadStart();
namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        List<FileDetail> Files;
        Tuple<int, int, int, int> H_Matrix1;
        Tuple<double, double, double, double, double> H_Matrix2;
        public static string zee = "";
        public static int counter = 0;

        public Form1()
        {
            InitializeComponent();
            Files = new List<FileDetail>();
            button2.Visible = false;
            dataGridView1.Visible = false;



        }


        private void button1_Click(object sender, EventArgs e)
        {
             folderBrowserDialog1.ShowDialog();
           string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                if(selectedFolderPath != string.Empty)
                    {
                        button1.Visible = false;
                        button2.Visible = false;
                panel2.Visible = true;
                panel3.Visible = true;
                dataGridView1.Visible = true;
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
        private static Tuple<string[],int[],int> CalculateComplexity(string Path)
        {
            string line;
            int numberOfMethod=0;
            int[] MethodComplexity = new int[999];
            string[] MethodName = new string[999];
            Boolean lineIsMethod = false;
            int methodCounter = 0;
            System.IO.StreamReader str = new System.IO.StreamReader(@"" + Path);
            while ((line = str.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                    continue;



                if (line.Contains("void")   && line.Contains("(") && line.Contains(")") && !line.Contains("=") ||
                    line.Contains("int")    && line.Contains("(") && line.Contains(")") && !line.Contains("=") || 
                    line.Contains("float")  && line.Contains("(") && line.Contains(")") && !line.Contains("=") ||
                    line.Contains("string") && line.Contains("(") && line.Contains(")") && !line.Contains("=") || 
                    line.Contains("char") && line.Contains("(") && line.Contains(")") && !line.Contains("="))
                   {
                    numberOfMethod++;
                    lineIsMethod = true;
                    methodCounter++;
                    if (line.Contains("protected"))
                    line= line.Replace("protected", "");

                    if (line.Contains("public"))
                        line = line.Replace("public", "");

                    if (line.Contains("private"))
                        line = line.Replace("private", "");

                    if (line.Contains("static"))
                        line = line.Replace("static", "");

                    if (line.Contains("final"))
                        line = line.Replace("final", "");

                    if (line.Contains("protected"))
                        line = line.Replace("protected", "");

                    MethodComplexity[methodCounter]++;

                    if (line.Contains("void"))
                    {
                        line = line.Replace("void", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }

                    if (line.Contains("int"))
                    {
                        line = line.Replace("int", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }

                    if (line.Contains("float"))
                    {
                        line = line.Replace("float", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }

                    if (line.Contains("char"))
                    {
                        line = line.Replace("char", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }

                    if (line.Contains("string"))
                    {
                        line = line.Replace("string", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }

                    if (line.Contains("String"))
                    {
                        line = line.Replace("String", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }
                    if (line.Contains("Boolean"))
                    {
                        line = line.Replace("Boolean", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }
                    if (line.Contains("boolean"))
                    {
                        line = line.Replace("boolean", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }
                    if (line.Contains("List<>"))
                    {
                        line = line.Replace("List<>", "");
                        var x = line.Split('(');
                        MethodName[methodCounter] = x[0];
                    }


                }
                if (lineIsMethod)
                {

                    if (line.Contains("if") || line.Contains("else") || line.Contains("case") || line.Contains("default"))
                    {
                        MethodComplexity[methodCounter]++;
                    }
                    if (line.Contains("for") || line.Contains("while") || line.Contains(" do ") || line.Contains("break") || line.Contains("continue"))
                    {
                        MethodComplexity[methodCounter]++;
                    }
                    if (line.Contains("&&") || line.Contains("||") || line.Contains("?"))
                    {
                        MethodComplexity[methodCounter]++;
                    }
                    if (line.Contains("catch") || line.Contains("finally") || line.Contains("throw") || line.Contains("throws"))
                    {
                        MethodComplexity[methodCounter]++;
                    }
                }
}
            return Tuple.Create(MethodName,MethodComplexity, numberOfMethod);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            var FileName = dataGridView1.SelectedCells[0].Value;
            var FileDirectory = dataGridView1.SelectedCells[1].Value;
            string FilePath = FileDirectory + "/" + FileName;
            int z = 0;
            Tuple<string[],int[],int> tpl= CalculateComplexity(FilePath);

            DataTable dt = new DataTable();

            dt.Columns.Add("Methods");
            dt.Columns.Add("Complexity");

            for (int i = 1; i < tpl.Item1.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = tpl.Item1[i];
                if(tpl.Item2[i]>0)
                dr[1] = tpl.Item2[i];
                dt.Rows.Add(dr);

            }
            label23.Text = tpl.Item3.ToString() ;
            
            dataGridView2.DataSource = dt;

            dataGridView2.Columns[0].Width = 170;// The id column 
            dataGridView2.Columns[1].Width = 70;
            //label28.Text= CalculateComplexity(FilePath).Item1[0]+" = " + CalculateComplexity(FilePath).Item2[0];




            H_Matrix1 = CalculateHalstead1(FilePath);
             H_Matrix2 = CalculateHalstead2(H_Matrix1.Item1, H_Matrix1.Item2, H_Matrix1.Item3, H_Matrix1.Item4);
            label11.Text = ""+H_Matrix1.Item3;
            label12.Text = "" + H_Matrix1.Item1;
            label13.Text = "" + H_Matrix1.Item2;
            label14.Text = "" + H_Matrix1.Item4;
            label15.Text = "" + H_Matrix2.Item1;
            label16.Text = "" + H_Matrix2.Item2;
            label17.Text = "" + H_Matrix2.Item3.ToString("#####.##");
            label18.Text = "" + H_Matrix2.Item4;
            label19.Text = "" + H_Matrix2.Item5.ToString("#####.##");


        }

        private Tuple<double, double, double, double , double> CalculateHalstead2(int Operand, int n1, int Operator, int n2)
        {
            double ProgramLenght =Operand+Operator;
            double SizeOfVocabulary=n1+n2;
            double ProgramVolume= ProgramLenght*(Math.Log(SizeOfVocabulary, 2));
            double DifficultyLevel = (n1/2)*(Operator / n2) ;
            double ProgramLevel = 1 / DifficultyLevel;


            return Tuple.Create(ProgramLenght, SizeOfVocabulary, ProgramVolume, DifficultyLevel, ProgramLevel);

        }

        private Tuple<int,int,int,int> CalculateHalstead1(string filePath)
        {


            int Operater = 0, n1 = 0, Operand = 0, n2 = 0 ;
      

            var line="";
            List<string> l = new List<string>();
            List<string> Operads = new List<string>();
            Boolean[] UniqueOperands= new Boolean[150];
            Boolean method = false ;
            int i = 0;
            System.IO.StreamReader str = new System.IO.StreamReader(@"" + filePath);
            while ((line = str.ReadLine()) != null)
            {
                if (line.Contains("void") && line.Contains("(") && line.Contains(")") || 
                    line.Contains("int") && line.Contains("(") && line.Contains(")") || 
                    line.Contains("float") && line.Contains("(") && line.Contains(")") ||
                    line.Contains("double") && line.Contains("(") && line.Contains(")") || 
                    line.Contains("char") && line.Contains("(") && line.Contains(")") ||
                    line.Contains("string") && line.Contains("(") && line.Contains(")") || 
                    line.Contains("List") && line.Contains("(") && line.Contains(")")) {
                    method = true;
                    continue;
                }
                if (line.Contains("break")) { Operater++; UniqueOperands[0]=true; line =line= line.Replace("break", " ");  }
                  if (line.Contains("case")) { Operater++; UniqueOperands[1] = true; line = line.Replace("case", " "); }
                if (line.Contains("continue")) { Operater++; UniqueOperands[2] = true; line = line.Replace("continue", " "); }
                if (line.Contains("default")) { Operater++; UniqueOperands[3] = true; line = line.Replace("default", " "); }
                if (line.Contains("if")) { Operater++; UniqueOperands[4] = true; line = line.Replace("if", " "); }
                if (line.Contains("else")) { Operater++; UniqueOperands[5] = true; line = line.Replace("else", " "); }
                if (line.Contains("for")) { Operater++; UniqueOperands[6] = true; line = line.Replace("for", " "); }
                if (line.Contains("goto")) { Operater++; UniqueOperands[7] = true; line = line.Replace("goto", " "); }
                if (line.Contains("new")) { Operater++; UniqueOperands[8] = true; line = line.Replace("new", " "); }
                if (line.Contains("return")) { Operater++; UniqueOperands[9] = true; line = line.Replace("return", " "); }
                if (line.Contains("operator")) { Operater++; UniqueOperands[10] = true; line = line.Replace("operator", " "); }
                if (line.Contains("private")) { Operater++; UniqueOperands[11] = true; line = line.Replace("private", " "); }
                if (line.Contains("protected")) { Operater++; UniqueOperands[12] = true; line = line.Replace("protected", " "); }
                if (line.Contains("public")) { Operater++; UniqueOperands[13] = true; line = line.Replace("public", " "); }
                if (line.Contains("protected")) { Operater++; UniqueOperands[14] = true; line = line.Replace("protected", " "); }
                if (line.Contains("sizeof")) { Operater++; UniqueOperands[15] = true; line = line.Replace("sizeof", " "); }
                if (line.Contains("struct")) { Operater++; UniqueOperands[16] = true; line = line.Replace("switch", " "); }
                if (line.Contains("switch")) { Operater++; UniqueOperands[17] = true; line = line.Replace("switch", " "); }
                if (line.Contains("union")) { Operater++; UniqueOperands[18] = true; line = line.Replace("union", " "); }
                if (line.Contains("while")) { Operater++; UniqueOperands[19] = true; line = line.Replace("while", " "); }
                if (line.Contains("this")) { Operater++; UniqueOperands[20] = true; line = line.Replace("this", " "); }
                if (line.Contains("namespace")) { Operater++; UniqueOperands[21] = true; line = line.Replace("namespace", " "); }
                if (line.Contains("using")) { Operater++; UniqueOperands[22] = true; line = line.Replace("using", " "); }
                if (line.Contains("try")) { Operater++; UniqueOperands[23] = true; line = line.Replace("try", " "); }
                if (line.Contains("catch")) { Operater++; UniqueOperands[24] = true; line = line.Replace("catch", " "); }
                if (line.Contains("throw")) { Operater++; UniqueOperands[25] = true; line = line.Replace("throw", " ");}
                if (line.Contains("throws")) { Operater++; UniqueOperands[26] = true; line = line.Replace("throws", " "); }
                if (line.Contains("finally")) { Operater++; UniqueOperands[27] = true; line = line.Replace("finally", " "); }
                if (line.Contains("strictfp")) { Operater++; UniqueOperands[28] = true; line = line.Replace("strictfp", " "); }
                if (line.Contains("instanceof")) { Operater++; UniqueOperands[29] = true; line = line.Replace("instanceof", " "); }
                if (line.Contains("interface")) { Operater++; UniqueOperands[30] = true; line = line.Replace("interface", " "); }
                if (line.Contains("extends")) { Operater++; UniqueOperands[31] = true; line = line.Replace("extends", " "); }
                if (line.Contains("implements")) { Operater++; UniqueOperands[32] = true; line = line.Replace("implements", " "); }
                if (line.Contains("abstract")) { Operater++; UniqueOperands[33] = true; line = line.Replace("abstract", " "); }
                if (line.Contains("concrete")) { Operater++; UniqueOperands[34] = true; line = line.Replace("concrete", " "); }
                if (line.Contains("const_cast")) { Operater++; UniqueOperands[35] = true; line = line.Replace("const_cast", " "); }
                if (line.Contains("static_cast")) { Operater++; UniqueOperands[36] = true; line = line.Replace("static_cast", " "); }
                if (line.Contains("dynamic_cast")) { Operater++; UniqueOperands[37] = true; line = line.Replace("dynamic_cast", " "); }
                if (line.Contains("reinterpret_cast")) { Operater++; UniqueOperands[38] = true; line = line.Replace("reinterpret_cast", " "); }
                if (line.Contains("typeid")) { Operater++; UniqueOperands[39] = true; line = line.Replace("break", " "); }
                if (line.Contains("explicit")) { Operater++; UniqueOperands[40] = true; line = line.Replace("typeid", " "); }
                if (line.Contains("true")) { Operater++; UniqueOperands[41] = true; line = line.Replace("true", " "); }
                if (line.Contains("false")) { Operater++; UniqueOperands[42] = true; line = line.Replace("false", " "); }
                if (line.Contains("typename")) { Operater++; UniqueOperands[43] = true; line = line.Replace("typename", " "); }
                if (line.Contains("explicit")) { Operater++; UniqueOperands[44] = true; line = line.Replace("explicit", " "); }
                if (line.Contains("auto")) { Operater++; UniqueOperands[45] = true; line = line.Replace("auto", " "); }
                if (line.Contains("extern")) { Operater++; UniqueOperands[46] = true; line = line.Replace("extern", " "); }
                if (line.Contains("register")) { Operater++; UniqueOperands[47] = true; line = line.Replace("register", " "); }
                if (line.Contains("static")) { Operater++; UniqueOperands[48] = true; line = line.Replace("static", " "); }
                if (line.Contains("typedef")) { Operater++; UniqueOperands[49] = true; line = line.Replace("typedef", " "); }
                if (line.Contains("virtual")) { Operater++; UniqueOperands[50] = true; line = line.Replace("virtual", " "); }
                if (line.Contains("mutable")) { Operater++; UniqueOperands[51] = true; line = line.Replace("mutable", " "); }
                if (line.Contains("inline")) { Operater++; UniqueOperands[52] = true; line = line.Replace("inline", " "); }
                if (line.Contains("const")) { Operater++; UniqueOperands[53] = true; line = line.Replace("const", " "); }
                if (line.Contains("friend")) { Operater++; UniqueOperands[54] = true; line = line.Replace("friend", " "); }
                if (line.Contains("volatile")) { Operater++; UniqueOperands[55] = true; line = line.Replace("volatile", " "); }
                if (line.Contains("transient")) { Operater++; UniqueOperands[56] = true; line = line.Replace("transient", " "); }
                if (line.Contains("final")) { Operater++; UniqueOperands[57] = true; line = line.Replace("final", " "); }
                if (line.Contains("!=")) { Operater++; UniqueOperands[58] = true; line = line.Replace("break", " "); }
                if (line.Contains("%")) { Operater++; UniqueOperands[59] = true; line = line.Replace("%", ""); }
                if (line.Contains("%=")) { Operater++; UniqueOperands[60] = true; line = line.Replace("%=", " "); }
                if (line.Contains("&")) { Operater++; UniqueOperands[61] = true; line = line.Replace("&", " "); }
                if (line.Contains("&&")) { Operater++; UniqueOperands[62] = true; line = line.Replace("&&", " "); }
                if (line.Contains("||")) { Operater++; UniqueOperands[63] = true; line = line.Replace("||", " "); }
                if (line.Contains("(")) { Operater++; UniqueOperands[64] = true; line = line.Replace("(", " "); }
                if (line.Contains(")")) { Operater++; UniqueOperands[65] = true; line = line.Replace(")", " "); }
                if (line.Contains("{")) { Operater++; UniqueOperands[66] = true; line = line.Replace("{", " "); }
                if (line.Contains("}")) {
                    if (method)
                    {
                        line = line.Replace("}", " ");
                        method = false;
                    }
                    else
                    {

                        Operater++; UniqueOperands[67] = true; line = line.Replace("}", " ");
                    }
                        
                                        }
                if (line.Contains("[")) { Operater++; UniqueOperands[68] = true; line = line.Replace("[", " "); }
                if (line.Contains("]")) { Operater++; UniqueOperands[69] = true; line = line.Replace("]", " "); }
                if (line.Contains("*")) { Operater++; UniqueOperands[70] = true; line = line.Replace("*", " "); }
                if (line.Contains("*=")){ Operater++; UniqueOperands[71] = true; line = line.Replace("*=", " "); }
                if (line.Contains("+")) { Operater++; UniqueOperands[72] = true; line = line.Replace("+", " "); }
                if (line.Contains("++")){ Operater++; UniqueOperands[73] = true; line = line.Replace("++", " "); }
                if (line.Contains("+=")) { Operater++; UniqueOperands[74] = true; line = line.Replace("+=", " "); }
                if (line.Contains(",")) { Operater++; UniqueOperands[75] = true; line = line.Replace(",", " "); }
                if (line.Contains("-")) { Operater++; UniqueOperands[76] = true; line = line.Replace("-", " "); }
                if (line.Contains("--")) { Operater++; UniqueOperands[77] = true; line = line.Replace("--", " "); }
                if (line.Contains("-=->")) { Operater++; UniqueOperands[78] = true; line = line.Replace("-=->", " "); }
                if (line.Contains(".")) { Operater++; UniqueOperands[79] = true; line = line.Replace(".", " "); }
                if (line.Contains("...")) { Operater++; UniqueOperands[80] = true; line = line.Replace("...", " "); }
                if (line.Contains("/")) { Operater++; UniqueOperands[81] = true; line = line.Replace("/", " "); }
                if (line.Contains("/=")) { Operater++; UniqueOperands[82] = true; line = line.Replace("/=", " "); }
                if (line.Contains("::")) { Operater++; UniqueOperands[83] = true; line = line.Replace("::", " "); }
                else if (line.Contains(":")) { Operater++; UniqueOperands[84] = true; line = line.Replace(":", " "); }
                if (line.Contains("<")) { Operater++; UniqueOperands[85] = true; line = line.Replace("<", " "); }
                if (line.Contains("<<")) { Operater++; UniqueOperands[86] = true; line = line.Replace("<<", " "); }
                if (line.Contains("<<=")) { Operater++; UniqueOperands[87] = true; line = line.Replace("<<=", " "); }
                if (line.Contains("<=")) { Operater++; UniqueOperands[88] = true; line = line.Replace("<=", " "); }
                if (line.Contains("==")) { Operater++; UniqueOperands[89] = true; line = line.Replace("==", " "); }
                else   if (line.Contains("=")) { Operater++; UniqueOperands[90] = true; line = line.Replace("=", " "); }
                if (line.Contains(">")) { Operater++; UniqueOperands[91] = true; line = line.Replace(">", " "); }
                if (line.Contains(">=")) { Operater++; UniqueOperands[92] = true; line = line.Replace(">=", " "); }
                if (line.Contains(">>")) { Operater++; UniqueOperands[93] = true; line = line.Replace(">>", " "); }
                if (line.Contains(">>>")) { Operater++; UniqueOperands[94] = true; line = line.Replace(">>>", " "); }
                if (line.Contains(">>=>>>=")) { Operater++; UniqueOperands[95] = true; line = line.Replace(">>=>>>=", " "); }
                if (line.Contains("?")) { Operater++; UniqueOperands[96] = true; line = line.Replace("?", " "); }
                if (line.Contains("^")) { Operater++; UniqueOperands[97] = true; line = line.Replace("^", " "); }
                if (line.Contains("^=")) { Operater++; UniqueOperands[98] = true; line = line.Replace("^=", " "); }
                if (line.Contains("|")) { Operater++; UniqueOperands[99] = true; line = line.Replace("|", " "); }
                if (line.Contains("|=")) { Operater++; UniqueOperands[100] = true; line = line.Replace("|=", " "); }
                if (line.Contains("~")) { Operater++; UniqueOperands[101] = true; line = line.Replace("~", " "); }
                if (line.Contains(";")) { Operater++; UniqueOperands[102] = true; line = line.Replace(";", " "); }
                if (line.Contains("=&")) { Operater++; UniqueOperands[103] = true; line = line.Replace("=&", " "); }
                if (line.Contains("“")) { Operater++; UniqueOperands[104] = true; line = line.Replace("“", " "); }
                if (line.Contains("'")) { Operater++; UniqueOperands[105] = true; line =line.Replace("'", " "); }
                if (line.Contains("int")) { Operater++; UniqueOperands[106] = true;line= line.Replace("int", " "); }
                if (line.Contains("float")) { Operater++; UniqueOperands[107] = true; line = line.Replace("float", " "); }
                if (line.Contains("double")) { Operater++; UniqueOperands[108] = true; line = line.Replace("double", " "); }
                if (line.Contains("char")) { Operater++; UniqueOperands[109] = true; line = line.Replace("char", " "); }
                if (line.Contains("string")) { Operater++; UniqueOperands[110] = true; line = line.Replace("string", " "); }
              else   if (line.Contains("String")) { Operater++; UniqueOperands[111] = true; line = line.Replace("String", " "); }
                if (line.Contains("boolean")) { Operater++; UniqueOperands[112] = true; line = line.Replace("boolean", " "); }
               else if (line.Contains("Boolean")) { Operater++; UniqueOperands[113] = true; line = line.Replace("Boolean", " "); }


                l.Add( line);

            }
            
            foreach (var x in l)
            {
                string[] st = x.Split(' ');
                foreach(string s in st)
                {
                    if(!string.IsNullOrEmpty(s))
                    Operads.Add(s);
                }
            }
            var g = Operads.Select(k => k).Distinct();

            Operand = Operads.Count();
            n2 = g.Count();


            for (int j = 0; j < UniqueOperands.Length; j++)
            {
                if (UniqueOperands[j])
                {

                      n1++;

                }
            }



            return Tuple.Create(Operater,n1,Operand,n2 );
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
