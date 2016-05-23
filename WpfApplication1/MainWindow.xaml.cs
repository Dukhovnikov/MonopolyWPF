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
            Thread th = new Thread(() =>
            {
                Dispatcher.Invoke(() => { form.Show();  });
            });
            th.Start();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var form = new Server.ServerForm();
            Thread th = new Thread(() =>
            {
                Dispatcher.Invoke(() => { form.Show(); });
            });
            th.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
