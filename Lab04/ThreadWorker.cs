using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lab04
{
    public class ThreadWorker
    {
        private PictureBox pictureBox;
        private bool isRunning;
        private bool shouldStop;
        private Thread thread;
        private Random random = new Random();

        public ThreadWorker(int index, MainForm parentForm)
        {
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(300, 300);
            pictureBox.Location = new Point(index * 300, 0);
            pictureBox.BackColor = Color.White;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            Button pauseButton = new Button();
            pauseButton.Text = "Зупинити";
            pauseButton.Size = new Size(100, 50);
            pauseButton.Location = new Point(index * 300 + 50, 310);
            pauseButton.Click += PauseButtonClick;

            Button resumeButton = new Button();
            resumeButton.Text = "Відновити";
            resumeButton.Size = new Size(100, 50);
            resumeButton.Location = new Point(index * 300 + 150, 310);
            resumeButton.Click += ResumeButtonClick;

            isRunning = true;
            shouldStop = false;

            switch (index + 1)
            {
                case 1:
                    thread = new Thread(Work1);
                    break;
                case 2:
                    thread = new Thread(Work2);
                    break;
                case 3:
                    thread = new Thread(Work3);
                    break;
                case 4:
                    thread = new Thread(Work4);
                    break;
            }
            thread.Start();

            parentForm.Controls.Add(pictureBox);
            parentForm.Controls.Add(pauseButton);
            parentForm.Controls.Add(resumeButton);
        }

        public void Stop()
        {
            shouldStop = true;
        }

        private void PauseButtonClick(object sender, EventArgs e)
        {
            isRunning = false;
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            isRunning = true;
        }

        private void Work1()
        {
            // Початкові координати кульки
            int x = pictureBox.ClientSize.Width / 2;
            int y = pictureBox.ClientSize.Height / 2;

            // Параметри траєкторії
            double angle = 0;
            double radius = 50;
            double angularSpeed = 0.1;

            while (!shouldStop)
            {
                if (isRunning)
                {
                    Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);

                        x = (int)(radius * Math.Cos(angle)) + pictureBox.ClientSize.Width / 2;
                        y = (int)(radius * Math.Sin(angle)) + pictureBox.ClientSize.Height / 2;

                        int ballSize = 30;
                        g.FillEllipse(Brushes.DimGray, x - ballSize / 2, y - ballSize / 2, ballSize, ballSize);
                    }

                    pictureBox.Image = bmp;
                    angle += angularSpeed;

                }
                Thread.Sleep(100);
            }
        }

        private void Work2()
        {
            while (!shouldStop)
            {
                if (isRunning)
                {
                    int centerX = pictureBox.ClientSize.Width / 2;
                    int centerY = pictureBox.ClientSize.Height / 2;

                    int rectangleWidth = random.Next(20, 100); ;
                    int rectangleHeight = random.Next(20, 100);

                    int rectangleX = centerX - rectangleWidth / 2;
                    int rectangleY = centerY - rectangleHeight / 2;
                    
                    Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);

                        g.FillRectangle(Brushes.DimGray, rectangleX, rectangleY, rectangleWidth, rectangleHeight);
                    }

                    pictureBox.Image = bmp;
                }
                Thread.Sleep(500);
            }
        }

        private void Work3()
        {
            double phase = 0;
            while (!shouldStop)
            {
                if (isRunning)
                {
                    Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);

                        Pen pen = new Pen(Color.DimGray);
                        int startY = pictureBox.ClientSize.Height / 2;
                        int prevX = 0;
                        int prevY = startY;

                        for (int x = 1; x < pictureBox.ClientSize.Width; x++)
                        {
                            double y = startY + 50 * Math.Sin(0.1 * x - phase);
                            g.DrawLine(pen, prevX, (float)prevY, x, (float)y);
                            prevX = x;
                            prevY = (int)y;
                        }
                    }

                    pictureBox.Image = bmp;

                    phase += 0.5;
                }
                Thread.Sleep(100);
            }
        }

        private void Work4()
        {
            double phase = 0;
            while (!shouldStop)
            {
                if (isRunning)
                {
                    Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);

                        Pen pen = new Pen(Color.DimGray);
                        int centerX = 0;
                        int centerY = pictureBox.ClientSize.Height / 2;
                        double r = 5;
                        double h = 10;

                        for (double t = 0; t < pictureBox.ClientSize.Width; t += 0.01)
                        {
                            double x = r * t - h * Math.Sin(t - phase) + centerX;
                            double y = r - h * Math.Cos(t - phase) + centerY;
                            g.FillRectangle(pen.Brush, (float)x, (float)y, 1, 1);
                        }
                    }

                    pictureBox.Image = bmp;
                    phase += 1;
                }
                Thread.Sleep(100);
            }
        }
    }
}
