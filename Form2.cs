using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Aquarium
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        // режим специальной визуализации
        bool WireMode = false;

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Visible = true;

            label2.Visible = true;

            label2.Text = "Доступно глобальное вращение сцены - R (увеличение угла), F (уменьшение угла)";

            label1.Text = "Доступно управление рыбками - WASD (перемещение группы рыбок)";

            KeyPreview = true;

            checkBox2.Checked = false;

            // инициализация библиотеки glut
            Glut.glutInit();

            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // инициализация библиотеки OpenIL
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(0, 160, 220, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы
            Gl.glLoadIdentity();

            // установка перспективы
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // начальная настройка параметров openGL (тест глубины, освещение и первый источник света)
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            BackgroundImage = Image.FromFile("..\\..\\texture\\aquarium.jpg");

            openFileDialog1.Filter = "ase files (*.ase)|*.ase|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            // активация таймера, вызывающего функцию для визуализации
            RenderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            // вызываем функцию отрисовки сцены
            WireMode = checkBox1.Checked;

            isFoodEnabled = checkBox4.Checked;

            RenderTimer.Interval = isFoodEnabled ? 60 : 30;

            label1.Visible = !checkBox2.Checked;

            Draw();
        }

        // функция отрисовки 3D моделей рыб при помощи Glut
        private void Draw()
        {
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(0, 160, 220, 1);

            // очищение текущей матрицы
            Gl.glLoadIdentity();

            Gl.glPushMatrix();

            Gl.glRotated(globalRotation, 1, 0, 0);
            Gl.glRotated(globalRotation, 0, 1, 0);
            Gl.glRotated(globalRotation, 0, 0, 1);

            Gl.glTranslated(xCoord, yCoord, cm);

            if (xCoord > 20)
            {
                xCoord = -15;
            }
            else if (xCoord < -15)
            {
                xCoord = 20;
            }
            else
            {
                if (checkBox2.Checked)
                {
                    xCoord += 0.5;
                }
            }

            Gl.glPushMatrix();

            Gl.glEnable(Gl.GL_COLOR_MATERIAL);

            Gl.glColor3f(1.0f, 1.0f, 0.0f);

            if (!WireMode)
            {
                // Первая рыба
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutSolidCone(2, 5, 10, 10);
                Glut.glutSolidSphere(3, 30, 30);

                Gl.glTranslated(0, 0, -5);
                Glut.glutSolidCone(3, 5, 10, 10);

                Gl.glTranslated(0, 0, 5);

                // Вторая рыба
                Gl.glScalef(0.5f, 0.5f, 0.5f);
                Gl.glColor3f(1.0f, 0.0f, 0.0f);
                Gl.glTranslated(0, -10, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutSolidCone(2, 5, 10, 10);
                Glut.glutSolidSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutSolidCone(3, 5, 10, 10);

                Gl.glScalef(2f, 2f, 2f);
                Gl.glTranslated(0, 0, 5);

                Gl.glTranslated(0, 0, -5);

                // Третья рыба
                Gl.glScalef(0.6f, 0.6f, 0.6f);
                Gl.glColor3f(1.0f, 0.0f, 1.0f);
                Gl.glTranslated(0, -6, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutSolidCone(2, 5, 10, 10);
                Glut.glutSolidSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutSolidCone(3, 5, 10, 10);

                Gl.glScalef(2f, 2f, 2f);
                Gl.glTranslated(0, 0, 5);

                // Четвертая рыба
                Gl.glScalef(0.8f, 0.8f, 0.8f);
                Gl.glColor3f(1.0f, 1.0f, 1.0f);
                Gl.glTranslated(0, -5, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutSolidCone(2, 5, 10, 10);
                Glut.glutSolidSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutSolidCone(3, 5, 10, 10);
            }
            else
            {
                // Первая рыба
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutWireCone(2, 5, 10, 10);
                Glut.glutWireSphere(3, 30, 30);
                Gl.glTranslated(0, 0, -5);
                Glut.glutWireCone(3, 5, 10, 10);

                Gl.glTranslated(0, 0, 5);

                // Вторая рыба
                Gl.glScalef(0.5f, 0.5f, 0.5f);
                Gl.glColor3f(1.0f, 0.0f, 0.0f);
                Gl.glTranslated(0, -10, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutWireCone(2, 5, 10, 10);
                Glut.glutWireSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutWireCone(3, 5, 10, 10);

                Gl.glScalef(2f, 2f, 2f);
                Gl.glTranslated(0, 0, 5);

                // Третья рыба
                Gl.glScalef(0.6f, 0.6f, 0.6f);
                Gl.glColor3f(1.0f, 0.0f, 1.0f);
                Gl.glTranslated(0, -6, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutWireCone(2, 5, 10, 10);
                Glut.glutWireSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutWireCone(3, 5, 10, 10);

                Gl.glScalef(2f, 2f, 2f);
                Gl.glTranslated(0, 0, 5);

                // Четвертая рыба
                Gl.glScalef(0.8f, 0.8f, 0.8f);
                Gl.glColor3f(1.0f, 1.0f, 1.0f);
                Gl.glTranslated(0, -5, 0);
                Gl.glRotated(270, 0, 1, 0);
                Gl.glRotated(90, 0, 1, 0);
                Glut.glutWireCone(2, 5, 10, 10);
                Glut.glutWireSphere(3, 30, 30);
                Gl.glRotated(0, 1, 0, 0);
                Gl.glTranslated(0, 0, -5);
                Glut.glutWireCone(3, 5, 10, 10);
            }

            if (Model != null)
            {
                Model.DrawModel();
            }

            if (isFoodEnabled)
            {
                if (Model != null)
                {
                    Gl.glTranslated(-5, -8, 5);

                    Gl.glRotated(90, 1, 0, 0);

                    Gl.glRotated(100, 0, 1, 0);

                    Gl.glRotated(-180, 0, 0, 1);

                    //Gl.glScalef(0.1f, 0.1f, 0.1f);
                }

                Gl.glTranslated(5, 0, 8);

                Gl.glScalef(0.05f, 0.05f, 0.05f);

                Gl.glColor3f(1f, 1f, 0f);

                Gl.glRotated(180, 0, 0, 1);

                for (int j = 0; j < 10; j++)
                {
                    Gl.glTranslated(0, 0, 10);

                    for (int i = 0; i < 10; i++)
                    {
                        Glut.glutSolidSphere(3, 30, 30);

                        var signX = random.Next(0, 2) == 0 ? 1 : -1;
                        var signY = random.Next(0, 2) == 0 ? 1 : -1;

                        Gl.glTranslated(signX * random.Next(100), signY * random.Next(100), 0);
                    }
                }

                Gl.glRotated(-90, 0, 0, 1);
            }

            // возвращаем состояние матрицы
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();

            // обновляем элемент AnT
            AnT.Invalidate();
        }

        private void выбратьaseФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Model = new anModelLoader();
                Model.LoadModel(openFileDialog1.FileName);
                RenderTimer.Start();
                checkBox2.Checked = true;
            }
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                yCoord += 1;
            }
            else if (e.KeyChar == 's')
            {
                yCoord -= 1;
            }
            else if (e.KeyChar == 'a')
            {
                xCoord -= 1;
            }
            else if (e.KeyChar == 'd')
            {
                xCoord += 1;
            }
            else if (e.KeyChar == 'r')
            {
                globalRotation += 5;
            }
            else if (e.KeyChar == 'f')
            {
                globalRotation -= 5;
            }
        }

        private Random random = new Random();

        private double am = 0, cm = -20, dm = -360;

        private double xCoord = -15;
        private double yCoord = 5;

        private double globalRotation = 0;

        anModelLoader Model = null;

        private bool isFoodEnabled;

        double a = 0, b = 0, c = -5, dx = -45, dy = 45, dz = 90, zoom = 0.5; // выбранные оси
    }
}
