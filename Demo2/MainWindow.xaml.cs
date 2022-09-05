using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Demo2
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

        private void buttonSleep_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);
            labelMessage.Content = "完成";
        }

        private void buttonDelay_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(2000);
            labelMessage.Content = "完成";
        }

        private async void buttonAwaitDelay_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);
            labelMessage.Content = "完成"; 
        }

        private void buttonDelayWait_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(2000).Wait();
            labelMessage.Content = "完成";
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            labelMessage.Content = "Label";
        }
    }
}