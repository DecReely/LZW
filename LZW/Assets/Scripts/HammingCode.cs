using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Hamming (15,11) Code Correction Algorithm
/// Source: https://gist.github.com/fend25/99347aba1903c881ae48#file-hammingcode-cs
/// </summary>
public class HammingCode : MonoBehaviour
{
    public const bool t = true;
        public const bool f = false;
        public const int startWith = 2;
        static int length = 15;

        public static bool[] Encode(bool[] code)
        {
            var encoded = new bool[length];

            int i = startWith, j = 0;
            while (i < length)
            {
                if (i == 3 || i == 7) i++;
                encoded[i] = code[j];

                i++;
                j++;
            }

            encoded[0] = Helpers.doXoringForPosition(encoded, length, 1);
            encoded[1] = Helpers.doXoringForPosition(encoded, length, 2);
            encoded[3] = Helpers.doXoringForPosition(encoded, length, 4);
            if (length > 7)
                encoded[7] = Helpers.doXoringForPosition(encoded, length, 8);

            return encoded;
        }

        public static bool[] Decode(bool[] encoded)
        {
            var decoded = new bool[11];

            int i = startWith, j = 0;
            while (i < length)
            {
                if (i == 3 || i == 7) i++;
                decoded[j] = encoded[i];

                i++;
                j++;
            }

            return decoded;
        }

        public static int ErrorSyndrome(bool[] encoded)
        {
            int syndrome =
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 1) ^ encoded[0])) +
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 2) ^ encoded[1]) << 1) +
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 4) ^ encoded[3]) << 2);
            if (length > 7) syndrome +=
               (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 8) ^ encoded[7]) << 3);

            return syndrome;
        }

        public static void MixinSingleError(bool[] encoded, int pos)
        {
            encoded[pos - 1] = !encoded[pos - 1];
        }

		/*
        private void Start()
        {
            //length = 15;
            int errorPosition = 10;
            string codeString = "01010101111";

            var code = Helpers.prettyStringToBoolArray(codeString);
            var encoded = Encode(code);
            
            Console.WriteLine(Helpers.boolArrayToPrettyString(code));
            Console.WriteLine(Helpers.boolArrayToPrettyString(encoded));

            MixinSingleError(encoded, errorPosition);
            Console.WriteLine(Helpers.boolArrayToPrettyString(encoded));

            Console.WriteLine(ErrorSyndrome(encoded));
            encoded[errorPosition-1] = !encoded[errorPosition-1];

            var decoded = Decode(encoded);
            Console.WriteLine(Helpers.boolArrayToPrettyString(decoded));

            Console.WriteLine(Enumerable.SequenceEqual(code, decoded));
                   
            Console.WriteLine();

            Console.ReadLine();
        }
		*/
		
}