using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.UICanvas.passwordScreen.activeInHierarchy)
        {
            if (!PlayerController.player.isSword)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;
                    Audio.instance.PlayEffects(0);
                }
                if (Input.GetMouseButton(0))
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        Audio.instance.PlayEffects(0);
                        shotCounter = timeBetweenShots;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerController.player.anim.SetTrigger("sword");
                    Audio.instance.PlayEffects(0);
                }
            }
        }
    }
}
