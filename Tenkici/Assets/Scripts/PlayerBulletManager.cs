using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerBulletManager : MonoBehaviour {

    //object pool za metke naseg playera:

    public int numberOfBullets;
    public GameObject tankBullet;
    public static Transform bulletSpawn;


    public static List<GameObject> playerAmmo = new List<GameObject>();
    private static GameObject player;
    public float reloadTime;

    public static bool isReloaded;

    void Start()
    {
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
        player = GameObject.Find("Player");
        isReloaded = true;
        CreateBullets(numberOfBullets);
    }

    

    //kreiramo ih bulletNumber broj i onda deaktiviramo (aktiviramo ih kad bude trebalo )
    private void CreateBullets(int bulletNumber)
    {
        GameObject bullet=null;
        for (int i = 0; i < bulletNumber; i++)
        {
            bullet = Instantiate(tankBullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
            playerAmmo.Add(bullet);
            bullet.SetActive(false);
        }
    }

    //uzima iz liste prvog dostupnog ne aktivnog i  aktivira ga
    public static void FireBullet()
    {
        for (int i = 0; i < playerAmmo.Count; i++)
        {
            if (!playerAmmo[i].activeInHierarchy)
            {
                playerAmmo[i].transform.position = bulletSpawn.position;
                playerAmmo[i].GetComponent<BulletMovement>().currentBulletDirection = GetBulletDirectionByPlayerRotation();
                playerAmmo[i].transform.rotation = player.transform.rotation;
                playerAmmo[i].SetActive(true);
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
        for (int i = 0; i < playerAmmo.Count; i++)
        {
            if (playerAmmo[i].name==bullet.name && playerAmmo[i].activeInHierarchy)
            {
                playerAmmo[i].SetActive(false);
                break;
            }
        }
    }


    private static BulletMovement.Direction GetBulletDirectionByPlayerRotation()
    {
        if (player.transform.rotation.eulerAngles.z == 0f)
        {
            return BulletMovement.Direction.up;
        }

        else if (player.transform.rotation.eulerAngles.z == 180f)
        {
            Debug.Log("dolje sam okrenut i pucam dolje");
            return BulletMovement.Direction.down;
        }

        else if (player.transform.rotation.eulerAngles.z == 270f)
            return BulletMovement.Direction.right;

        else
            return BulletMovement.Direction.left;

    }

    public IEnumerator fasterReloadForNSeconds(float n)
    {
        float initReloadTime = reloadTime;
        reloadTime /= 2;
        yield return new WaitForSeconds(n);
        reloadTime = initReloadTime;
    }
}
