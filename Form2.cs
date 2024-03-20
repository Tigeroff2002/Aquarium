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

            CalculateRotationBody();

            // активация таймера, вызывающего функцию для визуализации
            RenderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            // вызываем функцию отрисовки сцены
            WireMode = checkBox1.Checked;

            isFoodEnabled = checkBox4.Checked;

            //RenderTimer.Interval = isFoodEnabled ? 60 : 30;

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

            if (xCoord > 12)
            {
                xCoord = -15;
            }
            else if (xCoord < -15)
            {
                xCoord = 12;
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

            Gl.glTranslated(5, 8, -5);

            if (checkBox3.Checked)
            {
                DrawRotationBody();
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

        #region draw rotation body
        private void CalculateRotationBody()
        {
            count_elements = 25;

            var elements_count = 0;

            double x, y, z;

            int y1 = 50, y2 = 65, y3 = 30;

            int x1 = 9, x2 = 12, x3 = 30;

            double w1, w2, w3;

            // вычисления всех значений y для x, пренадлежащего промежутку от a=2 до b=8, с шагом в 0.1f 
            for (x = -10; x < 40; x += 2)
            {
                // задаем полином Лагранжа для набора приближений x1, x2, x3 -> y1, y2, y3

                w1 = ((x - x2) * (x - x3)) / ((x1 - x2) * (x1 - x3));
                w2 = ((x - x1) * (x - x3)) / ((x2 - x1) * (x2 - x3));
                w3 = ((x - x1) * (x - x2)) / ((x3 - x1) * (x3 - x2));

                y = w1 * y1 + w2 * y2 + w3 * y3;

                z = elements_count;

                // запись координаты x 
                GeometricTorArray[elements_count, 0] = x;
                // запись координаты y 
                GeometricTorArray[elements_count, 1] = y;
                // запись координаты z
                GeometricTorArray[elements_count, 2] = z;

                // подсчет элементов 
                elements_count++;
            }

            // построение геометрии тела вращения
            // принцип сводится к двум циклам - на основе первого перебираются
            // вершины в геометрической последовательности
            // второй использует параметр Iter - производит поворот последней линии геометрии вокруг центра тела вращения
            // при этом используется заранее определенный угол angle, который определяется как 2*Pi / количество меридиан объекта
            // за счет выполнения этого алгоритма получается набор вершин, описывающих оболочку тела врещения
            // остается только соединить эти точки в режиме рисования примитивов для получения
            // визуализированного объекта

            // цикл по последовательности точек кривой, на основе которой будет построено тело вращения
            for (int ax = 0; ax < count_elements; ax++)
            {

                // цикл по меридианам объекта, заранее определенным в программе
                for (int bx = 0; bx < Iter; bx++)
                {

                    // для всех (bx > 0) элементов алгоритма используются предыдушая построенная последовательность
                    // для ее поворота на установленный угол
                    if (bx > 0)
                    {

                        double new_x = ResaultTorGeometric[ax, bx - 1, 0] * Math.Cos(Angle) - ResaultTorGeometric[ax, bx - 1, 1] * Math.Sin(Angle);
                        double new_y = ResaultTorGeometric[ax, bx - 1, 0] * Math.Sin(Angle) + ResaultTorGeometric[ax, bx - 1, 1] * Math.Cos(Angle);
                        ResaultTorGeometric[ax, bx, 0] = new_x;
                        ResaultTorGeometric[ax, bx, 1] = new_y;
                        ResaultTorGeometric[ax, bx, 2] = GeometricTorArray[ax, 2];

                    }
                    else // для построения первого меридиана мы используем начальную кривую, описывая ее нулевым значением угла поворота
                    {

                        double new_x = GeometricTorArray[ax, 0] * Math.Cos(0) - GeometricTorArray[ax, 1] * Math.Sin(0);
                        double new_y = GeometricTorArray[ax, 1] * Math.Sin(0) + GeometricTorArray[ax, 1] * Math.Cos(0);
                        ResaultTorGeometric[ax, bx, 0] = new_x;
                        ResaultTorGeometric[ax, bx, 1] = new_y;
                        ResaultTorGeometric[ax, bx, 2] = GeometricTorArray[ax, 2];

                    }
                }
            }
        }

        private void DrawRotationBody()
        {
            Gl.glScalef(0.1f, 0.1f, 0.1f);

            // устанавливаем размер точек равный 5
            Gl.glPointSize(5.0f);

            Gl.glColor3f(0f, 1f, 1f);

            // отрисовка тора по координатам, рассчитанным по параметрическим уравнениям

            Gl.glBegin(Gl.GL_QUADS); // режим отрисовки полигонов, состоящих из 4 вершин

            for (int ax = 0; ax < count_elements; ax++)
            {
                for (int bx = 0; bx < Iter; bx++)
                {
                    // вспомогательные переменные для более наглядного использования кода при расчете нормалей
                    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, z1 = 0, z2 = 0, z3 = 0, z4 = 0;

                    // первая вершина
                    x1 = ResaultTorGeometric[ax, bx, 0];
                    y1 = ResaultTorGeometric[ax, bx, 1];
                    z1 = ResaultTorGeometric[ax, bx, 2];

                    if (ax + 1 < count_elements) // если текущий ax не последний
                    {

                        // берем следующую точку последовательности
                        x2 = ResaultTorGeometric[ax + 1, bx, 0];
                        y2 = ResaultTorGeometric[ax + 1, bx, 1];
                        z2 = ResaultTorGeometric[ax + 1, bx, 2];

                        if (bx + 1 < Iter - 1) // если текущий bx не последний
                        {

                            // берем следующую точку последовательности и следующий меридиан
                            x3 = ResaultTorGeometric[ax + 1, bx + 1, 0];
                            y3 = ResaultTorGeometric[ax + 1, bx + 1, 1];
                            z3 = ResaultTorGeometric[ax + 1, bx + 1, 2];

                            // точка, соотвествующуя по номеру только на соседнем меридиане
                            x4 = ResaultTorGeometric[ax, bx + 1, 0];
                            y4 = ResaultTorGeometric[ax, bx + 1, 1];
                            z4 = ResaultTorGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            // если это последний меридиан, то в качестве следующего мы берем начальный (замыкаем геометрию фигуры)
                            x3 = ResaultTorGeometric[ax + 1, 0, 0];
                            y3 = ResaultTorGeometric[ax + 1, 0, 1];
                            z3 = ResaultTorGeometric[ax + 1, 0, 2];

                            x4 = ResaultTorGeometric[ax, 0, 0];
                            y4 = ResaultTorGeometric[ax, 0, 1];
                            z4 = ResaultTorGeometric[ax, 0, 2];

                        }

                    }
                    else // данный элемент ax последний, следовательно мы будем использовать начальный (нулевой) вместо данного ax
                    {

                        // слудуещей точкой будет нулевая ax
                        x2 = ResaultTorGeometric[0, bx, 0];
                        y2 = ResaultTorGeometric[0, bx, 1];
                        z2 = ResaultTorGeometric[0, bx, 2];


                        if (bx + 1 < Iter - 1)
                        {

                            x3 = ResaultTorGeometric[0, bx + 1, 0];
                            y3 = ResaultTorGeometric[0, bx + 1, 1];
                            z3 = ResaultTorGeometric[0, bx + 1, 2];

                            x4 = ResaultTorGeometric[ax, bx + 1, 0];
                            y4 = ResaultTorGeometric[ax, bx + 1, 1];
                            z4 = ResaultTorGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            x3 = ResaultTorGeometric[0, 0, 0];
                            y3 = ResaultTorGeometric[0, 0, 1];
                            z3 = ResaultTorGeometric[0, 0, 2];

                            x4 = ResaultTorGeometric[ax, 0, 0];
                            y4 = ResaultTorGeometric[ax, 0, 1];
                            z4 = ResaultTorGeometric[ax, 0, 2];

                        }

                    }

                    // переменные для расчета нормали
                    double n1 = 0, n2 = 0, n3 = 0;

                    // нормаль будем расчитывать как векторное произведение граней полигона
                    // для нулевого элемента нормаль мы будем считать немного по-другому

                    // на самом деле разница в расчете нормали актуальна только для последнего и первого полигона на меридиане

                    if (ax == 0) // при расчете нормали для ax мы будем использовать точки 1,2,3
                    {

                        n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                        n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                        n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    }
                    else // для остальных - 1,3,4
                    {

                        n1 = (y4 - y3) * (z1 - z3) - (y1 - y3) * (z4 - z3);
                        n2 = (z4 - z3) * (x1 - x3) - (z1 - z3) * (x4 - x3);
                        n3 = (x4 - x3) * (y1 - y3) - (x1 - x3) * (y4 - y3);

                    }


                    // если не включен режим GL_NORMILIZE, то мы должны в обязательном порядке
                    // произвести нормализацию вектора нормали, перед тем как передать информацию о нормали
                    double n5 = (double)Math.Sqrt(n1 * n1 + n2 * n2 + n3 * n3);
                    n1 /= (n5 + 0.01);
                    n2 /= (n5 + 0.01);
                    n3 /= (n5 + 0.01);

                    // передаем информацию о нормали
                    Gl.glNormal3d(-n1, -n2, -n3);

                    // передаем 4 вершины для отрисовки полигона
                    Gl.glVertex3d(x1, y1, z1);
                    Gl.glVertex3d(x2, y2, z2);
                    Gl.glVertex3d(x3, y3, z3);
                    Gl.glVertex3d(x4, y4, z4);

                }
            }

            // завершаем выбранный режим рисования полигонов
            Gl.glEnd();

            Gl.glScalef(10f, 10f, 10f);
        }
        #endregion

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

        private int count_elements;

        private double Angle = 2 * Math.PI / 64;

        private int Iter = 64;

        private double[,] GeometricTorArray = new double[64, 3];
        private double[,,] ResaultTorGeometric = new double[64, 64, 3];

        private Random random = new Random();

        private double am = 0, cm = -20, dm = -360;

        private double xCoord = -15;
        private double yCoord = 5;

        private double globalRotation = 0;

        anModelLoader Model = null;

        private bool isFoodEnabled;

        private float raxial = 2, rx = 1, ry = 0.5f, rz = 0.5f;

        double a = 0, b = 0, c = -5, dx = -45, dy = 45, dz = 90, zoom = 0.5; // выбранные оси
    }
}
