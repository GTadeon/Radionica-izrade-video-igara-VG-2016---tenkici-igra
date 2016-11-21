using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyOneBulletManager : MonoBehaviour
{

    //object pool za metke nasih enemya:

    public int numberOfBullets;
    public GameObject tankBulletEnemy;
    public static Transform bulletSpawn;


    public static List<GameObject> enemy1ammo = new List<GameObject>();
    private static GameObject Enemy;
    public float reloadTime;

    public static bool isReloaded;

    public float tryFireRate;


    public GameObject bulletExplosion;
    public AudioSource enemyShotSFX;

    void Start()
    {

        bulletExplosion.SetActive(false);

        bulletSpawn = GameObject.Find("BulletSpawnEnemy1").transform;
        Enemy = GameObject.Find("Enemy1");
        isReloaded = true;
        CreateBullets(numberOfBullets);
        StartCoroutine(TryFireEachNSeconds());
    }


    private void PlayShotExplosionAnimation()
    {
        bulletExplosion.SetActive(true);
        Invoke("DeactivateExplosion", 0.5f);
    }

    private void DeactivateExplosion()
    {
        bulletExplosion.SetActive(false);

    }

    private IEnumerator TryFireEachNSeconds()
    {
        yield return new WaitForSeconds(tryFireRate);
        FireBullet();
        AudioManager.PlayAudio(enemyShotSFX);
        PlayShotExplosionAnimation();

        StartCoroutine(TryFireEachNSeconds());

    }


    //kreiramo ih bulletNumber broj i onda deaktiviramo (aktiviramo ih kad bude trebalo )
    private void CreateBullets(int bulletNumber)
    {
        GameObject bullet = null;
        for (int i = 0; i < bulletNumber; i++)
        {
            bullet = Instantiate(tankBulletEnemy, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
            enemy1ammo.Add(bullet);
            bullet.SetActive(false);
        }
    }

    //uzima iz liste prvog dostupnog ne aktivnog i  aktivira ga
    public static void FireBullet()
    {
        for (int i = 0; i < enemy1ammo.Count; i++)
        {
            if (!enemy1ammo[i].activeInHierarchy)
            {
                enemy1ammo[i].transform.position = bulletSpawn.position;
                enemy1ammo[i].GetComponent<BulletEnemyMovement>().currentBulletDirection = GetBulletDirectionByPlayerRotation();
                enemy1ammo[i].transform.rotation = Enemy.transform.rotation;
                enemy1ammo[i].SetActive(true);
                break;
            }
        }
    }

    public IEnumerator StartReloadWait()
    {
        isReloaded = false;
        yield return new WaitForSeconds(reloadTime);
        isReloaded = true;
    }

    //poziva se kad metak dode do mete il dotakne neki drugi kolajder (iz bullet movement skripte)
    public  static void KillBullet(GameObject bullet)
    {
        Debug.Log("uvijam metak enemy");
        for (int i = 0; i < enemy1ammo.Count; i++)
        {
            if (enemy1ammo[i].name == bullet.name && enemy1ammo[i].activeInHierarchy)
            {
                enemy1ammo[i].SetActive(false);
                break;
            }
        }
    }


    private  static BulletEnemyMovement.Direction GetBulletDirectionByPlayerRotation()
    {
        if (Enemy.transform.rotation.eulerAngles.z == 0f)
        {
            return BulletEnemyMovement.Direction.up;
        }

        else if (Enemy.transform.rotation.eulerAngles.z == 180f)
        {
            Debug.Log("dolje sam okrenut i pucam dolje");
            return BulletEnemyMovement.Direction.down;
        }

        else if (Enemy.transform.rotation.eulerAngles.z == 270f)
            return BulletEnemyMovement.Direction.right;

        else
            return BulletEnemyMovement.Direction.left;

    }
}
