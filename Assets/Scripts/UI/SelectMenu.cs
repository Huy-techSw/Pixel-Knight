using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void StartScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // Update is called once per frame
    public void Stonerelics()
    {
        SceneManager.LoadScene("Ingame");
    }

    // Ví dụ cho nút chọn màn "Level2"
    public void Oldstoneland()
    {
        SceneManager.LoadScene("Ingame2");
    }

    // Ví dụ cho nút chọn màn "Level3"
    public void Blacksandland()
    {
        SceneManager.LoadScene("Ingame3");
    }

    public void Barrendesert()
    {
        SceneManager.LoadScene("Ingame4");
    }
    public void Quit()
    {
        SceneManager.LoadScene("Start");
    }
}
