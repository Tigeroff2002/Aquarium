using System.IO;
using System.Linq;
using Tao.OpenGl;

namespace Aquarium
{
    internal class anModelLoader
    {
        public anModelLoader()
        {

        }

        // имя файла
        public string FName = "";

        // загружен ли (флаг)
        private bool isLoad = false;
        // счетчик подобъектов
        private int count_limbs;
        // переменная для хранения номера текстуры
        private int mat_nom = 0;

        // номер дисплейного списка с данной моделью
        private int thisList = 0;

        // данная переменная будет указывать на количество прочитанных символов в строке при чтении информации из файла
        private int GlobalStringFrom = 0;

        // массив подобъектов
        LIMB[] limbs = null;

        // массив для хранения текстур
        TexturesForObjects[] text_objects = null;

        // описание ориентации модели
        //Model_Prop coord = new Model_Prop();

        // загрузка модели
        public int LoadModel(string FileName)
        {

            // модель может содержать до 256 подобъектов
            limbs = new LIMB[256];
            // счетчик скинут
            int limb_ = -1;

            // имя файла
            FName = FileName;

            // начинаем чтение файла
            StreamReader sw = File.OpenText(FileName);

            // временные буферы
            string a_buff = "";
            string b_buff = "";
            string c_buff = "";

            // счетчики вершин и полигонов
            int ver = 0, fac = 0;

            // если строка успешно прочитана
            while ((a_buff = sw.ReadLine()) != null)
            {
                // получаем первое слово
                b_buff = GetFirstWord(a_buff, 0);
                if (b_buff[0] == '*') // определеям, является ли первый символ звездочкой
                {

                    switch (b_buff) // если да, то проверяем какое управляющее слово содержится в первом прочитаном слове
                    {

                        case "*MATERIAL_COUNT": // счетчик материалов
                            {

                                // получаем первое слово от символа, указанного в GlobalStringFrom
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat = System.Convert.ToInt32(c_buff);

                                // создаем объект для текстуры в памяти
                                text_objects = new TexturesForObjects[mat];
                                continue;

                            }

                        case "*MATERIAL_REF": // номер текстуры
                            {

                                // записываем для текущего подобъекта номер текстуры
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat_ref = System.Convert.ToInt32(c_buff);

                                // устанавливаем номер материала, соответствующий данной модели
                                limbs[limb_].SetMaterialNom(mat_ref);
                                continue;

                            }

                        case "*MATERIAL": // указание на материал
                            {

                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                mat_nom = System.Convert.ToInt32(c_buff);
                                continue;

                            }

                        case "*GEOMOBJECT": // начинается описание геометрии подобъекта
                            {

                                limb_++; // записываем в счетчик подобъектов
                                continue;

                            }

                        case "*MESH_NUMVERTEX": // количесвто вершин в подобъекте
                            {

                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                ver = System.Convert.ToInt32(c_buff);
                                continue;

                            }

                        case "*BITMAP": // имя текстуры
                            {

                                c_buff = ""; // обнуляем временный буффер

                                for (int ax = GlobalStringFrom + 2; ax < a_buff.Length - 1; ax++)
                                    c_buff += a_buff[ax]; // считываем имя текстуры

                                var buff = new string(c_buff.Where(char.IsLetterOrDigit).ToArray());

                                buff = buff.Substring(0, buff.Length - 3) + ".jpg";

                                text_objects[mat_nom] = new TexturesForObjects(); // новый объект для текстуры

                                text_objects[mat_nom].LoadTextureForModel(buff); // загружаем текстуру

                                continue;

                            }

                        case "*MESH_NUMTVERTEX": // количество текстурных координат; данное слово говорит о наличии текстурных координат, следовательно мы должны выделить память для них
                            {


                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                if (limbs[limb_] != null)
                                {

                                    limbs[limb_].createTextureVertexMem(System.Convert.ToInt32(c_buff));

                                }
                                continue;

                            }

                        case "*MESH_NUMTVFACES": // память для текстурных координат (faces)
                            {

                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);

                                if (limbs[limb_] != null)
                                {

                                    // выделяем память для текстурных координат
                                    limbs[limb_].createTextureFaceMem(System.Convert.ToInt32(c_buff));

                                }
                                continue;

                            }

                        case "*MESH_NUMFACES": // количество полигонов в подобъекте
                            {

                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                fac = System.Convert.ToInt32(c_buff);

                                // если было объвляющее слово *GEOMOBJECT (гарантия выполнения условия limb_ > -1) и было указано количство вершин
                                if (limb_ > -1 && ver > -1 && fac > -1)
                                {

                                    // создаем новый подобъект в памяти
                                    limbs[limb_] = new LIMB(ver, fac);

                                }
                                else
                                {

                                    // иначе завершаем неудачей
                                    return -1;

                                }
                                continue;

                            }

                        case "*MESH_VERTEX": // информация о вершине
                            {

                                // подобъект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем информацию о кооринатах и номере вершины
                                // (получаем все слова в строке)
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразовываем в целое число
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой на запятые, чтобы правильно выполнилась функция
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем информацию о вершине
                                limbs[limb_].vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        case "*MESH_FACE": // информация о полигоне
                            {

                                // подобъект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);
                                a5 = GetFirstWord(a_buff, GlobalStringFrom);
                                a6 = GetFirstWord(a_buff, GlobalStringFrom);
                                a7 = GetFirstWord(a_buff, GlobalStringFrom);

                                // получаем номер полигона из первого слова в строке, заменив последний символ ":" после номера на флаг окончания строки
                                int NomFace = System.Convert.ToInt32(a1.Replace(':', '\0'));

                                // записываем номера вершин, которые нас интересуют
                                limbs[limb_].face[0, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].face[1, NomFace] = System.Convert.ToInt32(a5);
                                limbs[limb_].face[2, NomFace] = System.Convert.ToInt32(a7);

                                continue;

                            }

                        // текстурые координаты
                        case "*MESH_TVERT":
                            {

                                // подобъект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер вершины
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой на запятые, чтобы правильно выполнилась функция
                                // преобразование строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем значение вершины
                                limbs[limb_].t_vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].t_vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].t_vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        // привязка текстурных координат к полигонам
                        case "*MESH_TFACE":
                            {

                                // подобъект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер полигона
                                int NomFace = System.Convert.ToInt32(a1);

                                // записываем номера вершин, которые опиывают полигон
                                limbs[limb_].t_face[0, NomFace] = System.Convert.ToInt32(a2);
                                limbs[limb_].t_face[1, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].t_face[2, NomFace] = System.Convert.ToInt32(a4);

                                continue;

                            }

                    }

                }

            }
            // пересохраняем количество полигонов
            count_limbs = limb_;


