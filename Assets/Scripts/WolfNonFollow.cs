using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfNonFollow : MonoBehaviour
{

    public bool MoveRight;
    private Rigidbody2D rb;
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
        if(MoveRight)
        {
            transform.Translate(2*Time.deltaTime*moveSpeed, 0,0);
            transform.localScale = new Vector2(-1,1);
        }
        else
        {
            transform.Translate(-2*Time.deltaTime*moveSpeed, 0,0);   
            transform.localScale = new Vector2(1,1);
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Spikes"))
        {
            if(MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }
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

    public void TakeDamage (int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Disappear();
        }
    }

    void Disappear()
    {
        if(PlayerController.gotHit == false)
        {
            ScoreScript.combo++;
        }
        else
        {
            ScoreScript.combo = 0;
            PlayerController.gotHit = false;
        }
        if(ScoreScript.combo == 0)
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 10;
        else
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 10 + ScoreScript.combo;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
