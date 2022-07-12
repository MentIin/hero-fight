using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Drone : HeroMovement
{
    private float flyHight = 6f;
    private float normalHight = 0.85f;
    private bool isFlying = false;
    private float flyForce = 200f;
    private float maxYVel = 2.3f;
    private float maxXVel = 3.5f;
    private float rotationCoef = 3f;
    private int projectilesCol = 3;
    private float timeBetweenShots=0.15f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    void Awake()
    {
        heroId = 2;
    }
    void LateUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir,  Mathf.Infinity, layerMask);
        float hight = normalHight;
        if (isFlying) hight = flyHight;

        if (hit.collider != null)
        {
            if (hit.collider.tag == ("Ground") || hit.collider.tag == "Platform")
            {
                if (_rb.velocity.y < -2.5f)
                {
                    _rb.AddForce(Vector2.up * flyForce * Time.deltaTime);
                }

                if ((hit.distance <= hight && canMove) && _rb.velocity.y <= maxYVel)
                {
                    
                    _rb.AddForce(Vector2.up * flyForce * Time.deltaTime * 2);
                    if (isFlying) _rb.AddForce(Vector2.up * flyForce * Time.deltaTime);

                }
            }
        }
        SetRotation();
    }
    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, -_rb.velocity.x * rotationCoef);
    }
    public override void Jump()
    {
        isFlying = true;
    }
    public override void EndJump()
    {
        isFlying = false;
    }
    public override void Skill()
    {
        StartCoroutine(Fire());

    }
    public IEnumerator Fire()
    {
        for (int i = 0; i < projectilesCol; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
            projectile.GetComponent<DroneProjectile>().red = red;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
    public override void Move()
    {
        if (xDirection > 0)
        {
            if (_rb.velocity.x < maxXVel)
            {
                _rb.AddForce(Vector2.right * speed * Time.deltaTime);
            }
        }
        else
        {
            if (_rb.velocity.x > -maxXVel)
            {
                _rb.AddForce(Vector2.left * speed * Time.deltaTime);
            }
        }

    }
    protected override void CheckToward()
    {
        Vector2 offset = new Vector2(xDirection * (_collider.radius + 0.1f), 0);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = Vector2.right * xDirection;
        float distance = 0.5f  + _collider.radius;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, layerMask);

        if (hit.collider != null)
        {
            //Debug.Log("Target tag: " + hit.collider.gameObject.tag);
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Hero"))
            {
                ChangeDirection();
                _rb.AddForce(Vector2.right * xDirection * 0.25f, ForceMode2D.Impulse);
            }
        }


        // проверка нижней части
        offset = Vector2.down * _collider.radius;
        origin = new Vector2(transform.position.x, transform.position.y);
        dir = Vector2.right * xDirection;
        distance =  _collider.radius;
        hit = Physics2D.Raycast(origin + offset, dir, distance, layerMask);

        if (hit.collider != null)
        {
            //Debug.Log("Target tag: " + hit.collider.gameObject.tag);
            if (hit.collider.tag == ("Ground"))
            {
                ChangeDirection();
                _rb.AddForce(Vector2.up * xDirection * 0.25f, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        _rb.AddForce(Vector2.right * -xDirection * 0.2f, ForceMode2D.Impulse);
        //if (other.collider.CompareTag("Hero")) ChangeDirection();
    }
}