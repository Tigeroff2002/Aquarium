using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Tao.FreeGlut;
using Tao.OpenGl;

namespace Aquarium
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;

            Change_Visibility_Fish_Controls(false);

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 410;
            trackBar2.Minimum = 0;
            trackBar2.Maximum = 530;
            trackBar3.Minimum = 0;
            trackBar3.Maximum = 60;

            Is2DModeEnabled = false;
            Is3DModeEnabled = false;

            timerIteration = 0;

            BackgroundImage = Image.FromFile("..\\..\\texture\\background.jpg");

            RenderTimer1.Start();
        }

        #region buttons clicks
        private void button1_Click(object sender, EventArgs e)
        {
            Is2DModeEnabled = true;
            Is3DModeEnabled = false;
            isFishEnabled = false;

            aquariumDrawer = new Aquarium();

            BackgroundImage = Image.FromFile("..\\..\\texture\\aquarium.jpg");

            Init2DGlut();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackgroundImage = Image.FromFile("..\\..\\texture\\vodorosli.jpg");

            isFractalEnabled = true;
            Is2DModeEnabled = false;
            Is3DModeEnabled = false;

            Init2DGlut();

            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            // очищаем ее 
            Gl.glLoadIdentity();

            // делим строки обрабатываемого изображения между потоками
            threadInputParams[0] = new ParamsForThread(0, 75, 800);
            threadInputParams[1] = new ParamsForThread(75, 150, 800);
            threadInputParams[2] = new ParamsForThread(150, 225, 800);
            threadInputParams[3] = new ParamsForThread(225, 300, 800);
            threadInputParams[4] = new ParamsForThread(300, 375, 800);
            threadInputParams[5] = new ParamsForThread(375, 450, 800);
            threadInputParams[6] = new ParamsForThread(450, 525, 800);
            threadInputParams[7] = new ParamsForThread(525, 600, 800);


            threadInputParams[0]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[1]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[2]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[3]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[4]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[5]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[6]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);
            threadInputParams[7]._pointerToDraw = new ParamsForThread._RenderDLG(DrawFractal);

            threadInputParams[0].code_mode = comboBox1.SelectedIndex;
            threadInputParams[1].code_mode = comboBox1.SelectedIndex;
            threadInputParams[2].code_mode = comboBox1.SelectedIndex;
            threadInputParams[3].code_mode = comboBox1.SelectedIndex;
            threadInputParams[4].code_mode = comboBox1.SelectedIndex;
            threadInputParams[5].code_mode = comboBox1.SelectedIndex;
            threadInputParams[6].code_mode = comboBox1.SelectedIndex;
            threadInputParams[7].code_mode = comboBox1.SelectedIndex;

            /*создаем 8 потоков, в качестве параметров передаем имя Выполняемой функции*/
            th_1 = new Thread(CalculateImage);
            th_2 = new Thread(CalculateImage);
            th_3 = new Thread(CalculateImage);
            th_4 = new Thread(CalculateImage);
            th_5 = new Thread(CalculateImage);
            th_6 = new Thread(CalculateImage);
            th_7 = new Thread(CalculateImage);
            th_8 = new Thread(CalculateImage);

            //расставляем приоритеты для потоков ниже среднего
            th_1.Priority = ThreadPriority.Lowest;
            th_2.Priority = ThreadPriority.Lowest;
            th_3.Priority = ThreadPriority.Lowest;
            th_4.Priority = ThreadPriority.Lowest;
            th_5.Priority = ThreadPriority.Lowest;
            th_6.Priority = ThreadPriority.Lowest;
            th_7.Priority = ThreadPriority.Lowest;
            th_8.Priority = ThreadPriority.Lowest;

            th_1.Start(threadInputParams[0]);
            th_2.Start(threadInputParams[1]);
            th_3.Start(threadInputParams[2]);
            th_4.Start(threadInputParams[3]);
            th_5.Start(threadInputParams[4]);
            th_6.Start(threadInputParams[5]);
            th_7.Start(threadInputParams[6]);
            th_8.Start(threadInputParams[7]);

            Thread.Sleep(3000);

            aquariumDrawer = new Aquarium(isFractalEnabled, PixelsArray);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            Is3DModeEnabled = true;

            // инициализация бибилиотеки glut
            Glut.glutInit();

            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(0, 150, 220, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            // очистка матрицы
            Gl.glLoadIdentity();

            // установка перспективы
            Glu.gluPerspective(155, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // начальная настройка параметров openGL (тест глубины, освещение и первый источник света)
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            */

            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Change_Visibility_Fish_Controls(true);

            isFishEnabled = true;

            fish_coord = (rnd.Next(0, 100), rnd.Next(100, 400), 0);

            trackBar1.Value = fish_coord.Item1;
            trackBar2.Value = fish_coord.Item2;
            trackBar3.Value = fish_coord.Item3;

            label5.Text = fish_coord.Item1.ToString();
            label6.Text = fish_coord.Item2.ToString();
            label7.Text = Angle_fish.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Change_Visibility_Fish_Controls(false);

            isFishEnabled = false;

            label5.Text = string.Empty;
            label6.Text = string.Empty;
            label7.Text = string.Empty;
        }
        #endregion

        #region init 2D glut matrix
        private void Init2DGlut()
        {
            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(0, 150, 220, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            // очистка матрицы
            Gl.glLoadIdentity();

            // определение параметров настройки проекции, в зависимости от размеров сторон элемента AnT.
            if ((float)AnT.Width <= (float)AnT.Height)
            {
                Glu.gluOrtho2D(0.0, 500.0, 0.0, 500.0 * (float)AnT.Height / (float)AnT.Width);

            }
            else
            {
                Glu.gluOrtho2D(0.0, 500.0 * (float)AnT.Width / (float)AnT.Height, 0.0, 500.0);
            }

            // установка объектно-видовой матрицы
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
        #endregion

        #region redrawing by timer
        private void RenderTimer1_Tick(object sender, EventArgs e)
        {
            button1.Visible = !Is2DModeEnabled || Is3DModeEnabled;
            button3.Visible = !Is3DModeEnabled || Is2DModeEnabled;

            button2.Visible = !isFractalEnabled;
            button4.Visible = (isFractalEnabled && comboBox1.SelectedIndex != 1 
                || Is2DModeEnabled) && !isFishEnabled;
            button5.Visible = (isFractalEnabled && comboBox1.SelectedIndex != 1 
                || Is2DModeEnabled) && isFishEnabled;

            comboBox1.Visible = isFractalEnabled;
            label8.Visible = isFractalEnabled;

            label9.Text = Is2DModeEnabled
                ? "Используется режим простой 2D визуализации"
                : isFractalEnabled
                    ? "Используется режим визуализации растровой сцены с фильтрами"
                    : Is3DModeEnabled
                        ? "Используется режим 3D визуализации"
                        : "Визуализация не ведется";

            label2.Visible = isFractalEnabled && isFishEnabled;
            label5.Visible = isFractalEnabled && isFishEnabled;
            trackBar1.Visible = isFractalEnabled && isFishEnabled;

            timerIteration++;

            DrawAquarium();
        }

        private void DrawAquarium()
        {
            if (Is2DModeEnabled)
            {
                aquariumDrawer.Draw2DAquarium(
                    timerIteration,
                    isFishEnabled,
                    fish_coord);
            }

            else if (isFractalEnabled)
            {
                aquariumDrawer.Draw2DRasterScene(
                    isFishEnabled,
                    fish_coord,
                    isFilteredNow,
                    isFilteredWasDisabled);
            }

            else if (Is3DModeEnabled)
            {
                aquariumDrawer.Draw3DFish();
            }

            // сигнал для обновление элемента, реализующего визуализацию 
            AnT.Invalidate();
        }
        #endregion

        #region comboboxes controls
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // применить фильтр
            if (comboBox1.SelectedIndex == 1 && !isFilteredNow)
            {
                isFilteredNow = true;
                isFilteredWasDisabled = false;
            }

            if (comboBox1.SelectedIndex == 0 && isFilteredNow)
            {
                isFilteredNow = false;
                isFilteredWasDisabled = true;
            }
        }
        #endregion

        #region track bars scrolls
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            fish_coord.Item1 = trackBar1.Value;
            label5.Text = fish_coord.Item1.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            fish_coord.Item2 = trackBar2.Value;
            label6.Text = fish_coord.Item2.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            fish_coord.Item3 = trackBar3.Value;
            label7.Text = fish_coord.Item3.ToString();
        }
        #endregion

        #region fractal drawing
        // функция отрисовки
        private void DrawFractal()
        {
            Gl.glClearColor(0, 150, 220, 1f);

            // устанавливаем позицию вывода
            Gl.glRasterPos2i(-1, -1);

            // визуализируем массив
            Gl.glDrawPixels(800, 600, Gl.GL_GREEN, Gl.GL_UNSIGNED_BYTE, PixelsArray);

            Gl.glFlush();

            AnT.Invalidate();
        }

        // функция вычисления изображения
        static void CalculateImage(object Settings)
        {
            // получаем параметры через объект Settings, приводя его к типу ParamsForThread
            ParamsForThread thisThreadSettings = (ParamsForThread)Settings;

            // инициализация начальных значений переменных согласно индивидуальному варианту
            double xmin = -1.5;
            double ymin = -1.2;
            double xmax = 1.5;
            double ymax = 1.5;

            int W = 800;
            int H = 600;

            double dx = (xmax - xmin) / (double)(W - 1);
            double dy = (ymax - ymin) / (double)(H - 1);

            double x, y, X, Y, Cx, Cy;

            xmin = -1.5;
            ymin = -1.2;
            xmax = 1.5;
            ymax = 1.5;

            W = 800;
            H = 600;

            dx = (xmax - xmin) / (double)(W - 1);
            dy = (ymax - ymin) / (double)(H - 1);

            // циклы по всем пикселям результирующего изображения
            for (int ax = thisThreadSettings._FromImageH; ax < thisThreadSettings._ToImageH; ax++)
            {
                for (int bx = 0; bx < thisThreadSettings._ImageW; bx++)
                {
                    // подготовка к выполнению итерации

                    x = xmin + ax * dx;
                    y = ymin + bx * dy;

                    // настройка параметров индивидуального варианта
                    Cx = x;
                    Cy = y;

                    X = x;
                    Y = y;

                    double ix = 0, iy = 0, n = 0;

                    // выполнение итерации
                    while ((ix * ix + iy * iy < 4) && (n < 64))
                    {
                        (ix, iy) = CalculateFractalValueInPower(2, 3, X, Y, Cx, Cy);

                        n++;
                        X = ix;
                        Y = iy;
                    }

                    // заносим значение цвета в массив, описывающий результирующее изображение
                    PixelsArray[bx, ax, 0] = (byte)(255 - n * 4);
                    PixelsArray[bx, ax, 1] = 255;
                    PixelsArray[bx, ax, 2] = (byte)(255 - n * 4);

                    // вызываем функцию отрисовки
                    thisThreadSettings._pointerToDraw();
                }
            }
        }

        private static (double, double) CalculateFractalValueInPower(
            int currentPower, int targetPower, double X, double Y, double Cx, double Cy)
        {
            var x = X * X - Y * Y + Cx;
            var y = 2 * X * Y + Cy;

            var cx = Math.Pow(Cx, 2);
            var cy = 2 * Cx * Cy - Math.Pow(Cy, 2);

            if (currentPower == targetPower)
            {
                return (x, y);
            }

            else
                return CalculateFractalValueInPower(currentPower + 1, targetPower, x, y, Cx, Cy);
        }
        #endregion

        private void Change_Visibility_Fish_Controls(bool value)
        {
            label1.Visible = value;
            label2.Visible = false;
            label3.Visible = value;
            label4.Visible = value;
            label5.Visible = false;
            label6.Visible = value;
            label7.Visible = value;

            trackBar1.Visible = false;
            trackBar2.Visible = value;
            trackBar3.Visible = value;

            button4.Visible = !value;
            button5.Visible = value;
        }

        private bool isFilteredNow;
        private bool isFilteredWasDisabled;
        private bool isFishEnabled;
        private (int, int, int) fish_coord;

        // объекты, содержащие настройки для потоков
        ParamsForThread[] threadInputParams = new ParamsForThread[8];

        // Слово делегат (delegate) используется в C# для обозначения хорошо известного понятия. Делегат задает определение функционального типа (класса) данных. Экземплярами класса являются функции. Описание делегата в языке C# представляет собой описание еще одного частного случая класса. Каждый делегат описывает множество функций с заданной сигнатурой. Каждая функция (метод), сигнатура которого совпадает с сигнатурой делегата, может рассматриваться как экземпляр класса, заданного делегатом.
        delegate void RenderDLG();

        // массив пикселей
        static private byte[,,] PixelsArray = new byte[800, 600, 3];

        // массив до фильтрации
        static private byte[,,] PixelsArrayBeforeFilter = new byte[800, 600, 3];

        // объявляем объекты для управления потоками
        Thread th_1 = null;
        Thread th_2 = null;
        Thread th_3 = null;
        Thread th_4 = null;
        Thread th_5 = null;
        Thread th_6 = null;
        Thread th_7 = null;
        Thread th_8 = null;

        private Aquarium aquariumDrawer = null;

        private int timerIteration;

        private Random rnd = new Random();

        private float Angle_fish;

        private bool Is2DModeEnabled;
        private bool Is3DModeEnabled;

        private bool isFractalEnabled;
    }
}
