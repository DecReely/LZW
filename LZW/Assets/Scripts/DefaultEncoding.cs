using System.Collections.Generic;
using UnityEngine;
using System;

public class DefaultEncoding : MonoBehaviour
{
    // Written by me by simplifying the LZW Algorithm.
    public static List<int> Encode(String text) {
        int dictSize = 512;
        Dictionary<String, int> dictionary = new Dictionary<string, int>();
        for (int i = 0; i < dictSize; i++) {
            dictionary.Add(((char) i).ToString(), i);
        }
        List<int> result = new List<int>();
        foreach (char character in text.ToCharArray()) {
            if (dictionary.ContainsKey(character.ToString())) {
                result.Add(dictionary[character.ToString()]);
            }
        }
        return result;
    }
}
