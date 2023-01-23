using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class data : MonoBehaviour
{
    public dataList list = new dataList();
    //public List<gameData> list = new List<gameData>();
    gameData player = new gameData();
    gameData player_two = new gameData();
    void Start()
    {
        createData();
        savaDataFile();
    }

    public void createData()
    {
        player.killAmount = 0; ;
        player_two.killAmount = 0;

        player.highScore = 0;
        player_two.highScore = 0;

        list.data.Add(player);
        list.data.Add(player_two);
    }

    public void loadDataFile()
    {
        string jsonData;
        string file = Application.streamingAssetsPath + "/data.json";

        StreamReader sr = new StreamReader(file);
        jsonData = sr.ReadToEnd();
        sr.Close();
        list = JsonUtility.FromJson<dataList>(jsonData);
        player = list.data[0];
        player_two = list.data[1];
    }

    public void savaDataFile()
    {
        string json = JsonUtility.ToJson(list,true);
        string file = Application.streamingAssetsPath + "/data.json";

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(json);
        sw.Dispose();
        sw.Close();
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class gameData
{
    public float highScore;
    public float killAmount;
}

public class dataList
{
    public List<gameData> data = new List<gameData>();
}

