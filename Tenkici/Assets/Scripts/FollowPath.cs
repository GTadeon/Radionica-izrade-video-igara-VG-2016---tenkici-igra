using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {
	//2 su stila kretnje platforme:
	public enum StilPracenjaPutanje {kretnjaPrema,Lerp}

	//defaultna postavka:
	public StilPracenjaPutanje defaultniStil = StilPracenjaPutanje.kretnjaPrema;
	public PathDefinition pathDefinition; //instanciram objekt, treba mi za metodu DohvatiPutanju()
	public float MaxUdaljenostDoTocke=.1f;
	public float brzina=1f;

	private IEnumerator<Transform> trenutnaTocka;


    //public BulletEnemyMovement bulletEnemyMovement;


    void Awake()
    {
    }

    public void Start()
	{
        //bulletEnemyMovement = GameObject.Find("EnemyBulletManager").GetComponent<EnemyBulletManager>().tankBulletEnemy.GetComponent<BulletEnemyMovement>();

        if (pathDefinition == null) 
		{
			Debug.LogError("ne moze biti putanja null",gameObject);
			return;
		}
		trenutnaTocka = pathDefinition.DohvatiPutanju ();
		trenutnaTocka.MoveNext (); //da ide na slijedeu tocku



        if (trenutnaTocka.MoveNext () == false) 
		{
			return; //prekidam Start funkciju,nema vise tocaka u toj putanji/arrayu...
		}
		//da po defautlu platforma bude na poziciji prve tocke iz arraya,tj one TRENUTNE prve:
		transform.position = trenutnaTocka.Current.position; 

	}
	public void Update()
	{
		if (trenutnaTocka==null || trenutnaTocka.Current==null)
			return; //ukoliko nema tocaka ili nema trenutne..

		if (defaultniStil == StilPracenjaPutanje.kretnjaPrema)
			transform.position = Vector3.MoveTowards (transform.position, trenutnaTocka.Current.position, Time.deltaTime * brzina);
		else if (defaultniStil == StilPracenjaPutanje.Lerp)
			transform.position = Vector3.Lerp (transform.position,trenutnaTocka.Current.position,Time.deltaTime*brzina);

		//provjera blizine do odredisne tocke,tj one koju prati treutno platforma:
		float kvadriranaUdaljenost = (transform.position - trenutnaTocka.Current.position).sqrMagnitude;
        if (kvadriranaUdaljenost < MaxUdaljenostDoTocke * MaxUdaljenostDoTocke)
        {
            trenutnaTocka.MoveNext();
        
        }
    }



    public void RotateTankBySide(PathDefinition.TankDirection side)
    {
        if (side == PathDefinition.TankDirection.up)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            //bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.up;

        }
        else if (side == PathDefinition.TankDirection.down)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, -1, 0);
            //bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.down;

        }
        else if (side == PathDefinition.TankDirection.right)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 270f);
            // bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.right;

        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 90f);
            //  bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.left;

        }
    }



    public void RotateTankApropriately (int currentPointIndex)
    {
        if (pathDefinition.tankOnThisPoint[currentPointIndex]==PathDefinition.TankDirection.up)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            // bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.up;

        }
        else if (pathDefinition.tankOnThisPoint[currentPointIndex] == PathDefinition.TankDirection.down)
        {
            this.gameObject.transform.rotation = new Quaternion(0,0,-1,0);
            //  bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.down;


        }
        else if (pathDefinition.tankOnThisPoint[currentPointIndex] == PathDefinition.TankDirection.right)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0,0,270f);
            // bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.right;


        }
        else if (pathDefinition.tankOnThisPoint[currentPointIndex] == PathDefinition.TankDirection.left)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 90f);
            // bulletEnemyMovement.currentBulletDirection = BulletEnemyMovement.Direction.left;

        }
    }


    public  void ResetFollow()
    {
        trenutnaTocka = pathDefinition.DohvatiPutanju();
        trenutnaTocka.MoveNext();
        Debug.Log(trenutnaTocka.Current);
    }

}
