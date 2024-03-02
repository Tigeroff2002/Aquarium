using System;

using Tao.FreeGlut;
using Tao.OpenGl;

namespace Aquarium
{
    public sealed class Aquarium
    {
        #region constructor inits fractal matrix and fish pattern
        public Aquarium(
            bool isFractalEnabled = default,
            byte[,,] local_fractal_array = default)
        {
            fish_matrix[0, 0] = 10;
            fish_matrix[0, 1] = 5;
            fish_matrix[1, 0] = 17;
            fish_matrix[1, 1] = 10;
            fish_matrix[2, 0] = 24;
            fish_matrix[2, 1] = 5;
            fish_matrix[3, 0] = 34;
            fish_matrix[3, 1] = 5;
            fish_matrix[4, 0] = 22;
            fish_matrix[4, 1] = 13;
            fish_matrix[5, 0] = 28;
            fish_matrix[5, 1] = 30;
            fish_matrix[6, 0] = 37;
            fish_matrix[6, 1] = 28;
            fish_matrix[7, 0] = 36;
            fish_matrix[7, 1] = 33;
            fish_matrix[8, 0] = 26;
            fish_matrix[8, 1] = 43;
            fish_matrix[9, 0] = 23;
            fish_matrix[9, 1] = 45;
            fish_matrix[10, 0] = 20;
            fish_matrix[10, 1] = 43;
            fish_matrix[11, 0] = 16;
            fish_matrix[11, 1] = 32;
            fish_matrix[12, 0] = 6;
            fish_matrix[12, 1] = 30;
            fish_matrix[13, 0] = 5;
            fish_matrix[13, 1] = 25;
            fish_matrix[14, 0] = 9;
            fish_matrix[14, 1] = 27;
            fish_matrix[15, 0] = 9;
            fish_matrix[15, 1] = 21;
            fish_matrix[16, 0] = 16;
            fish_matrix[16, 1] = 17;
            fish_matrix[17, 0] = 10;
            fish_matrix[17, 1] = 5;

            var localMatrix = new int[18, 2];

            for (int k = 0; k < 18; k++)
            {
                fish_matrix[k, 0] += 20;

                localMatrix[k, 0] = fish_matrix[k, 0];
                localMatrix[k, 1] = fish_matrix[k, 1];
            }

            var increase = 2;

            for (int k = 0; k < 17; k++)
            {
                var hi = localMatrix[k + 1, 0] - localMatrix[k, 0];
                var hj = localMatrix[k + 1, 1] - localMatrix[k, 1];

                var currentIncreaseI = increase * hi;
                var currentIncreaseJ = increase * hj;

                fish_matrix[k + 1, 0] = fish_matrix[k, 0] + currentIncreaseI;
                fish_matrix[k + 1, 1] = fish_matrix[k, 1] + currentIncreaseJ;
            }

            if (isFractalEnabled)
            {
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 600; j++)
                    {
                        if (isFractalEnabled)
                        {
                            this.local_fractal_array[i, j, 0] = local_fractal_array[i, j, 0];
                            this.local_fractal_array[i, j, 1] = local_fractal_array[i, j, 1];
                            this.local_fractal_array[i, j, 2] = local_fractal_array[i, j, 2];

                            if (local_fractal_array[i, j, 0] > 235 &&
                                local_fractal_array[i, j, 1] == 251 &&
                                local_fractal_array[i, j, 2] > 254)
                            {
                                this.local_fractal_array[i, j, 0] = 0;
                                this.local_fractal_array[i, j, 1] = 255;
                                this.local_fractal_array[i, j, 2] = 0;
                            }

                            if (local_fractal_array[i, j, 0] > 239)
                            {
                                this.local_fractal_array[i, j, 0] = 0;
                                this.local_fractal_array[i, j, 1] = 150;
                                this.local_fractal_array[i, j, 2] = 220;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        // рисование растровой проработанной сцены с фракталами и подобием рыб
        // с доступным фильтром для увеличения резкости
        public void Draw2DRasterScene(
            bool isFishEnabled,
            (int, int, int) fish_coord,
            (int, int, int) system_fish_coord,
            bool isFilterEnabled = false,
            bool isFilteredJustDisabled = false)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(0, 150, 220, 1);

            Gl.glLoadIdentity();

            var rnd = new Random();

            if (isFishEnabled)
            {
                for (int i = 1; i < 600; i++)
                {
                    for (int j = 1; j < 600; j++)
                    {
                        if (local_fish_bool_array[i, j] == 2)
                        {
                            RecreateAllArrays();

                            throw new ArgumentException();
                        }

                        var isNeedToDrawFish = IsNeedToDrawFish(i, j, fish_coord);

                        var isNeedToDrawSystemFish = 
                            IsNeedToDrawFish(
                                i, 
                                system_fish_coord.Item2 + system_fish_coord.Item2 - j,
                                system_fish_coord);

                        local_array_with_fish[i, j, 0] = local_fractal_array[i, j, 0];
                        local_array_with_fish[i, j, 1] = local_fractal_array[i, j, 1];
                        local_array_with_fish[i, j, 2] = local_fractal_array[i, j, 2];

                        if (local_fish_bool_array[i, j] > 0
                            && !isNeedToDrawFish && !isNeedToDrawSystemFish)
                        {
                            local_fractal_array[i, j, 0] -= 0;
                            local_fractal_array[i, j, 1] -= (byte)rnd.Next(10);
                            local_fractal_array[i, j, 2] -= (byte)rnd.Next(10);

                            if (local_fractal_array[i, j, 1] < 0)
                            {
                                local_fractal_array[i, j, 1] = 0;
                            }

                            if (local_fractal_array[i, j, 2] < 0)
                            {
                                local_fractal_array[i, j, 2] = 0;
                            }

                            local_array_with_fish[i, j, 0] = local_fractal_array[i, j, 0];
                            local_array_with_fish[i, j, 1] = local_fractal_array[i, j, 1];
                            local_array_with_fish[i, j, 2] = local_fractal_array[i, j, 2];

                            local_fish_bool_array[i, j] = 0;
                        }

                        if (isNeedToDrawFish || isNeedToDrawSystemFish)
                        {
                            local_fish_bool_array[i, j] = 
                                isNeedToDrawFish == isNeedToDrawSystemFish
                                ? (byte)2 : (byte)1;

                            local_array_with_fish[i, j, 0] = (byte)(50 + rnd.Next(200));
                            local_array_with_fish[i, j, 1] = (byte)(rnd.Next(150));
                            local_array_with_fish[i, j, 2] = 0;
                        }
                    }
                }
            }

            if (isFilterEnabled)
            {
                filterDisableIteration = 0;

                Filter(isFishEnabled);
            }

            else if (isFilteredJustDisabled)
            {
                if (filterDisableIteration == 0)
                {
                    for (int i = 0; i < 800; i++)
                    {
                        for (int j = 0; j < 600; j++)
                        {
                            local_fractal_array[i, j, 0]
                                = local_fractal_array_before_filter[i, j, 0];
                            local_fractal_array[i, j, 1]
                                = local_fractal_array_before_filter[i, j, 1];
                            local_fractal_array[i, j, 2]
                                = local_fractal_array_before_filter[i, j, 2];

                            if (isFishEnabled)
                            {
                                local_array_with_fish[i, j, 0]
                                    = local_array_with_fish_before_filter[i, j, 0];
                                local_array_with_fish[i, j, 1]
                                    = local_array_with_fish_before_filter[i, j, 1];
                                local_array_with_fish[i, j, 2]
                                    = local_array_with_fish_before_filter[i, j, 2];
                            }
                        }
                    }
                }

                filterDisableIteration++;
            }

            Gl.glRasterPos2i(-1, -1);

            if (isFishEnabled)
            {
                // визуализируем массив
                Gl.glDrawPixels(600, 600, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, local_array_with_fish);

                Gl.glColor3f(255, 0, 0);

                PrintText2D(200, 200, "2222222222222");
            }
            else
            {
                // визуализируем массив
                Gl.glDrawPixels(600, 600, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, local_fractal_array);
            }

            Gl.glFlush();
        }

        // рисование 2D аквариума в векторном упрощенном виде
        public void Draw2DAquarium(
            int timerIteration,
            bool isFishEnabled,
            (int, int, int) fish_coord,
            uint mGlTextureObject,
            bool isTextureEnabled = false)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(0, 150, 220, 1);

            Gl.glLoadIdentity();

            if (isTextureEnabled)
            {
                // активация проекционной матрицы
                Gl.glMatrixMode(Gl.GL_PROJECTION);

                // очистка матрицы
                Gl.glLoadIdentity();

                // установка перспективы
                Glu.gluPerspective(30, 800 / 600, 1, 100);

                Gl.glMatrixMode(Gl.GL_MODELVIEW);

                Gl.glLoadIdentity();

                // включаем режим текстурирования
                Gl.glEnable(Gl.GL_TEXTURE_2D);

                // включаем режим текстурирования, указывая идентификатор mGlTextureObject
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject);

                // сохраняем состояние матрицы
                Gl.glPushMatrix();

                // выполняем перемещение для более наглядного представления сцены
                Gl.glTranslated(0, -1, -5);
                // реализуем поворот объекта

                // отрисовываем полигон
                Gl.glBegin(Gl.GL_QUADS);

                // указываем поочередно вершины и текстурные координаты
                Gl.glVertex2d(-3, -3);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex2d(3, -3);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex2d(3, 3);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex2d(-3, 3);
                Gl.glTexCoord2f(0, 1);

                // завершаем отрисовку
                Gl.glEnd();

                // возвращаем матрицу
                Gl.glPopMatrix();

                // отключаем режим текстурирования
                Gl.glDisable(Gl.GL_TEXTURE_2D);

                // активация проекционной матрицы
                Gl.glMatrixMode(Gl.GL_PROJECTION);

                // очистка матрицы
                Gl.glLoadIdentity();

                Glu.gluOrtho2D(0.0, 500.0 * 800 / 600, 0.0, 500.0);

                // установка объектно-видовой матрицы
                Gl.glMatrixMode(Gl.GL_MODELVIEW);

                Gl.glLoadIdentity();
            }

            var hi = timerIteration * 10;

            Gl.glColor3f(255, 0, 0);
            Gl.glLineWidth(10);

            // Левая часть внешнего контура
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (int k = 0; k < 18; k++)
            {
                if (k == 0)
                {
                    first_system_fish_coord = (fish_matrix[k, 1] + hi % 540, fish_matrix[k, 0] + 170);
                }

                Gl.glVertex2d(
                    fish_matrix[k, 1] + hi % 540,
                    fish_matrix[k, 0] + 200);
            }

            Gl.glEnd();

            PrintText2D(first_system_fish_coord.Item1, first_system_fish_coord.Item2, "Guppi");

            Gl.glColor3f(150, 0, 120);

            // Левая часть внешнего контура
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (int k = 0; k < 18; k++)
            {
                if (k == 0)
                {
                    second_system_fish_coord = (fish_matrix[k, 1] + hi % 540, fish_matrix[k, 0] + 270);
                }

                Gl.glVertex2d(
                    fish_matrix[k, 1] + hi % 540,
                    fish_matrix[k, 0] + 300);
            }

            Gl.glEnd();

            PrintText2D(second_system_fish_coord.Item1, second_system_fish_coord.Item2, "Petushok");

            Gl.glColor3f(20, 80, 30);

            // Левая часть внешнего контура
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (int k = 0; k < 18; k++)
            {
                if (k == 0)
                {
                    third_system_fish_coord = (fish_matrix[k, 1] + hi % 540, fish_matrix[k, 0] + 370);
                }

                Gl.glVertex2d(
                    fish_matrix[k, 1] + hi % 540,
                    fish_matrix[k, 0] + 400);
            }

            Gl.glEnd();

            PrintText2D(third_system_fish_coord.Item1, third_system_fish_coord.Item2, "Koridoras");

            if (isFishEnabled)
            {
                Gl.glColor3f(0, 0, 0);

                Gl.glLineWidth(10);

                // Левая часть внешнего контура
                Gl.glBegin(Gl.GL_LINE_STRIP);

                for (int k = 0; k < 18; k++)
                {
                    user_fish_matrix[k, 0] = fish_matrix[k, 0] + fish_coord.Item1;
                    user_fish_matrix[k, 1] = fish_matrix[k, 1] + fish_coord.Item2;

                    var nRad = (fish_coord.Item3 * Math.PI) / 180;

                    var sin = (float)Math.Sin(nRad);
                    var cos = (float)Math.Cos(nRad);

                    var rotateMatrix = new float[2, 2];

                    rotateMatrix[0, 0] = cos;
                    rotateMatrix[0, 1] = -sin;
                    rotateMatrix[1, 0] = sin;
                    rotateMatrix[1, 1] = cos;

                    var newMatrix = MatrixMultiplication(user_fish_matrix, rotateMatrix);

                    if (k == 0)
                    {
                        user_system_fish_coord = ((int)newMatrix[k, 1] % 650, (int)newMatrix[k, 0] - 30 % 500);
                    }

                    Gl.glVertex2d(
                        newMatrix[k, 1] % 650,
                        newMatrix[k, 0] % 500);
                }

                Gl.glEnd();

                PrintText2D(user_system_fish_coord.Item1, user_system_fish_coord.Item2, "Kirill");
            }

            Gl.glFlush();
        }

        // функция визуализации текста 
        private void PrintText2D(float x, float y, string text)
        {
            // устанавливаем позицию вывода растровых символов 
            // в переданных координатах x и y 
            Gl.glRasterPos2f(x, y);

            // в цикле foreach перебираем значения из массива text, 
            // который содержит значение строки для визуализации 
            foreach (char char_for_draw in text)
            {
                // визуализируем символ с помощью функции glutBitmapCharacter, используя шрифт GLUT_BITMAP_9_BY_15 
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_9_BY_15, char_for_draw);
            }
        }

