using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV;
using System.IO;
using System.Drawing.Imaging;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV.Face;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Linq;

namespace TiCi_Recognition
{
    public partial class MainWindow : Window
    {
        public static int maincam = 1;
        public static int widthheight = 100;
        FaceRecognizer face = new LBPHFaceRecognizer(1, 8, 8, 8, 95);
        public static string imgdir_add;
        public static string root = AppDomain.CurrentDomain.BaseDirectory;
        public static List<TCStudent> Student = new List<TCStudent>();
        public static List<TCClass> Class = new List<TCClass>();
        public List<bool> tfstudent = new List<bool>();
        public static List<bool> tfseat = new List<bool>();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Manager_Content.Visibility = Config_Content.Visibility = Tutorial_Content.Visibility = Detail_Content.Visibility = Camera_Content.Visibility = Visibility.Hidden;
            Home_Content.Visibility = Visibility.Visible;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Điểm danh",
                    Values = new ChartValues<int> { }
                },
                new ColumnSeries
                {
                    Title = "Tổng",
                    Values = new ChartValues<int> { }
                }
            };
            Labels = new[] { "", "", "", "", "", "", "", "", "", "", "" };
            InitalizeStudent();
            Formatter = value => value.ToString("N");
            DataContext = this;
            Ghi("19434", 1); Ghi("24334", 1); Ghi("", 0); // Write to log (Dashboard)

            oncam();
            Ghi(tfstudent.Count.ToString(), 1);

        }
        public void offcam()
        {
            if (cap2 != null) cap2.Stop();
        }
        VideoCapture cap2;
        public void oncam()
        {
            //WebCam webcam = new WebCam();
            //webcam.InitializeWebCam(ref Large_Cam);
            //webcam.Start();
            if (cap2 == null) cap2 = new VideoCapture(maincam);
            cap2.Start();
            cap2.ImageGrabbed += Cap2_ImageGrabbed;
        }

        private void Cap2_ImageGrabbed(object sender, EventArgs e)
        {
            Mat img = new Mat();
            cap2.Retrieve(img);
            Image<Bgr, byte> image2 = img.ToImage<Bgr, byte>();
            Dispatcher.Invoke(() =>
            {
                Large_Cam.Source = CreateBitmapSourceFromGdiBitmap(image2.Flip(FlipType.Horizontal).Bitmap);
            });
        }

        public void UpdateWrite()
        {
            using (StreamWriter str = new StreamWriter("Data/StudentInfo.tcfile", false))
            {
                foreach (TCStudent x in Student)
                {
                    str.WriteLine(x.FName); str.WriteLine(x.LName);
                    str.WriteLine(x.Gender);
                    str.WriteLine((x.Class).ToString());
                    str.WriteLine((x.Code).ToString());
                    str.WriteLine((x.AbsentNum).ToString());
                    str.WriteLine((x.WrongSeatNum).ToString());
                    str.WriteLine((x.chkexist).ToString());
                    str.WriteLine(x.ImgDir);
                }
            }
            using (StreamWriter str = new StreamWriter("Data/ClassInfo.tcfile", false))
            {
                foreach (TCClass x in Class)
                {
                    str.WriteLine(x.Name);
                    str.WriteLine((x.chkexist).ToString());
                    str.WriteLine((x.code).ToString());
                    str.WriteLine((x.member.Count).ToString());
                    foreach (int xx in x.member) str.WriteLine(xx.ToString());
                    str.WriteLine((x.seatdefine.Count).ToString());
                    foreach (int xx in x.seatdefine) str.WriteLine(xx.ToString());
                    str.WriteLine((x.numseatperrow.Count).ToString());
                    foreach (int xx in x.numseatperrow) str.WriteLine(xx.ToString());
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            UpdateWrite();
            if (cap != null) cap.Dispose();
            Close();
        }

        #region Control Button
        BrushConverter brushvar = new BrushConverter();
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            ConfigTB.Opacity = TutorialTB.Opacity = ManagerTB.Opacity = 0.45;
            Manager_Content.Visibility = Config_Content.Visibility = Tutorial_Content.Visibility = Visibility.Hidden;
            // Enable
            HomeTB.Opacity = 1;
            Home_Content.Visibility = Visibility.Visible;
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            HomeTB.Opacity = TutorialTB.Opacity = ManagerTB.Opacity = 0.45;
            Manager_Content.Visibility = Home_Content.Visibility = Tutorial_Content.Visibility = Visibility.Hidden;
            offcam();
            Left_Control.Visibility = Visibility.Hidden;
            // Enable
            ConfigTB.Opacity = 1;
            Config_Content.Visibility = Visibility.Visible;
            resetform_config();
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            ConfigTB.Opacity = HomeTB.Opacity = ManagerTB.Opacity = 0.45;
            Manager_Content.Visibility = Config_Content.Visibility = Home_Content.Visibility = Visibility.Hidden;
            offcam();
            // Enable
            TutorialTB.Opacity = 1;
            Tutorial_Content.Visibility = Visibility.Visible;
            Left_Control.Visibility = Visibility.Visible;
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            ConfigTB.Opacity = TutorialTB.Opacity = HomeTB.Opacity = 0.45;
            Home_Content.Visibility = Config_Content.Visibility = Tutorial_Content.Visibility = Visibility.Hidden;
            offcam();
            // Enable
            ManagerTB.Opacity = 1;
            Manager_Content.Visibility = Visibility.Visible;
            Left_Control.Visibility = Visibility.Visible;
        }
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            Detail.BorderBrush = Camera.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            DetailTB.Foreground = CameraTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            Detail_Content.Visibility = Camera_Content.Visibility = Visibility.Hidden;
            DetailIMG.Source = new BitmapImage(new Uri("../Image/filechart.png", UriKind.Relative));
            CameraIMG.Source = new BitmapImage(new Uri("../Image/camera.png", UriKind.Relative));
            // Enable
            Dashboard.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            DashboardTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            Dashboard_Content.Visibility = Visibility.Visible;
            DashboardIMG.Source = new BitmapImage(new Uri("../Image/speedometer_clicked.png", UriKind.Relative));
            Left_Control.Visibility = Visibility.Visible;
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            Dashboard.BorderBrush = Camera.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            DashboardTB.Foreground = CameraTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            Dashboard_Content.Visibility = Camera_Content.Visibility = Visibility.Hidden;
            DashboardIMG.Source = new BitmapImage(new Uri("../Image/speedometer.png", UriKind.Relative));
            CameraIMG.Source = new BitmapImage(new Uri("../Image/camera.png", UriKind.Relative));
            offcam();
            // Enable
            Detail.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            DetailTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            Detail_Content.Visibility = Visibility.Visible;
            DetailIMG.Source = new BitmapImage(new Uri("../Image/filechart_clicked.png", UriKind.Relative));
        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            Dashboard.BorderBrush = Detail.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            DashboardTB.Foreground = DetailTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            Dashboard_Content.Visibility = Detail_Content.Visibility = Visibility.Hidden;
            DashboardIMG.Source = new BitmapImage(new Uri("../Image/speedometer.png", UriKind.Relative));
            DetailIMG.Source = new BitmapImage(new Uri("../Image/filechart.png", UriKind.Relative));
            // Enable
            Camera.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            CameraTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            Camera_Content.Visibility = Visibility.Visible;
            CameraIMG.Source = new BitmapImage(new Uri("../Image/camera_clicked.png", UriKind.Relative));
            oncam();
        }
        private void StudentDetail_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            Add.BorderBrush = Data.BorderBrush = Seat.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            AddTB.Foreground = DataTB.Foreground = SeatTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            AddIMG.Source = new BitmapImage(new Uri("../Image/add.png", UriKind.Relative));
            DataIMG.Source = new BitmapImage(new Uri("../Image/database.png", UriKind.Relative));
            SeatIMG.Source = new BitmapImage(new Uri("../Image/seat.png", UriKind.Relative));
            Add_Content.Visibility = Data_Content.Visibility = Seat_Content.Visibility = Visibility.Hidden;
            // Enable
            StudentDetail.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            StudentDetailTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            StudentDetailIMG.Source = new BitmapImage(new Uri("../Image/studentdetail_clicked.png", UriKind.Relative));
            StudentDetail_Content.Visibility = Visibility.Visible;
            SDPanel_Update();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            StudentDetail.BorderBrush = Data.BorderBrush = Seat.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            StudentDetailTB.Foreground = DataTB.Foreground = SeatTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            StudentDetailIMG.Source = new BitmapImage(new Uri("../Image/studentdetail.png", UriKind.Relative));
            DataIMG.Source = new BitmapImage(new Uri("../Image/database.png", UriKind.Relative));
            StudentDetail_Content.Visibility = Data_Content.Visibility = Seat_Content.Visibility = Visibility.Hidden;
            SeatIMG.Source = new BitmapImage(new Uri("../Image/seat.png", UriKind.Relative));
            // Enable
            Add.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            AddTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            AddIMG.Source = new BitmapImage(new Uri("../Image/add_clicked.png", UriKind.Relative));
            Add_Content.Visibility = Visibility.Visible;
            UpdateClassList();
        }

        private void Data_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            StudentDetail.BorderBrush = Add.BorderBrush = Seat.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            StudentDetailTB.Foreground = AddTB.Foreground = SeatTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            StudentDetailIMG.Source = new BitmapImage(new Uri("../Image/studentdetail.png", UriKind.Relative));
            AddIMG.Source = new BitmapImage(new Uri("../Image/add.png", UriKind.Relative));
            SeatIMG.Source = new BitmapImage(new Uri("../Image/seat.png", UriKind.Relative));
            Add_Content.Visibility = StudentDetail_Content.Visibility = Seat_Content.Visibility = Visibility.Hidden;
            // Enable
            Data.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            DataTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            DataIMG.Source = new BitmapImage(new Uri("../Image/database_clicked.png", UriKind.Relative));
            Data_Content.Visibility = Visibility.Visible;
        }
        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            // Disable
            StudentDetail.BorderBrush = Add.BorderBrush = Data.BorderBrush = brushvar.ConvertFromString("#00000000") as System.Windows.Media.Brush;
            StudentDetailTB.Foreground = AddTB.Foreground = DataTB.Foreground = brushvar.ConvertFromString("#787e7e") as System.Windows.Media.Brush;
            StudentDetailIMG.Source = new BitmapImage(new Uri("../Image/studentdetail.png", UriKind.Relative));
            AddIMG.Source = new BitmapImage(new Uri("../Image/add.png", UriKind.Relative));
            DataIMG.Source = new BitmapImage(new Uri("../Image/database.png", UriKind.Relative));
            Add_Content.Visibility = StudentDetail_Content.Visibility = Data_Content.Visibility = Visibility.Hidden;
            // Enable
            Seat.BorderBrush = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            SeatTB.Foreground = brushvar.ConvertFromString("#2ad29c") as System.Windows.Media.Brush;
            SeatIMG.Source = new BitmapImage(new Uri("../Image/seat_clicked.png", UriKind.Relative));
            Seat_Content.Visibility = Visibility.Visible;
            UpdateClassList();
        }
        #endregion

        #region Log
        private Boolean AutoScroll = true;
        private void ScrollViewer_ScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange == 0)
            {
                if (Logs.VerticalOffset == Logs.ScrollableHeight)
                {
                    AutoScroll = true;
                }
                else
                {
                    AutoScroll = false;
                }
            }
            if (AutoScroll && e.ExtentHeightChange != 0)
            {
                Logs.ScrollToVerticalOffset(Logs.ExtentHeight);
            }
        }
        List<System.Windows.Media.Color> mau = new List<System.Windows.Media.Color>();
        System.Windows.Media.Color[] cmau = new System.Windows.Media.Color[4];
        public void Ghi(string x, int y)
        {
            cmau[0] = System.Windows.Media.Color.FromRgb(85, 169, 89);
            cmau[1] = System.Windows.Media.Color.FromRgb(141, 204, 144);
            cmau[2] = System.Windows.Media.Color.FromRgb(179, 229, 252);
            cmau[3] = System.Windows.Media.Color.FromRgb(250, 250, 250);
            TextBlock tb = new TextBlock();
            string t;
            string timeh = DateTime.Now.Hour.ToString();
            string timem = DateTime.Now.Minute.ToString();
            string times = DateTime.Now.Second.ToString();
            if (timeh.Length == 1) timeh = '0' + timeh;
            if (timem.Length == 1) timem = '0' + timem;
            if (times.Length == 1) times = '0' + times;
            if (y == 0) t = timeh + ':' + timem + ':' + times + "  --  " + "Đã thống kê điểm danh thành công!";
            else t = timeh + ':' + timem + ':' + times + "  --  " + "Học sinh " + x + " đã điểm danh";
            tb.Text = t;
            tb.FontFamily = new System.Windows.Media.FontFamily("Poster");
            tb.Background = new SolidColorBrush(cmau[0]);
            tb.FontSize = 20;
            tb.Height = 45;
            tb.Padding = new Thickness(3, 9, 0, 0);
            mySP.Children.Add(tb);
            mau.Add(cmau[0]);
            int n = mySP.Children.Count;
            mau[n - 1] = cmau[0];
            if (n - 2 >= 0)
            {
                mau[n - 2] = cmau[1];
                var tt = (TextBlock)mySP.Children[n - 2];
                tt.Background = new SolidColorBrush(cmau[1]);
                mySP.Children[n - 2] = tt;
            }
            if (n - 3 >= 0)
            {
                mau[n - 3] = cmau[1];
                var tt = (TextBlock)mySP.Children[n - 3];
                tt.Background = new SolidColorBrush(cmau[1]);
                mySP.Children[n - 3] = tt;
            }
            if (n - 5 >= 0)
            {
                if (mau[n - 5] == cmau[2])
                {
                    mau[n - 4] = cmau[3];
                    var tt = (TextBlock)mySP.Children[n - 4];
                    tt.Background = new SolidColorBrush(cmau[3]);
                    mySP.Children[n - 4] = tt;
                }
                else
                {
                    mau[n - 4] = cmau[2];
                    var tt = (TextBlock)mySP.Children[n - 4];
                    tt.Background = new SolidColorBrush(cmau[2]);
                    mySP.Children[n - 4] = tt;
                }
            }
            else if (n - 4 >= 0)
            {
                mau[n - 4] = Colors.White;
                var tt = (TextBlock)mySP.Children[n - 4];
                tt.Background = new SolidColorBrush(cmau[3]);
                mySP.Children[n - 4] = tt;
            }
            tb.PreviewMouseUp += Tb_PreviewMouseUp;
        }
        int temp = -1;
        private void Tb_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBlock;
            System.Windows.Media.Color cl = new System.Windows.Media.Color();
            cl = System.Windows.Media.Color.FromRgb(255, 202, 25);
            tb.Background = new SolidColorBrush(cl);
            if (temp != -1) if (mySP.Children[temp] is TextBlock)
                {
                    var tb2 = (TextBlock)mySP.Children[temp];
                    tb2.Background = new SolidColorBrush(mau[temp]);
                    mySP.Children[temp] = tb2;
                }
            if (temp == mySP.Children.IndexOf(tb)) temp = -1;
            else temp = mySP.Children.IndexOf(tb);
        }
        #endregion

        #region DetailPanel in Dashboard
        private void DetailPanel_Update()
        {
            DetailPanel.Items.Clear();
            foreach (TCStudent x in Student)
                if (x.chkexist == true) DetailPanel.Items.Add(new { num = x.Code, lname = x.LName, fname = x.FName, classs = Class[x.Class].Name, currentstt = tfstudent[x.Code].ToString(), absentnum = x.AbsentNum, wrongseatnum = x.WrongSeatNum });
        }
        #endregion

        #region Initalize Class-Student
        public void InitalizeStudent()
        {
            // Student
            using (StreamReader str = new StreamReader("Data/StudentInfo.tcfile"))
            {
                string txt;
                while ((txt = str.ReadLine()) != null)
                {
                    TCStudent tmpstu = new TCStudent();
                    tmpstu.FName = txt;
                    tmpstu.LName = str.ReadLine();
                    tmpstu.Gender = str.ReadLine();
                    tmpstu.Class = int.Parse(str.ReadLine());
                    tmpstu.Code = int.Parse(str.ReadLine());
                    tmpstu.AbsentNum = int.Parse(str.ReadLine());
                    tmpstu.WrongSeatNum = int.Parse(str.ReadLine());
                    tmpstu.chkexist = bool.Parse(str.ReadLine());
                    tmpstu.ImgDir = str.ReadLine();
                    Student.Add(tmpstu);
                    tfstudent.Add(false); tfseat.Add(false);
                }
            }
            // Class
            using (StreamReader str = new StreamReader("Data/ClassInfo.tcfile"))
            {
                string txt;
                while ((txt = str.ReadLine()) != null)
                {
                    TCClass tmpstu = new TCClass();
                    tmpstu.Name = txt;
                    tmpstu.chkexist = bool.Parse(str.ReadLine());
                    tmpstu.code = int.Parse(str.ReadLine());
                    int tmpnum = int.Parse(str.ReadLine());
                    for (int i = 0; i < tmpnum; ++i)
                    {
                        int tmpnum2 = int.Parse(str.ReadLine());
                        tmpstu.member.Add(tmpnum2);
                    }
                    tmpnum = int.Parse(str.ReadLine());
                    for (int i = 0; i < tmpnum; ++i)
                    {
                        int tmpnum2 = int.Parse(str.ReadLine());
                        tmpstu.seatdefine.Add(tmpnum2);
                    }
                    tmpnum = int.Parse(str.ReadLine());
                    for (int i = 0; i < tmpnum; ++i)
                    {
                        int tmpnum2 = int.Parse(str.ReadLine());
                        tmpstu.numseatperrow.Add(tmpnum2);
                    }
                    tmpstu.attnum = 0;
                    Class.Add(tmpstu);
                }
            }
            // Add region
            AddBox4.Items.Add("Nam");
            AddBox4.Items.Add("Nữ");
            // Graph region
            foreach (TCClass x in Class)
            {
                Labels[x.code] = x.Name;
                SeriesCollection[0].Values.Add(0);
                SeriesCollection[1].Values.Add(x.member.Count);
            }
        }
        public static bool needupdateclass = true;
        public void UpdateClassList()
        {
            if (needupdateclass == true)
            {
                SeatBox.Items.Clear();
                AddBox3.Items.Clear();
                foreach (TCClass x in Class)
                {
                    if (x.chkexist == true)
                    {
                        AddBox3.Items.Add(x.Name);
                        SeatBox.Items.Add(x.Name);
                    }
                }
                needupdateclass = false;
                foreach (TCClass x in Class)
                {
                    Labels[x.code] = x.Name;
                    if (SeriesCollection[0].Values.Count > x.code) {
                        SeriesCollection[0].Values[x.code] = x.attnum;
                        SeriesCollection[1].Values[x.code] = x.member.Count;
                    } else
                    {
                        SeriesCollection[0].Values.Add(0);
                        SeriesCollection[1].Values.Add(x.member.Count);
                    }
                }
            }
        }
        #endregion

        #region Add region in Manager
        private void AddBox5_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            AddBox5.Text = dialog.SelectedPath;
        }
        private void resetform_add()
        {
            AddBox3.Text = AddBox4.Text = "";
            AddBox5.Text = "(đường dẫn trống)";
            AddBox1.Text = "Ví dụ: Nguyễn Văn";
            AddBox2.Text = "Ví dụ: An";
        }
        private void AddBox3_Button_Click(object sender, RoutedEventArgs e)
        {
            AddEditClass edit = new AddEditClass();
            edit.Show();

        }
        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            if (AddBox1.Text == "" || AddBox2.Text == "")
            {
                Train();
            }
            else
            {
                int codeclass = new int();
                foreach (TCClass x in Class)
                {
                    if (x.Name == AddBox3.Text)
                    {
                        codeclass = x.code;
                        break;
                    }
                }
                TCStudent tmpstu = new TCStudent(AddBox1.Text, AddBox2.Text, codeclass, AddBox4.Text, AddBox5.Text);
                tmpstu.Code = Student.Count;
                Student.Add(tmpstu);
                Class[codeclass].member.Add(tmpstu.Code);
                Train();
                UpdateWrite();
                UpdateClassList();
                System.Windows.MessageBox.Show("Đầu tiên xin hãy khởi động lại hệ thống. Sau đó, " + ". Đề nghị qua cửa sổ điều chỉnh CHỖ NGỒI để thay đổi vị trí ngồi trong lớp " + Class[codeclass].Name + "."+" Mã số của " + tmpstu.LName + " " + tmpstu.FName + " là " + tmpstu.Code);
                resetform_add();
            }
        }

        private void AddButton3_Click(object sender, RoutedEventArgs e)
        {
            if (AddBox5.Text == "(đường dẫn trống)" || AddBox5.Text == "") System.Windows.MessageBox.Show("Vui lòng chọn đường dẫn chứa hình ảnh!");
            else
            {
                imgdir_add = AddBox5.Text;
                Chuphinh abc = new Chuphinh();
                abc.Show();
            }
        }

        private void AddButton2_Click(object sender, RoutedEventArgs e) { resetform_add(); }
        #endregion

        #region Student Detail Panel region in Manager
        private void SDPanel_Update()
        {
            SDPanel.Items.Clear();
            foreach (TCStudent x in Student)
                if (x.chkexist == true) SDPanel.Items.Add(new { num = x.Code, lname = x.LName, fname = x.FName, classs = Class[x.Class].Name, gender = x.Gender, imgdir = x.ImgDir });
        }
        private void SDButton1_Click(object sender, RoutedEventArgs e)
        {
            EditStudent al = new EditStudent();
            al.Show();
        }
        #endregion

        #region Data region in Manager
        private void DTCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // textBlock.Text = DTCalendar.SelectedDate.Value.ToString("dd/MM/yyyy");
        }


        #endregion

        #region Config Region
        void resetform_config()
        {
            using (StreamReader file = new StreamReader(System.IO.Path.Combine(root, "Data", "ImgRe.tcfile")))
            {
                file.ReadLine();
                CF_NameBox.Text = file.ReadLine();
                CF_EmailBox.Text = file.ReadLine();
                CF_PhoneBox.Text = file.ReadLine();
            }
            CF_Sys2.IsChecked = true;
            CF_Sys4Box.Text = "45";
            CF_Sys3Box.Text = "23, 59";
        }

        private void CF_Button2_Click(object sender, RoutedEventArgs e) { resetform_config(); }

        private void CF_Button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Thông báo: Thay đổi được xác nhận");
        }
        #endregion

        #region Recog + Update
        Image<Bgr, byte> image;
        VideoCapture cap;
        string fcasname = "haarcascade_frontalface_alt.xml";
        Thread t1;
        private void Diemdanh_Click(object sender, RoutedEventArgs e)
        {
            //t1 = new Thread(() => recog());
            //t1.Start();
            recog();
        }

        private void recog()
        {
            junk = 0;
            face.Read("train.yml");
            if (cap == null) cap = new VideoCapture(maincam);
            cap.Start();
            cap.ImageGrabbed += Cap_ImageGrabbed;
            //cap.Stop();
        }
        public class coord_id
        {
            public int id;
            public System.Drawing.Rectangle rect;
            public coord_id(int a, System.Drawing.Rectangle b)
            {
                id = a;
                rect = b;
            }
        }
        public BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
        int junk;
        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            for (int i = 0; i < tfstudent.Count; ++i) tfstudent[i] = false;
            FaceRecognizer.PredictionResult predictedLabel = new FaceRecognizer.PredictionResult();
            CascadeClassifier fcas = new CascadeClassifier(fcasname);
            Mat img = new Mat(), imgg = new Mat();
            cap.Retrieve(img);
            image = img.ToImage<Bgr, byte>();
            List<System.Drawing.Rectangle> faces = new List<System.Drawing.Rectangle>();
            CvInvoke.CvtColor(img, imgg, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(imgg, imgg);
            System.Drawing.Rectangle[] facedetect = fcas.DetectMultiScale(imgg, 1.1, 10, new System.Drawing.Size(20, 20));
            faces.AddRange(facedetect);
            Mat s_img = new Mat();
            List<coord_id> hs = new List<coord_id>();
            foreach (System.Drawing.Rectangle f in faces)
            //Parallel.ForEach(faces,(f) =>
            {
                Image<Gray, byte> image2 = new Image<Gray, byte>(image.ToBitmap());
                image2.ROI = f;
                //image2.ToBitmap(100, 100).Save(junk.ToString()+".jpg", ImageFormat.Jpeg);
                image2.Resize(MainWindow.widthheight, MainWindow.widthheight, Emgu.CV.CvEnum.Inter.Linear, false);
                s_img = image2.Mat;
                predictedLabel = face.Predict(s_img);
                //Ghi(predictedLabel.Label.ToString(), 1);
                //Dispatcher.BeginInvoke(new ThreadStart(() => textbox2.Text = junk.ToString()));
                try
                {
                    tfstudent[predictedLabel.Label] = true;
                    hs.Add(new coord_id(predictedLabel.Label, f));
                }
                catch
                {
                    image.Draw(f, new Bgr(0, 183, 149), 15);
                    // Dispatcher.BeginInvoke(new ThreadStart(() => textbox1.Text = predictedLabel.Label.ToString()));
                    continue;
                }
            }
            //++junk;
            if (hs.Count > 0) checkseat(hs);
            Dispatcher.Invoke(() =>
            {
                Small_Camera.Source = CreateBitmapSourceFromGdiBitmap(image.Flip(FlipType.Horizontal).Bitmap);
            });
            //Thread.Sleep(500);
        }
        public void SetTF(coord_id x, bool y)
        {
            if (y == true)
            {
                tfseat[x.id] = true;
                if (x.id==1) image.Draw(x.rect, new Bgr(100, 0, 0), 15);
                else if (x.id==2) image.Draw(x.rect, new Bgr(255, 0, 255), 15);
                else image.Draw(x.rect, new Bgr(0, 100, 0), 15);
            } else
            {
                tfseat[x.id] = false;
                image.Draw(x.rect, new Bgr(0, 0, 100), 15);
            }
        }
        public void checkseat(List<coord_id> hs)
        {
            hs.Sort((a, b) =>
            {
                return b.rect.Y.CompareTo(a.rect.Y);
            });
            List<coord_id> temp = new List<coord_id>();
            int col = 0, col2 = 0;
            foreach(int x in Class[0].numseatperrow)
            {
                temp.Clear();
                int tmpcnt = 0;
                for (int j = col; j < x + col; ++j)
                {
                    if (tfstudent[Class[0].seatdefine[j]] == false) ++tmpcnt;
                }
                for (int j = col2; j < x + col2 - tmpcnt; ++j) temp.Add(hs[j]);
                temp.Sort((a, b) =>
                {
                    return a.rect.X.CompareTo(b.rect.X);
                });
                for (int j = col2; j < x + col2 - tmpcnt; ++j) hs[j] = temp[j - col2];
                col += x;
                col2 += (x - tmpcnt);
            }
            //hs.Sort((a, b) =>
            //{
            //    return a.rect.X.CompareTo(b.rect.X);
            //});

            int count = 0; col = 0;
            for (int i = 0; i < Class[0].numseatperrow.Count; i++)
            {
                int alpha = Class[0].numseatperrow[i];
                double dar = (double)image.Width / alpha;
                for (int j = col; j < alpha + col; j++)
                {
                    if (tfstudent[Class[0].seatdefine[j]])
                    {
                        //label.Content = (hs[count].id.ToString() + " " + Class[0].seatdefine[j].ToString());
                        if (Class[0].seatdefine[j] == hs[count].id)
                        {
                            double delta = (1.00 * hs[count].rect.Width / 2);
                            if (dar * (j - col) <= hs[count].rect.X && dar * (j + 1 - col) >= (hs[count].rect.X + hs[count].rect.Width))
                            {
                                SetTF(hs[count], true);
                            }
                            else if (hs[count].rect.X < dar * (j - col))
                            {
                                if (Math.Abs(hs[count].rect.X - dar * (j - col)) < delta) SetTF(hs[count], true);
                                else SetTF(hs[count], false);
                            }
                            else if ((hs[count].rect.X + hs[count].rect.Width) > dar * (j + 1 - col))
                            {
                                if (Math.Abs(hs[count].rect.X + hs[count].rect.Width - dar * (j + 1 - col)) < delta) SetTF(hs[count], true);
                                else SetTF(hs[count], false);
                            }
                            else SetTF(hs[count], false);
                        }
                        else SetTF(hs[count], false);
                        count++;
                    }
                }
                col += alpha;
            }
            //for (int i = 0; i < Class[0].seatdefine.Count; i++)
            //    if (tfstudent[Class[0].seatdefine[i]])
            //    {
            //        if (Class[0].seatdefine[i] == hs[count].id)
            //        {
            //            tfseat[hs[count].id] = true;
            //            if (hs[count].id != 0) image.Draw(hs[count].rect, new Bgr(0, 100, 0), 15);
            //            else image.Draw(hs[count].rect, new Bgr(185, 0, 206), 15);
            //        }

            //        else
            //        {
            //            tfseat[hs[count].id] = false;
            //            image.Draw(hs[count].rect, new Bgr(0, 0, 100), 15);
            //        }
            //        count++;
            //        if (count == hs.Count) break;
            //    }
        }
        public void UpdateStatus()
        {
            for (int i = 0; i < Class.Count; ++i) Class[i].attnum = Class[i].member.Count;
            for (int i = 0; i < tfstudent.Count; ++i)
            {
                if (tfstudent[i] == false) { ++Student[i].AbsentNum; --Class[Student[i].Class].attnum; }
                if (tfseat[i] == false) ++Student[i].WrongSeatNum;
            }
            DetailPanel_Update();
            foreach(TCClass x in Class)
            {
                SeriesCollection[0].Values[x.code] = x.attnum;
            }
        }
        #endregion

        #region Seat in Manager
        int cdclass;
        private void reset_seattextbox()
        {
            if (Class[cdclass].seatdefine.Count == 0)
            {
                SeatTextBox.Text = "Chưa có dữ liệu. Vui lòng nhập dữ liệu vào!";
            } else
            {
                SeatTextBox.Text = "";
                int total = 0;
                foreach(int x in Class[cdclass].numseatperrow)
                {
                    for (int t = total; t < (total + x); ++t) SeatTextBox.Text += (Class[cdclass].seatdefine[t].ToString() + " ");
                    SeatTextBox.Text += "\n";
                    total += x;
                }
            }
        }
        private void SeatConfirm_Click(object sender, RoutedEventArgs e)
        {
            string tmp = "";
            int tab = 0;
            Class[cdclass].numseatperrow.Clear();
            Class[cdclass].seatdefine.Clear();
            foreach (char x in SeatTextBox.Text)
            {
                if (x == '\n')
                {
                    if (tmp!="")
                    {
                        Class[cdclass].numseatperrow.Add(++tab);
                        Class[cdclass].seatdefine.Add(int.Parse(tmp));
                        tab = 0; tmp = "";
                    }
                }
                else if (x == ' ' && tmp != "")
                {
                    ++tab;
                    Class[cdclass].seatdefine.Add(int.Parse(tmp));
                    tmp = "";
                }
                else if (x >= '0' && x <= '9') tmp += x;
            }
            if (tmp != "")
            {
                Class[cdclass].numseatperrow.Add(++tab);
                Class[cdclass].seatdefine.Add(int.Parse(tmp));
                tab = 0; tmp = "";
            }
            UpdateWrite();
            System.Windows.MessageBox.Show("Chỉnh sửa chỗ ngồi thành công");
        }
        private void SeatReset_Click(object sender, RoutedEventArgs e)
        {
            reset_seattextbox();
        }
        private void SeatBut_Click(object sender, RoutedEventArgs e)
        {
            foreach (TCClass x in Class)
            {
                if (x.Name == SeatBox.Text)
                {
                    cdclass = x.code;
                    reset_seattextbox();
                    break;
                }
            }
        }
        #endregion

        #region Train
        void Train() //targetdir chua cac folder, moi folder chua anh hoc sinh
        {
            int countimg = 0, j = 0, count_item = 0;
            Image<Gray, byte> img;
           
            //foreach (string folder in directory.getdirectories(targetdirectory))
            //    count_item += directory.getfiles(folder).length;
            for (int i = 0; i < Student.Count; i++)
                count_item += Directory.GetFiles(Student[i].ImgDir).Length;
            int[] labels = new int[count_item];
            Image<Gray, byte>[] img_list = new Image<Gray, byte>[count_item];
            for (int i = 0; i < Student.Count; i++)
            {
                if (Student[i].chkexist == true)
                {
                    foreach (string file in Directory.GetFiles(Student[i].ImgDir))
                    {
                        img = new Image<Gray, byte>(file);
                        img_list[countimg] = img.Resize(MainWindow.widthheight, MainWindow.widthheight, Inter.Linear);
                        labels[countimg] = Student[i].Code;
                        countimg++;
                    }
                }
            }
            face.Train(img_list, labels);
            face.Write("train.yml");
        }
        #endregion

        #region GotFocus Functions
        private void AddBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            AddBox1.Text = "";
        }

        private void AddBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            AddBox2.Text = "";
        }
        private void AddBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateClassList();
        }
        #endregion

        #region StopCam - Left Control   
        bool al = true;
        private void StopCam_Click(object sender, RoutedEventArgs e)
        {
            cap.ImageGrabbed -= Cap_ImageGrabbed;
            if (al==false) UpdateStatus();
            //textBlock.Text += '\n';
            //for (int i = 0; i < tfstudent.Count; ++i)
            //{
            //    textBlock.Text += (tfstudent[i].ToString() + " ");
            //}
            al ^= true;
        }
        #endregion

    }
    public class TCClass
    {
        public string Name;
        public int code, attnum;
        public bool chkexist;
        public List<int> member = new List<int>();
        public List<int> seatdefine = new List<int>();
        public List<int> numseatperrow = new List<int>();

        public TCClass(string v)
        {
            Name = v;
            chkexist = true;
        }
        public TCClass()
        {

        }
    }
}
