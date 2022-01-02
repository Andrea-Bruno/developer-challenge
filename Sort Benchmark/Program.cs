using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


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

		static public int[] Ar
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


		const int ntest = 100000;
		static int pointer = 0;
		public static int[] randomBase;
		public static Random r = new Random();
		const int max = 20;
		const int len = 55;
		const int rif = 10;
		public static double abComp = 0;

		//public static readonly int[] Ar = { 7, 13, 2, 16, 20, 1, 0, 11, 1, 5, 13, 17, 6, 15, 20, 1, 1, 14, 13, 8, 6, 4, 10, 4, 4, 20, 10, 19, 6, 11, 8, 8, 12, 15, 6, 9, 15, 6, 11, 19, 18, 12, 14, 11, 17, 18, 9, 10, 10, 15, 12, 17, 0, 1, 11 };
		//public static readonly int[] Ar = { 5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

		static void Main(string[] args)
		{
			var l = ntest + len;
			randomBase = new int[l];
			for (var i = 0; i < l; i++)
				randomBase[i] = r.Next(0, max + 1);
			//put in temperature the CPU
			DateTime time = DateTime.Now.AddSeconds(5);
			var counter = 0;
			do
			{
				counter++;
				var sqr = counter ^ 2;
			} while (DateTime.Now < time);

			static void printResult(Func<int[], int, int[]> routine, string author)
			{
				var a = Ar;
				var t1 = 0; foreach (var n in a) t1 += 1;
				var v = routine(a, rif);
				var t2 = 0; foreach (var n in v) t2 += 1;
				if (t1 != t2)
					Debugger.Break(); //Wrong algo!
				Console.WriteLine(author);
				foreach (var value in v)
				{
					Console.WriteLine(value + " " + Math.Abs(value - rif));
				}
				Console.WriteLine();
			}

			//============================
			//   Print sequence
			//============================

			printResult(sortMubasher, "Mubasher");
			printResult(sortSadiq, "Sadiq");
			printResult(sortFaig, "Faig");
			printResult(sortKirill1, "Kirill1");
			printResult(sortKirill2, "Kirill2");
			printResult(sortMusabir, "Musabir");
			printResult(AndreaBruno, "Andrea Bruno");
			printResult(sortTuriddu, "Turiddu");
			printResult(sortAsekir, "Asekir");
			printResult(sortJacopo, "Jacopo");
			printResult(sortAndreaManzini,"Andrea Manzini");		

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
					var b = Ar;
					b = routine(b, rif);
					counter++;
				} while (counter <= ntest);
				var ms = (DateTime.Now - time).TotalMilliseconds;
				if (abComp == 0)
					abComp = ms / 2;
				Console.WriteLine(author + "=" + (int)ms + "ms;  ab=" + Math.Round(ms / abComp, 2));
			}

			Benchmark(sortMubasher, "Mubasher"); //run this on first!
			Benchmark(sortSadiq, "Sadiq");
			Benchmark(sortFaig, "Faig");
			Benchmark(sortKirill1, "Kirill1");
			Benchmark(sortKirill2, "Kirill2");
			Benchmark(sortMusabir, "Musabir");
			Benchmark(AndreaBruno, "Andrea Bruno");
			Benchmark(sortTuriddu, "Turiddu");
			Benchmark(sortAsekir, "Asekir");
			Benchmark(sortJacopo, "Jacopo");
			Benchmark(sortAndreaManzini,"Andrea Manzini");

		}



		//=============================================================
		//https://www.facebook.com/mubashar.shahzad.23
		//=============================================================
		static int[] sortMubasher(int[] v, int rif)
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
		static int[] sortSadiq(int[] array, int rif)
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
		static int[] sortFaig(int[] v, int rif)
		{
			int[] k = v.OrderBy(x => Math.Abs((long)x - rif)).ToArray();
			return k;
		}


		//=============================================================
		//		Kirill Sergeyevich Rodionov
		//=============================================================
		static int[] sortKirill1(int[] v, int rif)
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

		static int[] sortKirill2(int[] v, int rif)
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
		static int[] sortMusabir(int[] v, int rif)
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
		//	 Andrea Bruno - the King!
		//=============================================================
		static int[] AndreaBruno(int[] v, int rif)
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
		//	 Turiddu
		//=============================================================

		static int[] sortTuriddu(int[] v, int rif)
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
		static int[] sortAsekir(int[] v, int rif)
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
		//	 Jacopo
		//=============================================================
		static int[] sortJacopo(int[] v, int rif)
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
		//	 Andrea Manzini
		//=============================================================
		static int[] sortAndreaManzini(int[] v, int rif)
		{
			// create an array for counting entries:
			// double length to keep negatives as 0..max , positives as max..max*2
			var d=new int[max*2];
			foreach(int item in v) {
				d[rif-item+max]++;
			}
 			int j=0;
			// in the center, delta=0 so i can copy the reference number
			while(d[max]-- >0 ) { v[j++]=rif;}
			// going from the center iterate both to left (1) and right (2)
			for(int i=1;i<max;i++)
			{
				while (d[max-i]-- >0) { v[j++]=rif+i;} // (1)
				while (d[max+i]-- >0) { v[j++]=rif-i;} // (2)
			} 
			return v;
		}

	}


}
