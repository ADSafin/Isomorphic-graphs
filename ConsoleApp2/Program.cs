using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class MatrixData{
        public int[,]? matrix1 {get; set;}
        public int[,]? matrix2 {get; set;}
        public bool? boom {get; set;}

    }
    class Solution_Isomorphism{
        // 1 Ищем кол-во вершин
        public int[] Search_Vertex(int[,] matrix, int[] solution){
            int N = matrix.GetLength(0);
            solution[0] = N;
            return solution;
        }
        // 2 Кол-во ребер
        public int[] Search_Edge(int[,] matrix, int[] solution){
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = matrix.GetLength(1) - 1; j > i; j--) {
                    if (matrix[i, j] > 0) {
                        count++;
                    }
                }
            }
            solution[1] = count;
            return solution;
        }
        // 3 Нахождение K компонента связности
        public int[] Search_K(int[,] matrix, int[] solution){
            int count = 0; // проверка ребер
            for (int i = 0; i < matrix.GetLength(0); i++){
                count = 0;
                for(int j = 0; j < matrix.GetLength(1); j++){
                    if(matrix[i, j] > 0) count++;
                }
                if (count == 0){
                    solution[2] = count;
                    return solution;
                }
            }
            solution[2] = 1;
            return solution;
        }
        // 4 Последовательность степеней вершин P
        public int[] Search_P(int[,] matrix){
            int N = matrix.GetLength(0);
            int[] G = new int[N];
            int count = 0;
            for (int i = 0; i < N; i++){
                count = 0;
                for(int j = 0; j < N; j++){
                    if(matrix[i, j] != 0){count++;}
                }
                G[i] = count;
            }
            Array.Sort(G);
            Array.Reverse(G);
            return G;
        }
        // 5 Цикломатическое число графа pG
        public int[] Search_pG(int[,] matrix, int[] solution){
            int pG = solution[1] + solution[2] - solution[0];
            solution[4] = pG;
            return solution;
        }
        //  диаметр графа
        public int[] Search_Diam(int[,] matrix_dist, int[] solution){
            int maxPrice = matrix_dist[0,0];
            for (int i = 0; i < matrix_dist.GetLength(0); i++){
                for(int j = 0; j < matrix_dist.GetLength(1); j++){
                    if((matrix_dist[i, j] > 0) && (matrix_dist[i, j] > maxPrice)){
                        maxPrice = matrix_dist[i,j];
                    }
                }                
            }
            solution[6] = maxPrice;
            return solution;
        }
        // 7 Индекс Рандича
        public double Search_Randich(int[,] matrix){
            int count = 0;
            int[] counts = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++) {
                count = 0;
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    if (matrix[i, j] > 0) {
                        count++;
                    }
                }
                counts[i] = count;
            }
            double summa = 0;
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = matrix.GetLength(1) - 1; j > i; j--) {
                    if (matrix[i, j] > 0) {
                        summa += 1/Math.Sqrt(counts[i] * counts[j]);
                    }
                }
            }
                        
            return summa;
        }
        // 8 Индекс Винера
        public int[] Search_Vinear(int[,] matrix_dist, int[] solution){
            int Price = 0;
            for (int i = 0; i < matrix_dist.GetLength(0); i++) {
                for (int j = matrix_dist.GetLength(1) - 1; j > i; j--) {
                    if (matrix_dist[i, j] > 0) {
                        Price += matrix_dist[i, j];
                    }
                }
            }
            solution[8] = Price;
            return solution;
        }
    }
    class InputOutputData
    {
        public static void Write_Graph(MatrixData mtx){
            try{
                int N = mtx.matrix1.GetLength(0);
                string fileMatrix = "Matrix 1: \n";
                for(int i = 0; i < N; i++){
                    for(int j = 0; j < N; j++){
                        if(j == N-1){
                            fileMatrix += ($"{mtx.matrix1[i, j]}");
                            fileMatrix += "\n";
                            break;
                        }
                        fileMatrix += ($"{mtx.matrix1[i, j]} ");
                    }
                }
                fileMatrix += "\nMatrix 2: \n";
                for(int i = 0; i < N; i++){
                    for(int j = 0; j < N; j++){
                        if(j == N-1){
                            fileMatrix += ($"{mtx.matrix2[i, j]}");
                            fileMatrix += "\n";
                            break;
                        }
                        fileMatrix += ($"{mtx.matrix2[i, j]} ");
                    }
                }
                fileMatrix += "\n";
                fileMatrix += $"Изоморфизм графов - {mtx.boom}";
                
                File.WriteAllText(@"C:\Users\Айнур\Desktop\ConsoleApp2\addon\output_data.txt", fileMatrix);
                Console.WriteLine("Данные записаны");
            }catch(Exception ex){
                Console.WriteLine("Случилась не предвиденная ошибка, при записи в файл! :( \n{0}",ex);
            }
        }
        public static int[,] Read_Graph(string Path)
        {
            try{
                string[] lines = File.ReadAllLines(Path);
                string[][] el = new string[lines.Length][];
                for (int i = 0; i < lines.Length; i++)
                {
                    el[i] = lines[i].Split(' ');
                }
                int[,] graph = new int[lines.Length, el[0].Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int k = 0; k < el[0].Length; k++)
                    {
                        graph[i, k] = int.Parse(el[i][k]);
                    }

                }
                return graph;
            }catch(Exception ex){
                Console.WriteLine("Ошибка при считывании файла! {0}", ex);
            }
            int[,] graphh = new int[0,0];
            return graphh;
            
        }
    }
    
    class Program
    {
        public static void Print_List(int[] list){
            for(int i = 0; i < list.Length; i++){
                Console.Write("{0} ", list[i]);
            }
        }
        public static void Print_Graph(int[,] graph)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    Console.Write("{0,3}", graph[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static bool Comparison_Lst(int[] lst, int[] lst2){
            bool comp = false;
            for(int i = 0; i < lst.Length; i++){
                if(lst[i] == lst2[i]){
                    comp = true;
                }else{
                    return false;
                }
            }
            return comp;
        }
        
        public static void Main(string[] args)
        {
            Solution_Isomorphism finesol = new Solution_Isomorphism();

            // инциализация матриц
            int[,] matrix1 = InputOutputData.Read_Graph(@"C:\Users\Айнур\Desktop\ConsoleApp2\addon\matrix1.txt"); 
            int[,] matrix2 = InputOutputData.Read_Graph(@"C:\Users\Айнур\Desktop\ConsoleApp2\addon\matrix2.txt");

            int[,] matrix1_dist = InputOutputData.Read_Graph(@"C:\Users\Айнур\Desktop\ConsoleApp2\addon\input1.txt"); 
            int[,] matrix2_dist = InputOutputData.Read_Graph(@"C:\Users\Айнур\Desktop\ConsoleApp2\addon\input2.txt");
            
            // вывод матриц
            Console.WriteLine("\n1 граф\n");
            Print_Graph(matrix1);
            Console.WriteLine("\n2 граф\n");
            Print_Graph(matrix2);
            Console.WriteLine("\n1 граф расстояний\n");
            Print_Graph(matrix1_dist);
            Console.WriteLine("\n2 граф расстояний\n");
            Print_Graph(matrix2_dist);

            int[] solution1 = new int[9] {0,0,0,0,0,0,0,0,0};
            int[] solution2 = new int[9] {0,0,0,0,0,0,0,0,0};
            int[] solution_P1;
            int[] solution_P2;
            double sol_Ran1;
            double sol_Ran2;

            // 1
            solution1 = finesol.Search_Vertex(matrix1, solution1);
            solution2 = finesol.Search_Vertex(matrix2, solution2);
            // 2
            solution1 = finesol.Search_Edge(matrix1, solution1);
            solution2 = finesol.Search_Edge(matrix2, solution2);
            // 3
            solution1 = finesol.Search_K(matrix1, solution1);
            solution2 = finesol.Search_K(matrix2, solution2);
            // 4
            solution_P1 = finesol.Search_P(matrix1);
            solution_P2 = finesol.Search_P(matrix2);
            // 5
            solution1 = finesol.Search_pG(matrix1, solution1);
            solution2 = finesol.Search_pG(matrix2, solution2);
            // 7
            solution1 = finesol.Search_Diam(matrix1_dist, solution1);
            solution2 = finesol.Search_Diam(matrix2_dist, solution2);
            // 8
            sol_Ran1 = finesol.Search_Randich(matrix1);
            sol_Ran2 = finesol.Search_Randich(matrix2);
            // 9
            solution1 = finesol.Search_Vinear(matrix1_dist, solution1);
            solution2 = finesol.Search_Vinear(matrix2_dist, solution2);

            // Вывод решение
            Console.WriteLine("\nРешение вашей матрицы 1");
            Console.WriteLine("Функции 1 2 3 (4 - список) 5 (6 - не считаем) 7 (8 - индекс Рандича) 9\n");
            Print_List(solution1);
            Console.WriteLine("4 - список");
            Print_List(solution_P1);
            Console.WriteLine("\nИндекс Рандича - {0}", sol_Ran1);

            Console.WriteLine("\nРешение вашей матрицы 2");
            Console.WriteLine("Функции 1 2 3 (4 - список) 5 (6 - не считаем) 7 (8 - индекс Рандича) 9\n");
            Print_List(solution2);
            Console.WriteLine("4 - список");
            Print_List(solution_P2);
            Console.WriteLine("\nИндекс Рандича - {0}", sol_Ran2);

            Console.WriteLine("Изоморфизм графов:");
            Console.WriteLine("Сравнение функций - {0}", Comparison_Lst(solution1, solution2));
            Console.WriteLine("Сравнение индекса Рандича - {0}", (Math.Round(sol_Ran1, 4) == Math.Round(sol_Ran2, 4)));
            Console.WriteLine("Сравнение последовательности степеней - {0}", Comparison_Lst(solution_P1, solution_P2));
            bool boom = Comparison_Lst(solution1, solution2) && (Math.Round(sol_Ran1, 4) == Math.Round(sol_Ran2, 4)) && Comparison_Lst(solution_P1, solution_P2);
            MatrixData mtx = new MatrixData(){matrix1 = matrix1, matrix2 = matrix2, boom = boom};
            InputOutputData.Write_Graph(mtx);
        }
    }
}
