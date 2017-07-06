using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumForecast
{
    /// <summary>
    /// 利用周易数字卦预测吉凶
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //请输入三个三位数字
            Console.WriteLine("请输入三个三位数字，中间以空格分隔");

            while (true)
            {
                string num = Console.ReadLine();

                var numbs = GetNumbs(num);

                if (numbs.Count < 3) Console.WriteLine("请输入三个三位数字，中间以空格分隔");

                Console.WriteLine("你输入的数字为：" + string.Join(",", numbs));

                if (numbs.Count == 3)
                {
                    var diagrams = ComputerEightDiagrams(numbs);
                    Console.WriteLine("你所算的卦：" + MakeDia(diagrams) + "(" + string.Join(",", diagrams) + ")");

                    //打印卦象
                    Print(diagrams);
                }
            }
        }

        private static void Print(List<int> diagrams)
        {
            //存储
            string[] keys = { "111", "011", "101", "001", "110", "010", "100", "000" };

            var up = diagrams[1];
            var down = diagrams[0];

            var upStr = keys[up - 1];
            var downStr = keys[down - 1];

            n = 0;
            PrintYao(upStr.ToCharArray(), diagrams[2]);
            PrintYao(downStr.ToCharArray(), diagrams[2]);

            Console.ResetColor();
        }

        private static int n = 0;
        private static void PrintYao(char[] chars, int yao)
        {
            bool isYao=false;

            foreach (var item in chars)
            {
                n++;

                Console.WriteLine("");

                isYao = n == 6 - yao + 1;

                if (item == '1')
                {
                    Console.BackgroundColor =isYao? ConsoleColor.Yellow:ConsoleColor.Blue;
                    Console.WriteLine("            ");
                }
                else if (item == '0')
                {
                    Console.BackgroundColor = isYao ? ConsoleColor.Yellow : ConsoleColor.DarkRed;
                    
                    var color = Console.BackgroundColor;

                    Console.Write("     ");

                    Console.ResetColor();
                    Console.Write("  ");

                    Console.BackgroundColor = color;
                    Console.Write("     ");
                    Console.Write("\r\n");
                }
            }
        }
        /// <summary>
        /// 从输入中获取数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static List<int> GetNumbs(string num)
        {
            List<int> result = new List<int>();
            string[] numbs = num.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int n = 0;
            foreach (var item in numbs)
            {
                int.TryParse(item, out n);

                if (item.Length == 3 && n > 0)
                {
                    result.Add(n);
                }
            }
            return result;
        }
        /// <summary>
        /// 根据数字确定八卦的上下卦，以及爻
        /// </summary>
        /// <param name="numbs"></param>
        /// <returns></returns>
        private static List<int> ComputerEightDiagrams(List<int> numbs)
        {
            List<int> dias = new List<int>();

            for (int i = 0; i < numbs.Count; i++)
            {
                if (i != numbs.Count - 1)
                {
                    var m = numbs[i] % 8;

                    if (m == 0) m = 8;

                    dias.Add(m);

                }
                else
                {
                    var n = numbs[i] % 6;

                    if (n == 0) n = 6;

                    dias.Add(n);
                }
            }
            return dias;
        }

        /// <summary>
        /// 确定卦
        /// </summary>
        /// <returns></returns>
        private static string MakeDia(List<int> diagrams)
        {
            Dictionary<Node, string> dics = new Dictionary<Node, string>();

            //首先把64卦，存储起来，然后查询

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    dics.Add(new Node() { Down = j, Up = i }, GetName(i, j));
                }
            }

            return dics[new Node() { Down = diagrams[0], Up = diagrams[1] }];

        }

        private static string GetName(int i, int j)
        {
            string[,] names ={
                              {"乾","履","同人","无妄","姤","讼","遁","否"},
                            {"夬","兑","革","随","大过","困","咸","萃"},
                             {"大有","睽","离","噬嗑","鼎","未济","旅","晋"},
                            {"大壮","归妹","丰","震","恒","解","小过","豫"},
                            {"小畜","中孚","家人","益","巽","涣","渐","观"},
                            {"需","节","既济","屯","井","坎","蹇","比"},
                             {"大畜","损","贲","颐","蛊","蒙","艮","剥"},
                            {"泰","临","明夷","复","升","师","谦","坤"}
                            };

            return names[i - 1, j - 1];
        }
    }

    public struct Node
    {
        /// <summary>
        /// 上卦
        /// </summary>
        public int Up { set; get; }

        /// <summary>
        /// 下卦
        /// </summary>
        public int Down { set; get; }
    }
}
