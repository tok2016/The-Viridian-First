using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon = 0,
    HealthUpgrade = 1,
    Ammo = 2,
}

public class ItemPlace : MonoBehaviour
{
    public GameObject keyMessage;
    public GameObject moneyLackMessage;
    public GameObject itemImage;
    public GameObject itemInfo;
    public GameObject enemyToSpawn;
    public Transform enemyPoint;

    public int itemCoast;
    public string itemName;
    public ItemType itemType;

    private bool isShopOfficial;
    private bool inBuyZone;

    public GunController potentialWeapon;
    public bool canBuy;

    void Start()
    {
        isShopOfficial = this.GetComponentInParent<Shop>().isShopOfficial;
        itemInfo.GetComponent<Text>().text = itemName + System.Environment.NewLine + "- " + itemCoast.ToString() + " coins -";
    }

    void Update()
    {
        if (inBuyZone && Input.GetKeyDown(KeyCode.E))
        {
            if (LevelManager.lvlManager.currentCoinsAmount > itemCoast) //проверить на количество имеющихся монет
            {
                if (!isShopOfficial)
                {
                    var randomChance = Random.Range(-2, 6);
                    if (randomChance > 0)
                    {
                        LevelManager.lvlManager.SpendCoins(itemCoast + randomChance);
                    }
                    else if (randomChance < 0)
                    {
                        PlayerHealth.player.DamagePlayer(-randomChance);
                    }
                    else
                    {
                        Instantiate(enemyToSpawn, enemyPoint.position, enemyPoint.rotation);
                    }
                }

                LevelManager.lvlManager.SpendCoins(itemCoast);
                switch (itemType)
                {
                    case ItemType.Weapon:
                        if (canBuy)
                        {
                            if (potentialWeapon.tag != "PlayerSword")
                            {
                                GunController newWeapon = Instantiate(potentialWeapon);
                                newWeapon.transform.parent = PlayerController.player.shootArm.transform;
                                newWeapon.transform.localPosition = new Vector3(0.443f, -0.549f, 0f);
                                newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                                newWeapon.transform.localScale = Vector3.one;
                                PlayerController.player.weapons.Add(newWeapon);
                                PlayerController.player.currentWeapon = PlayerController.player.weapons.Count - 1;
                            }
                            else
                            {
                                PlayerController.player.isSwordAvailable = true;
                                PlayerController.player.currentWeapon++;
                            }
                            PlayerController.player.SwithTheWeapon();
                        }
                        break;
                    case ItemType.HealthUpgrade:
                        //повысить количесиво макс.хп
                        break;
                    case ItemType.Ammo:
                        //уменьшить количество имеющихся патронов
                        break;
                }
                itemImage.SetActive(false);
                itemInfo.SetActive(false);
                keyMessage.SetActive(false);
            }
            else
            {
                keyMessage.SetActive(false);
                moneyLackMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            keyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            keyMessage.SetActive(false);
            moneyLackMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
