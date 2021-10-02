using System;

namespace LR3
{
    class Program
    {
        static void Main(string[] args)
        { //подготовка ключей. Шаг первый
            var rnd_p = GetRandom();
            var p = GenPrime(rnd_p); //простое число
            var rnd_q = GetRandom();
            var q = GenPrime(rnd_q); //простое число
            int m = p * q;
            int f = (p - 1) * (q - 1);
            var rnd_e = GetRandom();
            var e = GenPrime(rnd_e);
           if(e < f) 
            {
                if (NOD(e, f) == 1)
                {
                    e = GenPrime(rnd_e);
                }
            }
            var rnd_d = GetRandom();
            var d = GenPrime(rnd_d); //простое число
            if((d*e)%f == 1)
            {
                Console.WriteLine("d = {0} ", d);
            }
            Console.WriteLine("p = {0} ", p);
            Console.WriteLine("q = {0} ", q);
            Console.WriteLine("f = {0} ", f);
            Console.WriteLine("e = {0} ", e);
            //var A = OpenKey(19, e, m);
            //Console.WriteLine("A = {0} ", A);
        }
        public static int NOD (int a, int b)
        {
            if (a == b)
                return a;
            else
            if (a > b)
                return NOD(a - b, b);
            else
                return NOD(b - a, a);
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
            int value = rnd.Next(1, 10);
            return value;
        }
        
    }
}
