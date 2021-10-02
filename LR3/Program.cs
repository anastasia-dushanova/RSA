﻿using System;
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
            var p = GenPrime(10); //простое число
            //var rnd_q = GetRandom();
            //var q = GenPrime(GetRandom()); //простое число
            var q = GenPrime(10); //простое число
            Console.WriteLine("Простое число p = {0} ", p);
            Console.WriteLine("Простое число q = {0} ", q);
            int n = p * q;
            Console.WriteLine("Определим произведение n = {0} ", n);
            long f = (p - 1) * (q - 1);
            Console.WriteLine("Определим функцию Эйлера f = {0} ", f);
            long d = Calc_d(f);
            long e = Calc_e(d, f);
            Console.WriteLine("Определим число d = {0} ", d);
            Console.WriteLine("Определим число e = {0} ", e);
            Console.WriteLine("(e,n) = ({0},{1}) - открытый ключ", e, n);
            Console.WriteLine("(d,n) = ({0},{1}) - закрытый ключ", d, n);
            string text = File.ReadAllText("input.txt");
            text = text.ToUpper();
            List<string> shifr = Shifr(text, e, n);
            Console.WriteLine();
            Rashifr(shifr, d, n);
            
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
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += arr[index].ToString();
            }

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
            foreach(string s in result)
                 Console.Write("{0} ", s);
            return result;
        }
        public static long Calc_d(long f) //вычисление параметра d взамнопростое с f
        {
            long d = f - 1;
            for(long i=2; i<=f; i++)
            {
                if ((f % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }
            }
            return d;
        }
        public static long Calc_e( long d, long f) //вычисление параметра е
        {
            var e = GenPrime_e(GetRandom());
            List<int> es = new List<int>();
            for (int i = 0; i < e.Count; i++)
            {
                if (e[i] < f)
                {
                    if ((e[i] * d) % f == 1)
                    {
                        es.Add(e[i]);
                    }
                }
                else break;
            }
            var num = e[new Random().Next(0, e.Count)];
            return num;
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
            for (int i = 2; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i]) { array[j] = i; } //получаем массив из простых чисел
                }
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