            // получаем ID для создаваемого дисплейного списка
            int nom_l = Gl.glGenLists(1);
            thisList = nom_l;
            // генерируем новый дисплейный список
            Gl.glNewList(nom_l, Gl.GL_COMPILE);
            // отрисовываем геометрию
            CreateList();
            // завершаем дисплейный список
            Gl.glEndList();

            // загрузка завершена
            isLoad = true;

            return 0;
        }

        // функция отрисовки
        private void CreateList()
        {

            // сохраняем текущую матрицу

            Gl.glPushMatrix();

            // проходим циклом по всем подобъектам
            for (int l = 0; l <= count_limbs; l++)
            {

                // если текстура необходима
                if (limbs[l].NeedTexture())
                    if (text_objects[limbs[l].GetTextureNom()] != null) // текстурный объект существует
                    {

                        Gl.glEnable(Gl.GL_TEXTURE_2D); // включаем режим текстурирования

                        // ID текстуры в памяти
                        uint nn = text_objects[limbs[l].GetTextureNom()].GetTextureObj();
                        // активируем (привязываем) эту текстуру
                        Gl.glBindTexture(Gl.GL_TEXTURE_2D, nn);

                    }

                Gl.glEnable(Gl.GL_NORMALIZE);

                // начинаем отрисовку полигонов
                Gl.glBegin(Gl.GL_TRIANGLES);

                // по всем полигонам
                for (int i = 0; i < limbs[l].VandF[1]; i++)
                {

                    // временные переменные, чтобы код был более понятен
                    float x1, x2, x3, y1, y2, y3, z1, z2, z3 = 0;

                    // вытаскиваем координаты треугольника (полигона)
                    x1 = limbs[l].vert[0, limbs[l].face[0, i]];
                    x2 = limbs[l].vert[0, limbs[l].face[1, i]];
                    x3 = limbs[l].vert[0, limbs[l].face[2, i]];
                    y1 = limbs[l].vert[1, limbs[l].face[0, i]];
                    y2 = limbs[l].vert[1, limbs[l].face[1, i]];
                    y3 = limbs[l].vert[1, limbs[l].face[2, i]];
                    z1 = limbs[l].vert[2, limbs[l].face[0, i]];
                    z2 = limbs[l].vert[2, limbs[l].face[1, i]];
                    z3 = limbs[l].vert[2, limbs[l].face[2, i]];

                    // рассчитываем номраль
                    float n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                    float n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                    float n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    // устанавливаем номраль
                    Gl.glNormal3f(n1, n2, n3);

                    // если установлена текстура
                    if (limbs[l].NeedTexture() && (limbs[l].t_vert != null) && (limbs[l].t_face != null))
                    {

                        // устанавливаем текстурные координаты для каждой вершины и сами вершины
                        Gl.glTexCoord2f(limbs[l].t_vert[0, limbs[l].t_face[0, i]], limbs[l].t_vert[1, limbs[l].t_face[0, i]]);
                        Gl.glVertex3f(x1, y1, z1);

                        Gl.glTexCoord2f(limbs[l].t_vert[0, limbs[l].t_face[1, i]], limbs[l].t_vert[1, limbs[l].t_face[1, i]]);
                        Gl.glVertex3f(x2, y2, z2);

                        Gl.glTexCoord2f(limbs[l].t_vert[0, limbs[l].t_face[2, i]], limbs[l].t_vert[1, limbs[l].t_face[2, i]]);
                        Gl.glVertex3f(x3, y3, z3);

                    }
                    else // иначе - отрисовка только вершин
                    {

                        Gl.glVertex3f(x1, y1, z1);
                        Gl.glVertex3f(x2, y2, z2);
                        Gl.glVertex3f(x3, y3, z3);

                    }

                }

                // завершаем отрисовку
                Gl.glEnd();
                Gl.glDisable(Gl.GL_NORMALIZE);

                // отключаем текстурирование
                Gl.glDisable(Gl.GL_TEXTURE_2D);

            }

            // возвращаем сохраненную ранее матрицу
            Gl.glPopMatrix();
        }

