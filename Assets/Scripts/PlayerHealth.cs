using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthValue;
    public int currentHealthValue;
    public float invincibleTime;
    private float invincibleTimeCounter;

    public static PlayerHealth player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = this;
    }
    void Start()
    {
        currentHealthValue = maxHealthValue;
        UIController.UICanvas.healthBar.maxValue = maxHealthValue;
        UIController.UICanvas.healthBar.value = currentHealthValue;
        UIController.UICanvas.healthBarText.text = currentHealthValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleTimeCounter > 0)
        {
            invincibleTimeCounter -= Time.deltaTime;
            if (invincibleTimeCounter <= 0)
            {
                PlayerController.player.playerBody.color = new Color(PlayerController.player.playerBody.color.r,
                    PlayerController.player.playerBody.color.g, PlayerController.player.playerBody.color.b, 1f);
                PlayerController.player.shootArm.color = new Color(PlayerController.player.shootArm.color.r, 
                    PlayerController.player.shootArm.color.g, PlayerController.player.shootArm.color.b, 1f);
            }
        }
    }

    public void DamagePlayer(int damageValue)
    {
        if (invincibleTimeCounter <= 0)
        {
            currentHealthValue -= damageValue;
            PlayerController.player.playerBody.color = new Color(PlayerController.player.playerBody.color.r,
                PlayerController.player.playerBody.color.g, PlayerController.player.playerBody.color.b, 0.5f);
            PlayerController.player.shootArm.color = new Color(PlayerController.player.shootArm.color.r,
                PlayerController.player.shootArm.color.g, PlayerController.player.shootArm.color.b, 0.5f);
            if (currentHealthValue <= 0)
            {
            PlayerController.player.gameObject.SetActive(false);
                UIController.UICanvas.deathScreen.SetActive(true);
            }
            invincibleTimeCounter = invincibleTime;
            UIController.UICanvas.healthBar.value = currentHealthValue;
            UIController.UICanvas.healthBarText.text = currentHealthValue.ToString();
        }
    }

    public void HealPlayer(int healValue)
    {
        currentHealthValue += healValue;
        if (currentHealthValue > maxHealthValue)
            currentHealthValue = maxHealthValue;
        UIController.UICanvas.healthBar.value = currentHealthValue;
        UIController.UICanvas.healthBarText.text = currentHealthValue.ToString();
    }
}
