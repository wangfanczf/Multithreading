using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Demo4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string _filePath;

        public MainWindow()
        {
            InitializeComponent();

            _filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "file.txt");
        }

        /// <summary>
        /// 异步创建文件
        /// </summary>
        private void CreateFile(string filePath)
        {
            Task.Run(() =>
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
                fileStream.Dispose();

                FileInfo fileInfo = new FileInfo(filePath);
                StreamWriter streamWriter = fileInfo.AppendText();

                for (int i = 0; i <= 10000; i++)
                {
                    streamWriter.WriteLine("多线程");
                    Thread.Sleep(1);
                }

                streamWriter.Flush();
                streamWriter.Close();
            });
        }

        /// <summary>
        /// 直接等待，文件还没创建完成就执行了判断
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(_filePath);

            if (File.Exists(_filePath) && new FileInfo(_filePath).Length == 110011)
            {
                labelMessage.Content = "文件已创建";
            }
        }

        /// <summary>
        /// 使用SpinWait判断文件已经创建完成
        /// </summary>
        private void buttonSpinWait_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(_filePath);

            SpinWait.SpinUntil(() =>
            {
                return File.Exists(_filePath) && new FileInfo(_filePath).Length == 110011;
            });

            labelMessage.Content = "文件已创建";
        }

        /// <summary>
        /// 使用SpinWait判断文件已经创建完成，并加上超时时间
        /// </summary>
        private void buttionTimeout_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(_filePath);

            bool isCreateSuccess = SpinWait.SpinUntil(() =>
            {
                return File.Exists(_filePath) && new FileInfo(_filePath).Length == 110011;
            }, 100000);

            if (isCreateSuccess)
            {
                labelMessage.Content = "文件已创建";
            }
            else
            {
                labelMessage.Content = "超时";
            }
        }

        /// <summary>
        /// 扩展SpinUntilAsync，支持异步等待
        /// </summary>
        private async void buttonNotBlocking_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(_filePath);

            SpinWait spinWait = new SpinWait();
            bool isCreateSuccess = await spinWait.SpinUntilAsync(() =>
            {
                return File.Exists(_filePath) && new FileInfo(_filePath).Length == 110011; 
            }, 30000);

            if (isCreateSuccess)
            {
                labelMessage.Content = "文件已创建";
            }
            else
            {
                labelMessage.Content = "超时";
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.Delete(_filePath);
                labelMessage.Content = "Label";
            }
            catch { }
        }
    }
}