using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Demo3
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
        /// 模拟耗时的方法
        /// </summary>
        private Task TimeConsumeFunction()
        {
            Task task = Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(10);
                }
            });
            return task;
        }

        /// <summary>
        /// 等待任务完成且有超时判定，不阻塞UI
        /// </summary>
        private async void buttonDelay_Click(object sender, RoutedEventArgs e)
        {
            Task businessTask = TimeConsumeFunction();
            Task completedTask = await Task.WhenAny(businessTask, Task.Delay(500));
            if (completedTask == businessTask)
            {
                labelMessage.Content = "完成";
            }
            else
            {
                labelMessage.Content = "超时";
            }
        }

        /// <summary>
        /// 等待任务完成且有超时判定，阻塞UI
        /// </summary>
        private void buttonWait_Click(object sender, RoutedEventArgs e)
        {
            if (TimeConsumeFunction().Wait(800))
            {
                labelMessage.Content = "完成";
            }
            else
            {
                labelMessage.Content = "超时";
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            labelMessage.Content = "Label";
        }
    }
}