using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private LayerMask ground;

    private PlayerActionControls playerActionControls;

    private Rigidbody2D rb;

    private Collider2D col;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioClip deathsound;

    private AudioSource audioSource;

    public int maxHealth = 100;
    public int currentHealth = 0;
    public static bool gotHit = false;

    public HealthBar healthBar;

    [SerializeField] private Transform fp;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();  
        rb = GetComponent<Rigidbody2D>(); 
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerActionControls.Land.Jump.performed += _ => Jump();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        deathsound = (AudioClip)Resources.Load("deathsound");
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
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

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Read the movement value
        float movementInput = playerActionControls.Land.Move.ReadValue<float>();
        //Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        //Animation
        if(movementInput != 0) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);

        //Sprite Flip
        Vector2 spriteScale = spriteRenderer.transform.localScale;
        if(movementInput == -1)
        {
            spriteScale.x = -1;
        	fp.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
            
        else if (movementInput ==1)
        {
            spriteScale.x = 1;
            fp.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        spriteRenderer.transform.localScale = spriteScale;

    }  
     

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            StartCoroutine(PlaySound());
            TakeDamage(20);
            gotHit = true;
            ScoreScript.combo = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(PlaySound());
            TakeDamage(20);
            gotHit = true;
            ScoreScript.combo = 0;
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator PlaySound()
    {
        audioSource.clip = deathsound;
        audioSource.Play();
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}