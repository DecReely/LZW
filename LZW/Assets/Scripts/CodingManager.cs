using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;


public class CodingManager : MonoBehaviour
{
	public List<bool[]> hammingCodesCoded11 = new List<bool[]>();
	private List<bool[]> hammingCodes15 = new List<bool[]>();
	private List<bool[]> hammingCodesDecoded11 = new List<bool[]>();
	private List<int> decompressedIntArray = new List<int>();

	void Start()
	{
		#region LZW Test

	    /*
	    List<int> compressed = LZW.Encode("dedi naber dedim iyidir dedi eee dedi dedi nee dedi iyidir dedim ne dedin dedim ne dedi kim dedim dedi kim ne dedi dedi");
	    foreach (int i in compressed)
	    {
		    Debug.Log(i);
	    }

	    String decompressed = LZW.Decode(compressed);
	    Debug.Log(decompressed);
	    */

	    #endregion

	    #region Hamming Test

	    /*
	    int errorPosition = 10;
	    string codeString = "01010101111";

	    var code = Helpers.prettyStringToBoolArray(codeString);
	    var encoded = HammingCode.Encode(code);

	    Debug.Log(Helpers.boolArrayToPrettyString(code));
	    Debug.Log(Helpers.boolArrayToPrettyString(encoded));

	    HammingCode.MixinSingleError(encoded, errorPosition);
	    Debug.Log(Helpers.boolArrayToPrettyString(encoded));

	    Debug.Log(HammingCode.ErrorSyndrome(encoded));
	    encoded[errorPosition-1] = !encoded[errorPosition-1];

	    var decoded = HammingCode.Decode(encoded);
	    Debug.Log(Helpers.boolArrayToPrettyString(decoded));

	    Debug.Log(Enumerable.SequenceEqual(code, decoded));
	     */

	    #endregion

	    #region Other Tests

	    // Int to bool[] test
	    /*
	    foreach (var VARIABLE in Helpers.ConvertIntToBoolArrayOfLengthEleven(5))
	    {
		    Debug.Log(VARIABLE);
	    }
		*/
	    
	    // Debug.Log((int)'ı');
	    
	    // List<int> compressed = LZW.Encode("qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230");
	    
	    // List<int> compressed = LZW.Encode("qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230qwertyuıopğüasdfghjklşi,zxcvbnmöç.<Ç:oelyq qş.çqwuoeH :p31461230");


	    #endregion

	}

	public void ClearCaches()
	{
		hammingCodesCoded11 = new List<bool[]>();
		hammingCodes15 = new List<bool[]>();
		hammingCodesDecoded11 = new List<bool[]>();
		decompressedIntArray = new List<int>();
	}

	// Main function that handles encoding and decoding by using LZW and Hamming in tandem.
	public String EncodeAndDecode(string text)
	{
		// Initial coding algorithm with LZW
		List<int> compressed = LZW.Encode(text);
		
		// Applying code correction algorithm with Hamming (15,11), first converting the codes from LZW to 11 byte binary code.
		foreach (int LZWCode in compressed)
		{
			hammingCodesCoded11.Add(Helpers.ConvertIntToBoolArrayOfLengthEleven(LZWCode));
		}

		// Applying code correction algorithm with Hamming (15,11), then transforming the 11 byte code to 15 byte according to Hamming algorithm.
		foreach (bool[] hammingCode11 in hammingCodesCoded11)
		{
			hammingCodes15.Add(HammingCode.Encode(hammingCode11));
		}
	    
		// Decoding the Hamming code back to 11 bytes.
		foreach (bool[] hammingCode15 in hammingCodes15)
		{
			hammingCodesDecoded11.Add(HammingCode.Decode(hammingCode15));
		}
	    
		// Converting 11 byte bool[] to int to decode with LZW.
		foreach (bool[] hammingCodeDecoded11 in hammingCodesDecoded11)
		{
			decompressedIntArray.Add(Helpers.ConvertBoolArrayOfLengthElevenToInt(hammingCodeDecoded11));
		}
	    
		// Decompressing the code by using LZW.
		String decompressedString = LZW.Decode(decompressedIntArray);

		// Return for UIManager.cs
		return decompressedString;
	}

	public static string PrepareForLossyEncoding(string text)
	{
		text = ConvertToLowercase(text);
		text = RemoveSpacesAndPunctuations(text);
		text = RemoveVowels(text);
		text = TransformSimilarLetters(text);

		return text;
	}
	
	// Made by ChatGPT
	static string RemoveSpacesAndPunctuations(string text)
	{
		return Regex.Replace(text, @"[\s\p{P}]+", "");
	}

	// Made by ChatGPT
	static string RemoveVowels(string text)
	{
		return Regex.Replace(text, "[aeiıoöuüAEİIOÖUÜ]", "");
	}
	
	// Made by ChatGPT
	static string ConvertToLowercase(string text)
	{
		return text.ToLower();
	}

	// Made by ChatGPT, did revision myself
	static string TransformSimilarLetters(string text)
	{
		text = text.Replace("ç", "c");
		text = text.Replace("ş", "s");
		text = text.Replace("ğ", "g");
		// Add more transformation rules if needed

		return text;
	}
	
	// Made by ChatGPT
	static double CalculateEntropy(List<int> data)
	{
		int totalCount = data.Count;
		var counts = data.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

		double entropy = 0.0;

		foreach (var count in counts.Values)
		{
			double probability = (double)count / totalCount;
			entropy -= probability * Math.Log(probability, 2);
		}

		return entropy;
	}

	// Made by ChatGPT, did revision myself
	public static double CalculateGainPercentage(List<int> LZWCode, List<int> defaultCode)
	{
		if (defaultCode == null || LZWCode == null)
			return 0;
		
		double gain = (CalculateEntropy(LZWCode) - CalculateEntropy(defaultCode)) / CalculateEntropy(defaultCode) * 100;
		return gain;
	}

	// Made by ChatGPT, did revision myself
	public static double CalculateCompressionRatePercentage(List<int> LZWCode, List<int> defaultCode)
	{
		return (1 - ((float)LZWCode.Count / defaultCode.Count)) * 100;
	}
	
	public double CalculateChannelCapacityUtilisationPercentage()
	{
		// 2048 = 2^11
		return (float)decompressedIntArray.Count / 2048 * 100;
	}
}

