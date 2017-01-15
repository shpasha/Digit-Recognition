using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                  
        }

        bool p = false;
        static int n = 20, m = 15, cs = 10;

        int[,] input = new int[n, m];
        int[,,] weights = new int[10,n,m];


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p)
            {
                Pen blackPen = new Pen(Color.Black, 1);
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

                int px = PointToClient(Cursor.Position).X - pictureBox1.Left;
                int py = PointToClient(Cursor.Position).Y - pictureBox1.Top;
                if (px < pictureBox1.Width - 1
                    && px >= 0 &&
                    py < pictureBox1.Height - 1 &&
                    py >= 0)
                {
                    input[py / cs, px / cs] = 1;
                }
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        if (input[i, j] == 1)
                            g.FillRectangle(new SolidBrush(Color.Black),
                   j * cs,
                   i * cs, cs, cs);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            p = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            p = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    weights[Convert.ToInt32(label2.Text), i, j] -= input[i,j];
                }
            }
            saveWeights(Convert.ToInt32(label2.Text));

          /*  for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    weights[Convert.ToInt32(textBox1.Text), i, j] += input[i, j]*2;
                }
            }
            saveWeights(Convert.ToInt32(textBox1.Text));
            */

            check();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check();
        }

        private void check()
        {
            int[] f = new int[10];
            int summax = -100, nummax = 0;
            for (int k = 0; k < 10; k++)
            {
                int sum = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        sum += weights[k, i, j] * input[i, j];
                    }
                }
                if (sum < 0) f[k] = 0;
                else
                f[k] = sum;
                if (sum >= summax)
                {
                    summax = sum;
                    nummax = k;
                }
            }
            double[] p = new double[10];
            int s = 0;
            for (int i = 0; i < 10; i++)
                s += f[i];
            listBox1.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                p[i] = (double)f[i] / s;
                listBox1.Items.Add(i.ToString() + " " + p[i].ToString());
            }

            label2.Text = nummax.ToString();
            textBox1.Text = label2.Text;
        }

        private void saveWeights(int k) {
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@k.ToString() + ".txt"))
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            file.Write(weights[k, i, j].ToString() + " ");
                        }
                        file.WriteLine("");
                    }
                }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    weights[Convert.ToInt32(textBox1.Text), i, j] += input[i,j];
                }
            }

           saveWeights(Convert.ToInt16(textBox1.Text));
           // Init();
        }

        private void Init()
        {
            pictureBox1.Width = m * cs + 1;
            pictureBox1.Height = n * cs + 1;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    input[i, j] = 0;

            Pen blackPen = new Pen(Color.Black, 1);

            Bitmap bmp;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;

            Graphics g = Graphics.FromImage(pictureBox1.Image);

            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);

            for (int i = 0; i <= pictureBox1.Width; i += cs)
                g.DrawLine(blackPen, i, 0, i, pictureBox1.Height);

            for (int j = 0; j <= pictureBox1.Height; j += cs)
                g.DrawLine(blackPen, 0, j, pictureBox1.Height, j);

            string[] lines;
            for (int k = 0; k < 10; k++)
            {
                using (System.IO.StreamReader file =
                        new System.IO.StreamReader(@k.ToString() + ".txt"))
                {
                    for (int i = 0; i < n; i++)
                {
                        lines = file.ReadLine().Split(' ');
                        for (int j = 0; j < m; j++)
                        {
                            weights[k, i, j] = Convert.ToInt32(lines[j]);
                        }
                    }
                }
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Init();
        }
    }
}
