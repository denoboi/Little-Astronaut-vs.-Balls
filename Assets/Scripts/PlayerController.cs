using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStrength = 10;
    public float turnSpeed;
    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public float jumpForce;
    private Animator moveAnim;
    public bool isOnGround = true;
    public bool gameOver;
    
    
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
        powerupIndicator.transform.position = gameObject.transform.position + new Vector3(0, 1f, 0);

        Jump();
         

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp")) //if other thing that we run into is a power up:
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>(); //enemy rigidbodysi ama bunu tam carptigimizda aliyoruz
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; //enemy'nin player'dan uzakligi. bunu da enemy pozisyonundan playerin pozisyonunu cikararak bulabiliriz.
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); //aninda bir hizlanma istedigimiz icin forcemode.impulse kullandik.
            Debug.Log("Collided with" + gameObject + "with power up set to: " + hasPowerup);
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
        powerupIndicator.gameObject.SetActive(false);
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
