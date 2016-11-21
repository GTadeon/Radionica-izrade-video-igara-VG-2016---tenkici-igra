using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameOverManager : MonoBehaviour {

    public static GameObject  GameOverTxt;
    public static GameObject RestartTxt;


    void Start()
    {
        GameOverTxt = GameObject.Find("GameOverTxt");
        RestartTxt = GameObject.Find("RestartTxt");

        GameOverTxt.SetActive(false);
        RestartTxt.SetActive(false);
    }


    public static void EndGame()
    {
        GameOverTxt.SetActive(true);
        RestartTxt.SetActive(true);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