        #region filtering functions
        private void Filter(bool isFishEnabled)
        {
            // собираем матрицу
            float[] mat = new float[9];
            mat[0] = -0.1f;
            mat[1] = -0.1f;
            mat[2] = -0.1f;
            mat[3] = -0.1f;
            mat[4] = 1.8f;
            mat[5] = -0.1f;
            mat[6] = -0.1f;
            mat[7] = -0.1f;
            mat[8] = -0.1f;

            if (filterIteration == 0)
            {
                for (int i = 0; i < 800; i++)
                {
                    for (int j = 0; j < 600; j++)
                    {
                        local_fractal_array_before_filter[i, j, 0]
                            = local_fractal_array[i, j, 0];
                        local_fractal_array_before_filter[i, j, 1]
                            = local_fractal_array[i, j, 1];
                        local_fractal_array_before_filter[i, j, 2]
                            = local_fractal_array[i, j, 2];

                        if (isFishEnabled)
                        {
                            local_array_with_fish_before_filter[i, j, 0]
                                = local_array_with_fish[i, j, 0];
                            local_array_with_fish_before_filter[i, j, 1]
                                = local_array_with_fish[i, j, 1];
                            local_array_with_fish_before_filter[i, j, 2]
                                = local_array_with_fish[i, j, 2];
                        }
                    }
                }
            }

            filterIteration++;

            // вызываем функцию обработки, передавая туда матрицу и дополнительные параметры
            PixelTransformation(mat, 0, 1, false, local_fractal_array);

            if (isFishEnabled)
            {
                PixelTransformation(mat, 0, 1, false, local_array_with_fish);
            }
        }

