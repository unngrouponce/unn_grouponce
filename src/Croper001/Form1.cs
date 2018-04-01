using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Croper001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = false;
            pic.Parent = pictureBox1;
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Visible = false;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            pictureBox1.Width = Form1.ActiveForm.Width;
            pictureBox1.Height = Form1.ActiveForm.Height;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // new Parser(folderBrowserDialog1.SelectedPath);
                path = folderBrowserDialog1.SelectedPath;
                pngs = Directory.GetFiles(folderBrowserDialog1.SelectedPath).ToList().FindAll(x => x.EndsWith(".png"));
                if (pngs.Count == 0) Environment.Exit(0);
                Bitmap t = new Bitmap((Bitmap)Image.FromFile(pngs.ElementAt(0)));
                wcoef = t.Width / (double)pictureBox1.Width;
                hcoef = t.Height / (double)pictureBox1.Height;
                reload();
            }
        }
        int dir_count = 0;
        int file_count = 0;
        string path;
        List<string> pngs=new List<string>();
        double hcoef=1;
        double wcoef=1;
        Image i;
        PictureBox pic = new PictureBox();
        private int begin_x;
        private int begin_y;


        void reload()
        {
          if(i!=null)  i.Dispose();
            i = new Bitmap((Bitmap)Image.FromFile(pngs.ElementAt(file_count)),
                pictureBox1.Width,
                pictureBox1.Height);
            pictureBox1.BackgroundImage = i;
            GC.Collect();
        }
        void cut()
        {
            string folder = path + $"\\out{dir_count}";
            Directory.CreateDirectory(folder);
            dir_count++;
            foreach (var i in pngs)
            {
                Image s = Image.FromFile(i);
                Image bmp = Crop(s, new Rectangle((int)(begin_x*wcoef), (int)(begin_y*hcoef), (int)(pic.Width*wcoef), (int)(pic.Height*hcoef)));
                bmp.Save(i.Replace(path, folder));
            }
            pic.Width = 0;
            pic.Height = 0;
            pic.Visible = false;
            Console.Beep();
        }
        void cancel()
        {
            pic.Width = 0;
            pic.Height = 0;
            pic.Visible = false;
        }
        void quit()
        {
            Environment.Exit(0);
        }
        void next()
        {
            file_count++;
            if (!(file_count < pngs.Count)) {
                file_count--;  Console.Beep();return;
            }
            
            reload();
           
        }
        void prev()
        {
            file_count--;
            if (!(file_count < pngs.Count) || file_count <= 0) { file_count++; Console.Beep();return; }
            
            reload();
           
        }
        Image Crop(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);
            image.Dispose();
            return cropBmp;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyValue)
            {
                case 81: quit(); break;
                case 27: quit();break;
                case 39: next();break;
                case 37:prev();break;
                case 88:cut();break;
                case 67: cancel();break;

            }
          //  MessageBox.Show(e.KeyValue.ToString());
            // Console.Beep();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                begin_x = e.X;
                begin_y = e.Y;
                pic.Left = e.X;
                pic.Top = e.Y;
                pic.Width = 0;
                pic.Height = 0;
                pic.Visible = true;
                
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pic.Width = e.X - begin_x;
                pic.Height = e.Y - begin_y;


              

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
