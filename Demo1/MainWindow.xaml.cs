using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Demo1
{
    /// <summary>
    /// 无返回值，异步报告进度
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 模拟耗时的方法
        /// </summary>
        private Task TimeConsumeFunction(IProgress<int> reportProgress)
        {
            Task task = Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    reportProgress.Report(i);
                }
            });
            return task;
        }

        /// <summary>
        /// 直接执行Task，没有阻塞UI，但进度完成提前执行了
        /// </summary>
        private void buttonStart_Task_Click(object sender, RoutedEventArgs e)
        {
            Progress<int> progressReporter = new Progress<int>((progress) => { progressBar.Value = progress; });
            TimeConsumeFunction(progressReporter);
            labelResult.Content = "进度完成";
        }

        /// <summary>
        /// 等待Task执行完成，再执行进度完成，执行顺序正确，但阻塞UI
        /// </summary>
        private void buttonStart_TaskWait_Click(object sender, RoutedEventArgs e)
        {
            Progress<int> progressReporter = new Progress<int>((progress) => { progressBar.Value = progress; });
            TimeConsumeFunction(progressReporter).Wait();
            labelResult.Content = "进度完成";
        }

        /// <summary>
        /// 异步执行方法，执行顺序正确，且不阻塞UI
        /// </summary>
        private async void buttonStart_await_Click(object sender, RoutedEventArgs e)
        {
            Progress<int> progressReporter = new Progress<int>((progress) => { progressBar.Value = progress; });
            await TimeConsumeFunction(progressReporter);
            labelResult.Content = "进度完成";
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            labelResult.Content = "Label";
            progressBar.Value = 0;
        }
    }
}