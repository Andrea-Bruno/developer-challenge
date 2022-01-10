using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Sort_Benchmark.Algorithms;

/*
														-	CONTEST -

Application test, problem:
You have an array containing integers, and you have a reference number.
Example:
Array = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20}
Reference = 10
You have to sort the array, based on the distance from the reference number, from the closest to the most distant, and get a result similar to this:
{10,9,11,8,12,7,13,6,14,5,15,4,16,3,17,2,18,1,19,20}
The input sequence can be different and random, there may be duplicate numbers. Different outputs can be valid, in the example also the sequence that starts with {10,11,9 ...}.
Another example to test your algorithm:
Reference = 25
Input: {11,7,42,7,3,8,5,48,24,45,32,21}
Output: {24,21,32,11,8,42,7,7,5,45,3,48}
The winner will be the one who writes the algorithm that does this faster. I will do a benchmark test to test the speed.
Send me the solution privately so the others don't copy it
You can take this code as a base and complete it:
https://dotnetfiddle.net/mwzSZ4

 */

namespace Sort_Benchmark
{

    class Program
    {
        static int pointer = 0;
        static int[] randomBase;
        static Random r = new Random();
        const int ntest = 1000000;
        const int max = 1000; 
        const int len = 100; 
        static int rif;
 

        static int[] Ar
        {
            get
            {
                if (pointer == ntest)
                    pointer = 0;
                var output = new int[len];
                Array.Copy(randomBase, pointer, output, 0, len);
                pointer++;
                return output;
            }
        }


        static double abComp = 0;

        static void Main(string[] args)
        {
            var l = ntest + len;
            randomBase = new int[l];
            for (var i = 0; i < l; i++)
                randomBase[i] = r.Next(0, max + 1);
            DateTime time = DateTime.Now.AddSeconds(10);
            var counter = 0;
            do
            {
                counter++;
                var sqr = counter ^ 2;
            } while (DateTime.Now < time);


            //============================
            //   Benchmark test
            //============================

            Console.WriteLine("bm = comparison with Bimbominkia, the lower values are better");

            void Benchmark(Func<int[], int, int[]> routine, string author)
            {
                DateTime time = DateTime.Now;
                var counter = 0;
                do
                {	
                	rif =r.Next(0, max + 1);
                    var b = Ar;
                    b = routine(b, rif);
                    counter++;
                } while (counter <= ntest);
                var ms = (DateTime.Now - time).TotalMilliseconds;
                if (abComp == 0)
                    abComp = ms;
                Console.WriteLine(author + "=" + (int)ms + "ms;  bm=" + Math.Round(ms / abComp, 2));
            }

            // ATTENTION: Run the benchmark in Release modality
            Benchmark(sortJacopo, "Jacopo"); //run this on first!
            Benchmark(sortMubasher, "Mubasher"); 		         
            Benchmark(sortSadiq, "Sadiq");
            Benchmark(sortFaig, "Faig");
            Benchmark(sortKirill1, "Kirill1");
            Benchmark(sortKirill2, "Kirill2");
            Benchmark(sortMusabir, "Musabir");
            Benchmark(AndreaBruno, "Andrea Bruno");
            //Benchmark(AndreaBruno2, "Andrea Bruno 2");
            //Benchmark(AndreaManzini, "Andrea Manzini");
            Benchmark(sortMuhammadZubair, "Muhammad");
            Benchmark(sortTuriddu, "Turiddu");
            Benchmark(sortAsekir, "Asekir");
            Benchmark(sortAli, "Ali");
			Benchmark(sortFrancescoBrocato, "Francesco Brocato");   
			Benchmark(sortFrancescoBrocato2, "Francesco Brocato 2"); 	            
 
            Console.ReadKey();
        }



    }
    public static class Algorithms
    {

        //=============================================================
        //https://www.facebook.com/mubashar.shahzad.23
        //=============================================================
        public static int[] sortMubasher(int[] v, int rif)
        {
            //write here the algoritm		

            Dictionary<int, List<int>> resultDic = new Dictionary<int, List<int>>();

            foreach (var inputVal in v)
            {
                int difference = Math.Abs(inputVal - rif);

                if (!resultDic.ContainsKey(difference))
                {
                    //List<int> temp = new List<int>();
                    //temp.Add(val);
                    //resultDic.Add(difference, temp);
                    // OR
                    resultDic.Add(difference, new List<int> { inputVal });
                }
                else
                {
                    //List<int> temp = resultDic[difference];
                    //temp.Add(val);
                    //resultDic[difference] = temp;
                    // OR
                    resultDic[difference].Add(inputVal);
                }
            }

            var result = resultDic.OrderBy(e => e.Key).Select(a => a.Value);

            List<int> finalResult = new List<int>();

            foreach (var dicValues in result)
            {
                foreach (var value in dicValues)
                {
                    finalResult.Add(value);
                }
            }
            return finalResult.ToArray();
        }

