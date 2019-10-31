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

namespace PLOT_CSV
{
    public partial class Form1 : Form
    {

        string str_file_name;
        int count = 0;
        int max_count;
        double[,] data = new double[100000, 4];

        public Form1()
        {
            InitializeComponent();
        }

        //written by whoopsidaisies

        private void button1_Click(object sender, EventArgs e) //sin,cos波を表示
        {
                // 1.Seriesの追加
                chart1.Series.Clear();
                chart1.Series.Add("sin");
                chart1.Series.Add("cos");

                // 2.グラフのタイプの設定
                chart1.Series["sin"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["cos"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                // 3.座標の入力
                for (double theta = 0.0; theta <= 2 * Math.PI; theta += Math.PI / 360)
                {
                    chart1.Series["sin"].Points.AddXY(theta, Math.Sin(theta));
                    chart1.Series["cos"].Points.AddXY(theta, Math.Cos(theta));
                }
            
        }
        
        private void button2_Click(object sender, EventArgs e) //PLOT_CSV
        {
            
            
            {
                //OpenFileDialogクラスのインスタンスを作成
                OpenFileDialog ofd = new OpenFileDialog();

                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                ofd.FileName = "default.html";
                //はじめに表示されるフォルダを指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                ofd.InitialDirectory = @"C:\Users\NITIC-AKITAlab\";
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                ofd.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
                //[ファイルの種類]ではじめに選択されるものを指定する
                //2番目の「すべてのファイル」が選択されているようにする
                ofd.FilterIndex = 2;
                //タイトルを設定する
                ofd.Title = "開くファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                ofd.RestoreDirectory = true;
                //存在しないファイルの名前が指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                ofd.CheckFileExists = true;
                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                ofd.CheckPathExists = true;


                //ダイアログを表示する
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき、選択されたファイル名を表示する
                    str_file_name = ofd.FileName;
                    Console.WriteLine(str_file_name);
                }

                StreamReader sr = new StreamReader(str_file_name);
                {
                    // 末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        // CSVファイルの一行を読み込む
                        string line = sr.ReadLine();
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        string[] values = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // 配列からリストに格納する
                        List<string> lists = new List<string>();
                        lists.AddRange(values);
                        //配列をdouble型に変更*/

                        List<double> dList = lists.ConvertAll(x => double.Parse(x));

                        data[count, 0] = dList[0];
                        data[count, 1] = dList[1];
                        data[count, 2] = dList[2];
                        data[count, 3] = dList[3];

                        System.Console.Write("{0},{1},{2},{3}", data[count, 0], data[count, 1], data[count, 2], data[count,3]);
                        System.Console.WriteLine();

                        count++;
                    }

                    max_count = count;

                    //NSグラフの設定
                    chart1.Series.Clear();
                    chart1.Series.Add("NS");
                    chart1.Series["NS"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    // 座標の入力
                    for (count = 0; count < max_count; count++)
                    {
                        chart1.Series["NS"].Points.AddXY(data[count,0], data[count,1]); 
                    }
                    count = 0;

                    //EWグラフの設定
                    chart2.Series.Clear();
                    chart2.Series.Add("EW");
                    chart2.Series["EW"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    // 座標の入力
                    for (count = 0; count < max_count; count++)
                    {
                        chart2.Series["EW"].Points.AddXY(data[count, 0], data[count, 2]); 
                    }
                    count = 0;

                    //UDグラフの設定
                    chart3.Series.Clear();
                    chart3.Series.Add("UD");
                    chart3.Series["UD"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    // 座標の入力
                    for (count = 0; count < max_count; count++)
                    {
                        chart3.Series["UD"].Points.AddXY(data[count, 0], data[count, 3]); 
                    }
                    count = 0;

                    label1.Text = ("FINISH");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
