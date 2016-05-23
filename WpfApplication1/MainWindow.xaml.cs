using System.Windows;
using System.Threading;
using System;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var form = new User.UserForm();
            //Thread th = new Thread(() =>
            //{
            //    form.ShowDialog();
            //});
            //th.Start();
            Dispatcher.Invoke(() => { form.ShowDialog(); });
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var form = new Server.ServerForm();
            //Thread th = new Thread(() =>
            //{
            //    form.ShowDialog();
            //});
            //th.Start();
            Dispatcher.Invoke(() => { form.ShowDialog(); });
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }
}
