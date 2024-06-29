//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Networking;
//using TMPro;

//public class Api : MonoBehaviour
//{
//    private string url = "http://localhost:8080/api/pru/word/THREE";


//    public Text keyText;

//    void Start()
//    {
//        StartCoroutine(GetWord());

//    }

//    IEnumerator GetWord()
//    {
//        using (UnityWebRequest request = UnityWebRequest.Get(url))
//        {
//            yield return request.SendWebRequest();
//            if (request.result == UnityWebRequest.Result.ConnectionError)
//            {
//                Debug.LogError(request.error);
//            }
//            else
//            {
//                string json = request.downloadHandler.text;
//                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

//                keyText.text = "Key Word: " + stats["key"];
//            }
//        }

//    }





//}

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;

public class Api : MonoBehaviour
{
    private string url = "http://localhost:8080/api/pru/word/THREE";
    private string changeStatusBaseUrl = "http://localhost:8080/api/pru/word/";
    private int wordId;

    public Text keyText;
    public Button changeStatusButton;

    void Start()
    {
        StartCoroutine(GetWord());

        // Add listener to the button
        changeStatusButton.onClick.AddListener(OnChangeStatusButtonClicked);
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
                JSONNode stats = JSON.Parse(json);

                wordId = stats["id"];
                keyText.text = "Maxim: " + stats["key"];
            }
        }
    }

    void OnChangeStatusButtonClicked()
    {
        // Start the coroutine to change the status
        StartCoroutine(ChangeStatus(wordId));
    }

    IEnumerator ChangeStatus(int id)
    {
        string changeStatusUrl = changeStatusBaseUrl + id.ToString();

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(changeStatusUrl, ""))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Status changed successfully");
            }
        }
    }
}

