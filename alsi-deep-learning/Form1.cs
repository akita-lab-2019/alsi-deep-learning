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

namespace alsi_deep_learning
{
    public partial class Form1 : Form
    {
        int count = 0;
        int max_count;
        int file_count;
        string row;
        double g = 9.80665;
        string str_file_name;
        string current_folder_name;
        string move_folder_name;
        string move_file_name;
        string new_file_name;
        string make_file_name;
        string output_file_name;
        string[][] data = new string[100000][];
        double[,] doubleArray = new double[100000,3];
        string[,] change_style = new string[100000, 4];


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
        }//ファイル名を取得

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new StreamReader(str_file_name);
            {

                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split(',');

                    // 配列からリストに格納する
                    List<string> lists = new List<string>();
                    lists.AddRange(values);

                    // コンソールに出力する
                    foreach (string list in lists)
                    {
                        System.Console.Write("{0} ", list);
                    }
                    System.Console.WriteLine();
                }
                sr.Close(); // ファイルを閉じる
            }
        }//CSVの読み込み

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbd.Description = "フォルダを選択してください";

            // デフォルトのフォルダを指定する
            fbd.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbd.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);

                current_folder_name = fbd.SelectedPath;
                string DirectoryPath = current_folder_name;
                string[] files = Directory.GetFiles(DirectoryPath);　//ファイルのパスを配列に格納

                //移動先フォルダを選択するダイアログを表示する
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(fbd.SelectedPath);
                    
                    move_folder_name = fbd.SelectedPath;
                    //string MoveDirectoryPath = move_folder_name;

                    for (int i = 0; i < files.Length; i++)
                    {

                        new_file_name = System.IO.Path.GetFileName(files[i]);//ファイル名を取得
                        make_file_name = move_folder_name + "\\" + new_file_name;

                        Console.WriteLine(make_file_name);


                        // 読み込みたいCSVファイルのパスを指定して開く
                        StreamReader sr = new StreamReader(files[i]);
                        {
                            // 末尾まで繰り返す
                            while (!sr.EndOfStream)
                            {
                                // CSVファイルの一行を読み込む
                                string line = sr.ReadLine();
                                // 読み込んだ一行をカンマ毎に分けて配列に格納する
                                data[count] = new string[] { line };


                                System.Console.Write("{0}", data[count]);
                                System.Console.WriteLine();

                                count += 1;
                            }

                            max_count = count;

                            // appendをtrueにすると，既存のファイルに追記
                            //         falseにすると，ファイルを新規作成する
                            var append = true;
                            // 出力用のファイルを開く
                            using (var sw = new System.IO.StreamWriter(make_file_name, append))
                            {
                                for (int count = 7; count < max_count; ++count)
                                {
                                    // 
                                    sw.WriteLine("{0}", data[count]);
                                }
                            }
                            sr.Close(); // ファイルを閉じる
                            count = 0;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("キャンセルされました");
                }
                // オブジェクトを破棄する
                fbd.Dispose();
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }
            label2.Text = ("FINISH");
            
        }//フォルダ内の全ファイルのヘッダーを削除


        private void button6_Click(object sender, EventArgs e)
        {
            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new StreamReader(str_file_name);
            {
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    data[count] = new string[] { line };


                    System.Console.Write("{0}",data[count]);
                    System.Console.WriteLine();

                    count += 1;
                }

                max_count = count;
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
                ofd.Title = "出力先ファイルを選択してください";
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
                    output_file_name = ofd.FileName;
                    Console.WriteLine(output_file_name);
                }

                // appendをtrueにすると，既存のファイルに追記
                //         falseにすると，ファイルを新規作成する
                var append = true;
                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(output_file_name, append))
                {
                    for (int count = 7; count < max_count; ++count)
                    {
                        // 
                        sw.WriteLine("{0}", data[count]);
                    }
                }
                sr.Close(); // ファイルを閉じる
            }


        }//ヘッダー削除

        private void button7_Click(object sender, EventArgs e)//順番の変更、演算
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbd.Description = "フォルダを選択してください";

            // デフォルトのフォルダを指定する
            fbd.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbd.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);

                current_folder_name = fbd.SelectedPath;
                string DirectoryPath = current_folder_name;
                string[] files = Directory.GetFiles(DirectoryPath);　//ファイルのパスを配列に格納

                //移動先フォルダを選択するダイアログを表示する
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(fbd.SelectedPath);

                    move_folder_name = fbd.SelectedPath;
                    //string MoveDirectoryPath = move_folder_name;

                    for (int i = 0; i < files.Length; i++)
                    {

                        new_file_name = System.IO.Path.GetFileName(files[i]);//ファイル名を取得
                        make_file_name = move_folder_name + "\\" + new_file_name;

                        Console.WriteLine(make_file_name);


                        // 読み込みたいCSVファイルのパスを指定して開く
                        StreamReader sr = new StreamReader(files[i]);
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

                                /* double[] doubleArray = lists
                                 .Select(double.Parse)
                                 .ToArray();                   //配列をdouble型に変更*/

                                List<double> dList = lists.ConvertAll(x => double.Parse(x));

                                doubleArray[count, 0] = dList[0];
                                doubleArray[count, 1] = dList[1];
                                doubleArray[count, 2] = dList[2];

                                System.Console.Write("{0},{1},{2}", doubleArray[count,1] / g, doubleArray[count,0] / g, doubleArray[count,2] / g);
                                System.Console.WriteLine();

                                count++;
                            }

                            max_count = count;

                            // appendをtrueにすると，既存のファイルに追記
                            //         falseにすると，ファイルを新規作成する
                            var append = false;
                            // 出力用のファイルを開く
                            using (var sw = new System.IO.StreamWriter(make_file_name, append))
                            {
                                for (int count = 0; count < max_count; ++count)
                                {
                                    // 
                                    sw.WriteLine("{0},{1},{2},{3}",count*0.01, doubleArray[count,1] / g, doubleArray[count,0] / g, doubleArray[count,2] / g);
                                }
                            }
                            sr.Close(); // ファイルを閉じる
                            count = 0;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("キャンセルされました");
                }
                // オブジェクトを破棄する
                fbd.Dispose();
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }
            label2.Text = ("FINISH");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbd.Description = "フォルダを選択してください";

            // デフォルトのフォルダを指定する
            fbd.SelectedPath = @"C:";

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbd.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);

                current_folder_name = fbd.SelectedPath;
                string DirectoryPath = current_folder_name;
                string[] files = Directory.GetFiles(DirectoryPath);　//ファイルのパスを配列に格納

                //移動先フォルダを選択するダイアログを表示する
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(fbd.SelectedPath);

                    move_folder_name = fbd.SelectedPath;
                    //string MoveDirectoryPath = move_folder_name;

                    for (int i = 0; i < files.Length; i++)
                    {

                        new_file_name = System.IO.Path.GetFileName(files[i]);//ファイル名を取得
                        make_file_name = move_folder_name + "\\" + new_file_name;

                        Console.WriteLine(make_file_name);


                        // 読み込みたいCSVファイルのパスを指定して開く
                        StreamReader sr = new StreamReader(files[i]);
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

                                change_style[count, 0] = lists[0];
                                change_style[count, 1] = dList[1].ToString("F9");
                                change_style[count, 2] = dList[2].ToString("F9");
                                change_style[count, 3] = dList[3].ToString("F9");

                                System.Console.Write("{0},{1},{2}", change_style[count, 0], change_style[count, 1], change_style[count, 2]);
                                System.Console.WriteLine();

                                count++;
                            }

                            max_count = count;

                            // appendをtrueにすると，既存のファイルに追記
                            //         falseにすると，ファイルを新規作成する
                            var append = false;
                            // 出力用のファイルを開く
                            using (var sw = new System.IO.StreamWriter(make_file_name, append))
                            {
                                for (int count = 0; count < max_count; ++count)
                                {
                                    // 
                                    sw.WriteLine("{0},{1},{2},{3}", change_style[count, 0], change_style[count, 1], change_style[count, 2], change_style[count, 3]);

                                }
                            }
                            sr.Close(); // ファイルを閉じる
                            count = 0;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("キャンセルされました");
                }
                // オブジェクトを破棄する
                fbd.Dispose();
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }
            label2.Text = ("FINISH");
        }
    }
}
