using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rigidBody;
    public Animator anim;
    private Camera theCamera;
    private float angle;
    public SpriteRenderer arm;
    public SpriteRenderer shootArm;
    public Sprite[] armSprites;
    private bool isRunning;
    public GameObject gun;
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        rigidBody.velocity = moveInput * moveSpeed;
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCamera.WorldToScreenPoint(transform.localPosition);
        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        arm.enabled = false;
        shootArm.enabled = true;
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
            isRunning = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
        }
        anim.SetBool("isShooting", true);

        if (angle < 22.5f && angle >= 0f || angle <= 0f && angle > -22.5f)
            ArmRotation(3, new Vector3(-0.079f, 1.445f, 0f), new Vector3(-0.0806f, 1.5001f, 0f),
                new Vector3(-0.133f, 0.804f, 0f), Quaternion.Euler(0f, 0f, 90f));
        else if (angle < 67.5f && angle >= 22.5f || angle <= -22.5f && angle > -67.5f)
            ArmRotation(1, new Vector3(0.209f, 1.029f, 0f), new Vector3(0.262f, 1.185f, 0f),
                new Vector3(0.517f, 0.598f, 0f), Quaternion.Euler(0f, 0f, 45f));
        else if (angle < 112.5f && angle >= 67.5f || angle <= -67.5f && angle > -112.5f)
            ArmRotation(0, new Vector3(0.2089f, 0.922f, 0f), new Vector3(0.2085f, 0.9737f, 0f),
                new Vector3(0.784f, -0.015f, 0f), Quaternion.Euler(0f, 0f, 0f));
        else if (angle < 157.5f && angle >= 112.5f || angle <= -112.5f && angle > -157.5f)
            ArmRotation(2, new Vector3(0.1036f, 0.763f, 0f), new Vector3(0.1037f, 0.8687f, 0f),
                new Vector3(0.68f, -0.446f, 0f), Quaternion.Euler(0f, 0f, -45f));
        else if (angle <= 180f && angle >= 157.5f || angle <= -157.5f && angle >= -180f)
            ArmRotation(4, new Vector3(-0.0803f, 0.6584f, 0f), new Vector3(-0.0803f, 0.7634f, 0f), 
                new Vector3(0.299f, -0.819f, 0f), Quaternion.Euler(0f, 0f, -90f));

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }
        if(Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void ArmRotation(int spriteNumber, Vector3 idlePos, Vector3 runningPos, Vector3 gunPos, Quaternion angle)
    {
        shootArm.sprite = armSprites[spriteNumber];
        if (!isRunning)
            shootArm.gameObject.transform.localPosition = idlePos;
        else
            shootArm.gameObject.transform.localPosition = runningPos;
        gun.transform.localPosition = gunPos;
        gun.transform.localRotation = angle;
    }
}
