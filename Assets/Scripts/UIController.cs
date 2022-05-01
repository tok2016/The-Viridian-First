using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController UICanvas;
    public Slider healthBar;
    public Text healthBarText;
    public GameObject deathScreen;
    public Text coinBarText;

    public GameObject passwordScreen;
    public Text inputText;
    public GameObject badPasswordAlerte;

    private void Awake()
    {
        UICanvas = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (passwordScreen.activeInHierarchy)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Computer.computer.ChangePassword(inputText.text);
                passwordScreen.SetActive(false);
                Time.timeScale = 1f;
            }

            if (inputText.text.Length != 0 
                && Computer.computer.PasswordQualityToHealAmount(inputText.text) < Computer.computer.healByGoodPassword)
                badPasswordAlerte.SetActive(true);
            else
                badPasswordAlerte.SetActive(false);
        }
    }
}
