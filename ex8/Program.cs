using System;
using System.Collections.Generic;
using MyLib;

namespace ex8
{
    internal class Program
    {
        private static void Main()
        {
            MyGraph graph;
            switch (Ask.Menu("Ввод графа: ", "Случайный граф", "Ввод"))
            {
                case 1:
                    graph = MyGraph.RandomGraph();
                    break;
                default:    // задача графа юзверем
                    var countVertex = Ask.Num("Введите количество вершин в графе (от 1 до 26): ", 1, 26);
                    graph = new MyGraph(countVertex);

                    for (byte i = 0; i < countVertex; i++)
                    {
                        Console.WriteLine("Вершина " + MyGraph.ToLetter(i));
                        graph.ShowVertex(i);

                        var countEdges = Ask.Num($"Введите количество вершин, с которыми нужно связать {i + 1}: ", 0, countVertex - 1);
                        for (int j = 0; j < countEdges; j++)
                        {
                            var connectEdge = Ask.Num($"Введите номер вершины, с которой нужно связать {i + 1}: ", 1, countVertex) - 1;
                            while (connectEdge == i || graph.NumEdge(i, connectEdge) != -1)
                                connectEdge = Ask.Num("Петли и параллельные вершины в графе недопустимы", 1, countVertex) - 1;

                            graph.Add(i, connectEdge);
                        }
                    }
                    break;
            }
            Console.WriteLine();
            graph.Show();

            Console.WriteLine("Эйлеров путь: ");
            Console.WriteLine(graph.EulerPath());

            OC.Stay();
        }










        
    }
}
