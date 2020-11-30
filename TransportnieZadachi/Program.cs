using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TransportnieZadachi
{
    class Program
    {
        struct Element
        {
            public int Delivery { get; set; }
            public int Value { get; set; }
            public int BasePoint { get; set; }

            public static List<int[]> VerticesOfCycle = new List<int[]>();
            public static int FindMinElement(int a, int b)
            {
                if (a > b) return b;
                if (a == b) { return a; }
                else return a;
            }
            public static void OutPut(Element[,] Shipment, int[] Supplier, int[] Consumer, bool WhithBasePoint = false)
            {
                if (WhithBasePoint)
                {
                    for (int j = 0; j < Supplier.Length; j++) { Console.Write("    " + Supplier[j] + " "); }
                    Console.WriteLine("\n");
                    //выводим массив на экран
                    for (int i = 0; i < Consumer .Length; i++)
                    {
                        Console.Write($"{Consumer[i], -4}");
                        for (int j = 0; j < Supplier.Length; j++)
                        {
                         //   Console.Write(Shipment[i, j].Delivery + $"({Shipment[i, j].Value})" + " ");
                            Console.Write($"{Shipment[i, j].Delivery, 2}({Shipment[i, j].Value, -2}) ");
                        }
                        Console.WriteLine();
                    };
                    Console.WriteLine("\n Таблица базисных точек");
                    for (int i = 0; i < Consumer.Length; i++)
                    {
                        for (int j = 0; j < Supplier.Length; j++)
                            Console.Write(Shipment[i, j].BasePoint + " ");
                        Console.WriteLine();
                    }


                }
                else
                {
                    for (int i = 0; i < Supplier.Length; i++) { Console.Write("    " + Supplier[i] + " "); }
                    Console.WriteLine("\n");
                    //выводим массив на экран
                    for (int i = 0; i < Consumer.Length; i++)
                    {
                        Console.Write($"{Consumer[i],-4}");
                        for (int j = 0; j < Supplier.Length; j++)
                        {
                            Console.Write($"{Shipment[i, j].Delivery,2}({Shipment[i, j].Value,-2}) ");
                        }
                        Console.WriteLine();
                    };
                }
            }
            public static void NorthWestMethod(int[] supplier, int[] Consumer, Element[,] Shipment)
            {
                int x, y;
                int[] SupplierForAlgoritm = new int[supplier.Length];
                int[] ConsumerForAlgoritm = new int[Consumer.Length];

                Array.Copy(supplier, SupplierForAlgoritm, supplier.Length);
                Array.Copy(Consumer, ConsumerForAlgoritm, Consumer.Length);
                x = 0;
                y = 0;
                while (x < SupplierForAlgoritm.Length || y < ConsumerForAlgoritm.Length)
                {
                    if (SupplierForAlgoritm[y] == 0) { y++; }
                    if (y == SupplierForAlgoritm.Length) { break; }
                    if (ConsumerForAlgoritm[x] == 0) { x++; }
                    if (x == ConsumerForAlgoritm.Length) { break; }
                    Shipment[x, y].Delivery = Element.FindMinElement(ConsumerForAlgoritm[x], SupplierForAlgoritm[y]);
                    SupplierForAlgoritm[y] -= Shipment[x, y].Delivery;
                    ConsumerForAlgoritm[x] -= Shipment[x, y].Delivery;
                }
            }

            ///////////////////////////////////////////// ДАЛЕЕ ДЛЯ МЕТОДА ПОТЕНЦИАЛОВ ////////////////////////////////////////////////////////////
            public static bool ShipmentVerification(int[] supplier, int[] Consumer, Element[,] Shipment)
            {
                int Test;
                for (int i = 0; i < Consumer.Length; i++)
                {
                    Test = 0;
                    for (int j = 0; j < supplier.Length; j++)
                    {
                        Test += Shipment[i, j].Delivery;
                    }
                    if (Test != Consumer[i]) Console.WriteLine("Ошибка: алгоритм сработал не правильно, Отгружено больше чем можно"); return false; 
                }
                for (int j = 0; j < Consumer.Length; j++)
                {
                    Test = 0;
                    for (int i = 0; i < supplier.Length; i++)
                    {
                        Test += Shipment[j, i].Delivery;
                    }
                    if (Test != supplier[j]) Console.WriteLine("Ошибка: алгоритм сработал не правильно, Получено больше чем можно"); return false;
                }
                return true;
            }
            public static void FindNewBasePoint(int[] supplier, int[] Consumer, Element[,] Shipment)
            {
                int i, j;
                bool ok = false;
                bool IsTrue = true;
                for (i = 0; i < Consumer.Length; i++)
                {
                    for (j = 0; j < supplier.Length; j++)
                        if (Shipment[i, j].BasePoint == 0)
                        {
                            ok = true;
                            break;
                        }
                    if (ok) { break; }
                }
                if (!ok)
                {
                    Console.WriteLine("ОШИБКА! НУЖНЫХ ТОЧЕК НЕТУ");
                }


                
                while (IsTrue)
                {
                    Random randForI = new Random();
                    Random randForJ = new Random();
                    i = randForI.Next(0, Consumer.Length);
                    j = randForJ.Next(0, supplier.Length);
                    if (Shipment[i, j].BasePoint == 1)
                        continue;
                    if (Shipment[i, j].Delivery != 0)
                        Console.WriteLine("ОЧень странно, базовая точка не нулевой быть не может");
                    //if (FindCycle(i, j, Shipment))
                    //    continue;
                    Shipment[i, j].BasePoint = 1;
                    Console.WriteLine("В базис введена ячейка: [" + i + ", " + j + "].");
                    IsTrue = false;
                }  
            }

            public static bool FindCycle(int i0, int j0, Element[,] Shipment)
            {
                int count;
                count = (InitValueForSupplier.Length)*(InitValueForConsumer.Length);
                VerticesOfCycle.Clear();
                if (FindCycleHorizontally(i0, j0, count, Shipment))
                    return true;
                return false;
            }
            public static bool FindCycleHorizontally(int i0, int j0, int Count, Element[,] Shipment)             //i0, j0 координаты начала цикла
            {
                Count --;
                if (Count == 0) {Console.WriteLine("Зациклились"); return false;  }
                for ( int j = 0; j < InitValueForSupplier.Length; j++)
                {
                    if (j == j0)
                        continue;
                    if (Shipment[i0, j].BasePoint == 0)
                        continue;
                    if (FindCycleVertically(i0, j, Count, Shipment))
                    {
                        VerticesOfCycle.Add(new int[] {i0, j });
                        return true;
                    }
                }
                return false;
            }
            public static bool FindCycleVertically (int i0, int j0, int Count, Element[,] Shipment)
            {
                for (int i = 0; i < InitValueForConsumer.Length; i++)
                {
                    if (j0 == StartCycleSup & i == StartCycleCon)
                    {
                        VerticesOfCycle.Add(new int[]{i, j0});
                        return true;
                    }
                    if (i == i0)
                        continue;
                    if (Shipment[i, j0].BasePoint == 0)
                        continue;
                    if (FindCycleHorizontally(i, j0, Count, Shipment))
                    {
                        VerticesOfCycle.Add(new int[] { i, j0 });
                        return true;
                    }
                }
                return false;
            }
            
            /////////////////////////////////////////////// 
            
            public static bool FindPotantial(Element[,] Shipment)
            {
                for (int i = 0; i < InitValueForConsumer.Length; i++)
                    u[i] = null;
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                    v[j] = null;
                u[0] = 0;
                int Count = InitValueForSupplier.Length * InitValueForConsumer.Length;
                FindPotantialHorizontally(0, Count, Shipment);

                for (int i = 0; i < InitValueForConsumer.Length; i++)
                    if (u[i] == null) { Console.WriteLine("Не удалось вычислить потенциал, отсутствует значение v[" + i + "]."); return false; }
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                    if (v[j] == null) { Console.WriteLine("Не удалось вычислить потенциал, отсутствует значение u[" + j + "]."); return false; }
                return true;
            }
            public static void FindPotantialVerticalle(int j, int count, Element[,] Shipment)
            {
                if (v[j] == null) { Console.WriteLine("Ошибка получения потенциалов"); return; }
                for (int i = 0; i < InitValueForConsumer.Length; i++)
                {
                    if (Shipment[i, j].BasePoint == 0)
                        continue;
                    if (u[i] != null)
                        continue;
                    else
                    {
                        u[i] = Shipment[i, j].Value - v[j];
                        FindPotantialHorizontally(i, count, Shipment);
                    }
                }
            }
            public static void FindPotantialHorizontally(int i, int count, Element[,] Shipment)
            {
                count--;
                if (count == 0) { Console.WriteLine("Зациклились на потенциалах"); return; }
                if (u[i] == null) { Console.WriteLine("Ошибка получения потенциалов"); return; }
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                {
                    if (Shipment[i, j].BasePoint == 0)
                        continue;
                    if (v[j] != null)
                        continue;
                    else
                    {
                        v[j] = Shipment[i, j].Value - u[i];
                        FindPotantialVerticalle(j, count, Shipment);
                    }
                }
            }
            
            public static bool OptimalityCheck (Element[,] Shipment)
            {
                bool isOptimal = true;
                int? minDelta = null,
                    Delta;
                for (int i = 0; i < InitValueForConsumer.Length; i++ )
                {
                    string Message = "";
                    Message += "Дельта = ";
                    for (int j = 0; j < InitValueForSupplier.Length; j++)
                    {
                        if (Shipment[i, j].BasePoint == 1)
                            Delta = 0;
                        else
                            Delta = Shipment[i, j].Value - u[i] - v[j];
                        Message += Delta + " ;";
                        if (Delta < 0)
                            isOptimal = false;
                        if (minDelta == null)
                        {
                            minDelta = Delta;
                            StartCycleCon = i;
                            StartCycleSup = j;
                        }
                        else if (Delta < minDelta)
                        {
                            minDelta = Delta;
                            StartCycleCon = i;
                            StartCycleSup = j;
                        }
                    }
                    Console.WriteLine(Message);
                }
                return isOptimal;
            }
            public static bool Redistribution (Element[,] Shipment, bool WithControlCost = false)
            {
                int value;
                int i, j;
                CostOfRegruz = 0;
                if (!WithControlCost)
                    Console.WriteLine("Перераспределение по циклу  " + VerticesOfCycle.Count);
                if (VerticesOfCycle.Count < 4) { Console.WriteLine("Цикл имеет меньше 4 вершин"); return false; }
                int? Teta = null; ;
                string Sign = "+";
                if (!WithControlCost)
                {
                    for (int count = 0; count < VerticesOfCycle.Count; count++)
                    {
                        i = VerticesOfCycle[count][0];
                        j = VerticesOfCycle[count][1];
                        if (Sign == "-")
                        {
                            value = Shipment[i, j].Delivery;
                            if (Teta == null)
                                Teta = value;
                            else if (value < Teta)
                                Teta = value;
                            Sign = "+";
                        }
                        else
                            Sign = "-";
                    }
                    if (Teta == null) { Console.WriteLine("Не удалось вычислить переменную тета."); }
                    Console.WriteLine("Тета = " + Teta);
                    if (Teta == 0) { return false; }
                }
                Sign = "+";

                for (int count = 0; count < VerticesOfCycle.Count; count++)
                {
                    i = VerticesOfCycle[count][0];
                    j = VerticesOfCycle[count][1];
                    if (Sign == "-")
                    {
                        if (WithControlCost)
                            CostOfRegruz -= Shipment[i, j].Value;
                        else
                            Shipment[i, j].Delivery = Shipment[i, j].Delivery - int.Parse(Teta.ToString());
                        Sign = "+";
                    }
                    else if (Sign == "+")
                    {
                        if (WithControlCost)
                            CostOfRegruz += Shipment[i, j].Value;
                        else
                            Shipment[i, j].Delivery = Shipment[i, j].Delivery + int.Parse(Teta.ToString());
                        Sign = "-";
                    }
                }
                return true;
            }
        }


        //public static int[,] InPutShipment = new int[,] { { 2, 5}, { 3, 2} };
        //public static int[] InitValueForSupplier = { 5, 6 };
        //public static int[] InitValueForConsumer = { 10, 8 };


        public static int[,] InPutShipment = new int[,] { { 2, 5, 6}, { 3, 2, 3} };
        public static int[] InitValueForSupplier = { 5, 6, 8 };
        public static int[] InitValueForConsumer = { 10, 8 };

        //public static int[,] InPutShipment = new int[,] { {3, 5, 7 , 2}, { 2, 4, 7, 5}, {6, 1, 2, 8} };
        //public static int[] InitValueForSupplier = { 45, 36, 25, 33};
        //public static int[] InitValueForConsumer = { 39, 28, 72 };
        public static int?[] v = new int?[InitValueForSupplier.Length];
        public static int?[] u = new int?[InitValueForConsumer.Length];
        public static int StartCycleSup, StartCycleCon, CostOfRegruz;

        public static int[] ReadAndParseLine(string row)                                                             //Функция для чтения из файла, возмонжно пригодится 
        {
            int i = 0;
            int[] ReturnValue = new int[row.Split(' ').Length];
            foreach (string element in row.Split(' '))
            {
                ReturnValue[i] = Int32.Parse(element);
                ++i;
            }
            return ReturnValue;
        }

        public static void Method(int[,] Shipment, int[] InitValueForCon, int[] InitValueForSup)
        {
            {
                //Вводим поставщиков 
                int CountOfCon = InitValueForCon.Length;                                                                          //Количество поставщиков 
                if (CountOfCon > Shipment.GetUpperBound(0) + 1)                                                                         //проеверка на соответствие
                {
                    Console.WriteLine("Колчиство поставщиков не соответствует матрице");
                    return;
                }
                int[] Con = new int[CountOfCon];                                                                                    //Значение для Алгоритма

                //Вводим Потребителей
                int CountOfSup = InitValueForSup.Length;
                if (CountOfCon > Shipment.GetUpperBound(1) + 1)                                                                         //проеверка на соответствие
                {
                    Console.WriteLine("Колчиство потребителей не соответствует матрице");
                    return;
                }
                int[] Sup = new int[CountOfSup];                                                                                      //Значение для алгоритма                                                                                                            //Минимальное значение стоимости доставки
                int CostOfDeliv;

                Element[,] ArrayForPermit;                                                                                  //Массив который подлежит тасовке
                Element[,] minValueArr = new Element[CountOfCon, CountOfSup];                                     //Массив с наименьшим значением доставки

                int[] NumberForPermut = new int[] { 0, 1, 2, 3 };                                      //Числа для тасовки индексов
                formPermut test = new formPermut(NumberForPermut, 0, CountOfCon - 1);          //Объект, который создаст необходимые комбинации 
                test.prnPermut();                                                                   //Тасовка индексов                                   //Записываем значения
                List<int[]> Combination = test.getCombination();
                //List<int[]> Combination = new List<int[]>() { new int[] { 0, 1, 2 } };

                formPermut test2 = new formPermut(NumberForPermut, 0, CountOfSup - 1);          //Объект, который создаст необходимые комбинации 
                test2.prnPermut();                                                                   //Тасовка индексов
                List<int[]> CombinationSup = test2.getCombination();

                //Нам необходимо тасовать не только столбики, но и строчки, по этому два цикла
                foreach (int[] IndexForCon in Combination)         // Тасовка столбиков       
                {
                    foreach (int[] IndexForSup in CombinationSup)                   //Тасовка строчек
                    {
                        ArrayForPermit = new Element[CountOfCon, CountOfSup];
                        for (int i = 0; i < CountOfCon; i++)
                        {
                            Con[i] = InitValueForCon[IndexForCon[i]];
                        }

                        for (int i = 0; i < CountOfCon; i++)
                        {
                            for (int j = 0; j < CountOfSup; j++)
                            {
                                ArrayForPermit[i, j].Value = Shipment[IndexForCon[i], IndexForSup[j]];
                                Sup[j] = InitValueForSup[IndexForSup[j]]; //Построение тасовонного трафика
                            }                                                         
                        }

                        Element.NorthWestMethod(Sup, Con, ArrayForPermit);
                        Element.OutPut(ArrayForPermit, Sup, Con);
                        

                        CostOfDeliv = 0;
                        //считаем целевую функцию
                        for (int i = 0; i < Con.Length; i++)
                        {
                            for (int j = 0; j < Sup.Length; j++) { CostOfDeliv += (ArrayForPermit[i, j].Value * ArrayForPermit[i, j].Delivery); }
                        }
                        Console.WriteLine(" Result = {0}", CostOfDeliv);
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Количество базисных планов " + Combination.Count * CombinationSup.Count);
                Console.Read();
            }
        }
        public static void MethodOfPotential()
        {
            int CountOfBasePoint;

            //Заполняем структуру полученными значениями
            Element[,] Shipment = new Element[InitValueForConsumer.Length, InitValueForSupplier.Length];
            for (int i = 0; i < InitValueForConsumer.Length; i++)                                                            
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                    Shipment[i, j].Value = InPutShipment[i, j];

            // Расбрасываем предварительный план перевозок методом С.З. угла
            Element.NorthWestMethod(InitValueForSupplier, InitValueForConsumer, Shipment);
            while (true)
            {
                // Проверяем нет ли перебора в перевозках
                Element.ShipmentVerification(InitValueForSupplier, InitValueForConsumer, Shipment);

                // Помечаем базовые точки и подсчет их количество         
                CountOfBasePoint = 0;
                for (int i = 0; i < InitValueForConsumer.Length; i++)
                    for (int j = 0; j < InitValueForSupplier.Length; j++)
                    {
                        if (Shipment[i, j].Delivery > 0)
                        {
                            Shipment[i, j].BasePoint = 1;
                            CountOfBasePoint++;
                        }
                        else if (Shipment[i, j].Delivery < 0)
                        {
                            Console.WriteLine("Базовая точка не может быть отрицательной");
                            break;
                        }
                        else Shipment[i, j].BasePoint = 0;
                    }

                // Вывод промежуточного результата
                Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer, true);

                // Если задача является вырожденной, необходимо добавить точку для ввода в базис и построение цикла
                while (CountOfBasePoint < (InitValueForSupplier.Length + InitValueForConsumer.Length - 1))
                {
                    Console.WriteLine("Задача является вырожденной так как (M+N)-1 = "+ (InitValueForSupplier.Length + InitValueForConsumer.Length - 1) + " а количество базовых точек = " + CountOfBasePoint);
                    Element.FindNewBasePoint(InitValueForSupplier, InitValueForConsumer, Shipment);
                    CountOfBasePoint += 1;
                }

                if (!Element.FindPotantial(Shipment)) { continue; }
                if (Element.OptimalityCheck(Shipment)) { Console.WriteLine("Решение оптимально"); break; }
                Console.WriteLine("Решение не оптимально");

                //Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer);

                if (!Element.FindCycle(StartCycleCon, StartCycleSup, Shipment)) { Console.WriteLine("Не удалось найти цикл"); return; }
                Element.Redistribution(Shipment);
            }

            int CostOfDeliv = 0;
            for (int i = 0; i < InitValueForConsumer.Length; i++)
            {
                for (int j = 0; j < InitValueForSupplier.Length; j++) { CostOfDeliv += (Shipment[i, j].Value * Shipment[i, j].Delivery); }
            }

            Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer);

            Console.WriteLine("Стоимость перевозки = " + CostOfDeliv);
        }
        public static void RasprMethod()
        {
            int CountOfBasePoint;
            
            //Заполняем структуру полученными значениями
            Element[,] Shipment = new Element[InitValueForConsumer.Length, InitValueForSupplier.Length];
            for (int i = 0; i < InitValueForConsumer.Length; i++)
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                    Shipment[i, j].Value = InPutShipment[i, j];

            // Расбрасываем предварительный план перевозок методом С.З. угла
            Element.NorthWestMethod(InitValueForSupplier, InitValueForConsumer, Shipment);

            // Проверяем нет ли перебора в перевозках
            Element.ShipmentVerification(InitValueForSupplier, InitValueForConsumer, Shipment);

            // Помечаем базовые точки и подсчет их количество         
            CountOfBasePoint = 0;
            for (int i = 0; i < InitValueForConsumer.Length; i++)
                for (int j = 0; j < InitValueForSupplier.Length; j++)
                {
                    if (Shipment[i, j].Delivery > 0)
                    {
                        Shipment[i, j].BasePoint = 1;
                        CountOfBasePoint++;
                    }
                    else if (Shipment[i, j].Delivery < 0)
                    {
                        Console.WriteLine("Базовая точка не может быть отрицательной");
                        break;
                    }
                    else Shipment[i, j].BasePoint = 0;
                }

            // Вывод промежуточного результата
            Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer, true);

            // Если задача является вырожденной, необходимо добавить точку для ввода в базис и построение цикла
            while (CountOfBasePoint < (InitValueForSupplier.Length + InitValueForConsumer.Length - 1))
            {
                Console.WriteLine("Решение вырождено, нужно порешать");
                Element.FindNewBasePoint(InitValueForSupplier, InitValueForConsumer, Shipment);
                CountOfBasePoint += 1;
            }
            while (true)
            { 
                bool Goal = false;
                for (int i = 0; i < InitValueForConsumer.Length; i++)
                {
                    for (int j = 0; j < InitValueForSupplier.Length; j++)
                    {
                        if (Shipment[i, j].BasePoint == 0)
                        {
                            StartCycleCon = i;
                            StartCycleSup = j;
                            if (Element.FindCycle(StartCycleCon, StartCycleSup, Shipment))
                            {
                                Element.Redistribution(Shipment, true);
                                if (CostOfRegruz < 0)
                                {
                                    Element.Redistribution(Shipment);
                                    CountOfBasePoint = 0;
                                    for (int ii = 0; ii < InitValueForConsumer.Length; ii++)
                                        for (int jj = 0; jj < InitValueForSupplier.Length; jj++)
                                        {
                                            Shipment[ii, jj].BasePoint = 0;
                                            if (Shipment[ii, jj].Delivery > 0)
                                            {
                                                Shipment[ii, jj].BasePoint = 1;
                                                CountOfBasePoint++;
                                            }
                                        }
                                    while (CountOfBasePoint < (InitValueForSupplier.Length + InitValueForConsumer.Length - 1))
                                    {
                                        Console.WriteLine("Решение вырождено, нужно порешать");
                                        Element.FindNewBasePoint(InitValueForSupplier, InitValueForConsumer, Shipment);
                                        CountOfBasePoint += 1;
                                    }
                                    Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer, true);
                                    Goal = true;
                                    break;
                                }
                            }
                            else
                                Console.WriteLine("Цикл не найден");
                        }
                    }
                    if (Goal)
                    {
                        Console.WriteLine("Перераспределение завершено, \nпереходим к следующей иттерации");
                        break;
                    }
                }
                if (!Goal)
                {
                    Console.WriteLine("РЕШЕНИЕ ОПТИМАЛЬНО");
                    break;
                }
            }

            int CostOfDeliv = 0;
            for (int i = 0; i < InitValueForConsumer.Length; i++)
            {
                for (int j = 0; j < InitValueForSupplier.Length; j++) { CostOfDeliv += (Shipment[i, j].Value * Shipment[i, j].Delivery); }
            }

            Element.OutPut(Shipment, InitValueForSupplier, InitValueForConsumer);

            Console.WriteLine("Стоимость перевозки = " + CostOfDeliv);
            return;
        }

        static void Main(string[] args)
        {
            int Chose;
            while (true)
            {
                Console.WriteLine("Выбор необходимой функции:" + "\n" + "1 - Подсчет количеств планов" + "\n" + "2 - Решение методом потенциалов " + "\n" + "3 - Решение распределительным методом");
                Chose = int.Parse(Console.ReadLine());
                if (Chose == 1)
                    Method(InPutShipment, InitValueForConsumer, InitValueForSupplier); Console.WriteLine("---------------------------------------------------------------------------------------------");
                if (Chose == 2)
                    MethodOfPotential(); Console.WriteLine("---------------------------------------------------------------------------------------------");
                if (Chose == 3)
                    RasprMethod(); Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
        }

    }
}