        //=============================================================
        //		Sadiq Eyvazov sadiqeyvazov8@gmail.com
        //=============================================================
        public static int[] sortSadiq(int[] array, int rif)
        {
            Array.Sort(array, new CustomComparator(rif));
            return array;
        }

        public class CustomComparator : IComparer<int>
        {
            public CustomComparator(int reference)
            {
                this.reference = reference;
            }

            public int Compare(int o1, int o2)
            {
                return (Math.Abs((this.reference - o1)) - Math.Abs((this.reference - o2)));
            }
            public int reference;
        }


        //=============================================================
        //		Faig Jafarguliyev
        //=============================================================
        public static int[] sortFaig(int[] v, int rif)
        {
            int[] k = v.OrderBy(x => Math.Abs((long)x - rif)).ToArray();
            return k;
        }


        //=============================================================
        //		Kirill Sergeyevich Rodionov
        //=============================================================
        public static int[] sortKirill1(int[] v, int rif)
        {
            int lng = v.Length;
            int[] sibling = new int[lng];

            for (int i = 0; i < lng; i++)
            {
                sibling[i] = Math.Abs(v[i] - rif);
            }

            for (int i = 0; i < lng; i++)
            {
                int min = Int32.MaxValue;
                int pt = i;

                for (int j = i; j < lng; j++)
                {
                    if (sibling[j] < min)
                    {
                        pt = j;
                        min = sibling[j];
                    }
                }

                int plhldr1 = sibling[i];
                sibling[i] = sibling[pt];

                int plhldr2 = v[i];
                v[i] = v[pt];

                for (int j = i + 1; j <= pt; j++)
                {
                    int shift1 = sibling[j];
                    sibling[j] = plhldr1;
                    plhldr1 = shift1;

                    int shift2 = v[j];
                    v[j] = plhldr2;
                    plhldr2 = shift2;
                }
            }
            return v;
        }

        public static int[] sortKirill2(int[] v, int rif)
        {
            int[] temp = new int[v.Length];

            for (int i = 0; i < v.Length; i++)
            {
                temp[i] = Math.Abs(v[i] - rif);
            }
            for (int i = 1; i < v.Length; i++)
            {
                int cntr = i;
                while (cntr > 0)
                {
                    if (temp[i] < temp[cntr - 1]) cntr--;
                    else break;
                }


                int plhldr1 = temp[cntr];
                temp[cntr] = temp[i];

                int plhldr2 = v[cntr];
                v[cntr] = v[i];


                for (int j = ++cntr; j <= i; j++)
                {
                    int shift1 = temp[j];
                    temp[j] = plhldr1;
                    plhldr1 = shift1;

                    int shift2 = v[j];
                    v[j] = plhldr2;
                    plhldr2 = shift2;
                }

            }
            return v;
        }

        //=============================================================
        //		Musabir Musabayli
        //=============================================================
        public static int[] sortMusabir(int[] v, int rif)
        {
            Array.Sort(v);
            int[] d = new int[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                d[i] = Math.Abs(rif - v[i]);
            }
            Array.Sort(d, v);
            return v;
        }

        //=============================================================
        //	 Jacopo
        //=============================================================
        public static int[] sortJacopo(int[] v, int rif)
        {
            int[] pesi = new int[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                pesi[i] = Math.Abs(rif - v[i]);
            }
            Array.Sort(pesi, v);
            return v;
        }

        //=============================================================
        //	 Andrea Bruno 1
        //=============================================================
        public static int[] AndreaBruno(int[] v, int rif)
        {
            var q = v.Max();
            var a = new int[1 + (rif > (q - rif) ? rif : q) * 2];
            foreach (var e in v)
                a[((e - rif) << 1) ^ (e - rif) >> 31]++;
            var idx = 0;
            while (idx < a[0])
                v[idx++] = rif;
            for (var n = 1; n < (a.Length + 1) >> 1; n++)
            {
                for (var mi = 0; mi < a[(n << 1) - 1]; mi++)
                    v[idx++] = rif - n;
                for (var ma = 0; ma < a[n << 1]; ma++)
                    v[idx++] = rif + n;
            }
            return v;
        }