        private void PixelTransformation(
            float[] mat,
            int corr, 
            float COEFF,
            bool need_count_correction,
            byte[,,] arrayForFiltering)
        {
            // массив для получения результирующего пикселя
            float[] resault_RGB = new float[3];
            int count = 0;
            // проходим циклом по всем пикселям слоя
            for (int Y = 0; Y < 600; Y++)
            {
                for (int X = 0; X < 600; X++)
                {
                    // цикл по всем составляющим (0-2, т.е. R G B)
                    for (int c = 0, ax = 0, bx = 0; c < 3; c++)
                    {
                        // обнуление составляющей результата
                        resault_RGB[c] = 0;
                        // обнуление счетчика обработок
                        count = 0;

                        // два цикла для захвата области 3х3 вокруг обрабатываемого пикселя
                        for (bx = -1; bx < 2; bx++)
                        {
                            for (ax = -1; ax < 2; ax++)
                            {
                                // если мы не попали в рамки, просто используем центральный пиксель, и продолжаем цикл
                                if (X + ax < 0 || X + ax > 800 - 1 || Y + bx < 0 || Y + bx > 600 - 1)
                                {
                                    // считаем составляющую в одной из точек, используем коэфицент в матрице (под номером текущей итерации), коэфицент усиления (COEFF) и прибовляем коррекцию (corr)
                                    resault_RGB[c] += (float)(arrayForFiltering[X, Y, c]) * mat[count] * COEFF + corr;
                                    // счетчик обработок = ячейке матрицы с необходимым коэфицентом
                                    count++;
                                    // продолжаем цикл
                                    continue;
                                }

                                // иначе, если мы укладываемся в изображение (не пересекаем границы), используем соседние пиксели, корректируем ячейку массива параметрами ax, bx
                                resault_RGB[c] += (float)(arrayForFiltering[X + ax, Y + bx, c]) * mat[count] * COEFF + corr;
                                // счетчик обработок = ячейке матрицы с необходимым коэфицентом
                                count++;
                            }
                        }

                    }

                    // теперь для всех составляющих корректируем цвет
                    for (int c = 0; c < 3; c++)
                    {
                        // если требуется разделить результат до приведения к 0-255, разделив на количество проведенных операций
                        if (count != 0 && need_count_correction)
                        {
                            // выполняем данное деление
                            resault_RGB[c] /= count;
                        }

                        // если значение меньше нуля
                        if (resault_RGB[c] < 0)
                        {
                            // - приравниваем к нулю
                            resault_RGB[c] = 0;
                        }

                        // если больше 255
                        if (resault_RGB[c] > 255)
                        {
                            // приравниваем к 255
                            resault_RGB[c] = 255;
                        }
                        // записываем в массив цветов слоя новое значение
                        arrayForFiltering[X, Y, c] = (byte)resault_RGB[c];
                    }
                }
            }
        }
        #endregion

