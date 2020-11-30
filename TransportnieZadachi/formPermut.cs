using System.Collections.Generic;


namespace TransportnieZadachi
{
    class formPermut
    {
        private List<int[]> combination = new List<int[]>();
        private int[] list;
        private int k;
        private int m;
        private int[] ArrayForCombination;
        public formPermut(int[] list, int k, int m)
        {
            this.list = list;
            this.k = k;
            this.m = m;
        }
        private string Numbers;
        public void swapTwoNumber(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        private void prnPermutThis(int[] list, int k, int m)
        {
            int i;
            if (k == m)
            {
                this.ArrayForCombination = new int[m + 1];
                for (i = 0; i <= m; i++) 
                {
                    Numbers += list[i];
                    ArrayForCombination[i] = list[i];
                }
                combination.Add(ArrayForCombination);
                Numbers += " ";
            }
            else
                for (i = k; i <= m; i++)
                {
                    swapTwoNumber(ref list[k], ref list[i]);
                    prnPermutThis(list, k + 1, m);
                    swapTwoNumber(ref list[k], ref list[i]);
                }
        }
        public void prnPermut()
        {
            prnPermutThis(list, k, m);
        }
        public List<int[]> getCombination()
        {
            return combination;
        }
    }
}
