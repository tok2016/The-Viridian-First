using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public static Computer computer;

    private static HashSet<string> previousPasswords = new HashSet<string>();

    public int healByBadPassword, healByNormalPassword, healByGoodPassword;

    public GameObject keyMessage;

    private bool inComputerZone = false;

    private void Awake()
    {
        computer = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inComputerZone && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            UIController.UICanvas.passwordScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            keyMessage.SetActive(true);
            inComputerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            keyMessage.SetActive(false);
            inComputerZone = false;
        }
    }

    public void ChangePassword(string newPassword)
    {
        if (!previousPasswords.Contains(newPassword))
        {
            var passwordStrength = PasswordQualityToHealAmount(newPassword);
            if (passwordStrength < 34)
                PlayerHealth.player.HealPlayer(healByBadPassword);
            else if (passwordStrength < 67)
                PlayerHealth.player.HealPlayer(healByNormalPassword);
            else if (passwordStrength >= 67)
                PlayerHealth.player.HealPlayer(healByGoodPassword);
            previousPasswords.Add(newPassword);
        }
    }

    public int PasswordQualityToHealAmount(string password)
    {
        if (password.Length < 4)
            return 0;
        var passwordStrength = 4 * password.Length;

        for (var i = 2; i <= 4; i++)
            passwordStrength += password.Length - CompressStringLength(password, i);

        var containLetters = ContainLetters(password);
        var containSigns = ContainSigns(password);
        var containNumbers = ContainNumbers(password);
        var containLettersBothType = ContainLettersBothType(password);
        if (containNumbers)
            passwordStrength += 5;
        if (containSigns)
            passwordStrength += 5;
        if (containLettersBothType)
            passwordStrength += 10;
        if (containNumbers && containLetters)
            passwordStrength += 15;
        if (containNumbers && containSigns)
            passwordStrength += 15;
        if (containLetters && containSigns)
            passwordStrength += 15;
        if ((containLetters && !containNumbers) || (!containLetters && containNumbers))
            passwordStrength -= 10;
        return passwordStrength;
    }

    private bool ContainLettersBothType(string password)
    {
        bool containUppercaseLetters = false, containLowercaseLetters = false;
        foreach (var c in password)
        {
            if (containUppercaseLetters && containLowercaseLetters)
                return true;
            if (64 < c && c < 91)
                containUppercaseLetters = true;
            if (96 < c && c < 123)
                containLowercaseLetters = true;
        }
        return false;
    }

    private bool ContainNumbers(string password)
    {
        foreach (var c in password)
            if (47 < c && c < 58)
                return true;
        return false;
    }

    private bool ContainSigns(string password)
    {
        var signs = new HashSet<char>() { '!', '@', '#', '%', '$', '^', '&', '*', '|' };
        foreach (var c in password)
            if (signs.Contains(c))
                return true;
        return false;
    }

    private bool ContainLetters(string password)
    {
        foreach (var c in password)
            if ((96 < c && c < 123) || (47 < c && c < 58))
                return true;
        return false;
    }

    private int CompressStringLength(string password, int compressingWidth)
    {
        var leftChar = password[0];
        var result = 1;
        var currentCompressingWidth = 1;
        var c = 0;
        for (var i = 1; i < password.Length - compressingWidth + 1; i++)
        {
            var currentChar = password[i];

            if (currentCompressingWidth < compressingWidth)
                currentCompressingWidth++;
            else
                leftChar = password[i - compressingWidth + 1];

            if (leftChar == currentChar)
                c = c == 0 ? 2 : c + 1;
            else
                c = 0;

            if (c >= compressingWidth)
                result -= compressingWidth - 1;
        }
        return result;
    }
}