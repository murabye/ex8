using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

namespace ex8
{
    class Program
    {
        static void Main(string[] args)
        {
            // массив интовых чисел, где
            // каждый элемент обозначает конкретную вершину
            // а значение в нем - число, каждый бит которого отвечает за связь
            // с другой вершиной
            // таким образом, максимум вершин - 16

            // инициализация списка
            var number = Ask.Num("Введите количество вершин (от 1 до 16): ", 1, 16);
            var list = new ulong[number];

            // чтение списка
            for (var i = 0; i < number; i++)
            {
                list[i] = 0;                                        // обнуление текущего
                var letter = (char)((int)'A' + i);

                Console.Write("Введите через пробел вершины, с которыми связана данная ({0}): ", letter);
                var relation = (Console.ReadLine()).Trim().Split(' ');     // читает и делит

                foreach (var point in relation)                     // для каждой из найденных
                {
                    var res = (ulong)(point[0]) - (ulong)'A' + 1;
                    list[i] = list[i] | res;
                }
            }





            OC.Stay();
        }
    }
}
