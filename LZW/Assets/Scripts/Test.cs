using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Test : MonoBehaviour
{
	void Start()
    {
	    /*
	    List<int> compressed = LZW.Encode("dedi naber dedim iyidir dedi eee dedi dedi nee dedi iyidir dedim ne dedin dedim ne dedi kim dedim dedi kim ne dedi dedi");
	    foreach (int i in compressed)
	    {
		    Debug.Log(i);
	    }

	    String decompressed = LZW.Decode(compressed);
	    Debug.Log(decompressed);
	    */
	    
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
    }
}

