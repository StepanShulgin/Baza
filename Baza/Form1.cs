using System.Drawing;
using System.Windows.Forms;

namespace Baza
{
    public partial class Form1 : Form
    {

        int radius = 0;
        float angle = 0;
        const float pi = 3.14F;
        int side = 10;
        int currX, currY;
        const int sleepTime = 50;

        public Form1()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {


            label1.Text = "N/A";
            label2.Text = "N/A";
            Print_System();


        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Print_System();
        }
        private void pictureBox1_Move(object sender, MouseEventArgs e)
        {
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;
            int x = Convert.ToInt32(e.X); // координата по оси X
            int y = Convert.ToInt32(e.Y); // координата по оси Y
            label1.Text = (x - w).ToString();
            label2.Text = (-(y - h)).ToString();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.TranslateTransform(w, h);
            Point[] line = new Point[360];
            for (int i = 0; i < angle; i++)
            {
                line[i] = new Point((int)(radius * Math.Sin(i * pi / 180)), (int)(radius * Math.Cos(i * pi / 180)));
            }
            for (int i = 0; i < angle; i++)
            {
                Print_Figure(line[i], i);
                Thread.Sleep(sleepTime);
            }





        }

        private void Print_Track()
        {
            
            Pen trackLine = new Pen(Color.Coral, 2);
            trackLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.TranslateTransform(w, h);
            graphics.Clear(BackColor);
            Print_System();
            int x = Convert.ToInt32(label1.Text);
            currX = x;
            int y = Convert.ToInt32(label2.Text);
            currY = y;
            graphics.DrawEllipse(trackLine, x - radius, -y - radius, 2 * radius, 2 * radius);

        }
        private void Print_Figure(Point coord, int angle)
        {
            Pen figureLine = new Pen(Color.DarkRed, 3);
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(BackColor);
            Print_Track();

            graphics.TranslateTransform(w + coord.X + (currX), h - coord.Y - (currY));

            graphics.RotateTransform(angle);
            Print_System();
            Point[] triangle;
            triangle = new Point[3];
            triangle[0] = new Point(0, Convert.ToInt32((0 - side / Math.Sqrt(3))));
            triangle[1] = new Point(Convert.ToInt32(0 - (side / 2)), Convert.ToInt32((0 + side * Math.Sqrt(3) / 6)));
            triangle[2] = new Point(Convert.ToInt32(0 + (side / 2)), Convert.ToInt32((0 + side * Math.Sqrt(3) / 6)));
            graphics.DrawPolygon(figureLine, triangle);

        }


        private void Print_System()
        {
            Graphics graphics2 = pictureBox1.CreateGraphics();
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;

            graphics2.TranslateTransform(w, h);
            graphics2.DrawLine(Pens.Black, new Point(-w, 0), new Point(w, 0));
            graphics2.DrawLine(Pens.Black, new Point(0, h), new Point(0, -h));


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "") radius = Convert.ToInt32(textBox1.Text);
            else radius = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (Convert.ToInt32(textBox2.Text) < 0) textBox2.Text = "0";
                if (Convert.ToInt32(textBox2.Text) > 360) textBox2.Text = "360";
                else angle = Convert.ToInt32(textBox2.Text);
            }
            else angle = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "") side = Convert.ToInt32(textBox3.Text);
            else side = 0;
        }
    }

}