using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Api : MonoBehaviour
{
    private string url = "http://localhost:8080/api/pru/word/THREE";

    public Text keyText;
	void Start()
    {
        StartCoroutine(GetWord());
    }

    IEnumerator GetWord()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                keyText.text = "Key Word: " + stats["key"];
            }
        }

    }

 
}
