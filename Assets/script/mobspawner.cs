using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mobspawner : MonoBehaviour
{
    void Start()
    {
        
    }
    public Vector3[] position;
    public float spawn_time = 1;
    public int max_spawn_points = 6;
    public int min_timer = 2;
    public int max_timer = 10;
    
    System.Random rand= new System.Random();
    void Update()
    {
        spawn_time -= Time.deltaTime;
        if(spawn_time <= 0 && (gameInfo.nowEnemy > gameInfo.maxEnemy) == false)
        {
            spawn_time = rand.Next(min_timer, max_timer);
            List<Vector3> list = new List<Vector3>();
            int spawnAmount = rand.Next(1, max_spawn_points + 1);
            int spawnComplete = 0;
            while(true)
            {
                Vector3 someOne = position[rand.Next(0, max_spawn_points)];
                if(list.Contains(someOne) == false)
                {
                    list.Add(someOne);
                    spawnComplete += 1;
                }
                if(spawnComplete == spawnAmount)
                {
                    break;
                }
            }
            foreach(var x in list)
            {
                if(gameInfo.nowEnemy <= gameInfo.maxEnemy)
                {
                    PathologicalGames.SpawnPool pool = PathologicalGames.PoolManager.Pools["mypool"];
                    pool.Spawn("Zombie", x, new Quaternion(0, 0, 0, 0), null);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
