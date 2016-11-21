using UnityEngine;
using System.Collections;

public class ArtefactBulletDetect : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag.Contains("Bullet"))
        {
            GameOverManager.EndGame();
        }
    }
}
