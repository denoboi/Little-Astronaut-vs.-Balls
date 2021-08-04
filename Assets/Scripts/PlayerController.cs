using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStrength = 10;
    public float turnSpeed;
    public GameObject[] powerupIndicators;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public float jumpForce;
    private Animator moveAnim;
    public bool isOnGround = true;
    public bool gameOver;
    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        moveAnim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        
        float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        VerticalMove();
        

        // player ile power up indikator ayni anda hareket etmeli.
        powerupIndicators[0].transform.position = gameObject.transform.position + new Vector3(0, 1f, 0);
        powerupIndicators[1].transform.position = gameObject.transform.position + new Vector3(0.5f, 3f, 0.5f);
        

        Jump();

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
         

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp")) //if other thing that we run into is a power up:
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerupIndicators[0].gameObject.SetActive(true);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            
        }
        if(other.gameObject.CompareTag("PowerUp2")) //if other thing that we run into is a power up2:
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerupIndicators[1].gameObject.SetActive(true);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>(); //enemy rigidbodysi ama bunu tam carptigimizda aliyoruz
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; //enemy'nin player'dan uzakligi. bunu da enemy pozisyonundan playerin pozisyonunu cikararak bulabiliriz.
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); //aninda bir hizlanma istedigimiz icin forcemode.impulse kullandik.
            Debug.Log("Collided with" + gameObject + "with power up set to: " + currentPowerUp.ToString());
        }
        // if(collision.gameObject.CompareTag("Enemy") && !hasPowerup)
        // {
        //     Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            
        //     enemyRb.AddForce(collision.gameObject.transform.position);
        //     Debug.Log("Collided with" + gameObject + "with power up set to: " + !hasPowerup);
        // }

            
    }

    IEnumerator PowerupCountdownRoutine() //powerup suresi
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicators[0].gameObject.SetActive(false);
        powerupIndicators[1].gameObject.SetActive(false);
    }
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up,
            Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
                isOnGround = false;
            }
    }

    void VerticalMove()
    {
        
        {
            if(Input.GetKey(KeyCode.W))
            {
                playerRb.AddForce(transform.forward * speed);
                moveAnim.SetInteger("AnimationPar", 1);
            }
            else if(Input.GetKeyUp(KeyCode.W))
            {
                moveAnim.SetInteger("AnimationPar", 0);

            }
            if(Input.GetKey(KeyCode.S))
            {
                playerRb.AddForce(-transform.forward * speed);
                moveAnim.SetInteger("AnimationPar",1);

            }
            else if(Input.GetKeyUp(KeyCode.S))
            {
                moveAnim.SetInteger("AnimationPar",0);   
            }
            
           
            
            
        }
        
        

        
    }
}