        #region 3D Fish not used methods
        public void Draw3DFish()
        {
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(0, 150, 220, 1);

            // очищение текущей матрицы
            Gl.glLoadIdentity();

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            Glut.glutWireSphere(2, 16, 16);

            // возвращаем состояние матрицы
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();
        }
        #endregion

        // метод для умножения матриц
        private static float[,] MatrixMultiplication(int[,] matrixA, float[,] matrixB)
        {
            var matrixC = new float[18, 2];

            for (var i = 0; i < 18; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < 2; k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return matrixC;
        }

        private bool IsNeedToDrawFish(int i, int j, (int, int, int) fish_coord)
        {
            return 
                i >= fish_coord.Item1 && i <= fish_coord.Item1 + 60
                && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 80
                && !(i >= fish_coord.Item1 + 20 && i <= fish_coord.Item1 + 60
                    && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 30
                    && i + j <= fish_coord.Item1 + fish_coord.Item2 + 60)
                && !(i >= fish_coord.Item1 && i <= fish_coord.Item1 + 30
                    && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 30
                    && i + j <= fish_coord.Item1 - fish_coord.Item2)
                || i >= fish_coord.Item1 + 12 && i <= fish_coord.Item1 + 48
                && j >= fish_coord.Item2 + 80 && j <= fish_coord.Item2 + 160
                && !(i + j >= fish_coord.Item1 + 170 + fish_coord.Item2 - 20);
        }

        private void RecreateAllArrays()
        {
            local_array_with_fish = new byte[800, 600, 3];
            local_fractal_array_before_filter = new byte[800, 600, 3];
            local_fish_bool_array = new byte[800, 600];
            fish_matrix = new int[20, 2];
            user_fish_matrix = new int[18, 2];
        }

        private byte[,,] local_fractal_array = new byte[800, 600, 3];
        private byte[,,] local_fractal_array_before_filter = new byte[800, 600, 3];

        private byte[,,] local_array_with_fish = new byte[800, 600, 3];
        private byte[,,] local_array_with_fish_before_filter = new byte[800, 600, 3];

        private byte[,] local_fish_bool_array = new byte[800, 600];
        private int[,] fish_matrix = new int[20, 2];
        private int[,] user_fish_matrix = new int[18, 2];

        private (int, int) first_system_fish_coord;
        private (int, int) second_system_fish_coord;
        private (int, int) third_system_fish_coord;
        private (int, int) user_system_fish_coord;

        private int filterIteration;
        private int filterDisableIteration;
    }
}
