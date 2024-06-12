using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ResetApi : MonoBehaviour
{
	private string resetUrl = "http://localhost:8080/api/pru/word/reset";
	public Button newGameButton;


	void Start()
	{
		newGameButton.onClick.AddListener(OnNewGameButtonClick);

	}

	void OnNewGameButtonClick()
	{
		StartCoroutine(ResetGame());

	}
	IEnumerator ResetGame()
	{
		using (UnityWebRequest request = UnityWebRequest.Get(resetUrl))
		{
			yield return request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError(request.error);
			}
			else
			{
				Debug.Log("Game reset successful.");
				// Optionally, you can add additional code here to reset the game state in Unity
			}
		}
	}


}
