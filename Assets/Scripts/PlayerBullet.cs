using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBody;
    private Vector3 direction;
    public GameObject explosion;
    public int damageToGive = 20;
    public GameObject bulletEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.player.transform.localScale == new Vector3(-1f, 1f, 1f))
            direction = transform.right * -1f;
        else
            direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = direction * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D( Collider2D collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }
}
