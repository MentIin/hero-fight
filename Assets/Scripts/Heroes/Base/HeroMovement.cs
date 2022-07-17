using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public bool red = false;
    protected Animator animator;
    protected Rigidbody2D _rb;
    
    protected AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public float speed = 5f;
    public float jumpForce = 15f;
    public bool isOnGround;
    public float skillCountdown = 3f;
    public float skillCountdownTick = 0f;
    public int maxHeatpoints = 5;
    public int heatpoints = 5;
    protected const float immortalTime = 3f;
    protected float stunnedTimeTick = 0f;
    protected float immortalTimeTick = 0f;

    public bool isMoving;
    public int xDirection = 1;
    protected CircleCollider2D _collider;
    protected SpriteRenderer _sprite;
    protected int layerMask;
    protected float originalSpeed;
    private int deafulLayer;
    [HideInInspector]public  float lastClickOnJumpButton=0f;
    private float skillCooldownBoostSec=0f;
    public int heroId=-1;
    // Start is called before the first frame update
    void Awake()
    {
        originalSpeed = speed;
        heatpoints = maxHeatpoints;
        
    }
    public void Start()
    {
        if (red){
            deafulLayer = 10;
            gameObject.layer = 10;
        }else{
            deafulLayer = 11;
            gameObject.layer = 11;
        }
        audioSource = GetComponent<AudioSource>();
        layerMask = LayerMask.GetMask("Default");
        if (red)
        {
            transform.Rotate(Vector2.up, 180f);
            xDirection = -1;
        }
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        skillCountdownTick -= Time.fixedDeltaTime;
        stunnedTimeTick -= Time.fixedDeltaTime;
        if (skillCooldownBoostSec > 0){
            skillCooldownBoostSec -= Time.deltaTime;
            skillCountdownTick -= Time.deltaTime * 3;
        }
    }
    void Update()
    {
        lastClickOnJumpButton += Time.deltaTime;
        if (lastClickOnJumpButton <= 0.2f && canMove && isOnGround){
            Jump();
        }
        if (isImmortal)
        {
            immortalTimeTick -= Time.deltaTime;
        }
        else
        {
            ChangeColorA(1f);
        }
        if (!isStunned)
        {
            animator.SetBool("stunned", false);
            gameObject.layer = deafulLayer;
        }
        else
        {
            ChangeColorA(0.6f);
        }

        CheckToward();
        CheckOnGround();
        if (!canMove)
        {
            animator.SetInteger("speed_int", 0);
        }
        else if (isOnGround)
        {
            animator.SetInteger("speed_int", 1);
            animator.SetBool("jump_bool", false);
        }
        else
        {
            animator.SetBool("jump_bool", true);
        }
        if (canMove)
        {
            Move();
        }

        
    }
    virtual public void Move(){
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    public void GetDamage(int damage)
    {
        if (isImmortal) return;
        heatpoints -= damage;
        audioSource.PlayOneShot(hitSound);
        stunnedTimeTick = immortalTime;
        if (heatpoints <= 0)
        {
            Die();
        }
        ImmportalityOn();
    }
    public void Die()
    {
        Destroy(this.gameObject);
        GameManager.S.GameEnd(red);
    }
    virtual protected void CheckToward()
    {
        Vector2 offset = new Vector2(xDirection * (_collider.radius + 0.1f), 0);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = Vector2.right * xDirection;
        float distance = speed / 60 + _collider.radius;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, layerMask);

        if (hit.collider != null)
        {
            //Debug.Log("Target tag: " + hit.collider.gameObject.tag);
            if (hit.collider.tag == ("Ground"))
            {
                ChangeDirection();
            }
        }
        else
        {
            //Debug.Log("None");
        }
    }
    void CheckOnGround()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = Vector2.down;
        float distance = 0.1f + _collider.radius;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == ("Ground") || hit.collider.tag == "Platform")
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
        }
        else
        {
            isOnGround = false;
        }
        if (_rb.velocity.y > 0.5f){
            isOnGround = false;
        }
    }
    public virtual void ChangeDirection()
    {
        xDirection *= -1;
        if (xDirection == 1)
        {
            transform.Rotate(Vector3.up, 180f);
        }
        else
        {
            //_sprite.flipX = true;
            transform.Rotate(Vector3.up, 180f);
        }
        transform.Translate(Vector2.right * 0.1f);

    }
    public void StartMove()
    {
        isMoving = true;
        animator.SetInteger("speed_int", 1);
    }
    public void Stop()
    {
        isMoving = false;
        animator.SetInteger("speed_int", 0);
    }
    virtual public void Jump()
    {
        lastClickOnJumpButton = 0f;
        if (canMove && isOnGround)
        {
            if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
            lastClickOnJumpButton = 10f;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void UseSkill()
    {
        if (skillCountdownTick > 0 || isStunned || !isMoving)
        {
            return;
        }
        skillCountdownTick = skillCountdown;
        Skill();
    }
    virtual public void Skill()
    {

    }
    public void ImmportalityOn()
    {
        immortalTimeTick = immortalTime;
        animator.SetBool("stunned", true);
        gameObject.layer = 8; //immortal
    }
    public void ImmportalityOn(float time, bool stunned) 
    {
        immortalTimeTick = time;
        if (stunned)
        {
            animator.SetBool("stunned", true);
            gameObject.layer = 8; //immortal cuz cant contact wth anything except map
        }
        
    }

    void ChangeColorA(float a)
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, a);
    }
    
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (Random.value >= 0.1f) ChangeDirection();

        }
    }

    
    virtual public void EndJump(){
        if (_rb.velocity.y > 1f && !isOnGround && lastClickOnJumpButton > 9.5f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / 1.5f);
        }
    }
    public void SpeedUpSkillCooldown(float sec){
        skillCooldownBoostSec = sec;
    }
    public bool canMove
         {
             get
             {
                 return isMoving && !isStunned;
             }
         }
         public bool isImmortal
         {
             get
             {
                 return immortalTimeTick > 0;
             }
         }
         public bool isStunned
         {
             get
             {
                 return stunnedTimeTick > 0;
             }
         }
}
