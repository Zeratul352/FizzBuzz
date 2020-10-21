using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binare_task_algorythm
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 254;
            int onescount = 0;
            while(num > 0)
            {
                if((num>>1)<<1 != num)
                {
                    onescount++;
                }
                num = num >> 1;
            }
            int number = 0;
            
            for(int i = 0; i < onescount; i++)
            {
                number = (number << 1) + 1;
            }
            int lownum = number;
            int highnum = number;
            int rasryady = 16;
            highnum = highnum << (rasryady - onescount);
            Console.WriteLine(highnum);
            Console.WriteLine(lownum);
            Console.ReadKey();
        }
    }
}
