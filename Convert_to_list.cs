using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Random_Tree tree = new Random_Tree(random.Next(0, 100));
            for (int i = 0; i < 15; i++)
            {
                tree = Random_Tree.InsertBST(tree, random.Next(0, 100));
            }
            Random_Tree.NicePrint(tree, 10);
            List<int> list = new List<int>();
            Random_Tree.DFS(tree, list);
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
                Console.Write(" ,");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
    class Random_Tree
    {
        public Random_Tree(int Data)
        {
            data = Data;
            LeftSubtree = null;
            RightSubtree = null;
            size = 1;
        }
        public static Random random = new Random();
        public int data { get; set; }
        public int size { get; set; }
        Random_Tree LeftSubtree { get; set; }
        Random_Tree RightSubtree { get; set; }
        public static Random_Tree Insert(Random_Tree root, int k)
        {
            
            if(root == null)
            {
                return new Random_Tree(k);
            }
            if(random.Next()%(root.size + 1) == 0)
            {
                return InsertRoot(root, k);
            }
            if(root.data > k)
            {
                root.LeftSubtree = Insert(root.LeftSubtree, k);
            }
            else
            {
                root.RightSubtree = Insert(root.RightSubtree, k);
            }
            fixsize(root);
            //root.size = GetNumberElements(root);
            return root;
        }
        public static Random_Tree InsertBST(Random_Tree root, int k)
        {

            if (root == null)
            {
                return new Random_Tree(k);
            }
            /*if (random.Next() % (root.size + 1) == 0)
            {
                return InsertRoot(root, k);
            }*/
            if (root.data > k)
            {
                root.LeftSubtree = InsertBST(root.LeftSubtree, k);
            }
            else
            {
                root.RightSubtree = InsertBST(root.RightSubtree, k);
            }
            fixsize(root);
            //root.size = GetNumberElements(root);
            return root;
        }
        public static int getsize(Random_Tree p)
        {
            if (p == null)
            {
                return 0;
            }
            return p.size;
        }
        public static void fixsize(Random_Tree p)
        {
            p.size = getsize(p.LeftSubtree) + getsize(p.RightSubtree) + 1;
        }
        public static void Print(Random_Tree root)
        {
            if(root == null)
            {
                return;
            }
            Console.WriteLine(root.data);
            Print(root.LeftSubtree);
            Print(root.RightSubtree);
            
        }
        public static Random_Tree RotateRight(Random_Tree root)
        {
            Random_Tree q = root.LeftSubtree;
            if (q == null)
            {
                return root;
            }
            root.LeftSubtree = q.RightSubtree;
            q.RightSubtree = root;
            //q.size = root.size;
            fixsize(root);
            fixsize(q);
            //root.size = GetNumberElements(root);
            //q.size = GetNumberElements(q);
            return q;
        }
        public static Random_Tree RotateLeft(Random_Tree q)
        {
            Random_Tree root = q.RightSubtree;
            if(q == null)
            {
                return root;
            }
            q.RightSubtree = root.LeftSubtree;
            root.LeftSubtree = q;
            //root.size = q.size;
            fixsize(q);
            fixsize(root);
            //q.size = GetNumberElements(q);
            //root.size = GetNumberElements(root);
            
            return root;
        }
        public static Random_Tree InsertRoot(Random_Tree root, int k)
        {
            if(root == null)
            {
                return new Random_Tree(k);
            }
            if(k < root.data)
            {
                root.LeftSubtree = InsertRoot(root.LeftSubtree, k);
                return RotateRight(root);
            }
            else
            {
                root.RightSubtree = InsertRoot(root.RightSubtree, k);
                return RotateLeft(root);
            }
        }
        public static Random_Tree Join(Random_Tree first, Random_Tree second)
        {
            //Random random = new Random();
            if (first == null) return second;
            if (second == null) return first;
            if(random.Next()%(first.size + second.size) < first.size)
            {
                first.RightSubtree = Join(first.RightSubtree, second);
                fixsize(first);
                //first.size = GetNumberElements(first);
                return first;
            }
            else
            {
                second.LeftSubtree = Join(second.LeftSubtree, first);
                fixsize(second);
                //second.size = GetNumberElements(second);
                return second;
            }
        }
        public static Random_Tree Remove(Random_Tree root, int k)
        {
            if (root == null) return root;
            if(root.data == k)
            {
                Random_Tree q = Join(root.LeftSubtree, root.RightSubtree);
                // delete root
                return q;
            }else if(k < root.data)
            {
                root.LeftSubtree = Remove(root.LeftSubtree, k);
            }
            else
            {
                root.RightSubtree = Remove(root.RightSubtree, k);
            }
            return root;
        }
        public static Random_Tree Find(Random_Tree root, int k)
        {
            if(root == null)
            {
                return null;
            }
            if(root.data == k)
            {
                return root;
            }
            if(k < root.data)
            {
                return Find(root.LeftSubtree, k);
            }
            else
            {
                return Find(root.RightSubtree, k);
            }
        }
        public static int GetSize(Random_Tree root)
        {
            if (root == null) return 0;
            int left = GetSize(root.LeftSubtree);
            int right = GetSize(root.RightSubtree);
            if (left < right) return left + 1;
            else return right + 1;
        }
        public static int GetNumberElements(Random_Tree root)
        {
            if (root == null) return 0;
            return GetNumberElements(root.LeftSubtree) + GetNumberElements(root.RightSubtree) + 1;
        }
        public static void NicePrint(Random_Tree root, int level)
        {
            /*Queue<Random_Tree> Q = new Queue<Random_Tree>();
            Q.Enqueue(root);
            while(Q.Count() != 0)
            {
                Random_Tree temp = Q.Dequeue();
                
                if(temp.LeftSubtree != null)
                {
                    Q.Enqueue(temp.LeftSubtree);
                }
                if(temp.RightSubtree != null)
                {
                    Q.Enqueue(temp.RightSubtree);
                }

                     
            }*/
            if(root != null)
            {
                NicePrint(root.LeftSubtree, level + 1);
                for (int i = 0; i < level; i++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine(root.data);
               
                NicePrint(root.RightSubtree, level + 1);
            }
        }
        public static void RemoveToFull(Random_Tree root)
        {
            if (root == null)
            {
                return;
            }
            if((root.LeftSubtree != null) && (root.RightSubtree != null))
            {
                RemoveToFull(root.LeftSubtree);
                RemoveToFull(root.RightSubtree);
                if(GetSize(root.LeftSubtree) != GetSize(root.RightSubtree))
                {
                    root.RightSubtree = null;
                    root.LeftSubtree = null;
                }
            }
            else
            {
                root.RightSubtree = null;
                root.LeftSubtree = null;
            }
        }

        public static void DFS(Random_Tree root, List<int> result)
        {
            if(root == null)
            {
                return;
            }
            DFS(root.LeftSubtree, result);
            result.Add(root.data);
            DFS(root.RightSubtree, result);
            
        }
    }
}
