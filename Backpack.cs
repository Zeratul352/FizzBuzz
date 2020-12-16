using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack_dynamic
{
    class Program

    {
        public static List<int> answer = new List<int>();
        public static int[] w = { -1, 2, 3, 4, 5 };
        public static void FindAnswer(int k, int s, ref int[,] A)
        {
            if(A[k,s] == 0)
            {
                return;
            }

            if(A[k - 1, s] == A[k, s])
            {
                FindAnswer(k - 1, s,ref A);
            }
            else
            {
                FindAnswer(k - 1, s - w[k], ref A);
                answer.Add(k);
            }
        }

      /*  function findAns(int k, int s)
  if A[k][s] == 0 
    return
  if A[k - 1][s] == A[k][s]
    findAns(k - 1, s)
  else 
    findAns(k - 1, s - w[k])
    ans.push(k)*/
        static void Main(string[] args)
        {

            
            int[] p = {-1, 16, 19, 23, 28 };
            int W = 7;
            int N = 4;

            int[,] A =new int[N + 1, W + 1];
            for(int i = 0; i <= W; i++)
            {
                for(int j = 0; j <= N; j++)
                {
                    A[j, i] = 0;
                }
            }
            for(int k = 1; k <= N; k++)
            {
                for(int s = 1; s <= W; s++)
                {
                    if (s >= w[k])
                    {
                        A[k, s] = Math.Max(A[k - 1, s], A[k - 1, s - w[k]] + p[k]);
                    }
                    else
                    {
                        A[k, s] = A[k - 1, s];
                    }
                }
            }
            FindAnswer(N, W, ref A);
            for(int i = 0; i < answer.Count; i++)
            {
                Console.Write(answer[i] + " ");
            }
            Console.ReadKey();

        }
    }
}
