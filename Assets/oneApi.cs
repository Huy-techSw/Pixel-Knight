using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class oneApi : MonoBehaviour
{
    // Start is called before the first frame update
    private string url = "http://localhost:8080/api/pru/word/ONE";


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
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                keyText.text = "Maxim: " + stats["key"];
            }
        }

    }
}
