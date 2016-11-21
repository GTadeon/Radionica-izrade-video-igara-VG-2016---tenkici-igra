using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BulletEnemyTwoMovement : MonoBehaviour
{

    //metak je prefab.
    //smjer metka moze bit: gore, dolje, lijevo desno. Smjer dobivamo ocitanjem static enuma koji se dodjeljuje kad se i promjeni tocka do koje interpoliramo (nasi tenkovi enemyiji)
    //tak da kad tenk skrene desno i pukne, metak ode 

    public enum Direction { up, down, left, right }
    public Direction currentBulletDirection;
    public float velocity;
    private Rigidbody2D rb2D;

    [Header("place tags here")]
    public List<string> whatCanBulletDestroy = new List<string>();

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (currentBulletDirection == Direction.up)
        {
            rb2D.velocity = Vector2.up * velocity;
        }
        else if (currentBulletDirection == Direction.down)
        {
            rb2D.velocity = Vector2.down * velocity;
        }
        else if (currentBulletDirection == Direction.right)
        {
            rb2D.velocity = Vector2.right * velocity;
        }
        else if (currentBulletDirection == Direction.left)
        {
            rb2D.velocity = Vector2.left * velocity;
        }
    }


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag != "Enemy" && collider2D.name != this.gameObject.name)
        {
            EnemyTwoBulletManager.KillBullet(gameObject);

            for (int i = 0; i < whatCanBulletDestroy.Count; i++)
            {
                if (collider2D.tag == whatCanBulletDestroy[i])
                {
                    collider2D.gameObject.SetActive(false);
                }
            }
        }
    }

}