        //=============================================================
        //	 Andrea Bruno 2
        //=============================================================
        public static int[] AndreaBruno2(int[] v, int rif)
        {
            var q = v[0];
            int i = 1;
            do
            {
                if (v[i] > q)
                    q = v[i];
                i++;
            } while (i < v.Length);

            q++;
            var c = new int[q];
            i = 0;
            do
            {
                c[v[i]]++;
                i++;
            } while (i < v.Length);

            int p = 0;
            for (i = 0; i < c[rif]; i++)
            {
                v[p] = rif;
                p++;
            }

            int n;
            i = rif + 1;
            do
            {
                for (int f = 0; f < c[i]; f++)
                {
                    v[p] = i;
                    p++;
                }
                n = rif + (rif - i);
                for (int f = 0; f < c[n]; f++)
                {
                    v[p] = n;
                    p++;
                }
                i++;
            } while (i < q);
            if (n > 0)
                for (n -= 1; n >= 0; n--)
                    for (int f = 0; f < c[n]; f++)
                    {
                        v[p] = n;
                        p++;
                    }
            return v;
        }

        //=============================================================
        //	 Andrea Manzini
        //=============================================================
        //public static int[] AndreaManzini(int[] v, int rif)
        //{
        //    // create an array for counting entries:
        //    // double length to keep negatives as 0..max , positives as max..max*2
        //    var d = new int[max * 2];
        //    foreach (int item in v)
        //    {
        //        d[rif - item + max]++;
        //    }
        //    int j = 0;
        //    // in the center, delta=0 so i can copy the reference number
        //    while (d[max]-- > 0) { v[j++] = rif; }
        //    // going from the center iterate both to left (1) and right (2)
        //    for (int i = 1; i < max; i++)
        //    {
        //        while (d[max - i]-- > 0) { v[j++] = rif + i; } // (1)
        //        while (d[max + i]-- > 0) { v[j++] = rif - i; } // (2)
        //    }
        //    return v;
        //}

        //=============================================================
        //	 Muhammad
        //=============================================================

        public static int[] sortMuhammadZubair(int[] v, int rif)
        {
            int cnt = v.Count();
            int[] tempV = new int[cnt];
            int min = tempV[0], max = tempV[0];
            for (int i = 0; i < cnt; i++)
            {
                tempV[i] = v[i] - rif;
                if (tempV[i] < min)
                    min = tempV[i];
                else if (tempV[i] > max)
                    max = tempV[i];
            }

            int phs = max + 1, nhs = (min * -1) + 1;
            int[] ph = new int[phs], nh = new int[nhs];

            for (int i = 0; i < cnt; i++)
            {
                if (tempV[i] >= 0)
                    ph[tempV[i]]++;
                else
                    nh[tempV[i] * -1]++;
            }

            int j = 0, fc = (phs > nhs) ? phs : nhs;
            for (int i = 0; i < fc; i++)
            {
                if (i < nhs)
                {
                    int t = (i * -1) + rif;
                    while (nh[i]-- > 0)
                    {
                        v[j] = t;
                        j++;
                    }
                }
                if (i < phs)
                {
                    int t = i + rif;
                    while (ph[i]-- > 0)
                    {
                        v[j] = t;
                        j++;
                    }
                }
            }
            return v;
        }

        //=============================================================
        //	 Turiddu
        //=============================================================
        public static int[] sortTuriddu(int[] v, int rif)
        {
            var elements = v.Length;
            var d = new int[elements];
            for (var i = 0; i < elements; i++)
                d[i] = Math.Abs(rif - v[i]);
            for (var n = 1; n < elements; n++)
            {
                int m;
                for (m = n; m > 0; m--)
                {
                    if (d[n] >= d[m - 1])
                    {
                        break;
                    }
                }
                if (m < n)
                {
                    var tmpV = v[n];
                    var tmpD = d[n];
                    Array.Copy(v, m, v, m + 1, n - m);
                    Array.Copy(d, m, d, m + 1, n - m);
                    v[m] = tmpV;
                    d[m] = tmpD;
                }
            }
            return v;
        }

        //=============================================================
        //	 Asekir
        //=============================================================
        public static int[] sortAsekir(int[] v, int rif)
        {
            for (int i = 0; i < v.Length; i++)
            {
                int diff = Math.Abs(v[i] - rif);
                int temp = v[i];
                for (int j = i + 1; j < v.Length; j++)
                {
                    if (Math.Abs(v[j] - rif) < diff)
                    {
                        diff = Math.Abs(v[j] - rif);
                        v[i] = v[j];
                        v[j] = temp;
                        temp = v[i];
                    }
                }
            }
            return v;
        }

