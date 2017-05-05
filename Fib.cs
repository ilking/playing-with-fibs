using System;
using System.Collections.Generic;
using System.Linq;

namespace Fibber
{
  public class Program
  {
    public static Int64 FibArray(int n)
    {
      Int64[] nums = new Int64[n];
      
      if (n <= 2) return 1;
      
      nums[0] = 1;
      nums[1] = 1;
    
      for (int i = 2; i < nums.Length; i++) {
        nums[i] = nums[i-1] + nums[i-2];
      }
    
      return nums[n-1];
    }
    
    public static Int64 FibDPHelper(Dictionary<int, Int64> hash, int n) 
    {
      if (hash.ContainsKey(n)) return hash[n];
      Int64 f = 1;
      
      if (n > 2)
      {
        f = FibDPHelper(hash, n-1) + FibDPHelper(hash, n-2);
      }
      
      hash.Add(n, f);
      
      return f;
    }
    
    public static Int64 FibDP(int n) 
    {
      Dictionary<int, Int64> hash = new Dictionary<int, Int64>();
      return FibDPHelper(hash, n);
    }
    
    public static int[,] Dungeon(int[,] dungeon)
    {
      int m = dungeon.GetLength(0);
      int n = dungeon.GetLength(1);
      
      int[,] h = new int[m,n];
      h[m-1,n-1] = Math.Max(1-dungeon[m-1,n-1], 1);
      
      for(int i = m-2; i >= 0; i--) {      
        h[i,n-1] = Math.Max(h[i+1,n-1] - dungeon[i,n-1], 1);
      }
      
      for(int j = n-2; j >= 0; j--){
        h[m-1,j] = Math.Max(h[m-1,j+1] - dungeon[m-1,j], 1);
      }
      
      for(int i = m-2; i >= 0; i--){
        for(int j = n-2; j >= 0; j--) {
          int down = Math.Max(h[i+1,j] - dungeon[i,j], 1);
          int right = Math.Max(h[i,j+1] - dungeon[i,j], 1);
          h[i,j] = Math.Min(right, down);
        }
      }
      
      return h;
    }
    
    /* 
        if n is even then k = n/2:
        F(n) = [2*F(k-1) + F(k)]*F(k)

        If n is odd then k = (n + 1)/2
        F(n) = F(k)*F(k) + F(k-1)*F(k-1)
      */
    public static int LogNFibHelper(int n, Dictionary<int, int> found)
    {
      int result = 1;
      
      if (n == 0 || n == 1) return result;
      
      if (found.ContainsKey(n)) return found[n];
      
      if (n % 2 == 0)
      {
        int k = n/2;
        
        result = ((2 * LogNFibHelper(k - 1, found)) + LogNFibHelper(k, found)) * LogNFibHelper(k, found);
      }
      else
      {
        int k = (n+1)/2;
        result = (LogNFibHelper(k, found) * LogNFibHelper(k, found)) + (LogNFibHelper(k-1, found) * LogNFibHelper(k-1, found));
      }
      
      found.Add(n, result);
      
      return result;
    }
    
    public static int LogNFib(int n)
    {
      if (n == 0) return 0;
      if (n == 1 || n == 2) return 1;
      
      Dictionary<int,int> found = new Dictionary<int,int>();
      found.Add(0, 0);
      found.Add(1, 1);
      found.Add(2, 1);
      
      return LogNFibHelper(n, found);
    }
    
    public static void Main(string[] args)
    {
      int n = 31;
      Console.WriteLine("Array: {0}", FibArray(n));
      Console.WriteLine("LogN: {0}", LogNFib(n));
      Console.WriteLine("DPFib: {0}", FibDP(n));

    }
  }
}