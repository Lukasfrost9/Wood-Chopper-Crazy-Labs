using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Header("Variables")]
    public float TreeHealth;
    public int GoldReward;
    public int XPReward;
    public GameManager gameManager;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Chopped(float ChoppingPower)
    {
        TreeHealth -= ChoppingPower;
        if (TreeHealth <= 0 )
        {
            gameManager.AddedXp(Convert.ToInt32(ChoppingPower));
            StartCoroutine("TreeDeath");
            return gameManager.AddedMoney(GoldReward);

        }
        else
        {
            return gameManager.AddedXp(Convert.ToInt32(ChoppingPower));
        }
    }
    IEnumerator TreeDeath()
    {
        audioManager.Play("TreeDeath");
        yield return new WaitForSeconds(2);
        Destroy(gameObject) ;
    }

}
