using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TotalXP;
    public float TotalMoney;
    PlayerMovement Player;
    TMP_Text DebugText;
    public TMP_Text CoinText;
    public TMP_Text XPText;
    float CurrentPower = 1;
    float CurrentSpeed = 1;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = TotalMoney.ToString();
        XPText.text = ("XP " + TotalXP.ToString() + " / " + 100); //ADD XP LEVEL
    }

    public float AddedXp(int IncomingXP)
    {
        return TotalXP += IncomingXP;
    }
    public float AddedMoney(int IncomingMoney)
    {
        return TotalMoney += IncomingMoney;
    }

    public void Upgrade()
    {
        DebugText.GetComponent<TMP_Text>().enabled = true;
        StartCoroutine("DebugTextCooldown");
        if (TotalMoney >= CurrentPower * 5)
        {
            TotalMoney -= CurrentPower * 5;
            Player.ChoppingPower += CurrentPower;
            DebugText.text = "Your Chopping power has increased! \n Your current chopping power is " + Player.ChoppingPower;
            CurrentPower += 1;
            audioManager.Play("UpgradeSound");
        }
        else if (TotalMoney >= CurrentSpeed * 5)
        {
            TotalMoney -= CurrentSpeed * 5;
            Player.ChoppingTime -= CurrentSpeed / 2;
            DebugText.text = "Your Chopping speed has increased! \n Your current chopping speed is " + Player.ChoppingTime;
            CurrentSpeed += 1;
            audioManager.Play("UpgradeSound");
        }
        else
        {
            float Cash = CurrentSpeed * 5 - TotalMoney;
            DebugText.text = "Not Enough Money! \n You need " + Cash + " more coins for this upgrade";
        }
    }
    IEnumerator DebugTextCooldown()
    {
        yield return new WaitForSeconds(3);
        DebugText.GetComponent<TMP_Text>().enabled = false;
    }

}


