using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthValue;
    public int currentHealthValue;

    // Start is called before the first frame update
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
        
    }

    public void DamagePlayer(int damageValue)
    {
        currentHealthValue -= damageValue;
        if(currentHealthValue <= 0)
        {
            PlayerController.player.gameObject.SetActive(false);
        }
        UIController.UICanvas.healthBar.value = currentHealthValue;
        UIController.UICanvas.healthBarText.text = currentHealthValue.ToString();
    }
}
