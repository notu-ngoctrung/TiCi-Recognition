using System;
using System.Windows;
using System.IO;

namespace TiCi_Recognition
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Path.Combine(MainWindow.root, "Data", "ImgRe.tcfile")))
            {
                file.WriteLine(passwordBox.Password.ToString());
                file.WriteLine(textBox_Copy1.Text);
                file.WriteLine(textBox_Copy.Text);
                file.WriteLine(textBox.Text);
            }
            MessageBox.Show("Đăng ký thành công!");
            Close();
        }
    }
}
