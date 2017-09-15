using System;

namespace ex8
{
    public class MyGraph
    {
        private static Random r = new Random();
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
            if (number < 0 || number > 26)
                throw new ArgumentException("Не может быть более, чем 26 или менее, чем 1 вершин в графе", "number");
            
            return (char) ((int) 'A' + number);
        }
        public static byte ToNumber(byte letter)
        {
            // по букве графв возвращает номер в комп. представлении

            var letterTem = (byte)((int) letter - (int) 'A');
                
            // безопасность метода
            if (letterTem < 0 || letterTem > 26)
                throw new ArgumentException("Не может быть более, чем 26 или менее, чем 1 вершин в графе", "number");

            return letterTem;
        }
        public static MyGraph RandomGraph()
        {
            var count = r.Next(3, 11);
            var ans = new MyGraph(count);

            for (int i = 0; i < count; i++)                             // для каждой вершины
            {
                var connective = r.Next(0, 3);                          // сгенерить кол-во связей
                for (int j = 0; j < connective; j++)                    // для каждой связи
                {
                    var limit = 10;                                     // лимит в 10 попыток

                    while (limit > 0)                                   // пока он не исчерпан
                    {
                        var connectVert = RandomVertex(count, i);       // сгенерить случайную вершину, кроме изначальной (петля)
                        if (ans.NumEdge(i, connectVert) != -1)          // если связь с этой вершиной уже есть, то
                            limit--;                                    // уменьшить лимин
                        else
                        {
                            ans.Add(i, connectVert);                    // иначе добавить вершину
                            break;                                      // выйти из цикла
                        }
                    }
                }
            }

            return ans;
        }

        public void Show()
        {
            Console.WriteLine("Граф: \nВершина - вершины, связанные с ней, через запятую");

            for (byte i = 0; i < _countVertex; i++)
            {
                Console.Write(ToLetter(i) + ": ");
                for (byte j = 0; j < Last(i) + 1; j++)
                    Console.Write(ToLetter(_graph[i][j]) + ", ");
                Console.WriteLine();
            }
        }
        public void ShowVertex(int vertex)
        {
            for (int i = 0; i < Last(vertex) + 1; i++)
                Console.Write(_graph[vertex][i]);

            Console.WriteLine();
        }
        public void Add(int vertexOne, int vertexTwo)
        {
            _graph[vertexOne][26]++;
            _graph[vertexTwo][26]++;

            _graph[vertexOne][Last(vertexOne)] = (byte)vertexTwo;
            _graph[vertexTwo][Last(vertexTwo)] = (byte)vertexOne;
        }
        public int NumEdge(int vertexSource, int vertex)
        {
            // если нет, то -1
            // проверка, есть ли данное ребро в вершине-источнике, вывод ее номера 

            int ans = -1;
            for (int i = 0; i < Last(vertexSource) + 1; i++)
            {
                if (_graph[vertexSource][i] != vertex) continue;
                ans = i;
                break;
            }

            return ans;
        }
        public string EulerPath()
        {
            int indexOdd;
            var path = "";

            // проверить на несвязанные вершины

            if (!CheckEven(out indexOdd)) return "Не существует эйлерова пути: много нечетных вершин";
            indexOdd = indexOdd == -1 ? 0 : indexOdd;

            EulerFragment(indexOdd, ref path);

            return !CheckConnective() ? "Не существует эйлерова пути: несвязный граф" : path;
        }
        public void EulerFragment(int startVertex, ref string path)
        {
            path += ToLetter((byte)startVertex) + " ";

            if (Last(startVertex) <= -1) return;

            var edge = _graph[startVertex][Last(startVertex)];              // вершина, с кт соединена стартовая
            Delete(startVertex, edge);

            EulerFragment(edge, ref path);
        }

        private int Last(int vertex)
        {
            return _graph[vertex][26] - 1;
        }
        private void Delete(int vertexOne, int vertexTwo)
        {
            _graph[vertexOne][NumEdge(vertexOne, vertexTwo)] = _graph[vertexOne][Last(vertexOne)];
            _graph[vertexOne][26]--;
            
            _graph[vertexTwo][NumEdge(vertexTwo, vertexOne)] = _graph[vertexTwo][Last(vertexTwo)];
            _graph[vertexTwo][26]--;
        }
        private bool CheckEven(out int indexOdd)
        {
            var countOdd = 0;
            indexOdd = -1;

            for (var i = 0; i < _countVertex; i++)
            {
                if (_graph[i][26] % 2 == 0) continue;
                countOdd++;
                if (countOdd > 2) return false;
                indexOdd = i;
            }

            return countOdd != 1;
        }
        private bool CheckConnective()
        {
            foreach (var vertex in _graph)
                if (vertex[26] != 0) return false;

            return true;
        }
        private static int RandomVertex(int count, int exception)
        {
            var ans = r.Next(count);

            while (ans == exception)
                ans = r.Next(count);

            return ans;
        }
    }
}