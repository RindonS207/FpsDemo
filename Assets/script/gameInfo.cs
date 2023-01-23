using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameInfo : MonoBehaviour
{
    void Start()
    {
        life_text = life.GetComponent<Text>();
        hiscore_text = Hiscore.GetComponent<Text>();
        score_text = score.GetComponent<Text>();
        ammo_text = ammo.GetComponent<Text>();
        reStartGame.SetActive(false);
    }
    public GameObject life;
    public static int maxEnemy = 20;
    public static int nowEnemy = 0;
    Text life_text;
    public GameObject Hiscore;
    Text hiscore_text;
    public GameObject score;
    Text score_text;
    public GameObject ammo;
    Text ammo_text;
    public GameObject reStartGame;
    // Update is called once per frame
    void Update()
    {
        displayInfo();
        if(playerControl.isAlive == false)
        {
            reStartGame.SetActive (true);
        }
    }
    private void displayInfo()
    {
        if(playerControl.heart <= 0)
        {
            life_text.text = "生命值：0";
        }
        else
        {
            life_text.text = string.Format("生命值：{0}", playerControl.heart);
        }
        if(playerControl.score >= 1000)
        {
            hiscore_text.text = string.Format("最高分：{0}",playerControl.score);
        }
        score_text.text = string.Format("分数：{0}",playerControl.score);
        ammo_text.text = string.Format("100/{0}",playerControl.ammoAmount);
    }
    public void resetValues()
    {
        playerControl.heart = 100;
        playerControl.isAlive = true;
        playerControl.score = 0;
        playerControl.ammoAmount = 100;
        reStartGame.SetActive(false);
        SceneManager.LoadScene("demo");
    }
    
}
