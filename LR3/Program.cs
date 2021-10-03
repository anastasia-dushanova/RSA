using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace LR3
{

    class Program
    {
        


        static void Main(string[] args)
        { //подготовка ключей. Шаг первый
            //var rnd_p = GetRandom();
            //var p = GenPrime(GetRandom()); //простое число
            var p = 3; //простое число
            //var rnd_q = GetRandom();
            //var q = GenPrime(GetRandom()); //простое число
            var q = 7; //простое число
            Console.WriteLine("Простое число p = {0} ", p);
            Console.WriteLine("Простое число q = {0} ", q);
            int n = p * q;
            Console.WriteLine("Определим произведение n = {0} ", n);
            long f = (p - 1) * (q - 1);
            Console.WriteLine("Определим функцию Эйлера f = {0} ", f);
            //long e = Calc_e(f);
            //long d = Calc_d(e, f);
            //Console.WriteLine("Определим число e = {0} ", e);
            //Console.WriteLine("Определим число d = {0} ", d);
            //Console.WriteLine("(e,n) = ({0},{1}) - открытый ключ", e, n);
            //Console.WriteLine("(d,n) = ({0},{1}) - закрытый ключ", d, n);
            string input = File.ReadAllText("input.txt");
            input = input.ToUpper();
            //List<string> output = Shifr(input, e, n);
            Console.WriteLine();
            //Rashifr(output, d, n);
            var E = GenPrime(20);

        }
        public static string Rashifr(List<string> input, long d, long n)
        {
            char[] arr = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.ModPow(bi, (int)d, n);

                //BigInteger n_ = new BigInteger((int)n);

                //bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += arr[index].ToString();
            }
            foreach (char s in result)
                Console.Write("{0} ", s);
            return result;

        }
        public static List<string> Shifr(string text, long e, long n) //шифруем
        {
            char[] arr = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
            BigInteger bi;
            List<string> result = new List<string>();
            for(int i=0; i<text.Length; i++)
            {
                int index = Array.IndexOf(arr, text[i]);
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }
            File.WriteAllText("output.txt", result.ToString());
            foreach(string s in result)
                 Console.Write("{0} ", s);
            return result;
        }
        /// <summary>
        /// Вычисление параметра <e> 
        /// 1. простое
        /// 2. меньше <f> и больше 1
        /// 3. взаимно простое с <f>
        /// </summary>
        /// <param name="f">Функция Эйлера</param>
        /// <returns></returns>
        public static long Calc_e(long f) //вычисление параметра e взамнопростое с f
        {
            //var e = GenPrime_e(GetRandom());
            //List<int> es = new List<int>();
            /*long e = f - 1;
            for (int i=2; i<=f; i++)
            {
                if (e < f || e>1)
                {
                    if ((f % i == 0) && (e % i == 0)) //имеют общие делители
                    {
                        e--;
                        i = 1;
                    }
                    
                }
            }*/
            
            List<int> es = new List<int>();
            for (int i = 0; i <= es.Count; i++)
            {
                if (es[i] < f && es[i] > 1)
                {
                    if (NOD(es[i], f) == 1) es.Add(es[i]);

                }
            }
            var num = es[new Random().Next(0, es.Count)];
            return num;
        }
        public static int NOD(long a, long b)
        {
            if (a == b)
                return (int)a;
            else
                if (a > b)
                return NOD(a - b, b);
            else
                return NOD(b - a, a);
        }
        /// <summary>
        /// Вычисление параметра <d> 
        /// Обратное по модулю функции Эйлера
        /// </summary>
        /// <param name="d"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static long Calc_d( long e, long f) 
        {
            long d = 10;

            while (true)
            {
                if ((d * e) % f == 1)
                    break;
                else
                    d++;
            }

            return e;
        }
        public static List<int> GenPrime_e(int n) //n-мерный массив из которого выберется e
        {
            bool[] mass = new bool[n]; //булевый массив
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = true;
            }
            for (int p = 2; p < n; p++)
            {
                if (mass[p])
                {
                    for (int i = p * p; i < n; i += p)
                    {
                        mass[i] = false; //заменяем значения массива
                    }
                }
            }
            for (int i = 2; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i]) { array[j] = i; } //получаем массив из простых чисел
                }
            }
            //var num = array[new Random().Next(0, array.Length)]; //из массива простых чисел рандомно выбираем ОДНО число
            List<int> list = array.ToList();
            foreach (int s in list)
                Console.Write("{0} ", s);
            return list;
        }
        public static int GenPrime(int n) //поиск простого числа из n-го количества
        {
            bool[] mass = new bool[n]; //булевый массив
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = true;
            }
            for (int p = 2; p < n; p++)
            {
                if (mass[p])
                {
                    for (int i = p * p; i < n; i += p)
                    {
                        mass[i] = false; //заменяем значения массива
                    }
                }
            }


            int j = 0;
                for (int i = 2; i < n; i++)
                {
                    if (mass[i]) { array[j] = i; j++; } //получаем массив из простых чисел
                }
            
            for (int i = 0; i < array.Length; i++) 
            {

                Console.Write(" {0};", array[i]);
            }
            var num = array[new Random().Next(0, array.Length)]; //из массива простых чисел рандомно выбираем ОДНО число

            return num;
        }
        public static int GetRandom() //вывод рандомного числа
        {
            Random rnd = new Random();
            int value = rnd.Next(3, 10);
            return value;
        }
        
    }
}
