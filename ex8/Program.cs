using System;
using MyLib;

namespace ex8
{
    internal class Program
    {
        private static int[][] list = new int[0][];     // граф

        /*
         *   procedure FindEulerPath (V)
	     *   1. перебрать все рёбра, выходящие из вершины V;
		 *           каждое такое ребро удаляем из графа, и
		 *           вызываем FindEulerPath из второго конца этого ребра;
	     *   2. добавляем вершину V в ответ.
         *   
         */

        private static void FindEulerPath(int num)
        {
            // где num - номер соответствующей вершины графа


        }


        private static void Main(string[] args)
        {
            // инициализация списка
            var number = Ask.Num("Введите количество вершин: ", 1);
            list = new int[number][];

            // чтение списка
            for (var i = 0; i < number; i++)
            {
                var letter = (char)('A' + i);

                Console.Write("Введите через пробел вершины, с которыми связана ({0}): ", letter);
                var relation = Console.ReadLine().Trim().Split(' ');       // читает и делит
                list[i] = new int[relation.Length];                       // готовит массив
                var j = 0;

                foreach (var point in relation)                     // для каждой из найденных
                {
                    if (point[0] == letter) throw new ArgumentException("Нет задачи графа с петлей");
                    if (point.Length > 1) throw new ArgumentException("Более одного символа вершина не именуется");
                    if ((int)point[0] - 'A'> 15) throw new ArgumentException("Превышение заданного алфавита");

                    list[i][j++] = point[0] - 'A';
                }
            }





            OC.Stay();
        }
    }
}
