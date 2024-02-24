using System;

using Tao.FreeGlut;
using Tao.OpenGl;

namespace Aquarium
{
    public sealed class Aquarium
    {
        public Aquarium()
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
        }

        public void DrawAquarium(
            bool isFractalEnabled,
            byte[,,] PixelsArray,
            bool isFishEnabled,
            (int, int, int) fish_coord,
            int timerIteration)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(0, 150, 220, 1);

            Gl.glLoadIdentity();

            var rnd = new Random();

            for (int i = 1; i < 600; i++)
            {
                for (int j = 1; j < 600; j++)
                {
                    if (isFractalEnabled)
                    {
                        if (PixelsArray[i, j, 0] > 235 &&
                            PixelsArray[i, j, 1] == 251 &&
                            PixelsArray[i, j, 2] > 254)
                        {
                            PixelsArray[i, j, 0] = 0;
                            PixelsArray[i, j, 1] = 255;
                            PixelsArray[i, j, 2] = 0;
                        }

                        if (PixelsArray[i, j, 0] > 239)
                        {
                            PixelsArray[i, j, 0] = 0;
                            PixelsArray[i, j, 1] = 150;
                            PixelsArray[i, j, 2] = 220;
                        }
                    }

                    if (isFishEnabled)
                    {
                        if (!isFractalEnabled)
                        {
                            PixelsArray[i, j, 0] = 0;
                            PixelsArray[i, j, 1] = 150;
                            PixelsArray[i, j, 2] = 220;
                        }
                        else
                        {
                            if (PixelsArray[i, j, 2] == 0)
                            {
                                PixelsArray[i, j, 0] = 0;
                                PixelsArray[i, j, 1] = 150;
                                PixelsArray[i, j, 2] = 220;
                            }
                        }

                        var isNeedToDraw =
                            i >= fish_coord.Item1 && i <= fish_coord.Item1 + 60
                            && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 80
                            && !(i >= fish_coord.Item1 + 30 && i <= fish_coord.Item1 + 60
                                && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 30
                                && i + j <= fish_coord.Item1 + fish_coord.Item2 + 60)
                            && !(i >= fish_coord.Item1 && i <= fish_coord.Item1 + 30
                                && j >= fish_coord.Item2 & j <= fish_coord.Item2 + 30
                                && i + j <= fish_coord.Item1 - fish_coord.Item2)
                            || i >= fish_coord.Item1 + 16 && i <= fish_coord.Item1 + 44
                            && j >= fish_coord.Item2 + 80 && j <= fish_coord.Item2 + 140;

                        if (isNeedToDraw)
                        {
                            PixelsArray[i, j, 0] = (byte)(50 + rnd.Next(200));
                            PixelsArray[i, j, 1] = (byte)(rnd.Next(150));
                            PixelsArray[i, j, 2] = 0;
                        }
                    }
                }
            }

            if (isFractalEnabled)
            {
                Gl.glRasterPos2i(-1, -1);

                // визуализируем массив
                Gl.glDrawPixels(600, 600, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, PixelsArray);
            }
            else
            {
                var hi = timerIteration * 10;

                Gl.glColor3f(255, 0, 0);
                Gl.glLineWidth(10);

                // Левая часть внешнего контура
                Gl.glBegin(Gl.GL_LINE_STRIP);

                for (int k = 0; k < 18; k++)
                {
                    Gl.glVertex2d(
                        fish_matrix[k, 1] + hi % 540,
                        fish_matrix[k, 0] + 200);
                }

                Gl.glEnd();

                Gl.glColor3f(150, 0, 120);

                // Левая часть внешнего контура
                Gl.glBegin(Gl.GL_LINE_STRIP);

                for (int k = 0; k < 18; k++)
                {
                    Gl.glVertex2d(
                        fish_matrix[k, 1] + hi % 540,
                        fish_matrix[k, 0] + 300);
                }

                Gl.glEnd();

                Gl.glColor3f(20, 80, 30);

                // Левая часть внешнего контура
                Gl.glBegin(Gl.GL_LINE_STRIP);

                for (int k = 0; k < 18; k++)
                {
                    Gl.glVertex2d(
                        fish_matrix[k, 1] + hi % 540,
                        fish_matrix[k, 0] + 400);
                }

                Gl.glEnd();

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

                        Gl.glVertex2d(
                            newMatrix[k, 1] % 650,
                            newMatrix[k, 0] % 500);
                    }

                    Gl.glEnd();
                }
            }

            Gl.glFlush();
        }

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

        private int[,] fish_matrix = new int[20, 2];
        private int[,] user_fish_matrix = new int[18, 2];
    }
}
