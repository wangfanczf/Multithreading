using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo6
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
        /// task内操作了UI，且等待task完成
        /// </summary>
        private async Task<string> FunctionAsync()
        {
            Task task = Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        label.Content = "123";
                    });
                }
            });
            await task;
            string a = "123";
            return a;
        }

        /// <summary>
        /// task内操作了UI，不等待task完成
        /// </summary>
        private string Function()
        {
            Task task = Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        label.Content = "123";
                    });
                }
            });
            string a = "123";
            return a;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Button_Click异步执行，UI线程没有被阻塞
            // FunctionAsync 内等待 task完成，需要操作UI线程，此时可以切换到UI线程执行 label.Content = "123"，从而完成 await task;
            string a = await FunctionAsync();
            string b = a;
        }

        private void buttonLock_Click(object sender, RoutedEventArgs e)
        {
            // FunctionAsync 内等待 task完成， 而task需要操作UI线程
            // UI线程被buttonLock_Click占用，需要等到buttonLock_Click执行完成，才可以用UI线程
            // 而buttonLock_Click要执行完成，又必须FunctionAsync执行完成后，再执行后面的b=a，直至从方法体出去
            // 也就是说 FunctionAsync要完成，需等待 buttonLock_Click 先完成，以便可以使用UI线程，而 buttonLock_Click 要完成，则必须顺序执行完FunctionAsync及b=a
            // 二者互相等待，形成死锁
            string a = FunctionAsync().Result;
            string b = a;
        }

        private void buttonNoLock_Click(object sender, RoutedEventArgs e)
        {
            // task需要操作UI线程时，等待buttonNoLock_Click执行完成，才可以用UI线程，不会死锁
            string a = Function();
            string b = a;
        }
    }
}
