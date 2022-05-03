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
    public GameObject arm;
    public SpriteRenderer shootArm;
    public Sprite[] armSprites;
    private bool isRunning;
    public GameObject gun;
    //public GameObject bullet;
    //public Transform firePoint;
    //public float timeBetweenShots;
    //private float shotCounter;
    public SpriteRenderer playerBody;
    //public SpriteRenderer swordArm;
    public List<GunController> weapons;
    public int currentWeapon = 0;
    public bool isSword;
    public bool isSwordAvailable;

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
        arm.SetActive(false);
        SwithTheWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.UICanvas.passwordScreen.activeInHierarchy) 
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            rigidBody.velocity = moveInput * moveSpeed;
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCamera.WorldToScreenPoint(transform.localPosition);
            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
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
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                currentWeapon++;
                if (currentWeapon >= weapons.Count)
                    currentWeapon = 0;
                SwithTheWeapon();
            }    
            if (!isSword)
            {
                anim.SetBool("isShooting", true);
                shootArm.gameObject.SetActive(true);

                if (angle < 22.5f && angle >= 0f || angle <= 0f && angle > -22.5f)
                    ArmRotation(3, new Vector3(-0.079f, 0.474f, 0f), new Vector3(-0.0806f, 0.5291f, 0f),
                        new Vector3(-0.133f, 0.804f, 0f), Quaternion.Euler(0f, 0f, 90f));
                else if (angle < 67.5f && angle >= 22.5f || angle <= -22.5f && angle > -67.5f)
                    ArmRotation(1, new Vector3(0.209f, 0.058f, 0f), new Vector3(0.262f, 0.214f, 0f),
                        new Vector3(0.517f, 0.598f, 0f), Quaternion.Euler(0f, 0f, 45f));
                else if (angle < 112.5f && angle >= 67.5f || angle <= -67.5f && angle > -112.5f)
                    ArmRotation(0, new Vector3(0.2089f, -0.049f, 0f), new Vector3(0.2085f, 0.0027f, 0f),
                        new Vector3(0.784f, -0.015f, 0f), Quaternion.Euler(0f, 0f, 0f));
                else if (angle < 157.5f && angle >= 112.5f || angle <= -112.5f && angle > -157.5f)
                    ArmRotation(2, new Vector3(0.1036f, -0.208f, 0f), new Vector3(0.1037f, -0.1023f, 0f),
                        new Vector3(0.68f, -0.446f, 0f), Quaternion.Euler(0f, 0f, -45f));
                else if (angle <= 180f && angle >= 157.5f || angle <= -157.5f && angle >= -180f)
                    ArmRotation(4, new Vector3(-0.0803f, -0.3126f, 0f), new Vector3(-0.0803f, -0.2076f, 0f),
                        new Vector3(0.299f, -0.819f, 0f), Quaternion.Euler(0f, 0f, -90f));

                //if (Input.GetMouseButtonDown(0))
                //{
                //    Instantiate(bullet, firePoint.position, firePoint.rotation);
                //    shotCounter = timeBetweenShots;
                //    Audio.instance.PlayEffects(0);
                //}
                //if (Input.GetMouseButton(0))
                //{
                //    shotCounter -= Time.deltaTime;
                //    if (shotCounter <= 0)
                //    {
                //        Instantiate(bullet, firePoint.position, firePoint.rotation);
                //        Audio.instance.PlayEffects(0);
                //        shotCounter = timeBetweenShots;
                //    }
                //}
            }
            else
            {
                shootArm.gameObject.SetActive(false);
                //arm.SetActive(true);
                //weapons[0].SetActive(false);
                //if (Input.GetMouseButtonDown(0))
                //{
                //    //arm.SetActive(false);
                //    //SwithTheWeapon();
                //    anim.SetTrigger("sword");
                //    Audio.instance.PlayEffects(0);
                //}
                //if (Input.GetMouseButton(0))
                //{
                //    shotCounter -= Time.deltaTime;
                //    if (shotCounter <= 0)
                //    {
                //        arm.SetActive(false);
                //        SwithTheWeapon();
                //        anim.SetTrigger("sword");
                //        Audio.instance.PlayEffects(0);
                //        shotCounter = timeBetweenShots;
                //    }
                //}
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

    public void SwithTheWeapon()
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        if (weapons[currentWeapon].CompareTag("PlayerSword") && isSwordAvailable)
        {
            isSword = true;
            weapons[currentWeapon].gameObject.SetActive(true);
        }
        else
        {
            isSword = false;
            currentWeapon--;
            if (currentWeapon < 0)
                currentWeapon = 0;
            weapons[currentWeapon].gameObject.SetActive(true);
        }
    }
}
