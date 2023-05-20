using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	void Start()
    {
	    List<int> compressed = LZW.Encode("dedi naber dedim iyidir dedi eee dedi dedi nee dedi iyidir dedim ne dedin dedim ne dedi kim dedim dedi kim ne dedi dedi");
	    foreach (int i in compressed)
	    {
		    Debug.Log(i);
	    }

	    String decompressed = LZW.Decode(compressed);
	    Debug.Log(decompressed);
    }
}