        // функция получения первого слова строки
        private string GetFirstWord(string word, int from)
        {

            // from указывает на позицию, начиная с которой будет выполнятся чтение файла
            char a = word[from]; // первый символ
            string res_buff = ""; // временный буффер
            int L = word.Length; // длина слова

            if (word[from] == ' ' || word[from] == '\t') // если первый символ, с которого предстоит искать слово, является пробелом или знаком табуляции
            {

                // необходимо вычислить наличие секции пробелов или знаков табуляции и исключить их
                int ax = 0;
                // проходим до конца слова
                for (ax = from; ax < L; ax++)
                {

                    a = word[ax];
                    if (a != ' ' && a != '\t') // если встречаем символ пробела или табуляции
                        break; // выходим из цикла
                               // таким образом, мы исключаем все последовательности пробелов или знаков табуляции, с которых могла начинатся переданная строка

                }

                if (ax == L) // если вся представленная строка является набором пробелов или знаков табуляции - возвращаем res_buff
                    return res_buff;
                else
                    from = ax; // иначе сохраняем значение ax

            }
            int bx = 0;

            // теперь, когда пробелы и табуляция исключены, мы непосредственно вычисляем слово
            for (bx = from; bx < L; bx++)
            {

                // если встретили знак пробела или табуляции, завершаем чтение слова
                if (word[bx] == ' ' || word[bx] == '\t')
                    break;
                // записываем символ во временный буффер, постепенно получая таким образом слово
                res_buff += word[bx];

            }

            // если дошли до конца строки
            if (bx == L)
                bx--; // убираем последнее значение

            GlobalStringFrom = bx; // позиция в данной строке для чтения следующего слова в данной строке

            return res_buff; // возвращаем слово

        }

        // функция отрисовки 3D-модели
        public void DrawModel()
        {
            // если модель не загружена, возврат из функции
            if (!isLoad)
                return;

            // сохраняем матрицу
            Gl.glPushMatrix();

            Gl.glColor3f(1.0f, 0.4f, 0.5f);

            Gl.glTranslated(5, 10, -5);

            Gl.glRotated(-90, 1, 0, 0);

            Gl.glRotated(-100, 0, 1, 0);

            Gl.glRotated(180, 0, 0, 1);

            Gl.glScalef(10, 10, 10);

            // возврат матрицы
            Gl.glPopMatrix();
        }
    }
}
