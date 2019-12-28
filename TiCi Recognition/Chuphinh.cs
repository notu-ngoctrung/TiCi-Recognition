using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiCi_Recognition
{
    public partial class Chuphinh : Form
    {
        VideoCapture cap;
        int count;
        public Chuphinh()
        {
            InitializeComponent();
            imageBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            cap = new VideoCapture(MainWindow.maincam);
            count = 1;
            cap.Start();
            cap.ImageGrabbed += Cap_ImageGrabbed;
        }
        private void AES_Encrypt(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();

            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            AES.Mode = CipherMode.CFB;

            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    cs.Write(buffer, 0, read);
                }
                fsIn.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }
            return data;
        }
        private byte[] AES_Decrypt(string inputFile, string password)
        {

            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            //FileStream fsOut = new FileStream(inputFile + ".decrypted", FileMode.Create);
                
            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    //fsOut.Write(buffer, 0, read);
                }
            }
            catch (System.Security.Cryptography.CryptographicException ex_CryptographicException)
            {
                Debug.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                //fsOut.Close();
                fsCrypt.Close();
            }
            return buffer;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    if (cap!=null) cap.Dispose();
                    break;
                default:
                    if (cap!=null) cap.Dispose();
                    break;
            }
        }
        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox2.Text = text;
            }
        }
        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> image;
            bool kt = false;
            CascadeClassifier fcas = new CascadeClassifier("haarcascade_frontalface_alt.xml");
            Rectangle tmp = new Rectangle();
            Mat img = new Mat(), imgg = new Mat();
            cap.Retrieve(img);
            image = img.ToImage<Bgr, byte>();
            Image<Gray, byte> grayframe = img.ToImage<Gray, byte>();
            List<Rectangle> faces = new List<Rectangle>();
            CvInvoke.CvtColor(img, imgg, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(imgg, imgg);
            Rectangle[] facedetect = fcas.DetectMultiScale(imgg, 1.1, 10, new Size(20, 20));
            faces.AddRange(facedetect);
            foreach (Rectangle f in faces)
            {
                if (f.Width * f.Height > tmp.Width * tmp.Height)
                {
                    tmp = f;
                    kt = true;
                }
            }
            if (count > 500)
            {
                cap.Dispose();
                return;
            }
            if (kt)
            { 
                SetText(count.ToString());
                Image<Gray, byte> image2 = new Image<Gray, byte>(image.ToBitmap());
                image2.ROI = tmp;
                using (var m = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(MainWindow.imgdir_add + @"\" + count.ToString() + ".jpg", FileMode.Create, FileAccess.Write))
                    {
                        image2.ToBitmap(MainWindow.widthheight, MainWindow.widthheight).Save(m, ImageFormat.Jpeg);
                        byte[] abc = m.ToArray();
                        fs.Write(abc, 0, abc.Length);
                    }
                }
                image2.Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear, false);
                count++;
                imageBox1.Image = image;
            }

        }
        
    }
}
