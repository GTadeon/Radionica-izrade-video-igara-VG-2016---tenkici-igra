using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int score;
    public static int scoreIncrement;
    private static Text scoreTxt;
    

    void Start()
    {
        scoreIncrement = 100;
        score = 0;
        scoreTxt = this.gameObject.GetComponent<Text>();
    }

    public static void IncreaseScore()
    {
        Debug.Log("fdsfs");
        score += scoreIncrement;
        scoreTxt.text = "score: " + score.ToString();
    }

}
