using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1f;
    public int damage = 40;
    public Rigidbody2D rb;
    public float lifeTime;
    public GameObject destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("DestroyBullet", lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        WolfNonFollow wolf = hitInfo.GetComponent<WolfNonFollow>();
        if(enemy != null)
        {
            enemy.GetHurt(damage);
        }
        if(wolf != null)
        {
            wolf.TakeDamage(damage);
        }
        Destroy(gameObject);   
    }

    void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
