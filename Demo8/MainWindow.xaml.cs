using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace Demo8
{
    /// <summary>
    /// 协程
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (int i in GetData())
            {
                Debug.WriteLine("Button_Click，线程ID" + Thread.CurrentThread.ManagedThreadId);
                listBox.Items.Add(i);
            }
        }

        public static IEnumerable<int> GetData()
        {
            Thread.Sleep(1000);
            Debug.WriteLine("GetData，线程ID" + Thread.CurrentThread.ManagedThreadId);
            yield return 1;

            Thread.Sleep(1000);
            Debug.WriteLine("GetData，线程ID" + Thread.CurrentThread.ManagedThreadId);
            yield return 2;

            Thread.Sleep(1000);
            Debug.WriteLine("GetData，线程ID" + Thread.CurrentThread.ManagedThreadId);
            yield return 3;
        }
    }
}
