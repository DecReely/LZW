using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Helper Functions For Hamming (15,11) Code Correction Algorithm
/// Source: https://gist.github.com/fend25/99347aba1903c881ae48#file-hammingcode-cs
/// </summary>
public class Helpers
{
    public static String boolArrayToPrettyString(bool[] arr)
    {
        return String.Join("", arr.Select(x => Convert.ToInt32(x)));
    }

    public static bool[] prettyStringToBoolArray(String s)
    {
        return s.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToArray();
    }

    public static bool notPowerOf2(int x)
    {
        return !(x == 1 || x == 2 || x == 4 || x == 8);
    }

    public static int[] getPositionsForXoring(int length, int currentHammingPosition)
    {
        var positions = new List<int>();
        for (int i = 1; i <= length; i++)
        {
            if ((i & currentHammingPosition) > 0 && notPowerOf2(i))
                positions.Add(i);

        }
        return positions.ToArray();
    }

    public static bool doXoringForPosition(bool[] vector, int length, int currentHammingPosition)
    {
        return getPositionsForXoring(length, currentHammingPosition)
        .Select(x => vector[x - 1])
        .Aggregate((x, y) => x ^ y);
    }
}
