using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController enemy;
    public Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;   ///Player vector

    public Animator anim;

    public int health = 100;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;


    public SpriteRenderer EnemyBody;

    public SpriteRenderer enemyArm; //test
    private float angle; //test
    public SpriteRenderer armDiagonalDown;
    public SpriteRenderer armDown;
    public SpriteRenderer armUp;
    public SpriteRenderer armDiagonalUp;
    public SpriteRenderer armStraight;


    private void Awake()
    {
        enemy = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyBody.isVisible)
        {
            if (Vector3.Distance(transform.position, PlayerController.player.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.player.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;

            if (shouldShoot == true && Vector3.Distance(transform.position, PlayerController.player.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }

        if(transform.position.x > PlayerController.player.transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            
        }
        else
        {
            transform.localScale = Vector3.one;
            
        }

        if (moveDirection!=Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        Vector2 DistanceFromPlayerToEnemy = new Vector2(PlayerController.player.transform.position.x - transform.position.x, PlayerController.player.transform.position.y - transform.position.y);
        angle = Mathf.Atan2(DistanceFromPlayerToEnemy.x, DistanceFromPlayerToEnemy.y) * Mathf.Rad2Deg;

        enemyArm.enabled = true;
        armDiagonalDown.enabled = false;
        armUp.enabled = false;
        armDiagonalUp.enabled = false;
        armDown.enabled = false;
        armStraight.enabled = false;

        if (angle < 22.5 && angle >= 0f || angle <= 0f && angle >= -22.5f)
        {
            enemyArm.enabled = false;
            armUp.enabled = true;
            firePoint.transform.localPosition = new Vector3(0.33f, 0.33f, 0f);

        }
        else if (angle < 67.5f && angle >= 22.5f || angle <= -22.5f && angle > -67.5f)
        { 
            enemyArm.enabled = false;
            armDiagonalUp.enabled = true;
            firePoint.transform.localPosition = new Vector3(0.663f, 0.257f, 0f);
        }
        else if (angle < 112.5f && angle >= 67.5f || angle <= -67.5f && angle > -112.5f)
        {
            //enemyArm.enabled = false;
            //armStraight = true;      don't need 
            firePoint.transform.localPosition = new Vector3(0.48f, -0.23f, 0f);
        }
        else if (angle < 157.5f && angle >= 112.5f || angle <= -112.5f && angle > -157.5f)
        {
            enemyArm.enabled = false;
            armDiagonalDown.enabled = true;
            firePoint.transform.localPosition = new Vector3(0.55f, -0.75f, 0f);
        }
        else if (angle <= 180f && angle >= 157.5f || angle <= -157.5f && angle >= -180f)
        {
            enemyArm.enabled = false;
            armDown.enabled = true;
            firePoint.transform.localPosition = new Vector3(0.25f, -0.97f, 0f);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }
}
