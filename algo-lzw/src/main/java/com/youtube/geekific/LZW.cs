using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LZW
{
    public static List<int> Encode(String text) {
        int dictSize = 256;
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
        int dictSize = 256;
        Dictionary<int, String> dictionary = new Dictionary<int, string>();
        for (int i = 0; i < dictSize; i++) {
            dictionary.Add(i, ((char) i).ToString());
        }

        //String characters = "";
        String characters = ((char) encodedText[0]).ToString();
        Debug.Log(characters);
        encodedText.Remove(characters.ToCharArray()[0]);
        Debug.Log(((char)encodedText[0]).ToString());
        // String characters = ((char) encodedText.Remove(0).intValue()).ToString();
        StringBuilder result = new StringBuilder(characters);
        foreach (int code in encodedText) {
            String entry = dictionary.ContainsKey(code)
            ? dictionary[code]
            : characters + characters[0];
            result.Append(entry);
            dictionary.Add(dictSize++, characters + entry[0]);
            characters = entry;
        }
        
        Debug.Log("256" + dictionary[256]);
        Debug.Log("258" + dictionary[258]);
        return result.ToString();
    }
}