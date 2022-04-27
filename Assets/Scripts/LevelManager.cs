using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager lvlManager;
    public int currentCoinsAmount;
    public string sceneToLoad;

    private void Awake()
    {
        lvlManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(sceneToLoad);
    }

    public void GetCoins(int amount)
    {
        currentCoinsAmount += amount;
        UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoinsAmount -= amount;
        if (currentCoinsAmount < 0)
            currentCoinsAmount = 0;
        UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
    }
}
