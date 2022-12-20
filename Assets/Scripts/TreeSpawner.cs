    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public int HowManyToSpawn;
    public GameObject[] treeobject;
    bool DebugEnabled;
    public GameObject DebugUI;
    public GameObject DebugText;
    public AudioManager Audio;
    public GameObject StartingUI;
    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
        StartCoroutine("StartUI");
    }
    public void StartGame()
    {
        Audio.Play("MainTheme");
        for (int i = 0; i < HowManyToSpawn; i++)
        {
            GameObject Spawn = treeobject[Random.Range(1, treeobject.Length)].gameObject;
            float spawnradius = Spawn.GetComponent<Collider>().bounds.extents.x;
            float x = Random.Range(-60.0f, 60.0f);
            float y = 0;
            float z = Random.Range(-60.0f, 60.0f);
            Vector3 spawnPoint = new Vector3(x, y,z);
            Collider[] CollisionWithEnemy = Physics.OverlapSphere(spawnPoint, spawnradius);
            if (CollisionWithEnemy.Length == 0)
                Instantiate(Spawn, new Vector3(x, y, z), Quaternion.Euler( new Vector3 (-90,0,0)));
        }
    }
    public void EnableDebug()
    {
 
        if (DebugEnabled == true) {
            DebugText.GetComponent<TMP_Text>().enabled = false;
            DebugUI.SetActive(false);
            DebugEnabled = false;
        }
        else
        {
            DebugText.GetComponent<TMP_Text>().enabled = true;
            DebugEnabled = true;
            DebugUI.SetActive(true);
        }
       
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator StartUI()
    {
        yield return new WaitForSeconds(7);
        StartingUI.SetActive(false);
    }
}
