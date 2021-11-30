using System;
using System.Diagnostics;

class program
{
    public static Int32 num_m = 0;

    public static Int32 num_t = 0;
    public static Int32 num_cores = Environment.ProcessorCount;

    public static Int32 num_n = 0;

    static void Main()
    { 
        int num_lines = System.IO.File.ReadLines("names.txt").Count();

        String[] name = new String [num_lines];

        int i = 0;

         foreach (string lines in System.IO.File.ReadAllLines("names.txt"))
         {
                if (lines != "")
                {
                num_n ++;
                name[i] = lines;
                i++;                                   
                }
         }
         String[] res = new String[num_lines];

       
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();  
        res = mergeSort(name);
        stopWatch.Stop();

        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);


        int newlen = res.Length;

        for (int m = 0; m< newlen ; m++)
        {
            Console.WriteLine(res[m]);
        }

                Console.WriteLine("Num Names: " + num_n);
                Console.WriteLine("Num mergesorts: " + num_m);
                Console.WriteLine("Num Threads: " + num_t);
                Console.WriteLine("Num Cores: " + num_cores);
                Console.WriteLine("RunTime " + elapsedTime);

    }


    public static String[] mergeSort(String[] name)
    {

        num_m ++;

        int len = name.Length;

        String[] L ;
        String[] R ;
        String[] res = new String[len];
    
        if (len <=1)
        {
            return name;
        }
        int m = len/2;

        L = new String[m];

        if (len %2 == 0)
        {
            R = new String[m];
        }
        else
        {
            R = new String[m+1];
        }


        for (int i = 0; i < m; i++)
        {
            L[i] = name[i];
        }

        int j = 0;


        for (int i = m; i< len; i++)
        {
            R[j] = name[i];
            j++;
        }

        if (num_t < num_cores )
        {
            num_t ++;

        Thread thread_l = new Thread(() => L = mergeSort(L));
        thread_l.Start();
        thread_l.Join();


        Thread thread_r = new Thread(() => R = mergeSort(R));
        thread_r.Start();
        thread_r.Join();

                    
        }
        else{
            L = mergeSort(L);
            R = mergeSort(R);
        }

        res = merge(L,R);

        return res;
    }


    public static String[] merge(String[] L, String[] R)
    {
        String[] res = new String[L.Length + R.Length];
        int li = 0;
        int ri = 0;
        int resi = 0;

        while (li < L.Length || ri < R.Length)
        {

            if ( li < L.Length && ri < R.Length )
            {
                if (StrComp(L[li], R[ri]) == 1)
                
                {
                    res[resi] = L[li];

                    li++;
                    resi++;
                }
                else
                {
                    res[resi] = R[ri];
                    ri++;
                    resi++;
                }
                
            }

            else if (li < L.Length)
            {
                res[resi] = L[li];
                li++;
                resi++;
            }

            else if (ri < R.Length)
            {
                res[resi] = R[ri];
                ri++;
                resi++;
            }

        }
        return res;
    }

    public static int StrComp(String A, String B)
    {    
        string[] A_split = A.Split(' ');
        string[] B_split = B.Split(' ');

        String f_name_A = A_split[0];
        String l_name_A = A_split[1];
        String f_name_B= B_split[0];
        String l_name_B = B_split[1];

        int res = 0;

        int i = 0;
        int j = 0;

        while ( i< l_name_A.Length && j < l_name_B.Length)
        {
           int k = 0;
            int l = 0;

            if (l_name_A.Equals(l_name_B) == true)
            {
        while ( k< f_name_A.Length && l < f_name_B.Length)
        {
            if (f_name_A[k] == f_name_B[l])
            {
                k++;
                l++;
            }

        else if (f_name_A[k] < f_name_B[l])
                {
                    res = 1;
                        break;
                }
        else{
            res =0;
            break;
        }

        }

            }

            if (l_name_A[i] == l_name_B[j])
            {
                i++;
                j++;
            }
            else if (l_name_A[i] < l_name_B[i])
            {
                res = 1;
                break;
            }
            else
            {
                res = 0;
                break;
            }
          
        }
        return res;
    }

}