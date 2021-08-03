using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] private int force;
    
   
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position /*player'in pozisyonu*/ - transform.position /*enemy'nin pozisyonu*/).normalized /*hizi normallestiriyor*/;
        enemyRb.AddForce(lookDirection * speed);

        //burada dusman duserse eger daha hizli bir sekilde dusup yok oluyor.
        if (gameObject.transform.position.y < -2)
        {
            enemyRb.AddForce(Vector3.down * force);
        }
        if (gameObject.transform.position.y < -15)
        {
            Destroy(gameObject);
        }            
    }

    
        
}
