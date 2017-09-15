using System;

namespace ex8
{
    class MyGraph
    {
        private byte _countVertex;                              // кол-во вершин: от 1 до 26
        private byte[][] _graph;                                // способ представления графа: список вершин-связей
                                                                //      27 мест, где на 27 позиции - текущее кол-во ребер, далее -
                                                                //      ребра в порядке добавления
        
        public int CountVertex => _countVertex;
        public int CountEdge
        {
            get
            {
                var sum = 0;
                foreach (var vertex in _graph)                  // для каждой вершины
                    sum += vertex[26];                          // в последней позиции количество ребер

                sum /= 2;                                       // так как каждое ребро посчитано дважды

                return sum;
            }
        }

        public MyGraph(int countVertex)
        {
            // конструктор, по колву вершин, каждому массиву соответствует вершина

            // для безопасности метода
            if (countVertex < 1 || countVertex > 26)
                throw new ArgumentException("Не может быть более, чем 26 или менее, чем 1 вершин в графе", "countVertex");

            _countVertex = (byte)countVertex;               // заполнение данных класса

            _graph = new byte[_countVertex][];
            for (int i = 0; i < countVertex; i++)
                _graph[i] = new byte[27];
        }
        
        public static char ToLetter(byte number)
        {
            // по номеру возвращает букву в графе

            // безопасность метода
            if (number < 1 || number > 26)
                throw new ArgumentException("Не может быть более, чем 26 или менее, чем 1 вершин в графе", "number");
            
            return (char) ((int) 'A' + number);
        }
        public static byte ToNumber(byte letter)
        {
            // по букве графв возвращает номер в комп. представлении

            var letterTem = (byte)((int) letter - (int) 'A');
                
            // безопасность метода
            if (letterTem < 1 || letterTem > 26)
                throw new ArgumentException("Не может быть более, чем 26 или менее, чем 1 вершин в графе", "number");

            return letterTem;
        }
        
        // поддержка нижеследующих методов через цифры, не буквы
        public int Last(int vertex)
        {
            return _graph[vertex][26];
        }
        public void Add(int vertexOne, int vertexTwo)
        {
            _graph[vertexOne][26]++;
            _graph[vertexTwo][26]++;

            _graph[vertexOne][Last(vertexOne)] = (byte)vertexTwo;
            _graph[vertexTwo][Last(vertexTwo)] = (byte)vertexOne;
        }
        public char[] ShowVertex(int vertex)
        {
            var ans = new char[Last(vertex)];
            for (int i = 0; i < ans.Length; i++)
                ans[i] = MyGraph.ToLetter(_graph[vertex][i]);

            return ans;
        }
        public int NumEdge(int vertexSource, int vertex)
        {
            // если нет, то -1
            // проверка, есть ли данное ребро в вершине-источнике, вывод ее номера 

            int ans = -1;
            for (int i = 0; i < Last(vertexSource); i++)
            {
                if (_graph[vertexSource][i] != vertex) continue;
                ans = i;
                break;
            }

            return ans;
        }
        public void Delete(int vertexOne, int vertexTwo)
        {
            _graph[vertexOne][NumEdge(vertexOne, vertexTwo)] = _graph[vertexOne][Last(vertexOne)];
            _graph[vertexOne][26]--;
            
            _graph[vertexTwo][NumEdge(vertexTwo, vertexOne)] = _graph[vertexTwo][Last(vertexTwo)];
            _graph[vertexTwo][26]--;
        }
        public void EulerPath() { }

        /*
         * генератор! в качестве меню
         * найти эйлерову цепь (в том числе для проверки связности в графе и проверки четного кол-ва вершин)
         * */
    }
}
