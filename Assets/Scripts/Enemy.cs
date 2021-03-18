using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public  float moveSpeed = 2f;
    private Collider2D col;
    [SerializeField] private LayerMask ground;
    public int hp = 100;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        //float angle = Mathf.Atan(direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        if(direction.x > 0)
            spriteRenderer.flipX = true;
            
        else if (direction.x <= 0)
            spriteRenderer.flipX = false;
    }

    public void GetHurt (int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(PlayerController.gotHit == false)
        {
            ScoreScript.combo++;
        }
        else
        {
            PlayerController.gotHit = false;
        }
        if(ScoreScript.combo == 0)
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 10;
        else
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 10 + ScoreScript.combo-1;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
