using System;
using System.Collections.Generic;
using MyLib;

namespace ex8
{
    internal class Program
    {
        private static int[][] list = new int[0][];     // граф
        private static List<int> ans;
        
        private static void FindEulerPath(int num)
        {
            // где num - номер соответствующей вершины графа

            for (var i = 0; i < list[num].Length; i++)
                if (list[num][i] != -1)
                {
                    var path = list[num][i];

                    // удаление ребра из графа
                    list[num][i] = -1;
                    for (var j = 0; j < list[path].Length; j++)
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

            for (var i = 0; i < list.Length; i++)
                if (list[i].Length % 2 != 0)
                    odd.Enqueue(i);
            if (odd.Count != 2) throw new ArgumentException("В графе нет эйлеровой цепи");

            foreach (var point in odd)
            {
                var arr = (int[])list[point].Clone();

                list[point] = new int[arr.Length + 1];
                for (var i = 0; i < arr.Length; i++)
                    list[point][i] = arr[i];

                list[point][arr.Length] = point;
            }

            return odd;
        }

        private static void Main(string[] args)
        {
            // инициализация списка
            var number = Ask.Num("Введите количество вершин: ", 1);
            list = new int[number][];

            // чтение списка
            for (var i = 0; i < number; i++)
            {
                var letter = i + 1;

                Console.Write("Введите через пробел вершины, с которыми связана ({0}): ", letter);
                var relation = Console.ReadLine().Trim().Split(' ');       // читает и делит
                list[i] = new int[relation.Length];                       // готовит массив
                var j = 0;

                foreach (var point in relation)                     // для каждой из найденных
                {
                    if (point[0] == letter) throw new ArgumentException("Нет задачи графа с петлей");
                    if (point.Length > 1) throw new ArgumentException("Более одного символа вершина не именуется");
                    if ((int)point[0] - 'A' >= number) throw new ArgumentException("Не обнаружено вершины");

                    list[i][j++] = int.Parse(point[0].ToString());
                }
            }


            var odd = Check().ToArray();
            FindEulerPath(0);
            var ans1 = odd[0] + " " + odd[1];
            var ans2 = odd[1] + " " + odd[0];

            // ans.Remove(ans.IndexOf(ans1) > -1 ? ans.IndexOf(ans1) : ans.IndexOf(ans2), ans1.Length);

            Console.WriteLine("Ответ: " + ans);
            OC.Stay();
        }
    }
}
