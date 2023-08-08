using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// LZW Compression Algorithm
/// Implementation of a Java script to C#.
/// Source: https://www.youtube.com/watch?v=1KzUikIae6k
/// </summary>
public class LZW
{
    public static List<int> Encode(String text) {
        int dictSize = 512;
        Dictionary<String, int> dictionary = new Dictionary<string, int>();
        for (int i = 0; i < dictSize; i++) {
            dictionary.Add(((char) i).ToString(), i);
        }

        String foundChars = "";
        List<int> result = new List<int>();
        foreach (char character in text.ToCharArray()) {
            String charsToAdd = foundChars + character;
            if (dictionary.ContainsKey(charsToAdd)) {
                foundChars = charsToAdd;
            } else {
                result.Add(dictionary[foundChars]);
                dictionary.Add(charsToAdd, dictSize++);
                foundChars = character.ToString();
            }
        }
        if (foundChars != "") {
            result.Add(dictionary[foundChars]);
        }
        return result;
    }

    public static String Decode(List<int> encodedText) {
        int dictSize = 512;
        Dictionary<int, String> dictionary = new Dictionary<int, string>();
        for (int i = 0; i < dictSize; i++) {
            dictionary.Add(i, ((char) i).ToString());
        }

        String characters = ((char) encodedText[0]).ToString();
        encodedText.Remove(characters.ToCharArray()[0]);
        StringBuilder result = new StringBuilder(characters);
        foreach (int code in encodedText) {
            String entry = dictionary.ContainsKey(code)
            ? dictionary[code]
            : characters + characters[0];
            result.Append(entry);
            dictionary.Add(dictSize++, characters + entry[0]);
            characters = entry;
        }
        
        return result.ToString();
    }
}