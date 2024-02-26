namespace Aquarium
{
    // данный класс - это набор параметров, которые мы будем передавать в поток
    public class ParamsForThread
    {
        public ParamsForThread(int startH, int endH, int Width)
        {
            _FromImageH = startH;
            _ToImageH = endH;
            _ImageW = Width;
        }

        // элемент, установленный в comboBox1
        public int code_mode;

        public delegate void _RenderDLG();
        // указатель на функцию отрисовки
        public _RenderDLG _pointerToDraw = null;

        // параметры части изображения, которое будет расчитыватся в данном потоке
        public int _FromImageH;
        public int _ToImageH;
        public int _ImageW;

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
