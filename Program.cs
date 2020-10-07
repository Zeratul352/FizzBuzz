using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int Fizz = 3;
            int Buzz = 5;
            for(int i = 1; i <= 100; i++)
            {
                bool flag = false;
                if (i == Fizz)
                {
                    Console.Write("Fizz");
                    Fizz += 3;
                    flag = true;
                }
                if(i == Buzz)
                {
                    Console.Write("Buzz");
                    Buzz += 5;
                        flag = true;
                }
                if(!flag)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
