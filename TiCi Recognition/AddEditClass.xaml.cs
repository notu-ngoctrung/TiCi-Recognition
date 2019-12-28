using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TiCi_Recognition
{
    /// <summary>
    /// Interaction logic for AddEditClass.xaml
    /// </summary>
    public partial class AddEditClass : Window
    {
        public AddEditClass()
        {
            InitializeComponent();
            updatelistclass();
        }
        void updatelistclass()
        {
            EditBox1.Items.Clear();
            foreach (TCClass x in MainWindow.Class)
            {
                if (x.chkexist == true) EditBox1.Items.Add(x.Name);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TCClass tmp = new TCClass(AddBox.Text);
            tmp.code = MainWindow.Class.Count;
            MainWindow.Class.Add(tmp);
            MainWindow.Class.Sort(delegate(TCClass x, TCClass y)
            {
                return (x.Name.CompareTo(y.Name));
            });
            for (int i = 0; i < MainWindow.Class.Count; ++i) MainWindow.Class[i].code = i;
            updatelistclass();
            MessageBox.Show("Thêm lớp thành công!");
            MainWindow.needupdateclass = true;
        }
        private void AddButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (EditBox1.Text == "") return;
            if (EditBox2.Text == "" || EditBox2.Text=="(nhập tên mới)")
            {
                foreach(TCClass x in MainWindow.Class) 
                    if (x.Name == EditBox1.Text)
                    {
                        x.chkexist = false;
                        updatelistclass();
                        MessageBox.Show("Thao tác xóa thành công!");
                        break;
                    }
            }
            else
            {
                foreach(TCClass x in MainWindow.Class)
                {
                    if (x.Name == EditBox1.Text)
                    {
                        x.Name = EditBox2.Text;
                        MainWindow.Class.Sort(delegate (TCClass xx, TCClass y)
                        {
                            return (xx.Name.CompareTo(y.Name));
                        });
                        break;
                    }
                }
                MessageBox.Show("Thao tác chỉnh sửa thành công!");
            }
            MainWindow.needupdateclass = true;
        }

        private void AddBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddBox.Text = "";
        }

        private void EditBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            EditBox2.Text = "";
        }
    }
}
