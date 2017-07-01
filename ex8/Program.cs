using System;
using System.Collections.Generic;
using MyLib;

namespace ex8
{
    internal class Program
    {
        private static List<int>[] list = new List<int>[0];     // граф
        private static List<int> ans = new List<int>();
        private static bool eq = false;

        private static void FindEulerPath(int num)
        {
            // где num - номер соответствующей вершины графа
            for (var i = 0; i < list[num].Count; i++)
                if (list[num][i] != -1)
                {
                    var path = list[num][i];

                    // удаление ребра из графа
                    list[num][i] = -1;
                    for (var j = 0; j < list[path].Count; j++)
                    {
                        if (list[path][j] != num) continue;

                        list[path][j] = -1;
                        break;
                    }

                    FindEulerPath(path);
                }
            
            ans.Add(num + 1);

        }
        private static Queue<int> Check()
        {
            var odd = new Queue<int>();          // нечетные вершины

            // поиск нечетных вершин
            for (var i = 0; i < list.Length; i++)
                if (list[i].Count % 2 != 0)
                    odd.Enqueue(i);
            if (odd.Count != 2) throw new ArgumentException("В графе нет эйлеровой цепи");


            var ch1 = odd.Dequeue();
            var ch2 = odd.Peek();
            odd.Enqueue(ch1);

            // если нечетные вершины соединены, то удалить связь
            for (var i = 0; i < list[ch1].Count; i++)
            {
                if (list[ch1][i] != ch2) continue;

                list[ch1].Remove(ch2);
                list[ch2].Remove(ch1);
                eq = true;
                return odd;
            }

            // иначе добавить связь
            list[ch1].Add(ch2);
            list[ch2].Add(ch1);

            return odd;
        }

        private static void Main(string[] args)
        {
            // инициализация списка
            var number = Ask.Num("Введите количество вершин: ", 1);
            list = new List<int>[number];

            // чтение списка
            for (var i = 0; i < number; i++)
            {
                var letter = i + 1;
                list[i] = new List<int>();

                Console.Write("Введите через пробел вершины, с которыми связана ({0}): ", letter);
                var relation = Console.ReadLine().Trim().Split(' ');       // читает и делит
                var j = 0;

                foreach (var point in relation)                     // для каждой из найденных
                {
                    if (point[0] == letter) throw new ArgumentException("Нет задачи графа с петлей");
                    if (point.Length > 1) throw new ArgumentException("Более одного символа вершина не именуется");
                    if ((int)point[0] - 'A' >= number) throw new ArgumentException("Не обнаружено вершины");

                    list[i].Add(int.Parse(point[0].ToString()) - 1);
                }
            }


            var odd = Check().ToArray();
            FindEulerPath((int)odd.GetValue(0));

            if (eq) ans.Add((int)odd.GetValue(1));
            // нужен еще случай, когда нечетные степени не были соединены
            
            Console.WriteLine("Ответ: " + String.Join(", ", ans.ToArray()));
            OC.Stay();
        }
    }
}
