using UnityEngine;
using System.Collections;

public class PlayerControllerRogueLikeStyle : MonoBehaviour {

	public static bool shouldControl;
	public float maxSpeed;
    private float initialMaxSpeed;

	private Rigidbody2D rb;
    private Animation animation;


    public enum PlayerMechanics { Mechanics1, Mechanics2 }

    public PlayerMechanics Mehanics;

    private PlayerBulletManager playerBulletManager;
    public GameObject bulletExplosion;

    public string waterTag;
    [Range(1,100)]
    public float waterSlowDownPercentage;

    public AudioSource PlayerShotSound;
    public AudioSource playerDestroyed;
    public AudioSource powerUpPickUP;

    public GameObject playerExplosion;

    

    void Start()
    {
        playerExplosion.SetActive(false);
        initialMaxSpeed = maxSpeed;
        animation = GetComponent<Animation>();
        shouldControl = true;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerBulletManager = this.gameObject.GetComponent<PlayerBulletManager>();
        bulletExplosion.SetActive(false);


        //jer je mehanika prvog tipa = usmjerenje cijevi pomocu rotacije uzroovane kolizijom tenka i zidova
        if (Mehanics == PlayerMechanics.Mechanics2)
            rb.angularDrag = 999f;
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

    void Update()
	{
        if (Mehanics == PlayerMechanics.Mechanics2)
        {
            //pucanje:
            if (Input.GetKeyDown(KeyCode.Space) && PlayerBulletManager.isReloaded)
            {
                PlayerBulletManager.FireBullet();
                PlayShotExplosionAnimation();
                playerBulletManager.StartCoroutine(playerBulletManager.StartReloadWait());
                AudioManager.PlayAudio(PlayerShotSound);
            }

            //kretnja:
            if (Input.GetKey(KeyCode.S))
            {
                animation.Play("TankDown");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animation.Play("TankUp");

            }
            else if (Input.GetKey(KeyCode.A))
            {
                animation.Play("TankLeft");

            }
            else if (Input.GetKey(KeyCode.D))
            {
                animation.Play("TankRight");

            }
            
        }
	}

	void FixedUpdate()
	{
        if (shouldControl)
        {
            float kretnja_V = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(kretnja_V * maxSpeed, rb.velocity.y);

            float kretnja_H = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(kretnja_H * maxSpeed, rb.velocity.x);
        }
	}


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log(collider2D.tag);
        if (collider2D.tag == waterTag)
        {
            this.maxSpeed -= maxSpeed * (waterSlowDownPercentage / 100);
        }
        else if (collider2D.tag.Contains("Enemy"))
        {
            this.gameObject.SetActive(false);
            AudioManager.PlayAudio(playerDestroyed);
            playerExplosion.transform.position = this.gameObject.transform.position;
            playerExplosion.SetActive(true);

            GameOverManager.EndGame();
        }
        else if (collider2D.tag=="SpeedPowerUp")
        {
            StartCoroutine(fasterMovementForNseconds(5f));
            collider2D.gameObject.SetActive(false);
            AudioManager.PlayAudio(powerUpPickUP);

        }
        else if (collider2D.tag == "ReloadPowerUp")
        {
            playerBulletManager.StartCoroutine(playerBulletManager.fasterReloadForNSeconds(5f));
            collider2D.gameObject.SetActive(false);
            AudioManager.PlayAudio(powerUpPickUP);


        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == waterTag)
        {
            this.maxSpeed += initialMaxSpeed*(waterSlowDownPercentage/100);
        }
    }

    private IEnumerator fasterMovementForNseconds(float n)
    {
        maxSpeed *= 2;
        yield return new WaitForSeconds(n);
        maxSpeed = initialMaxSpeed;
    }

}
