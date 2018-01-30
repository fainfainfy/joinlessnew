using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace FyColocation
{
    public partial class Form_joinlessbegin : Form
    {
        #region
        public string inputfilepath = "";
        public static string outputfilepath = "";
        public static string outputname="";
        public double prev = 0.00;
        public int range = 0;
        public double rand = 0;
        public double occ = 0 ;
        public double quality = 0;
        public double w = 0;


        public List<Dictionary<string, Dictionary<string, HashSet<int>>>> fnn = new  List<Dictionary<string, Dictionary<string, HashSet<int>>>>();//存放频繁模式表实例
        public List<string> printf = new List<string>();//频繁模式,形式为1+2+3{12-16-19;12-16-20;13-17-21}(0.5,0.4,0.6)=0.4    
        public List<string> printf11 = new List<string>();//频繁模式,形式为1+2+3{12-16-19;12-16-20;13-17-21}(0.5,0.4,0.6)=0.4    
       // public List<List<string>> listfn = new List<List<string>>();//频繁模式,形式为1+2+3
        public  List<string> listfn = new List<string>();//频繁模式,形式为1+2+3
       // public List<List<string>> listout = new List<List<string>>();//频繁模式输出表实例1+2+3{1-2-3；2-3-4；}=0.2,0.4,0.4
        //Dictionary<string, Dictionary<string, HashSet<string>>> dicstar = new Dictionary<string, Dictionary<string, HashSet<string>>>();//存放星星表
        Dictionary<int, Dictionary<int,Dictionary<int,HashSet<int>>>> dicstar = new Dictionary<int, Dictionary<int,Dictionary<int, HashSet<int>>>>();//存放星星表
        public Dictionary<string, string> fsnn = new Dictionary<string, string>();//物化
        public Dictionary<int, Instance> sin = new Dictionary<int, Instance>();////存放所有实例输入变量
        public Dictionary<string, string> prvevaluedic = new Dictionary<string, string>();//存放所有频繁模式的prev

        public List<string> s1 = new List<string>();//s1 放的是有序的特征实例及其位置，s22放每个特征的个数，也是有序的
        public List<int> s22 = new List<int>(); //s22放的是特征对应的个数 如：[0]=0，[1]=5,[2]=7说明特征1有5个实例，特征2有2个实例 两个数组差为特征的实例范围
        public List<int> s3 = new List<int>();   //s3放的是实例对应的特征  如：[1]=1，[2]=1,[3]=2 说明实例1，2都是特征1的实例 实例3是特征2的实例
   
        public List<string> star = new List<String>();
        public List<string> fs11 = new List<string>();//二阶频繁模式的布尔频繁值，与FS1一一对应 
        public List<double> fs1 = new List<double>();//二阶频繁模式及其参与率[0]=1+2：pri,prj=PI
        public List<string> fname = new List<String>();//特征编号与真实特征的映射 [1]=A [2]=B
        public List<int> fs2 = new List<int>();//存放对于每个i其在队列中的位置，作用s22

     
        public List<int> sizecounti = new List<int>();//每个特征可能生成的最大阶数
        public List<string> starneighbor = new List<string>();//某特征在星星中与其二阶频繁的特征集
        public List<double> fsn = new List<double>();
        public List<int> fsn2 = new List<int>();

        #endregion
        //   public Dictionary<string, string> fsnn = new Dictionary<string, string>();
        public Form_joinlessbegin()
        {
            InitializeComponent();
        }
         

        private void button_datasource_Click(object sender, EventArgs e)//选择数据源
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt文件|*.txt|所有文件|*.*";
                openFileDialog.ShowDialog();
                this.Text = openFileDialog.FileName;
                //openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\   
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                inputfilepath = this.Text.ToString();
                label3.Text = "input：" + this.Text.ToString();
            }
            catch { MessageBox.Show("please choose an input file"); }
 
        }

        private void button_scan_Click(object sender, EventArgs e)//选择文件输出位置
        {

            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.Text = path.SelectedPath;
            textBox_file.Text = path.SelectedPath;
            outputfilepath = textBox_file.Text.ToString();
        }

        private void button_begin_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox_prev.Text == "" || textBox_r.Text == "")
                { MessageBox.Show("此值不能为缺省，请重新输入！"); }
                else if (double.Parse(textBox_prev.Text) > 0.00 && double.Parse(textBox_prev.Text) < 1.00 && double.Parse(textBox_r.Text) > 0)
                {
                    prev = double.Parse(textBox_prev.Text);
                    rand = double.Parse(textBox_r.Text);
                   // cds = double.Parse(textBox_cds.Text);
                    //occ = double.Parse(textBox_occupancy.Text);
                }
                else
                {
                    MessageBox.Show("此值不合法，请重新输入！");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("输入错误");
            }
        //    richTextBox1.AppendText("开始计算" + Environment.NewLine);
           Thread thread = new Thread(() =>
         {
           
            InDatasouce F = new FileDdatasouce();
            List<string> inputlist = new List<string>();
            inputlist = F.inputdata(inputfilepath);
            //  ------------------------------------------------------------------------------------获得初始数据
            string data = System.DateTime.Today.DayOfYear.ToString() + "shuchu" + System.DateTime.Now.Minute.ToString();
            outputname = outputfilepath + "\\" + data + ".txt";
             //=============================================================================计算程序运行时间
             Stopwatch  timecost= new Stopwatch();
             timecost.Start();
            beginfile();
            timecost.Stop();
            TimeSpan ts2 = timecost.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            MessageBox.Show("计算完毕，共花费"+ts2.TotalMilliseconds+"ms,为您输出结果到txt");
          
           });
           thread.Start();//启动线程
           thread.IsBackground = true;//后台运行
        //   MessageBox.Show("输出成功！");
           this.Close();
         //  MessageBox.Show("输出成功！");
        }//开始运行

        void beginfile()//获得初始数据
        {
            // try
            //  {
            // List<Instance> star = new List<Instance>();//存储数据的表

            InDatasouce F = new FileDdatasouce();
            List<string> inputlist = new List<string>();
            inputlist = F.inputdata(inputfilepath);
            List<double> ax = new List<double>();
            List<double> ay = new List<double>();
            //  ------------------------------------------------------------------------------------获得初始数据
            #region
           // MessageBox.Show("开始读入数据");
            for (int i = 0; i < inputlist.Count; i++)//获得初始数据
            {
                string[] inputa = inputlist[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
              //  string s = inputa[1].ToString() + "." + inputa[0].ToString() + "(" + System.Convert.ToDouble(inputa[2]) + "," + System.Convert.ToDouble(inputa[3]) + ")";
                StringBuilder s = new StringBuilder();
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCodea = (int)asciiEncoding.GetBytes(inputa[1].ToString())[0];
                s.Append(intAsciiCodea.ToString());
                s.Append(".");
                s.Append(inputa[0].ToString());
                s.Append("(");
                s.Append(System.Convert.ToDouble(inputa[2]));
                s.Append(",");
                s.Append(System.Convert.ToDouble(inputa[3]));
               // string b = inputa[1] + "(" + inputa[2] + "," + inputa[3] + ")";
                s1.Add(s.ToString());
                ax.Add(System.Convert.ToDouble(inputa[2]));
                ay.Add(System.Convert.ToDouble(inputa[3]));

            }
            MessageBox.Show("数据读取完毕，进入物化阶段");
            //字典序排序

            //-----------------------------------------------------------------------------------------全局变量赋值
            s1.Sort();//s1 放的是有序的特征实例及其位置，s2放每个特征的个数，也是有序的
            s1.Insert(0, "begin");
            int k = 1;
            int s3no = 1;
            s1.Add("over");//s1有一个over结尾，避免无法将其中的最后一个特征加不进S22的情况
            // s2.Add("0");
            s22.Add(0);
            s3.Add(0);
            s3.Add(1);
            for (int i = 1; i < s1.Count - 1; i++)
            {
                string[] a = s1[i].Split('.');
                string[] b = s1[i + 1].Split('.');
                
                if (!b[0].Equals(a[0]))
                {
                    s3no++;
                    s3.Add(s3no);//s3放的是实例对应的特征  如：[1]=1，[2]=1,[3]=2 说明实例1，2都是特征1的实例 实例3是特征2的实例
                    s22.Add(k);//s22放的是特征对应的个数 如：[0]=0，[1]=5,[2]=7说明特征1有5个实例，特征2有2个实例 两个数组差为特征的实例范围
                    k++;
                }
                else
                {
                    s3.Add(s3no);
                    k++;
                }
            }

            #endregion
            //--------------------------------------------------------------------------------------物化
            #region
            double tmpx = ax[0];
            double tmpy = ay[0];
            for (int i = 0; i < ax.Count; i++)//获得最大行数
            {
                if (ax[i] > tmpx)
                    tmpx = ax[i];
            }
            for (int j = 0; j < ay.Count; j++)//获得最大列数
            {
                if (ay[j] > tmpy)
                    tmpy = ay[j];
            }


            int maxx = (int)tmpx / ((int)rand);
            int maxy = (int)tmpy / ((int)rand);


            StringBuilder[,] co = new StringBuilder[maxx + 1, maxy + 1];//确定矩阵的大小
            
            for (int i = 0; i < maxx + 1; i++)//初始化矩阵
            {
                for (int j = 0; j < maxy + 1; j++)
                {
                    co[i, j] = new StringBuilder("");
                     
                }
            }
            star.Add("begin");//使得star表下标编号与实例完全对应     

            for (int i = 1; i < s1.Count - 1; i++)//物化
            {
                //将特征按照s1的下标重新顺序编号
                string[] a = s1[i].Split('(');//特征
                string[] b = a[0].Split('.');//实例
                string[] c = a[1].Split(  ',' );//坐标
                Instance si = new Instance();
                int hx = (int)(double.Parse(c[0]) / (rand));
                int hy = (int)(double.Parse(c[1]) / (rand));
                int kk = i;//对实例进行整体重新编号
                si.type = s3[kk];//寻找实例的类
                si.x = double.Parse(c[0]);
                si.y = double.Parse(c[1]);
                sin.Add(kk, si);//以重新编号的实例号为主键将其加入hash表
                star.Add(kk.ToString());
                string coo = kk.ToString();//放入实例编号
                StringBuilder m = new StringBuilder(1);
                co[hx, hy].Append(coo);
                co[hx, hy].Append(";");

            }
            MessageBox.Show("物化完毕:长" + maxx + "宽" + maxy + Environment.NewLine);
            
            
            
            #endregion

            //--------------------------------------------------------------------------------------星星化
            #region
       //     MessageBox.Show("开始星星化");
            for (int i = 0; i < maxx + 1; i++)
                for (int j = 0; j < maxy + 1; j++)
                {
                    if (!co[i, j].Equals(""))
                    {
                        string[] cp1 = co[i, j].ToString().Split(';');//拆分同一格子内实例号
                        List<int> indexcount = new List<int>();//存放统一特征编号
                        for (int ii = 0; ii < cp1.Count()-1; ii++)
                        {
                            int index = Int32.Parse(cp1[ii].ToString());
                            indexcount.Add(index);
                            //存放统一特征编号并且已经经过排序
                        }
                        indexcount.Sort();//------
                        for (int ii = 0; ii < indexcount.Count - 1; ii++) //----------------一格之间邻居，先查看是否是同一特征
                        {
                            for (int jj = ii + 1; jj < indexcount.Count; jj++)
                            {
                                if (distance(sin[indexcount[ii]].x, sin[indexcount[ii]].y, sin[indexcount[jj]].x, sin[indexcount[jj]].y, rand))
                                {
                                  //  MessageBox.Show(sin[indexcount[ii]].x + "," + sin[indexcount[ii]].y + "," + sin[indexcount[jj]].x + "," + sin[indexcount[jj]].y + "," + rand);
                                    if (sin[indexcount[ii]].type < sin[indexcount[jj]].type)
                                    {
                                        addstar(sin[indexcount[ii]].type, sin[indexcount[jj]].type, indexcount[ii], indexcount[jj]);
                                     //   MessageBox.Show(sin[indexcount[ii]].type + "," + sin[indexcount[jj]].type + "," + indexcount[ii] + "," + indexcount[jj]);
                                    }
                                    if (sin[indexcount[ii]].type > sin[indexcount[jj]].type)
                                    {
                                        addstar(sin[indexcount[jj]].type, sin[indexcount[ii]].type, indexcount[jj], indexcount[ii]);
                                    //    MessageBox.Show(sin[indexcount[jj]].type + "," + sin[indexcount[ii]].type + "," + indexcount[jj] + "," + indexcount[ii]);
                                    }
                                   
                                     
                                }
                               
                            }
                        }

                        for (int ii = 0; ii < cp1.Length - 1; ii++)//----------------不同格之间邻居
                        {
                            int index = Int32.Parse(cp1[ii].ToString());
                            double x = sin[index].x;
                            double y = sin[index].y;
                            if (i != maxx && j != maxy && j != 0)//---------------------中间的格子
                            {
                                StringBuilder p0 = new StringBuilder();
                                p0.Append(co[i, j + 1]); p0.Append(co[i + 1, j - 1]); p0.Append(co[i + 1, j]); p0.Append(co[i + 1, j + 1]);
                                //string[]p2=new string[100*1024*1024];
                                string[] p2 = p0.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int iii = 0; iii < p2.Length ; iii++)
                                {       //string[] p3 = p2[iii].Split('(', ',', ')');
                                    if (sin[Int32.Parse(p2[iii].ToString())].type != sin[index].type)
                                    {
                                        if (distance(x, y, sin[Int32.Parse(p2[iii].ToString())].x, sin[Int32.Parse(p2[iii].ToString())].y, rand))//若邻近
                                        {
                                            if (sin[Int32.Parse(p2[iii].ToString())].type < sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[Int32.Parse(p2[iii].ToString())].type, sin[index].type, Int32.Parse(p2[iii]), index);
                                            }
                                            if (sin[Int32.Parse(p2[iii].ToString())].type > sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[index].type, sin[Int32.Parse(p2[iii].ToString())].type, index, Int32.Parse(p2[iii]));
                                            }

                                        }
                                    }
                                }
                            }
                            else if (j == 0 && i != maxx && j != maxy)//---------------------最左边的格子
                            {
                                StringBuilder p0 = new StringBuilder();
                                p0.Append(co[i, j + 1]); p0.Append(co[i + 1, j]); p0.Append(co[i + 1, j + 1]);
                                string[] p2 = p0.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int iii = 0; iii < p2.Length ; iii++)
                                {
                                    // string[] p3 = p2[iii].Split( '(', ',', ')');
                                    if (sin[Int32.Parse(p2[iii])].type != sin[index].type)
                                    {
                                        if (distance(x, y, sin[Int32.Parse(p2[iii].ToString())].x, sin[Int32.Parse(p2[iii].ToString())].y, rand))//若邻近
                                        {
                                            if (sin[Int32.Parse(p2[iii].ToString())].type < sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[Int32.Parse(p2[iii].ToString())].type, sin[index].type, Int32.Parse(p2[iii]), index);
                                            }
                                            if (sin[Int32.Parse(p2[iii].ToString())].type > sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[index].type, sin[Int32.Parse(p2[iii].ToString())].type, index, Int32.Parse(p2[iii]));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (j == maxy && i != maxx)//---------------------最右边的格子
                            {
                                StringBuilder p0 = new StringBuilder();
                                p0.Append(co[i + 1, j - 1]); p0.Append(co[i + 1, j]);
                                string[] p2 = p0.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int iii = 0; iii < p2.Length ; iii++)
                                {
                                    if (sin[Int32.Parse(p2[iii])].type != sin[index].type)
                                    {
                                        if (distance(x, y, sin[Int32.Parse(p2[iii].ToString())].x, sin[Int32.Parse(p2[iii].ToString())].y, rand))//若邻近
                                        {
                                            if (sin[Int32.Parse(p2[iii].ToString())].type < sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[Int32.Parse(p2[iii].ToString())].type, sin[index].type, Int32.Parse(p2[iii]), index);
                                            }
                                            if (sin[Int32.Parse(p2[iii].ToString())].type > sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[index].type, sin[Int32.Parse(p2[iii].ToString())].type, index, Int32.Parse(p2[iii]));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (i == maxx && j != maxy)//---------------------最下的格子
                            {
                                string p1 = co[i, j + 1].ToString();
                                string[] p2 = p1.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int iii = 0; iii < p2.Length; iii++)
                                {
                                    if (sin[Int32.Parse(p2[iii])].type != sin[index].type)
                                    {
                                        if (distance(x, y, sin[Int32.Parse(p2[iii].ToString())].x, sin[Int32.Parse(p2[iii].ToString())].y, rand))//若邻近
                                        {
                                            if (sin[Int32.Parse(p2[iii].ToString())].type < sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[Int32.Parse(p2[iii].ToString())].type, sin[index].type, Int32.Parse(p2[iii]), index);
                                            }
                                             if (sin[Int32.Parse(p2[iii].ToString())].type > sin[index].type)//若邻居的特征号在前
                                            {
                                                addstar(sin[index].type, sin[Int32.Parse(p2[iii].ToString())].type, index, Int32.Parse(p2[iii]));
                                            }
                                        }
                                    }
                                }
                            }
                        }//for循环拆分co[i,j]
                    }//----若co[i,j]不等于空
                }//co[i,j]循环         
     //       MessageBox.Show("成功星星化！" + star.Count);
      /*      foreach (var item in dicstar)
            {
                string fi = item.Key.ToString();
                foreach (var item1 in dicstar[int.Parse(fi.ToString())])
                {
                    string ai = item1.Key.ToString();
                    foreach (var item2 in dicstar[int.Parse(fi.ToString())][int.Parse(ai.ToString())])
                    {
                        string fj = item2.Key.ToString();
                        StringBuilder aj = new StringBuilder();
                        foreach (var item3 in dicstar[int.Parse(fi.ToString())][int.Parse(ai.ToString())][int.Parse(fj.ToString())])
                        {
                            aj.Append(item3+";");
                            MessageBox.Show(fi + "," + ai + "," + fj + "{" + aj + "}");
                        }
                    }

                }
            }*/
           // MessageBox.Show("");
            sin.Clear();
            star.Clear();
            for (int i = 0; i < maxx + 1; i++)//初始化矩阵
            {
                for (int j = 0; j < maxy + 1; j++)
                {
                    co[i, j] .Clear();

                }
            }
 
            #endregion

            //--------------------------------------------------------------------------------------二阶
            #region
            //给定1阶模式的频繁度
            for (int pi = 1; pi < s22.Count; pi++)
            {
                prvevaluedic.Add(pi.ToString(),1+",");
            }
                
            fs11.Add("0");//存放形如i+j=的二阶
            fs1.Add(0);//存放i+j二阶是否频繁
            fs2.Add(0);//存放对于每个i其在队列中的位置，作用s22
            int feature = s22.Count;
            MessageBox.Show("feature" + (feature-1));
            int sum = 0;
            for (int i = 1; i < feature - 1; i++)//-----------------------初始化二阶队列
            {
                for (int j = i + 1; j < feature; j++)
                {
                    fs11.Add(i + "+" + j);
                    fs1.Add(0.00);
                }

                sum += feature - 1 - i;
                fs2.Add(sum);
            }
            List<HashSet<int>> listf2 = new List<HashSet<int>>();
            Dictionary<string, Dictionary<string, HashSet<int>>> dic2n = new Dictionary<string, Dictionary<string, HashSet<int>>>();
            for (int i = 1; i < fs11.Count(); i++)//--------------------测试二阶
            {
                Dictionary<int, HashSet<int>> dictest = new Dictionary<int, HashSet<int>>();
                int starneibori = 0;
                int sumi = 0;
                int sumj = 0;
                double prev1 = 0.00;
                double previ = 0.00;
                double prevj = 0.00;
                string[] a = fs11[i].Split('+');
                int ai = int.Parse(a[0].ToString());
                int aj = int.Parse(a[1].ToString());

                HashSet<int> hashi = new HashSet<int>() { }; //存放ai中有aj的ai实例集合
                HashSet<int> hashj = new HashSet<int>() { }; //存放ai中所有aj的实例集合

                Dictionary<string, HashSet<int>> dicij = new Dictionary<string, HashSet<int>>();//string放ai的实例，HashSet放ai实例中的aj实例集合
                if (!dicstar.ContainsKey(ai))
                { break; }
                else
                {
                    foreach (var item in dicstar[ai])//对于ai,遍历ai的实例,对于AI的每个实例
                    {
                        Dictionary<int, HashSet<int>> dic1 = new Dictionary<int, HashSet<int>>();
                        dic1 = item.Value; //int为aj特征 hash为aj实例                   
                        if (dic1.ContainsKey(aj))
                        {
                            //ai的某个实例中的aj实例集合
                            dicij.Add(item.Key.ToString(), dic1[aj]);
                            hashj.UnionWith(dic1[aj]);
                        }
                    }
                }
                //计算频繁度
                sumj = hashj.Count();
                sumi = dicij.Count();
                previ = double.Parse(dicij.Keys.Count().ToString()) / double.Parse((s22[ai] - s22[ai - 1]).ToString());
                prevj = double.Parse(hashj.Count().ToString()) / double.Parse((s22[aj] - s22[aj - 1]).ToString());
                prev1 = Math.Min(previ, prevj);
                fs1[i] = 0;
                if (Math.Round(prev1, 3) > prev || Math.Round(prev1, 3) == prev)
                {
                    StringBuilder prevkkk = new StringBuilder();
                    StringBuilder kkk = new StringBuilder(2);
                    kkk.Append(fs11[i] + "=");
                    prevkkk.Append(Math.Round(previ, 2));
                    prevkkk.Append(",");
                    prevkkk.Append(+Math.Round(prevj, 2));
                    kkk.Append(prevkkk);
                    //   printf11.Add(kkk.ToString());
                    prvevaluedic.Add(fs11[i].ToString(), prevkkk.ToString() + ",");
                    dic2n.Add(fs11[i], dicij);
                    listfn.Add(fs11[i]);
                   /* if (Math.Abs(previ - prevj) > cds || Math.Abs(previ - prevj) == cds)//若含有主导特征
                    {
                        kkk.Append(fs11[i] + "  is a DFCP and");
                        string[] h = fs11[i].Split('+');
                        if (previ > prevj)
                        {
                            kkk.Append("the dominant feature is：" + h[0] + (previ - prevj));

                        }
                        else
                        {
                            kkk.Append("the dominant feature is：" + h[1] + (prevj - previ));
                        }                         
                      
                    }
                    */
                    printf11.Add(kkk.ToString());
                   
                }
            }
             //   MessageBox.Show("二阶频繁模式生成！");
                fnn.Add(dic2n);
                dicstar.Clear();
                sin.Clear();
                dicstar.Clear();
                //================================================================输出头文件行1特征表行2min_prev
                StringBuilder fs2string = new StringBuilder();
                fs2string.Append(s22[1].ToString());
                for (int yy = 2; yy < s22.Count(); yy++)
                {
                    fs2string.Append("-");
                    fs2string.Append(s22[yy].ToString());
                }

                //=======================================================================================================输出二阶
                if (!System.IO.File.Exists(outputname))
                {
                    FileStream files1 = new FileStream(outputname, FileMode.Create, FileAccess.Write);//创建写入文件
                    StreamWriter sw = new StreamWriter(files1);
                    sw.WriteLine(fs2string);
                    sw.WriteLine("min_prev=" + prev.ToString());
                    sw.WriteLine("d=" + rand.ToString());
                    //sw.WriteLine("min_fd=" + cds.ToString());
                   
                    foreach (var item in printf11)//获得初始数据
                    {
                        sw.WriteLine(item);//开始写入值
                    }
                    sw.Close();
                    files1.Close();
                }
            
            #endregion

            //--------------------------------------------------------------------------------------多阶及挖掘关键特征
            #region

            int sizek =0;
          //  while (listfn .Count != 0)
          //  {
                for (sizek = 3; sizek < feature; sizek++)
                {
                    string patternname = "";
                    List<string> listfn1 = new List<string>();//频繁模式,形式为1+2+3
                    Dictionary<string, Dictionary<string, HashSet<int>>> prevdic = new Dictionary<string, Dictionary<string, HashSet<int>>>();//放同阶的所有频繁模式
                    listfn1.Clear(); prevdic.Clear();
                    for (int i = 0; i < listfn.Count(); i++)//模式后缀连接
                    {
                        List<int> tlist = new List<int>();                       
                        string[] h = listfn[i].Split('+');                       
                        int hf = int.Parse(h[h.Count() - 1]);
                        if (hf < feature - 1)//连接后子模式测试 剪纸步
                        {
                            for (int oo = hf + 1; oo < feature; oo++)
                            {
                                tlist.Clear();                                
                                // MessageBox.Show("测试:" + listfn[i] + "-+" + oo);
                                patternname = listfn[i] + "+" + oo;
                                int sign = 1;
                                string[] hpattern = patternname.Split('+');
                                for (int j = 0; j < hpattern.Count(); j++) //需测试的连接模式
                                {
                                    tlist.Add(Int32.Parse(hpattern[j]));
                                }
                               
                                foreach (var comb in Combinations(tlist, 0, tlist.Count, sizek - 1))//测试子模式
                                {
                                    StringBuilder l = new StringBuilder();
                                    int[] hcomb = comb.Take(sizek - 1).ToArray();
                                    l.Append(hcomb[0]);
                                    for (int jj = 1; jj < comb.Take(sizek - 1).Count(); jj++)
                                    {
                                        l.Append("+");
                                        l.Append(hcomb[jj]);
                                    }
                                    l.ToString();
                                    if (!fnn[0].ContainsKey(l.ToString()))
                                    {
                                        sign = 0;
                                        break;
                                    }//剪枝步
                                }
                                if (sign == 1)//子模式都频繁时开始计算表实例
                                {
                                    // MessageBox.Show("sign=1");
                                    // List<Dictionary<string,HashSet<int>>>listdictest=new  List<Dictionary<string,HashSet<int>>>();
                                    List<string> listend = new List<string>();//存放ABCD的行实例
                                    Dictionary<string, HashSet<int>> dictest = new Dictionary<string, HashSet<int>>();   
                                  //   Dictionary<string, List<int>> dictest = new Dictionary<string, List<int>>();   
                                    int lastf = oo;//待测模式的最后一个特征
                                    List<string> listtest = new List<string>();//存放ABC的行实例var item in fnn[0][listfn[i]])//对ABC中的每条行实例
                                    foreach (var item in fnn[0][listfn[i]])//对ABC中的每条行实例
                                    {
                                        foreach (var item1 in item.Value)
                                        {
                                            StringBuilder m = new StringBuilder() { };
                                            m.Append(item.Key);
                                            m.Append("-");
                                            m.Append(item1.ToString());
                                            listtest.Add(m.ToString());
                                        }
                                    }
                                    HashSet<int> lastfhash = new HashSet<int>() {};
                                    string fij0 = hpattern[0].ToString() + "+" + lastf.ToString();
                                  //  if (!dic2n.ContainsKey(fij0))
                                  //  { break; }
                                    foreach (var itemm in dic2n[fij0])
                                    {
                                        lastfhash.UnionWith(itemm.Value);
                                    }
                                    HashSet<int> lsatfeatureinshash = new HashSet<int>();                                    
                                  //HashSet<int> thash = new HashSet<int>();
                                    HashSet<int> thash = null;
                                    thash = new HashSet<int>();
                                   
                                    // compute the table instance of candidates
                                    for (int ii = 0; ii < listtest.Count(); ii++)//对每条行实例
                                    {
                                        thash.Clear();
                                        thash.UnionWith(lastfhash); 
                                        string[] listtesth = listtest[ii].Split('-');//拆分字串的某一行实例       
                                        //对每个行实例中的实例特征，定位其二阶表实例 fnn[test]及其实例位置[h1[ii]]                                        
                                        for (int jj = 0; jj < listtesth.Count(); jj++)//对ABC行实例中的每个特征实例 
                                        {
                                          //  string fi = s3[int.Parse(listtesth[jj].ToString())].ToString();//还可以优化 直接把特征用patterh进行循环取消S3取值
                                            string fi = h[jj].ToString();
                                            string test = fi + "+" + lastf.ToString(); //size 2 prevalent pattern
                                            if (!dic2n[test].ContainsKey(listtesth[jj].ToString()))//看AD中的A是否包含A1-B1-C1中的A1，是的话取A1中的所有D
                                            { thash.Clear(); break; }//优化 只有2阶存的时候用string，hash，其它全部变成字符串A1-B1-C1;A1-B1-C2
                                            else { thash.IntersectWith(dic2n[test][listtesth[jj].ToString()]); }                               
                                        }//测试的是AB里的一条行实例A1-B1-C1中A1与D，B1与D，C1与D的D的交集
                                        if (thash.Count > 0)//得到一条新的高阶行实例 
                                        {
                                            HashSet<int> thash1 = new HashSet<int>();
                                             thash1.UnionWith(thash);
                                             dictest.Add(listtest[ii], thash1);//
                                             lsatfeatureinshash.UnionWith(thash);
                                            List<int> listt = new List<int>();
                                            foreach (var item in thash)
                                            {
                                                listt.Add(item);
                                            }                                 
                                        }
                                    }
                                    
                                    //开始计算频繁程度
                                    // string[] h2 = listtest[i].Split('-');//拆分字串的某一行实例   
                                    string jo = dictest.Keys.ToString();
                                    List<HashSet<int>> sumij = new List<HashSet<int>>();
                                    for (int p1 = 0; p1 < h.Count();p1++ )
                                    {
                                        HashSet<int> hass = new HashSet<int>() { };
                                        sumij.Add(hass);
                                    }
                                    for (int p = 0; p < h.Count(); p++)
                                    {                                       
                                        foreach (var itemu in dictest)
                                        {
                                            string[] hu = itemu.Key.Split('-');
                                            sumij[p].Add(int.Parse(hu[p].ToString()));
                                        }
                                    }
                                    sumij.Add(lsatfeatureinshash);
                                    sumij.Count();
                                    List<double> prevk = new List<double>();
                                    for (int nn = 0; nn < h.Count(); nn++)
                                    {
                                        double im = double.Parse((s22[int.Parse(h[nn].ToString())] - s22[int.Parse(h[nn].ToString()) - 1]).ToString());
                                        double pr = double.Parse(sumij[nn].Count().ToString()) / im;
                                        im.ToString();
                                        prevk.Add(Math.Round(pr, 2)); 
                                    }
                                    double pr1 = double.Parse(sumij[h.Count()].Count().ToString()) / double.Parse((s22[lastf] - s22[lastf - 1]).ToString());
                                    prevk.Add(Math.Round(pr1, 2));
                                    //若频繁，则开始测试其是否含有主导特征
                                    if (MinimumK(prevk) > prev)
                                    { 
                                        prevdic.Add(patternname, dictest);
                                        StringBuilder pr = new StringBuilder();
                                        StringBuilder ta = new StringBuilder();// 
                                        pr.Append(prevk[0].ToString());
                                        for (int u = 1; u < prevk.Count; u++)
                                        {
                                            pr.Append(",");
                                            pr.Append(prevk[u]);
                                        }
                                        prvevaluedic.Add(patternname, pr.ToString() + ",");
                                        StringBuilder rr = new StringBuilder();
                                        rr.Append(patternname);
                                        rr.Append("=");
                                        rr.Append(pr);
                                        
                                        //printf.Add(rr.ToString());
                                        listfn1.Add(patternname);                                        
                                      /*  if (DicFII(patternname, prvevaluedic)["maxfii"] - DicFII(patternname, prvevaluedic)["minfii"] > cds||DicFII(patternname, prvevaluedic)["maxfii"] - DicFII(patternname, prvevaluedic)["minfii"] == cds)//若含有主导特征
                                        {
                                            rr.Append("==========================");
                                            rr.Append(patternname+"  is a DFCP=");
                                            rr.Append(DicFII(patternname, prvevaluedic)["maxfii"] - DicFII(patternname, prvevaluedic)["minfii"]);                                            
                                            rr.Append("; ");
                                            foreach (var item in DicDomF(patternname, prvevaluedic,cds))
                                            {
                                                rr.Append(item.Key);
                                                rr.Append(" is a dominant features and the domination is ");
                                                rr.Append(item.Value);
                                            }
                                        }*/
                                        printf.Add(rr.ToString());
                                        
                                    }


                                }//计算表实例结尾                                 
                            }//连接步                 
                        }//for循环
                    }//if条件结尾 再无法产生更多连接模式     
                    //-------------------------------------------逐阶输出txt                     
                    listfn.Clear();                   
                    for (int p = 0; p < listfn1.Count(); p++)
                    {
                        listfn.Add(listfn1[p]);
                    }
                  //  MessageBox.Show("写入完毕" + sizek);
                    fnn.RemoveAt(0);
                    fnn.Add(prevdic);
                    FileStream files2 = new FileStream(outputname, FileMode.Append);
                    StreamWriter sw2 = new StreamWriter(files2);
                    foreach (var item in printf)//获得初始数据
                    {
                        sw2.WriteLine(item);//开始写入值
                    }
                    sw2.Close();
                    files2.Close();
                    printf.Clear();
                  
                  //  MessageBox.Show("阶数" + sizek);
                    //  MessageBox.Show(sizek.ToString());
                    //  MessageBox.Show("多阶频繁模式生成");
            
                } //for循环结束  
            #endregion
            // catch { Exception ex; }//{ MessageBox.Show("数据加载失败！"); }//voidbegin标记
         //   } //while是否完全对上一阶的模式进行了无连接测试此处循环后sizek++  
        }// beginfile()结尾

        //================================================================================================================joinless函数
        #region
       
        private void addstar(int fi, int fj, int ai, int aj)//星星化函数
        {
            if (!dicstar.ContainsKey(fi))
            {
                Dictionary<int, Dictionary<int, HashSet<int>>> dic1 = new Dictionary<int, Dictionary<int, HashSet<int>>>();        
                dicstar.Add(fi, dic1);
            }
            if (!dicstar[fi].ContainsKey(ai))
            {
                Dictionary<int, HashSet<int>> dic2 = new Dictionary<int, HashSet<int>>();
                dicstar[fi].Add(ai, dic2);
            }
            if (!dicstar[fi][ai].ContainsKey(fj))
            {
                HashSet<int> has = new HashSet<int>();
                dicstar[fi][ai].Add(fj, has);
            }
            dicstar[fi][ai][fj].Add(aj);
        }
        public static IEnumerable<List<int>> Combinations(List<int> sq, int i0, int n, int c)//将特征组合成候选模式
        {
            if (c == 0) yield return sq;
            else
            {
                for (int i = 0; i < n; i++)
                {
                    foreach (var perm in Combinations(sq, i0 + 1, n - 1 - i, c - 1))
                        yield return perm;
                    RL(sq, i0, n); //
                }
            }
        }
        private static void RL(List<int> sq, int i0, int n)
        {
            int tmp = sq[i0];
            sq.RemoveAt(i0);
            sq.Insert(i0 + n - 1, tmp);
        }
        
        public List<String> combinedABC(Dictionary<string, HashSet<string>> dic)//将形如1-2{4,5}的哈希转换为行实例1-2-4,1-2-5的形式
        {
            List<string> l = new List<string>();
            List<string> l1 = new List<string>();
            foreach (var item in dic)
            {
                l1 = item.Value.ToList();
                string h = "";
                foreach (var item1 in l1)
                {
                   h = item.Key + "," + item1;
                }
                l.Add(h);
            }
            return l;
        }
        public double MinimumK(List<double> prev)
        {
            double min = prev[0];
            for (int i = 1; i < prev.Count; i++)

                if (prev[i] < min)
                {
                    min = prev[i];
                }

            return min;
        }

        public double MaximumK(List<double> prev)
        {
            double max = prev[0];
            for (int i = 1; i < prev.Count; i++)

                if (prev[i] > max)
                {
                    max = prev[i];
                }

            return max;
        }
        public Boolean distance(double x, double y, double x1, double y1, double rand)
        {
            if (((x - x1) * (x - x1) + (y - y1) * (y - y1)) < rand * rand || ((x - x1) * (x - x1) + (y - y1) * (y - y1)) == rand * rand)
            {
                return true;
            }
            else
                return false;
        }

        public string getnewstr(string str)
        {
            return Regex.Replace(str.Trim(), "\\s+", " "); ;
        }
        #endregion
        //================================================================================================================feature extrcting函数
        #region
      
       public static List<string> stringSplit(string str, string tag)//截取字符串功能
        {
            List<string> list = new List<string>();
            if(str == null)
            {
                return list;
            }
            int start = 0;
            int len = str.Length;
            int spliter = -1;
            string m = str.Substring(0,str.IndexOf(tag));
            list.Add(m);
            while (start < len)
            {
                // 获取待剩余的待处理字符串, start-len这段
                str = str.Substring(start);
                spliter = str.IndexOf(tag);
                string data = null;
                if(spliter >= 0)
                {
                    // 获取数据
                    data = str.Substring(0, spliter);
                    start = spliter+1;

                }else
                {
                    // 如果没有照到，那么意味后面都是一个数据项
                    data = str;
                    start = len;
                }
                list.Add(data);
                
            }
            list.RemoveAt(0);
            list.RemoveAt(list.Count-1);
            return list;
        }

       public static List<int> StringToInt(List<string> liststr)
       {
           List<int> listint = new List<int>();
           for (int i = 0; i < liststr.Count; i++)
           {
               listint.Add(int.Parse(liststr[i]));
           }
           return listint;
 
       }
       
       private static int GetF(string ck, string ckk)//得到ck与其子模式之间相差不同的那个特征
       {
           int f=0;
           List<string>listck=new List<string>();
           List<string>listckk=new List<string>();
           List<string>listexcept=new List<string>();
           listck = ck.Split('+').ToList();
           listckk = ckk.Split('+').ToList();
           listexcept=listck.Except(listckk).ToList();
           f=int.Parse(listexcept[0]);
           return f;
       }

       public int getorderf(string f, string patternname)//得到特征在模式中是第几个
       {
           int forder = 0;
           List<string> pattern = new List<string>();
           pattern = stringSplit(patternname + "+", "+");
           for (int i = 0; i < pattern.Count; i++)
           {
               if (f.Equals(pattern[i]))
               {
                   forder = i + 1;
               }
           }
           return forder;
       }
      
       public double LI(string ck, string ckk,Dictionary<string,string>dicprev)// 计算LI函数
       {            
           List<string> listckprev = new List<string>();
           List<string> listckkprev = new List<string>();
           List<double> lr = new List<double>();
           listckprev=stringSplit(dicprev[ck], ",");//CK的每个特征的参与率
           listckkprev=stringSplit(dicprev[ckk], ",");//CK-1的每个特征的参与率
           int f = GetF(ck, ckk);
           int p = ck.Split('+').ToList().IndexOf(f.ToString());
           listckprev.RemoveAt(p);   
           for (int i = 0; i < listckkprev.Count; i++)
           {
               lr.Add(double.Parse(listckkprev[i]) - double.Parse(listckprev[i]));
           }
          return MinimumK(lr);
       }
       // public double FII()
       public double FII(string ck, string ckk, Dictionary<string, string> dicprev)// 计算FII函数Feature Influence Index
       {
           double li= LI( ck,  ckk, dicprev);
           double fii = 0;
           fii = 1 - li;
           return fii;
 
       }

  

       public Dictionary<string, double> DicFII(string pk, Dictionary<string, string> dicprev)// 得到所有特征的FII
       {
           
           Dictionary<string, double> dicfii = new Dictionary<string, double>();
           List<double> listFII = new List<double>();
           List<int> listpk = new List<int>();
           listpk = StringToInt(stringSplit(pk + "+", "+"));
           List<int> listgetf = new List<int>();
           int sizek = listpk.Count;
           foreach (var comb in Combinations(listpk, 0, listpk.Count, sizek - 1))//测试子模式
           {
               StringBuilder l = new StringBuilder();
               int[] hcomb = comb.Take(sizek - 1).ToArray();
               l.Append(hcomb[0]);
               for (int jj = 1; jj < comb.Take(sizek - 1).Count(); jj++)
               {
                   l.Append("+");
                   l.Append(hcomb[jj]);
               }
               l.ToString(); ;//得到的K-1阶子模式
               listFII.Add(FII(pk, l.ToString(), dicprev));
               listgetf.Add(GetF(pk, l.ToString()));
               dicfii.Add(GetF(pk, l.ToString()).ToString(), FII(pk, l.ToString(), dicprev));
           }
           dicfii.Add("minfii", MinimumK(listFII));
           dicfii.Add("maxfii", MaximumK(listFII));
           return dicfii;        
           

       }

       public Dictionary<string,double> DicDomF(string pk, Dictionary<string, string> dicprev,double minfd)// 得到主导特征
       {

           Dictionary<string, double> dicdomf = new Dictionary<string, double>();
           foreach(var item in DicFII( pk, dicprev))
           {
               if (!item.Key.Equals("maxfii") && !item.Key.Equals("minfii"))
               {
                   if (item.Value - DicFII(pk, dicprev)["minfii"] > minfd || item.Value - DicFII(pk, dicprev)["minfii"] == minfd)
                   {

                       dicdomf.Add(item.Key, item.Value - DicFII(pk, dicprev)["minfii"]);
                   }
               }
           }
           return dicdomf;
       }

       public double MaxMinFII2(string ck, double previ, double prevj )// 计算二阶的FII
       {
           List<string> listck  = new List<string>();
           List<double> listckprev  = new List<double>();
           List<double> lr = new List<double>();
           string[] h = ck.Split('+');
           listck.Add(h[0]);
           listck.Add(h[1]);
           listckprev.Add(1-previ);
           listckprev.Add(1-prevj);            
           return MinimumK(listckprev);
       }
       
      
        
        private void textBox_r_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox_prev_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form_joinlessbegin_Load(object sender, EventArgs e)
        {

        }
        private void comboBox_datasource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox_KR_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox_file_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion



    }
}
