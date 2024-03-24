using Aquarium.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Aquarium
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form3_Load(object sender, System.EventArgs e)
        {
            BackgroundImage = Image.FromFile("..\\..\\texture\\vodorosli.jpg");

            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // очитка окна
            Gl.glClearColor(0, 0, 0, 1);

            // установка порта вывода в соотвествии с размерами элемента openGLControl
            Gl.glViewport(0, 0, _width, _height);

            // настройка проекции
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluOrtho2D(0, _width, 0, _height);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Gl.glDisable(Gl.GL_COLOR_MATERIAL);
            Gl.glDisable(Gl.GL_DEPTH_TEST);
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_LIGHT0);

            // Загружаем фрактал заново
            LoadFractal();

            RenderTimer.Interval = 50;

            RenderTimer.Start();
        }

        private void LoadFractal()
        {
            // создаем новый экземпляр класса anEngine
            _fractal = new FractalRomb(_width, _height);

            if (System.IO.File.Exists("..\\..\\texture\\fractal.png"))
            {
                var fractalBitmap = new Bitmap("..\\..\\texture\\fractal.png");

                // копируем изображение в нижний левый угол рабочей области
                _fractal.SetImageToMainLayer(fractalBitmap);
            }
            else
            {
                throw new InvalidOperationException("No such picture in resources");
            }
        }

        private void RenderTimer_Tick(object sender, System.EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Gl.glColor3f(0, 0, 0);

            _fractal.SwapImage();

            Gl.glFlush();

            AnT.Invalidate();

            timerIteration++;

            if (timerIteration >= 10)
            {
                RenderTimer.Interval = 30000;
            }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _fractal.Filter_1();

                RenderTimer.Stop();
                RenderTimer.Start();
            }
        }

        private FractalRomb _fractal;

        private readonly int _width = 800;
        private readonly int _height = 600;

        private int timerIteration = 0;
    }
}
