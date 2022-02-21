using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Point[] sperks;
        private Point Statuslocation;
        public Form1()
        {
            InitializeComponent();
            Statuslocation = new Point(807, 246);
            sperks = new Point[2] {new Point(175,310), new Point(400,700) };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;*.png;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;*.png;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(opnfd.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> Image1 = new Image<Bgr, byte>(new Bitmap(pictureBox1.Image));
            Image<Bgr, byte> Image2 = new Image<Bgr, byte>(new Bitmap(Properties.Resources.status)); 
            Point temp = findImage(Image1, Image2);
            Point offsett = new Point(Statuslocation.X - temp.X, Statuslocation.Y - temp.Y);

            Image2 = new Image<Bgr, byte>(new Bitmap(pictureBox2.Image));

            findImage(Image1, Image2, new Point(sperks[0].X - offsett.X, sperks[0].Y - offsett.Y), new Point(sperks[1].X - offsett.X, sperks[1].Y - offsett.Y));
        }

        private Point findImage(Image<Bgr,byte> image1, Image<Bgr, byte> image2)
        {
            double Threshold = (double)trackBar1.Value / 100; //set it to a decimal value between 0 and 1.00, 1.00 meaning that the images must be identical

            Image<Gray, float> Matches = image1.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed);

            for (int y = 0; y < Matches.Data.GetLength(0); y++)
            {
                for (int x = 0; x < Matches.Data.GetLength(1); x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold) 
                    {
                        
                        return new Point(x, y);
                    }
                }
            }
            return new Point(0,0);
        }

        private Point findImage(Image<Bgr, byte> image1, Image<Bgr, byte> image2, Point point1, Point point2)
        {
            double Threshold = (double)trackBar1.Value / 100; //set it to a decimal value between 0 and 1.00, 1.00 meaning that the images must be identical

            Image<Gray, float> Matches = image1.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed);

            for (int y = point1.Y; y < point2.Y; y++)
            {
                for (int x = point1.X; x < point2.X; x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold)
                    {
                        Bitmap Bitmap = new Bitmap(pictureBox1.Image);
                        using (Graphics g = Graphics.FromImage(Bitmap))
                        {
                            g.DrawRectangle(new Pen(Color.Yellow, 1), new Rectangle(x, y, pictureBox2.Image.Width, pictureBox2.Image.Height));
                            g.DrawRectangle(new Pen(Color.Yellow, 1), new Rectangle(point1.X, point1.Y, point2.X-point1.X, point2.Y - point1.Y));
                        }
                        pictureBox1.Image = Bitmap;
                        return new Point(x, y);
                    }
                }
            }
            return new Point(0, 0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 80;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = ((double)trackBar1.Value / 100).ToString();
        }

    }
}
