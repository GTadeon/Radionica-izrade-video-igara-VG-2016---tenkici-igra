using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameStartManager : MonoBehaviour {


    public GameObject startPanel;
    public AudioSource menuMusic;
    public AudioSource mainMusic;
    public AudioSource tankMovingSFX;




    void Start ()
    {
        Time.timeScale = 0;
        AudioManager.PlayAudio(menuMusic);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
    }


    private void StartGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        AudioManager.StopAudio(menuMusic);
        AudioManager.PlayAudio(mainMusic);
        AudioManager.PlayAudio(tankMovingSFX);



    }

}