        //=============================================================
        //	 Ali
        //=============================================================

        public static int[] sortAli(int[] v, int rif)
        {
            int[] temp = v;
            Array.ForEach(temp, i => Math.Abs(i - rif));
            Array.Sort(v, temp);
            return v;
        }

        //=============================================================
        //	 Francesco Brocato
        //=============================================================
		public static int[] sortFrancescoBrocato(int[] v, int rif) {
			
			int[] rv=new int[v.Length];
			int min=v[0];
			int max=v[v.Length-1];
			int[] underRifTMP=new int[v.Length];
			int[] overRifTMP=new int[v.Length];
			int h=0;
			int k=0;
			
			for (int i=0;i<v.Length; i++){
				if (v[i]<min){
					min=v[i];
				} else if (v[i]>max){
					max=v[i];
				} 
				if (v[i]<=rif){
					underRifTMP[h]=v[i];
					h++;
				} else if (v[i]>rif){
					overRifTMP[k]=v[i];
					k++;
				}		    		   		
			}	
			if (rif<=min) {
				Array.Sort(v);	
				return v;
			}
			if (rif>=max) {
				Array.Sort(v);	
				for (int i=0;i<=v.Length/2; i++){	
					rv[i]=v[v.Length-(i+1)];
					rv[v.Length-(i+1)]=v[i];
				}
				return rv;    		
			}
			int[] underRif=new int[h];
			int[] overRif=new int[k];    	
			for (int i=0;i<h;i++){
				underRif[i]=underRifTMP[h-i-1];
			}
			for (int i=0;i<k;i++){
				overRif[i]=overRifTMP[i];
			}    	
			Array.Sort(underRif);
			Array.Sort(overRif);

			int j=0;
			k=0;
			h=h-1;
			while (h>=0 && k<overRif.Length) {
				if(Math.Abs(underRif[h]-rif)<=Math.Abs(overRif[k]-rif)){
					v[j]=underRif[h];
					h--;
				} else {
					v[j]=overRif[k];
					k++;
				}
				j++;
			};
			if (h<=k){
				for (int i=k; i<overRif.Length;i++){
					v[j]=overRif[i];
					j++;
				}
			} else {
				for (int i=h; i>=0; i--){
					v[j]=underRif[i];
					j++;
				}					
			}    	

			return v;
		}

        
        //=============================================================
        //	Francesco Brocato 2
        //=============================================================

		public static int[] sortFrancescoBrocato2(int[] v, int rif) {
			
		//algoritmo di ordinamento counting sort più efficiente Array.sort() nel contesto specifico
			int n = v.Length;
	  		int max = 0;
			for (int i=0; i<n; i++) {  
				if(max < v[i]) {
				   max = v[i];
				} 
			}
		    int[] freq = new int[max+1];
		    for (int i=0; i<max+1; i++) {  
			freq[i] = 0;
		    } 
		    for (int i=0; i<n; i++) {  
			 freq[v[i]]++;
		    }
		    for (int i=0, y=0; i<=max; i++) {  
			  while(freq[i]>0) { 
			    v[y] = i;
			    y++;
			    freq[i]--;
		     }
		    }
	 	//fine counting sort
	 		
			//Array.Sort(v); 
			//int min=v[0];
			//int max=v[v.Length-1];
			int[] rv=new int[v.Length];
			int h=0;
			int j=0;
			int k=v.Length-1;    	
			
			if (rif<=v[0]) {
				return v;
			}
			if (rif>=max) {
				for (int i=0;i<=v.Length/2; i++){	
					rv[i]=v[v.Length-(i+1)];
					rv[v.Length-(i+1)]=v[i];
				}
				return rv;    		
			}
			
			while (h<k) {
				if(-1*(v[h]-rif)>=v[k]-rif){
					rv[rv.Length-1-j]=v[h];
					h++;
				} else {
					rv[rv.Length-1-j]=v[k];
					k--;
				}
				j++;
			};    	
		 
			return rv;
		}		
        
        //=============================================================
        //	 YourName
        //=============================================================
        public static int[] sortYourName(int[] v, int rif)
        {
            //Write your algorithm here!!!
            //Write your algorithm here!!!
            //Write your algorithm here!!!
            //Write your algorithm here!!!
            //Write your algorithm here!!!
            //Write your algorithm here!!!
            return v;
        }
    }

}
