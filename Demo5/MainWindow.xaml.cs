using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Demo5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 普通队列，线程不安全
        /// </summary>
        private void buttonQueue_Click(object sender, RoutedEventArgs e)
        {
            Queue<int> queue = new Queue<int>();

            // 串行计算
            int result = 0;
            for (int i = 0; i < 10000; i++)
            {
                queue.Enqueue(i);
                result += i;
            }
            labelSerial.Content = string.Format("串行计算结果：{0}", result);

            // 并行计算
            int concurrentResult = 0;
            Action action = () =>
            {
                while (queue.Count > 0)
                {
                    try
                    {
                        int localValue = queue.Dequeue(); // 线程不安全队列出队不是原子操作，其他线程访问Count时是不可靠状态
                        Interlocked.Add(ref concurrentResult, localValue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            };
            Parallel.Invoke(action, action, action, action);
            labelParallel.Content = string.Format("并行计算结果 = {0}", concurrentResult);
        }

        /// <summary>
        /// 线程安全队列，ConcurrentQueue
        /// </summary>
        private void buttonConcurrentQueue_Click(object sender, RoutedEventArgs e)
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

            // 串行计算
            int result = 0;
            for (int i = 0; i < 10000; i++)
            {
                queue.Enqueue(i);
                result += i;
            }
            labelSerial.Content = string.Format("串行计算结果：{0}", result);

            // 并行计算
            int concurrentResult = 0;
            Action action = () =>
            {
                while (queue.Count > 0)
                {
                    if (queue.TryDequeue(out int localValue)) // 线程安全队列出队是原子操作，其他线程访问Count时是最新状态
                    {
                        Interlocked.Add(ref concurrentResult, localValue);
                    }
                }
            };
            Parallel.Invoke(action, action, action, action);
            labelParallel.Content = string.Format("并行计算结果 = {0}", concurrentResult);
        }

        private void buttonRest_Click(object sender, RoutedEventArgs e)
        {
            labelSerial.Content = "";
            labelParallel.Content = "";
        }
    }
}