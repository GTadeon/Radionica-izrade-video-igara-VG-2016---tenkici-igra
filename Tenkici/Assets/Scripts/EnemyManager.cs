using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour {

    public GameObject enemyExplosion;
    public Transform[] spawnPoints;
    public static List<GameObject> enemiesToSpawn = new List<GameObject>();
    private FollowPath followPath;

    public AudioSource TankDestroyed;

    void Start()
    {
        followPath = this.gameObject.GetComponent<FollowPath>();
        enemyExplosion.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag=="Bullet")
            DeactivateEnemyAndRandomlySpawn();
    }

    private void DeactivateEnemyAndRandomlySpawn()
    {
        enemyExplosion.transform.position = this.gameObject.transform.position;
        enemyExplosion.SetActive(true);
        Invoke("DeactivateExplosion", 0.5f);
        enemiesToSpawn.Add(this.gameObject);
        Invoke("SpawnRandomly", 1f);
        ScoreManager.IncreaseScore();
        followPath.ResetFollow();
        AudioManager.PlayAudio(TankDestroyed);
        this.gameObject.SetActive(false);
    }
    private void DeactivateExplosion()
    {
        enemyExplosion.SetActive(false);
    }

    public  void SpawnRandomly()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            //enemiesToSpawn[i].transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            if (enemiesToSpawn[i].name=="Enemy2")
                enemiesToSpawn[i].transform.position = spawnPoints[1].position;
            else
                enemiesToSpawn[i].transform.position = spawnPoints[0].position;

            enemiesToSpawn[i].SetActive(true);
            enemiesToSpawn.Remove(enemiesToSpawn[i]);
        }
    }
}
