using System;
using System.Drawing;
using System.Windows.Forms;

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
            // инициализация бибилиотеки glut
            Glut.glutInit();

            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

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

            // активация таймера, вызывающего функцию для визуализации
            RenderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            // вызываем функцию отрисовки сцены
            WireMode = checkBox1.Checked;
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

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            Gl.glTranslated(xSpinFirstFish, bm, cm);

            if (xSpinFirstFish > 25)
                xSpinFirstFish = -25;
            else
                xSpinFirstFish += 0.3;

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

            // возвращаем состояние матрицы
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();

            // обновляем элемент AnT
            AnT.Invalidate();
        }

        private double am = 0, bm = 5, cm = -20, dm = -360;
        private double xSpinFirstFish = -20;

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
