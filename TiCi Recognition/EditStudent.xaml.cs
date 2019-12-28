using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TiCi_Recognition
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window
    {
        public EditStudent()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int maso = new int();
            if (int.TryParse(SearchBox.Text, out maso) && maso<MainWindow.Student.Count && MainWindow.Student[maso].chkexist)
            {
                AddBox1.Text = MainWindow.Student[maso].LName;
                AddBox2.Text = MainWindow.Student[maso].FName;
                AddBox3.Text = MainWindow.Class[MainWindow.Student[maso].Class].Name;
                AddBox4.Text = MainWindow.Student[maso].Gender;
                AddBox5.Text = MainWindow.Student[maso].ImgDir;
            }
            else
            {
                SearchBox.Text = "Mã số học sinh sai. Yêu cầu nhập lại!";
            }
        }

        private void AddBox5_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            AddBox5.Text = dialog.SelectedPath;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int maso = new int();
            if (int.TryParse(SearchBox.Text, out maso) && maso < MainWindow.Student.Count && MainWindow.Student[maso].chkexist)
            {
                MainWindow.Student[maso].chkexist = false;
                System.Windows.MessageBox.Show("Xóa học sinh thành công");
            } else
            {
                System.Windows.MessageBox.Show("Không thể xóa học sinh! (học sinh không tồn tại hoặc đã được xóa)");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int maso = new int();
            if (int.TryParse(SearchBox.Text, out maso) && MainWindow.Student[maso].chkexist)
            {
                MainWindow.Student[maso].LName = AddBox1.Text;
                MainWindow.Student[maso].FName = AddBox2.Text;
                MainWindow.Class[MainWindow.Student[maso].Class].Name = AddBox3.Text;
                MainWindow.Student[maso].Gender = AddBox4.Text;
                MainWindow.Student[maso].ImgDir = AddBox5.Text;            }
            else
            {
                SearchBox.Text = "Mã số học sinh sai. Yêu cầu nhập lại!";
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }
    }
}
