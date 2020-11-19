using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;

namespace GraphDrawer
{
    class Graph
    {
        private static int rad = 150;
        private static int centre = 200;
        private Matrix<double> matrix;
        private int vertexcount;
        private List<Point> VertexCoordinates;

        public Graph() { }
        public Graph(int dimention)
        {
            vertexcount = dimention;
            VertexCoordinates = new List<Point>();
            matrix = CreateMatrix.Dense(dimention, dimention, 0.0);
            for(int i = 0; i < dimention; i++)
            {
                Point point = new Point();
                point.X = Convert.ToInt32( rad * Math.Cos(Math.PI * 2 * i / dimention));
                point.Y = Convert.ToInt32( rad * Math.Sin(Math.PI * 2 * i / dimention));
                VertexCoordinates.Add(point);
            }
        }
        public Graph(int dimention, double[,] storage)
        {
            vertexcount = dimention;
            matrix = CreateMatrix.DenseOfArray(storage);
            for (int i = 0; i < dimention; i++)
            {
                Point point = new Point();
                point.X = Convert.ToInt32(rad * Math.Cos(Math.PI * 2 * i / dimention));
                point.Y = Convert.ToInt32(rad * Math.Sin(Math.PI * 2 * i / dimention));
                VertexCoordinates.Add(point);
            }
        }
        public Graph(int dimention, List<Point> coordinates)
        {
            vertexcount = dimention;
            matrix = CreateMatrix.Dense(dimention, dimention, 0.0);
            VertexCoordinates = coordinates;
        }
        public Graph(int dimention, double[,] storage, List<Point> coordinates)
        {
            vertexcount = dimention;
            matrix = CreateMatrix.DenseOfArray(storage);
            VertexCoordinates = coordinates;
        }

        private void ReCalcCoordinates()
        {
            VertexCoordinates.Clear();
            for (int i = 0; i < vertexcount; i++)
            {
                Point point = new Point();
                point.X = Convert.ToInt32(rad * Math.Cos(Math.PI * 2 * i / vertexcount));
                point.Y = Convert.ToInt32(rad * Math.Sin(Math.PI * 2 * i / vertexcount));
                VertexCoordinates.Add(point);
            }
        }
        public void AddVertex(int x1, int x2, int mass = 1)
        {
            matrix[x1, x2] = mass;
            matrix[x2, x1] = mass;
            ReCalcCoordinates();
        }
        public void AddVertexOrientied(int x1, int x2, int mass = 1)
        {
            matrix[x1, x2] = mass;
            ReCalcCoordinates();
        }

        public double FindWayDeikstra(int v1, int v2, List<int> moveway)
        {
            List<int> visited = new List<int>();
            
            List<double> minway = new List<double>();
            
            int current = v1;
            

            for(int i = 0; i < vertexcount; i++)
            {
                if(i == v1)
                {
                    minway.Add(0);
                    continue;
                }
                minway.Add(1000000);
            }
            List<int> previous = new List<int>();
            for(int i = 0; i < vertexcount; i++)
            {
                previous.Add(-1);
            }
            previous[v1] = v1;
            while (visited.Count < vertexcount)
            {
                visited.Add(current);
                for (int i = 0; i < vertexcount; i++)
                {
                    if (!visited.Contains(i) && matrix[i, current] > 0)
                    {
                        if(minway[i] > minway[current] + matrix[current, i])
                        {
                            minway[i] = minway[current] + matrix[current, i];
                            previous[i] = current;
                        }
                        /*current = i;
                        for(int k = 0; k < vertexcount; k++)
                        {
                            if(matrix[i, k] > 0 && !visited.Contains(k))
                            {
                                if (minway[i] > minway[k] + matrix[i, k])
                                {
                                    minway[i] = minway[k] + matrix[i, k];
                                    previous[i] = k;
                                }
                            }
                            
                        }*/
                        
                    }
                }
                
                double localmin = 1000000;
                for(int i = 0; i < vertexcount; i++)
                {
                    if(localmin > minway[i] && !visited.Contains(i))
                    {
                        current = i;
                        localmin = minway[i];
                    }
                }
            }
            double length = 0;
            moveway.Clear();
            current = v2;
            for(int i = 0; i < vertexcount; i++)
            {
                int prev = previous[current];
                length += matrix[prev, current];
                moveway.Add(current);
                current = prev;
                if(current == v1)
                {
                    moveway.Add(v1);
                    moveway.Reverse();
                    break;
                }
            }
            return length;
        }
    }
}
