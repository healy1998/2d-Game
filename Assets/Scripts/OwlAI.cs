using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OwlAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public int hp = 100;
    public GameObject deathEffect;


    private Seeker seeker;
    private Rigidbody2D rb;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    public void GetHit (int damage)
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
            ScoreScript.combo = ScoreScript.combo + 5;
        }
        else
        {
            PlayerController.gotHit = false;
        }
        if(ScoreScript.combo == 0)
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 20;
        else
        ScoreScript.ScoreValue = ScoreScript.ScoreValue + 20 + ScoreScript.combo-1;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f,1f,1f);
        }
        else if(force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f,1f,1f);
        }
        
    }
}
