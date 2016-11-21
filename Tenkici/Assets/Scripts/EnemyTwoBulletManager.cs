using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTwoBulletManager : MonoBehaviour
{

    //object pool za metke nasih enemya:

    public int numberOfBullets;
    public GameObject tankBulletEnemy;
    public static Transform bulletSpawn;


    public static List<GameObject> enemy2ammo = new List<GameObject>();
    private static GameObject Enemy;
    public float reloadTime;

    public static bool isReloaded;

    public float tryFireRate;


    public GameObject bulletExplosion;
    public AudioSource enemyShotSFX;

    void Start()
    {

        bulletExplosion.SetActive(false);

        bulletSpawn = GameObject.Find("BulletSpawnEnemy2").transform;
        Enemy = GameObject.Find("Enemy2");
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
            enemy2ammo.Add(bullet);
            bullet.SetActive(false);
        }
    }

    //uzima iz liste prvog dostupnog ne aktivnog i  aktivira ga
    public static void FireBullet()
    {
        for (int i = 0; i < enemy2ammo.Count; i++)
        {
            if (!enemy2ammo[i].activeInHierarchy)
            {
                enemy2ammo[i].transform.position = bulletSpawn.position;
                enemy2ammo[i].GetComponent<BulletEnemyTwoMovement>().currentBulletDirection = GetBulletDirectionByPlayerRotation();
                enemy2ammo[i].transform.rotation = Enemy.transform.rotation;
                enemy2ammo[i].SetActive(true);
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
    public static void KillBullet(GameObject bullet)
    {
        Debug.Log("uvijam metak enemy");
        for (int i = 0; i < enemy2ammo.Count; i++)
        {
            if (enemy2ammo[i].name == bullet.name && enemy2ammo[i].activeInHierarchy)
            {
                enemy2ammo[i].SetActive(false);
                break;
            }
        }
    }


    private static BulletEnemyTwoMovement.Direction GetBulletDirectionByPlayerRotation()
    {
        if (Enemy.transform.rotation.eulerAngles.z == 0f)
        {
            return BulletEnemyTwoMovement.Direction.up;
        }

        else if (Enemy.transform.rotation.eulerAngles.z == 180f)
        {
            Debug.Log("dolje sam okrenut i pucam dolje");
            return BulletEnemyTwoMovement.Direction.down;
        }

        else if (Enemy.transform.rotation.eulerAngles.z == 270f)
            return BulletEnemyTwoMovement.Direction.right;

        else
            return BulletEnemyTwoMovement.Direction.left;

    }
}
