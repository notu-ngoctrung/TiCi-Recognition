using System.IO;
using System.Windows;

namespace TiCi_Recognition
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Path.Combine(MainWindow.root, "Data", "ImgRe.tcfile")))
            {
                MessageBox.Show("Lỗi: Hệ thống đã được kích hoạt bởi một người quản trị khác trước đây!");
            } else
            {
                Register x = new Register();
                x.Show();
            }
        }

        private void Forgot_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Path.Combine(MainWindow.root, "Data", "ImgRe.tcfile")))
            {
                ForgotPassword x = new ForgotPassword();
                x.Show();
            }
            else
            {
                MessageBox.Show("Lỗi: Hệ thống chưa có người quản trị! Hãy đăng ký");
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader file = new StreamReader(Path.Combine(MainWindow.root, "Data", "ImgRe.tcfile")))
            {
                string tmp = file.ReadLine();
                if (passwordBox.Password.ToString() == tmp)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    MainWindow x = new MainWindow();
                    x.Show();
                    Close();
                } else
                {
                    label3.Content = "Thông báo: MẬT KHẨU KHÔNG KHỚP!";
                }
            }
        }
    }
}
